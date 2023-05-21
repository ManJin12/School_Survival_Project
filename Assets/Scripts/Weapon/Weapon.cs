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

    /** 무기가 주는 공격력 damage */
    public float NormalAttack;

    /** count 웨폰의 개수 */
    public int count;
    /** 불릿 몬스터 관통 횟수 */
    public int per;
    /** 웨폰 회전속도 */
    public float speed;
    /** 데미지 증가 폭 */
    public float UpDamage;
    /** 개수 증가 */
    public int UpCount;
    public int Index = 0;


    public PlayerController PlayerCtrl;

    float Timer;

    public Vector3 TargetPos;
    public Vector3 TargetDir;

    private void Awake()
    {
        PlayerCtrl = GameManager.GMInstance.playerCtrl;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.GMInstance.CurrentScene != Define.ESceneType.PlayScene)
        {
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GMInstance.bIsLive == false)
        {
            return;
        }

        if (GameManager.GMInstance.CurrentScene != Define.ESceneType.PlayScene)
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
            case 10:
            case 16:
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
            
            case 9:
            case 13:
                /** Timer는 틱당 증가 */
                Timer += Time.deltaTime;

                /** Timer가 speed보다 크면 */
                if (Timer > speed)
                {
                    /** Timer 0으로 초기화 */
                    Timer = 0.0f;

                    /** 크리처가 발견한 타킷이 있으면 발사 */
                    if (GameManager.GMInstance.CreatureScannerRef.CreatureNearestTarget)
                    {
                        /** 발사 함수 호출 */
                        Fire();
                    }
                }
                break;

            default:
                break;

        }

        /** TODO ## Weapon.cs Test 수정 */
        //if (Input.GetButtonDown("Jump"))
        //{
        //    Levelup(2, 1);
        //}
    }

    /** 시작할 때 호출되는 Init함수 */
    public void Init(SkillData data)
    {
        /** TODO ## 스킬 데이터 초기화 */
        //Basic Set
        name = "weapon " + data.Skill_Id;
        transform.parent = GameManager.GMInstance.Player.transform;
        transform.localPosition = Vector3.zero;

        // Property Set
        id = data.Skill_Id;
        damage = data.baseDamage;
        count = data.baseCount;
        speed = data.SkillSpeed;

        for(int index = 0; index < GameManager.GMInstance.PoolManagerRef.MonsterPrefabs.Length; index++)
        {
            if(data.projectile == GameManager.GMInstance.PoolManagerRef.MonsterPrefabs[index])
            {
                prefabId = index;
                break;
            }
        }

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
            case 9:
                break;

            default:
                break;
        }
        PlayerCtrl.BroadcastMessage("ApplyPassiveSkill", SendMessageOptions.DontRequireReceiver);
    }
    /** TODO ## Weapon.cs 스킬 레벨업 */
    /** 스킬 레벨업 시 효과 */
    public void Levelup(float _damage, int _count)
    {
        /** 매개변수만큼 데미지 증가 */
        this.damage = _damage;
        /** 매개변수만큼 수량 증가 */
        this.count += _count;

        /** id가 0이면 */
        if (id == 0)
        {
            /** Batch함수 호출 */
            Batch();
        }
        if (id == 2 || id == 3)
        {
            PlayerCtrl.BroadcastMessage("ApplyPassiveSkill", SendMessageOptions.DontRequireReceiver);
        }
    }

    /** TODO ## Weapon.cs ## 근접 무기 */
    #region Near Weapon Batch
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
            Vector3 rotVec = Vector3.back * 360 * index / count;
            /** bullet의 각도는 rotVec으로 한다 */
            bullet.Rotate(rotVec);
            /** bullet의 위치는 Space.World 기준으로 up방향으로 0.8만큼 위로 위치 */
            bullet.Translate(bullet.up * 0.8f, Space.World);
            /** bullet의 scale은 1.2로 만든다 */
            bullet.transform.localScale = new Vector3(1f, 1f, 1f);
            /** bullet의 스크립트에있는 Init함수를 들고온다. */
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1 is Infinity per.
        }
    }
    #endregion

    /** TODO ## Weapon.cs 원거리 무기 */
    #region Far Weapon Fire
    void Fire()
    {
        /** 플레이어 컨트롤러에서 스캔된 가까운 적이없으면 return */
        if (!PlayerCtrl.scanner.NearestTarget)
        {
            return;
        }

        /** bullet의 transform은 GameManager.GMInstance.PoolManagerRef.Get(prefabId).transform */
        Transform bullet = GameManager.GMInstance.PoolManagerRef.Get(prefabId).transform;
        Debug.Log(bullet.name);

        /**  bullet 오브젝트 태그가 Bullet(파이어볼)이라면 */
        if (bullet.CompareTag("Bullet"))
        {
            /** 효과음 재생 */
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.FireBall);

            /** 가까운 몬스터의 위치 */
            TargetPos = PlayerCtrl.scanner.NearestTarget.position;
            /** 플레이어가 가까운 적을 보는 방향 */
            TargetDir = TargetPos - transform.position;
            /** TargetDir을 정규화 해준다(0, 1)*/
            TargetDir = TargetDir.normalized;

            /** bullet의 scale은 1.2로 만든다 */
            bullet.transform.localScale = new Vector2(0.8f, 0.8f);
            /** bullet의 위치는 이 스크립트를 지닌 오브젝트의 위치 */
            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.down, TargetDir);

            bullet.GetComponent<Bullet>().Init(damage, per, TargetDir);
        }
        /** 만약 bullet태그가 IceArrow라면 */
        else if (bullet.CompareTag("IceArrow"))
        {
            /** 효과음 재생 */
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.IceArrow);

            /** 소환수와 가까운 몬스터의 위치 */
            TargetPos = GameManager.GMInstance.CreatureScannerRef.CreatureNearestTarget.position;
            /** 플레이어가 가까운 적을 보는 방향 */
            TargetDir = TargetPos - GameManager.GMInstance.SkillManagerRef.IceArrow.transform.position;
            /** TargetDir을 정규화 해준다(0, 1)*/
            TargetDir = TargetDir.normalized;

            /** bullet의 scale은 1.2로 만든다 */
            // bullet.transform.localScale = new Vector2(0.06f, 0.06f);
            /** bullet의 위치는 이 스크립트를 지닌 오브젝트의 위치 */
            bullet.position = GameManager.GMInstance.CreatureScannerRef.gameObject.transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.left, TargetDir);

            bullet.GetComponent<Bullet>().Init(damage, per, TargetDir);
        }
        /** 태그가 Arrow라면 */
        else if (bullet.CompareTag("Arrow"))
        {
            /** 효과음 재생 */
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.ArrowShoot);

            /** 가까운 몬스터의 위치 */
            TargetPos = PlayerCtrl.scanner.NearestTarget.position;
            /** 플레이어가 가까운 적을 보는 방향 */
            TargetDir = TargetPos - transform.position;
            /** TargetDir을 정규화 해준다(0, 1)*/
            TargetDir = TargetDir.normalized;

            /** bullet의 scale은 1.2로 만든다 */
            bullet.transform.localScale = new Vector2(0.7f, 0.7f);
            /** bullet의 위치는 이 스크립트를 지닌 오브젝트의 위치 */
            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.down, TargetDir);

            bullet.GetComponent<Bullet>().Init(damage, per, TargetDir);
        }
        /** 태그가 WindSpiritAttack라면 */
        else if (bullet.CompareTag("WindSpiritAttack"))
        {
            /** 효과음 재생 */
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.ArrowShoot);

            /** 바람 정령과 가까운 몬스터의 위치 */
            TargetPos = GameManager.GMInstance.CreatureScannerRef.CreatureNearestTarget.position;
            /** 바람 정령이 가까운 적을 보는 방향 */
            TargetDir = TargetPos - GameManager.GMInstance.SkillManagerRef.WindSpirit.transform.position;
            /** TargetDir을 정규화 해준다(0, 1)*/
            TargetDir = TargetDir.normalized;

            /** bullet의 scale은 1.2로 만든다 */
            bullet.transform.localScale = new Vector2(0.2f, 0.2f);
            /** bullet의 위치는 이 스크립트를 지닌 오브젝트의 위치 */
            bullet.position = GameManager.GMInstance.SkillManagerRef.WindSpirit.transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.down, TargetDir);

            bullet.GetComponent<Bullet>().Init(damage, per, TargetDir);
        }
        /** 태그가 BombArrow라면 */
        else if (bullet.CompareTag("BombArrow"))
        {
            /** 효과음 재생 */
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.ArrowShoot);
            /** 가까운 몬스터의 위치 */
            TargetPos = PlayerCtrl.scanner.NearestTarget.position;
            /** 플레이어가 가까운 적을 보는 방향 */
            TargetDir = TargetPos - transform.position;
            /** TargetDir을 정규화 해준다(0, 1)*/
            TargetDir = TargetDir.normalized;

            /** bullet의 scale은 1.2로 만든다 */
            bullet.transform.localScale = new Vector2(1.2f, 1.2f);
            /** bullet의 위치는 이 스크립트를 지닌 오브젝트의 위치 */
            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.down, TargetDir);

            bullet.GetComponent<Bullet>().Init(damage, per, TargetDir);
        }
    }
    #endregion
}
