using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using My;
public class ScrollManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /** ScrollbarŸ�� ������ �� �ִ� ���� ���� */
    public Scrollbar scrollbar;

    public Transform ContentTr;

    public Slider TapSlider;

    public RectTransform[] BtnRect;
    public RectTransform[] BtnImageRect;

    Vector3 BtnTargetPos;
    Vector3 BtnTargetScale;

    /** �κ�ȭ���� �ǳ� ���� */
    const int m_PanelSize = 5;
    /** ���� ��ư�� �ε��� ���� */
    int CurrentBtnIndex;

    /** ScrollBar Horizontal�� Value�� �����ϱ� ���� */
    float[] m_Pos = new float[m_PanelSize];
    /** �ǳ��� ��ġ�� �Ÿ� */
    float m_Distance;
    /** ȭ�� ��ȯ �� ��ǥ ��ġ */
    float m_TargetPos;
    /** ���� ��ġ */
    float m_CurPos;

    /** �巡�� �� ���� Ȯ�� */
    bool bIsDrag;
    /** Tap�ؽ�Ʈ Ȱ��ȭ */
    bool TextActive;

    // Start is called before the first frame update
    void Start()
    {

        GameManager.GMInstance.ScrollManagerRef = this;

        /** �ǳ��� ��ġ�� �Ÿ�ũ��� ��ü Value 1���� ��ü �ǳڰ������� -1�� ���ָ� �ȴ�. */
        m_Distance = 1.0f / (m_PanelSize - 1);
        
        /** �� �ǳ��� Value�� 0, 0.25, 0.5, 0.75, 1�� ����ȴ� */
        for (int i = 0; i < m_PanelSize; i++)
        {
            /** m_Pos[i]�� �ǳ��� ������ŭ ���� Value ���� */
            m_Pos[i] = m_Distance * i;
        }

    }

    // Update is called once per frame
    void Update()
    {
        

        /** TapSlider.value�� scrollbar.value�� �����ϰ� ���ش�. */
        TapSlider.value = scrollbar.value;

        if (bIsDrag == false)
        {
            /** scrollbar.value����  m_TargetPos���� 0.1�� �̵� */
            scrollbar.value = Mathf.Lerp(scrollbar.value, m_TargetPos, 0.1f);

            for (int i = 0; i < m_PanelSize; i++)
            {
                /** ��ư�� ������� TargetIndex�� ���ؼ� ������ xũ�⸦ 360�� ������ y��, �ƴϸ� x���� 180���� �Ѵ�. */
                BtnRect[i].sizeDelta = new Vector2(i == CurrentBtnIndex ? 360 : 180, BtnRect[i].sizeDelta.y);
            }
        }

        /** �κ�ȭ������ �̵� �� ���������� ������ ���� ���� */
        if (Time.time < 0.1f)
        {
            return;
        }


        for (int i = 0; i < m_PanelSize; i++)
        {
            /** BtnTargetPos�� BtnRect[i] ������ �ͼ� anchoredPosition3D(Vector3D) ������ �������ش�.*/
            BtnTargetPos = BtnRect[i].anchoredPosition3D;
            BtnTargetScale = Vector3.one;
            TextActive = false;

            /** ���� i�� �h�� TargetIndex�� ���ٸ� */
            if (i == CurrentBtnIndex)
            {
                /** BtnTargetPos.y�� ���� -40���� �Ѵ�. */
                BtnTargetPos.y = -20.0f;
                /** BtnTargetScale x, y���� 1.2�� �ٲ۴�. */
                BtnTargetScale = new Vector3(1.4f, 1.4f, 1f);
                TextActive = true;
            }

            /** BtnImageRect[i]�� anchoredPosition3D�� BtnImageRect[i].anchoredPosition3D���� BtnTargetPos���� 0.25��ŭ ���� */
            BtnImageRect[i].anchoredPosition3D = Vector3.Lerp(BtnImageRect[i].anchoredPosition3D, BtnTargetPos, 0.25f);
            /**  BtnImageRect[i].localScale�� BtnImageRect[i].localScale���� BtnTargetScale���� ƽ�� 0.25�� ũ��� ����*/
            BtnImageRect[i].localScale = Vector3.Lerp(BtnImageRect[i].localScale, BtnTargetScale, 0.25f);

            BtnImageRect[i].transform.GetChild(0).gameObject.SetActive(TextActive);
        }


    }

    /** IBeginDragHandler�������̽� ���� (�巡�� ����) */
    public void OnBeginDrag(PointerEventData eventData)
    {
        /** m_PanelSize�����ŭ �ݺ� */
        for (int i = 0; i < m_PanelSize; i++)
        {
            /** �巡�� �������� �� ��ġ�� ����� m_Pos[i] ��ġ�� 0.5�踸ŭ �� �ϰų� �� ���� */
            if (scrollbar.value < m_Pos[i] + m_Distance * 0.5 && scrollbar.value > m_Pos[i] - m_Distance * 0.5)
            {
                /** m_CurPos m_Pos[i]�� ��ġ�� �ȴ�. */
                m_CurPos = m_Pos[i];
            }
        }

        bIsDrag = true;
    }

    /** IDragHandler �������̽� ���� (�巡�� ��) */
    public void OnDrag(PointerEventData eventData)
    {
       
    }

    /** IEndDragHandler �������̽� ���� (�巡�� ��) */
    public void OnEndDrag(PointerEventData eventData)
    {
        /** bIsDrag�� ������ ������ false */
        bIsDrag = false;

        m_TargetPos = SetPos();

        /** TODO ## �κ� ȭ�� �̵� ��ȯ ���� */
        for (int i = 0; i < m_PanelSize; i++)
        {
            /** ���� �巡�װ� �������� �� ���� scroll.value�� m_Pos[i]�� ����� value�� ���ݺ��� �۰ų� Ŭ��. */
            if (scrollbar.value < m_Pos[i] + m_Distance * 0.5 && scrollbar.value > m_Pos[i] - m_Distance * 0.5)
            {
                /** scrollbar.value�� m_Pos[i]���� �ȴ�. */
                m_TargetPos = m_Pos[i];
            }
        }

        /** ��ũ�� ���� �̵� ���� �Լ� ȣ�� */
        VerticalScroll();
    }


    /** TODO ## �κ� �Ŵ��� ��ũ�� ���� �̵����� */
    void VerticalScroll()
    {
        /** ��ǥ�� ���� ��ũ���̰�, ȭ���� ������ �ŰܿԴٸ� ���� ��ũ���� �� �Ʒ��� ������. */
        for (int i = 0; i < m_PanelSize; i++)
        {
            /** Content �ڽĿ� ScrollScript�� �ְ� ���� ��ġ�� ���� ��ǥ��ġ�� ���� ������ġ�� ���� ������ */
            if (ContentTr.GetChild(i).GetComponent<ScrollScript>() && m_TargetPos == m_Pos[i] && m_CurPos != m_Pos[i])
            {
                /** Content �ڽ��� GetChild(1)���� Scrollbar�� value�� 0���� �Ѵ�. */
                ContentTr.GetChild(i).GetChild(1).GetComponent<Scrollbar>().value = 0;
            }
        }
    }

    public void OnClickTab(int n)
    {
        /** �Է¹��� ���� CurrentBtnIndex�� ���� */
        CurrentBtnIndex = n;
        /** m_TargetPos�� �Է¹��� n�� m_Pos[n] ��ġ�� �ȴ�. */
        m_TargetPos = m_Pos[n];
    }

    float SetPos()
    {
        /** ���ݰŸ��� �������� ����� ��ġ�� ��ȯ */
        for (int i = 0; i < m_PanelSize; i++)
        {
            if (scrollbar.value < m_Pos[i] + m_Distance * 0.5 && scrollbar.value > m_Pos[i] - m_Distance * 0.5)
            {
                /** CurrentBtnIndex�� i�� �־��ش�. */
                CurrentBtnIndex = i;
                /** m_Pos[i] ��ȯ�Ѵ�. */
                return m_Pos[i];
            }
        }
        return 0;
    }
}
