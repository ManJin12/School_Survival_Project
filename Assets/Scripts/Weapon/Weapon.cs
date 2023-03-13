using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My;
using UnityEngine.SceneManagement;

public class Weapon : MonoBehaviour
{

    /** 웨폰 아이디 */
    public int id;
    /** prefabId */
    public int prefabId;
    /** 무기가 주는 공격력 damage */
    public float damage;
    /** count 웨폰의 개수 */
    public int count;
    /** 웨폰 회전속도 */
    public float speed;

    PlayerController PlayerCtrl;

    float Timer;

    public Vector3 TargetPos;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "PlayScene")
        {
            return;
        }

        PlayerCtrl = GetComponentInParent<PlayerController>();
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "PlayScene")
        {
            return;
        }
        /** id에 따른 */
        switch (id)
        {
            /** id가 0이면 */
            case 0:
                /** id가 0이면 캐릭터 기준으로 반시계 방향으로 회전 */
                transform.Rotate(Vector3.forward * speed * Time.deltaTime); 
                break;

            case 1:
                /** Timer는 틱당 증가 */

                Timer += Time.deltaTime;
                /** Timer가 speed보다 크면 */
                if (Timer > speed)
                {
                    /** Timer 0으로 초기화 */
                    Timer = 0.0f;

                    /** 발사 함수 호출 */
                    Fire();
                   
                }
                break;

            default:
                break;

        }

        /** TODO## Test 수정 */
        if (Input.GetButtonDown("Jump"))
        {
            Levelup(20, 1);
        }
    }

    /** 시작할 때 호출되는 Init함수 */
    public void Init()
    {
        /** id에 따른 */
        switch (id)
        {
            /** id가 0이면 */
            case 0:
                /** speed를 150으로 한다. */
                speed = 150;
                /** Batch()함수 호출 */
                Batch();
                break;

            case 1:
                break;

            default:
                break;

        }
    }

    /** 스킬 레벨업 시 효과 */
    public void Levelup(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
            Batch();
    }

    void Batch()
    {
        /** 회전하고 있는 무기의 숫자만큼 */
        for (int index = 0; index < count; index++)
        {
            Transform bullet;

            /** 만약 index가 생성된 자식보다 작으면 */
            if (index < transform.childCount)
            {
                /** bullet은 자식의 index순번이 된다. */
                bullet = transform.GetChild(index);
            }
            else
            {
                /** bullet을 PoolManager의 자식으로 가져온다 */
                bullet = GameManager.GMInstance.PoolManagerRef.Get(prefabId).transform;
                /** bullet의 부모는 이 스크립트를 소유하고 있는 오브젝트로 한다. */
                bullet.parent = transform;
            }

            /** bullet의 위치값을 Vector(0, 0, 0)으로 바꾼다 */
            bullet.localPosition = Vector3.zero;
            /** bullet의 회전값을  */
            bullet.localRotation = Quaternion.identity;

            /** 생성된 Weopon을 위치시킬 각도 계산 */
            Vector3 rotVec = Vector3.forward * 360 * index / count;
            /** bullet의 각도는 rotVec으로 한다 */
            bullet.Rotate(rotVec);
            /** bullet의 위치는 Space.World 기준으로 up방향으로 0.8만큼 위로 위치 */
            bullet.Translate(bullet.up * 0.8f, Space.World);
            /** bullet의 scale은 1.2로 만든다 */
            bullet.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            /** bullet의 스크립트에있는 Init함수를 들고온다. */
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1 is Infinity per.
        }
    }

    void Fire()
    {
        /** 플레이어 컨트롤러에서 스캔된 가까운 적이없으면 return */
        if (!PlayerCtrl.scanner.NearestTarget)
        {
            return;
        }

        /** 가까운 몬스터의 위치 */
        TargetPos = PlayerCtrl.scanner.NearestTarget.position;
        /** 플레이어가 가까운 적을 보는 방향 */
        Vector3 TargetDir = TargetPos - transform.position;
        /** TargetDir을 정규화 해준다(0, 1)*/
        TargetDir = TargetDir.normalized;


        /** bullet의 transform은 GameManager.GMInstance.PoolManagerRef.Get(prefabId).transform */
        Transform bullet = GameManager.GMInstance.PoolManagerRef.Get(prefabId).transform;
        /** bullet의 scale은 1.2로 만든다 */
        bullet.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        /** bullet의 위치는 이 스크립트를 지닌 오브젝트의 위치 */
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, TargetDir);

        bullet.GetComponent<Bullet>().Init(damage, count, TargetDir);
        
    }
}
