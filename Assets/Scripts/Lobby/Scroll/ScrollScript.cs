using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using My;

public class ScrollScript : ScrollRect
{
    /** 부모가 컨트롤 하는지 안하는지 */
    bool bForParent;
    ScrollManager scrollmgr;
    ScrollRect ParentScrollRect;

    protected override void Start()
    {
        /** scrollmgr은 ScrollManager를 가져온다 */
        scrollmgr = GameManager.GMInstance.ScrollManagerRef;
        if (scrollmgr == null)
        {
            Debug.Log("ScrollScript : scrollmgr Fail");
            return;
        }
        else
        {
            Debug.Log("ScrollScript : scrollmgr Success");
        }

        /** ParentScrollRect는 부모의 ScrollRect값을 가져온다. */
        ParentScrollRect = GameManager.GMInstance.ScrollManagerRef.GetComponent<ScrollRect>();
        if (ParentScrollRect == null)
        {
            Debug.Log("ScrollScript : ParentScrollRect Fail");
            return;
        }
        else
        {
            Debug.Log("ScrollScript : ParentScrollRect Success");
        }
    }

    /** OnBeginDrag 상속 */
    public override void OnBeginDrag(PointerEventData eventData)
    {
        /** 드래그 시작하는 순간 수평이동이 크면 부모가 드래그 시작한것, 수직이동이 크면 자식이 드래그 시작한 것 */
        bForParent = Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y);

        /** bForParent이 true면 */
        if (bForParent == true)
        {
            /** 입력받은 eventData를 부모의 OnBeginDrag함수의 매개변수로 사용 */
            scrollmgr.OnBeginDrag(eventData);
            /** scrollmgr에만 전해주면 움직이지 않기때문에 ParentScrollRect에도 값을 전달해줘야한다. */
            ParentScrollRect.OnBeginDrag(eventData);
        }
        else if (bForParent == false)
        {
            /** 자식의 OnBeginDrag함수 실행 */
            base.OnBeginDrag(eventData);
        }
    }

    /** OnDrag 상속 */
    public override void OnDrag(PointerEventData eventData)
    {
        /** bForParent이 true면 */
        if (bForParent == true)
        {
            /** 입력받은 eventData를 부모의 OnDrag함수로 매개변수로 사용하여 실행 */
            scrollmgr.OnDrag(eventData);
            /** scrollmgr에만 전해주면 움직이지 않기때문에 ParentScrollRect에도 값을 전달해줘야한다. */
            ParentScrollRect.OnDrag(eventData);
        }
        else if (bForParent == false)
        {
            /** 자식의 OnDrag함수 실행 */
            base.OnDrag(eventData);
        }
    }

    /** OnEndDrag 상속 */
    public override void OnEndDrag(PointerEventData eventData)
    {
        /** bForParent이 true면 */
        if (bForParent == true)
        {
            /** 입력받은 eventData를 부모의 OnEndDrag함수로 매개변수로 사용하여 실행 */
            scrollmgr.OnEndDrag(eventData);
            /** scrollmgr에만 전해주면 움직이지 않기때문에 ParentScrollRect에도 값을 전달해줘야한다. */
            ParentScrollRect.OnEndDrag(eventData);
        }
        else if (bForParent == false)
        {
            /** 자식의 OnEndDrag함수 실행 */
            base.OnEndDrag(eventData);
        }
    }
}
