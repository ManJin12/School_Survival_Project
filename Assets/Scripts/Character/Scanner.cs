using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My;
public class Scanner : MonoBehaviour
{
    /** 스캔할 범위 */
    public float ScanRange;

    /** 어느 오브젝트를 검색할지 */
    public LayerMask TargetLayer;

    /** 스캔한 결과를 저장할 배열 */
    public RaycastHit2D[] Targets;

    /** 가장 가까운 목표를 담을 변수 */
    public Transform NearestTarget;


    void Start()
    {
        
    }
    void FixedUpdate()
    {
        /** 
        Targets는 CircleCastAll함수를 이용해서 현재 위치에서 ScanRange의 반지름만큼 캐스팅 방향은 없고,
        캐스팅 길이도 원형이여서 0으로 한 다음 TargetLayer를 검색한다.
        */
        Targets = Physics2D.CircleCastAll(transform.position, ScanRange, Vector2.zero, 0, TargetLayer);
        /** NearestTarget은 스캔된 타겟 중 가까운 오브젝트 위치를 반환한다. */
        NearestTarget = GetNearest();
    }

    Transform GetNearest()
    {
        /** Transform을 반환할 result 변수선언 */
        Transform result = null;
        /** 거리 계산 */
        float Diff = 100.0f;

        /** Targets배열에 저장된 값을 Target으로 불러온다 */
        foreach (RaycastHit2D Target in Targets)
        {
            /** MyPos는 이 스크립트를 가지고 있는 오브젝트의 위치 */
            Vector3 MyPos = transform.position;
            /** TargetPos는 foreach문에서 검색한 Target의 위치*/
            Vector3 TargetPos = Target.transform.position;
            /** 몬스터와 플레이어 사이의 거리 */
            float CurDiff = Vector3.Distance(MyPos, TargetPos);

            /** 현재 거리가 Diff보다 작으면 */
            if (CurDiff < Diff)
            {
                /** Diff는 CurDiff로 바꿔준다. */
                Diff = CurDiff;
                /** result는 Target.transform을 가진다. */
                result = Target.transform;
            }
        }

        return result;
    }
}
