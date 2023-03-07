using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D Coll;

    void Start()
    {
        Coll = GetComponent<Collider2D>();
    }

    /** 충돌했을때 발생하는 이벤트 */
    void OnTriggerExit2D(Collider2D collision)
    {
        /** 태그 이름이 Area가 아니면 */
        if (!collision.CompareTag("Area"))
        {
            /** 함수를 빠져나간다.*/
            return;
        }

        /** 캐릭터의 위치 값 */
        Vector3 PlayerPos =
            GameManager.GMInstance.playerCtrl.transform.position;

        /** 이 클래스를 가진 오브젝트의 위치 값 */
        Vector3 MyPos = transform.position;

        /** 플레이어와 클래스를 가진 오브젝트와 x거리차이 */
        float diff_X = Mathf.Abs(PlayerPos.x - MyPos.x);

        /** 플레이어와 클래스를 가진 오브젝트와 y거리차이 */
        float diff_Y = Mathf.Abs(PlayerPos.y - MyPos.y);

        /** 플레이어가 입력받은 값 */
        Vector3 PlayerDir = GameManager.GMInstance.playerCtrl.m_InputVec;

        /** 삼항 연산자로 플레이어의 x값의 크기로 방향을 구한다. */
        float DirX = PlayerDir.x < 0 ? -1 : 1;
        /** 삼항 연산자로 플레이어의 y값의 크기로 방향을 구한다. */
        float DirY = PlayerDir.y < 0 ? -1 : 1;


        /** TODO ## 몬스터 및 타일 재배치 로직 */
        switch (transform.tag)
        {
            case "Ground":
                /** 두 오브젝트의 거리 차이가 x축이 더 크다면 */
                if (diff_X > diff_Y)
                {
                    /** 오브젝트를 오른쪽 방향으로 40만큼 이동한다.  */
                    transform.Translate(Vector3.right * DirX * 40);

                }
                /** 두 오브젝트의 거리차이가 y축이 더 크다면 */
                else if (diff_X < diff_Y)
                {
                    /** 오브젝트를 윗 방향으로 40만큼 이동한다.  */
                    transform.Translate(Vector3.up * DirY * 40);

                }
                break;

            case "Monster":
                if (Coll.enabled)
                {
                    /** 오브젝트의 방향에서 20만큼 떨어진 곳에 생성.  */
                    transform.Translate(PlayerDir * 20
                        + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0.0f));
                }
                break;
        }
    }
}