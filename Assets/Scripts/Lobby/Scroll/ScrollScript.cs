using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using My;

public class ScrollScript : ScrollRect
{
    /** �θ� ��Ʈ�� �ϴ��� ���ϴ��� */
    bool bForParent;
    ScrollManager scrollmgr;
    ScrollRect ParentScrollRect;

    protected override void Start()
    {
        /** scrollmgr�� ScrollManager�� �����´� */
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

        /** ParentScrollRect�� �θ��� ScrollRect���� �����´�. */
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

    /** OnBeginDrag ��� */
    public override void OnBeginDrag(PointerEventData eventData)
    {
        /** �巡�� �����ϴ� ���� �����̵��� ũ�� �θ� �巡�� �����Ѱ�, �����̵��� ũ�� �ڽ��� �巡�� ������ �� */
        bForParent = Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y);

        /** bForParent�� true�� */
        if (bForParent == true)
        {
            /** �Է¹��� eventData�� �θ��� OnBeginDrag�Լ��� �Ű������� ��� */
            scrollmgr.OnBeginDrag(eventData);
            /** scrollmgr���� �����ָ� �������� �ʱ⶧���� ParentScrollRect���� ���� ����������Ѵ�. */
            ParentScrollRect.OnBeginDrag(eventData);
        }
        else if (bForParent == false)
        {
            /** �ڽ��� OnBeginDrag�Լ� ���� */
            base.OnBeginDrag(eventData);
        }
    }

    /** OnDrag ��� */
    public override void OnDrag(PointerEventData eventData)
    {
        /** bForParent�� true�� */
        if (bForParent == true)
        {
            /** �Է¹��� eventData�� �θ��� OnDrag�Լ��� �Ű������� ����Ͽ� ���� */
            scrollmgr.OnDrag(eventData);
            /** scrollmgr���� �����ָ� �������� �ʱ⶧���� ParentScrollRect���� ���� ����������Ѵ�. */
            ParentScrollRect.OnDrag(eventData);
        }
        else if (bForParent == false)
        {
            /** �ڽ��� OnDrag�Լ� ���� */
            base.OnDrag(eventData);
        }
    }

    /** OnEndDrag ��� */
    public override void OnEndDrag(PointerEventData eventData)
    {
        /** bForParent�� true�� */
        if (bForParent == true)
        {
            /** �Է¹��� eventData�� �θ��� OnEndDrag�Լ��� �Ű������� ����Ͽ� ���� */
            scrollmgr.OnEndDrag(eventData);
            /** scrollmgr���� �����ָ� �������� �ʱ⶧���� ParentScrollRect���� ���� ����������Ѵ�. */
            ParentScrollRect.OnEndDrag(eventData);
        }
        else if (bForParent == false)
        {
            /** �ڽ��� OnEndDrag�Լ� ���� */
            base.OnEndDrag(eventData);
        }
    }
}
