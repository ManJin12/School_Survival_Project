using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using My;
public class ScrollManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /** Scrollbar타입 접근할 수 있는 변수 선언 */
    public Scrollbar scrollbar;

    public Transform ContentTr;

    public Slider TapSlider;

    public RectTransform[] BtnRect;
    public RectTransform[] BtnImageRect;

    Vector3 BtnTargetPos;
    Vector3 BtnTargetScale;

    /** 로비화면의 판넬 갯수 */
    const int m_PanelSize = 5;
    /** 현재 버튼의 인덱스 저장 */
    int CurrentBtnIndex;

    /** ScrollBar Horizontal의 Value를 저장하기 위한 */
    float[] m_Pos = new float[m_PanelSize];
    /** 판넬이 위치한 거리 */
    float m_Distance;
    /** 화면 전환 시 목표 위치 */
    float m_TargetPos;
    /** 현재 위치 */
    float m_CurPos;

    /** 드래그 중 인지 확인 */
    bool bIsDrag;
    /** Tap텍스트 활성화 */
    bool TextActive;

    // Start is called before the first frame update
    void Start()
    {

        GameManager.GMInstance.ScrollManagerRef = this;

        /** 판넬이 위치한 거리크기는 전체 Value 1에서 전체 판넬갯수에서 -1을 해주면 된다. */
        m_Distance = 1.0f / (m_PanelSize - 1);
        
        /** 각 판넬의 Value는 0, 0.25, 0.5, 0.75, 1로 저장된다 */
        for (int i = 0; i < m_PanelSize; i++)
        {
            /** m_Pos[i]에 판넬의 갯수만큼 나눠 Value 저장 */
            m_Pos[i] = m_Distance * i;
        }

    }

    // Update is called once per frame
    void Update()
    {
        

        /** TapSlider.value는 scrollbar.value와 동일하게 해준다. */
        TapSlider.value = scrollbar.value;

        if (bIsDrag == false)
        {
            /** scrollbar.value에서  m_TargetPos까지 0.1씩 이동 */
            scrollbar.value = Mathf.Lerp(scrollbar.value, m_TargetPos, 0.1f);

            for (int i = 0; i < m_PanelSize; i++)
            {
                /** 버튼의 사이즈는 TargetIndex와 비교해서 맞으면 x크기를 360에 기존의 y값, 아니면 x값을 180으로 한다. */
                BtnRect[i].sizeDelta = new Vector2(i == CurrentBtnIndex ? 360 : 180, BtnRect[i].sizeDelta.y);
            }
        }

        /** 로비화면으로 이동 시 순간적으로 아이콘 모임 방지 */
        if (Time.time < 0.1f)
        {
            return;
        }


        for (int i = 0; i < m_PanelSize; i++)
        {
            /** BtnTargetPos는 BtnRect[i] 가지고 와서 anchoredPosition3D(Vector3D) 값으로 설정해준다.*/
            BtnTargetPos = BtnRect[i].anchoredPosition3D;
            BtnTargetScale = Vector3.one;
            TextActive = false;

            /** 만약 i가 햔재 TargetIndex와 같다면 */
            if (i == CurrentBtnIndex)
            {
                /** BtnTargetPos.y의 값을 -40으로 한다. */
                BtnTargetPos.y = -20.0f;
                /** BtnTargetScale x, y값을 1.2로 바꾼다. */
                BtnTargetScale = new Vector3(1.4f, 1.4f, 1f);
                TextActive = true;
            }

            /** BtnImageRect[i]의 anchoredPosition3D를 BtnImageRect[i].anchoredPosition3D에서 BtnTargetPos까지 0.25만큼 증가 */
            BtnImageRect[i].anchoredPosition3D = Vector3.Lerp(BtnImageRect[i].anchoredPosition3D, BtnTargetPos, 0.25f);
            /**  BtnImageRect[i].localScale은 BtnImageRect[i].localScale에서 BtnTargetScale까지 틱당 0.25의 크기로 증가*/
            BtnImageRect[i].localScale = Vector3.Lerp(BtnImageRect[i].localScale, BtnTargetScale, 0.25f);

            BtnImageRect[i].transform.GetChild(0).gameObject.SetActive(TextActive);
        }


    }

    /** IBeginDragHandler인터페이스 구현 (드래그 시작) */
    public void OnBeginDrag(PointerEventData eventData)
    {
        /** m_PanelSize사이즈만큼 반복 */
        for (int i = 0; i < m_PanelSize; i++)
        {
            /** 드래그 시작했을 때 위치가 저장된 m_Pos[i] 위치의 0.5배만큼 더 하거나 뺀 값이 */
            if (scrollbar.value < m_Pos[i] + m_Distance * 0.5 && scrollbar.value > m_Pos[i] - m_Distance * 0.5)
            {
                /** m_CurPos m_Pos[i]의 위치가 된다. */
                m_CurPos = m_Pos[i];
            }
        }

        bIsDrag = true;
    }

    /** IDragHandler 인터페이스 구현 (드래그 중) */
    public void OnDrag(PointerEventData eventData)
    {
       
    }

    /** IEndDragHandler 인터페이스 구현 (드래그 끝) */
    public void OnEndDrag(PointerEventData eventData)
    {
        /** bIsDrag가 끝났기 때문에 false */
        bIsDrag = false;

        m_TargetPos = SetPos();

        /** TODO ## 로비 화면 이동 전환 로직 */
        for (int i = 0; i < m_PanelSize; i++)
        {
            /** 만약 드래그가 끝났을을 때 현재 scroll.value가 m_Pos[i]에 저장된 value의 절반보다 작거나 클때. */
            if (scrollbar.value < m_Pos[i] + m_Distance * 0.5 && scrollbar.value > m_Pos[i] - m_Distance * 0.5)
            {
                /** scrollbar.value는 m_Pos[i]값이 된다. */
                m_TargetPos = m_Pos[i];
            }
        }

        /** 스크롤 수직 이동 관련 함수 호출 */
        VerticalScroll();
    }


    /** TODO ## 로비 매니저 스크롤 수직 이동관련 */
    void VerticalScroll()
    {
        /** 목표가 수직 스크롤이고, 화면을 옆에서 옮겨왔다면 수직 스크롤을 맨 아래로 내린다. */
        for (int i = 0; i < m_PanelSize; i++)
        {
            /** Content 자식에 ScrollScript가 있고 현재 위치가 다음 목표위치와 같고 현재위치가 같지 않을때 */
            if (ContentTr.GetChild(i).GetComponent<ScrollScript>() && m_TargetPos == m_Pos[i] && m_CurPos != m_Pos[i])
            {
                /** Content 자식의 GetChild(1)에서 Scrollbar의 value를 0으로 한다. */
                ContentTr.GetChild(i).GetChild(1).GetComponent<Scrollbar>().value = 0;
            }
        }
    }

    public void OnClickTab(int n)
    {
        /** 입력받은 값을 CurrentBtnIndex에 대입 */
        CurrentBtnIndex = n;
        /** m_TargetPos은 입력받은 n의 m_Pos[n] 위치가 된다. */
        m_TargetPos = m_Pos[n];
    }

    float SetPos()
    {
        /** 절반거리를 기준으로 가까운 위치를 반환 */
        for (int i = 0; i < m_PanelSize; i++)
        {
            if (scrollbar.value < m_Pos[i] + m_Distance * 0.5 && scrollbar.value > m_Pos[i] - m_Distance * 0.5)
            {
                /** CurrentBtnIndex에 i를 넣어준다. */
                CurrentBtnIndex = i;
                /** m_Pos[i] 반환한다. */
                return m_Pos[i];
            }
        }
        return 0;
    }
}
