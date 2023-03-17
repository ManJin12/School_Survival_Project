using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My;
public class Scanner : MonoBehaviour
{
    /** ��ĵ�� ���� */
    public float ScanRange;

    /** ��� ������Ʈ�� �˻����� */
    public LayerMask TargetLayer;

    /** ��ĵ�� ����� ������ �迭 */
    public RaycastHit2D[] Targets;

    /** ���� ����� ��ǥ�� ���� ���� */
    public Transform NearestTarget;


    void Start()
    {
        
    }
    void FixedUpdate()
    {
        /** 
        Targets�� CircleCastAll�Լ��� �̿��ؼ� ���� ��ġ���� ScanRange�� ��������ŭ ĳ���� ������ ����,
        ĳ���� ���̵� �����̿��� 0���� �� ���� TargetLayer�� �˻��Ѵ�.
        */
        Targets = Physics2D.CircleCastAll(transform.position, ScanRange, Vector2.zero, 0, TargetLayer);
        /** NearestTarget�� ��ĵ�� Ÿ�� �� ����� ������Ʈ ��ġ�� ��ȯ�Ѵ�. */
        NearestTarget = GetNearest();
    }

    Transform GetNearest()
    {
        /** Transform�� ��ȯ�� result �������� */
        Transform result = null;
        /** �Ÿ� ��� */
        float Diff = 100.0f;

        /** Targets�迭�� ����� ���� Target���� �ҷ��´� */
        foreach (RaycastHit2D Target in Targets)
        {
            /** MyPos�� �� ��ũ��Ʈ�� ������ �ִ� ������Ʈ�� ��ġ */
            Vector3 MyPos = transform.position;
            /** TargetPos�� foreach������ �˻��� Target�� ��ġ*/
            Vector3 TargetPos = Target.transform.position;
            /** ���Ϳ� �÷��̾� ������ �Ÿ� */
            float CurDiff = Vector3.Distance(MyPos, TargetPos);

            /** ���� �Ÿ��� Diff���� ������ */
            if (CurDiff < Diff)
            {
                /** Diff�� CurDiff�� �ٲ��ش�. */
                Diff = CurDiff;
                /** result�� Target.transform�� ������. */
                result = Target.transform;
            }
        }

        return result;
    }
}
