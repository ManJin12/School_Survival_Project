using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My;
using UnityEngine.SceneManagement;

public class Weapon : MonoBehaviour
{

    /** ���� ���̵� */
    public int id;
    /** prefabId */
    public int prefabId;

    /** ���Ⱑ �ִ� ���ݷ� damage */
    public float damage;

    /** ���Ⱑ �ִ� ���ݷ� damage */
    public float NormalAttack;

    /** count ������ ���� */
    public int count;
    /** �Ҹ� ���� ���� Ƚ�� */
    public int per;
    /** ���� ȸ���ӵ� */
    public float speed;
    /** ������ ���� �� */
    public float UpDamage;
    /** ���� ���� */
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

        /** id�� ���� */
        switch (id)
        {
            /** id�� 0�̸� */
            case 0:
                /** id�� 0�̸� ĳ���� �������� �ݽð� �������� ȸ�� */
                transform.Rotate(Vector3.forward * speed * Time.deltaTime); 
                break;

            case 1:
            case 10:
            case 16:
                /** Timer�� ƽ�� ���� */
                Timer += Time.deltaTime;

                /** Timer�� speed���� ũ�� */
                if (Timer > speed)
                {
                    /** Timer 0���� �ʱ�ȭ */
                    Timer = 0.0f;

                    /** �߻� �Լ� ȣ�� */
                    Fire();
                }
                break;
            
            case 9:
            case 13:
                /** Timer�� ƽ�� ���� */
                Timer += Time.deltaTime;

                /** Timer�� speed���� ũ�� */
                if (Timer > speed)
                {
                    /** Timer 0���� �ʱ�ȭ */
                    Timer = 0.0f;

                    /** ũ��ó�� �߰��� ŸŶ�� ������ �߻� */
                    if (GameManager.GMInstance.CreatureScannerRef.CreatureNearestTarget)
                    {
                        /** �߻� �Լ� ȣ�� */
                        Fire();
                    }
                }
                break;

            default:
                break;

        }

        /** TODO ## Weapon.cs Test ���� */
        //if (Input.GetButtonDown("Jump"))
        //{
        //    Levelup(2, 1);
        //}
    }

    /** ������ �� ȣ��Ǵ� Init�Լ� */
    public void Init(SkillData data)
    {
        /** TODO ## ��ų ������ �ʱ�ȭ */
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

        /** id�� ���� */
        switch (id)
        {
            /** id�� 0�̸� */
            case 0:
                /** speed�� 150���� �Ѵ�. */
                speed = 150;
                /** Batch()�Լ� ȣ�� */
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
    /** TODO ## Weapon.cs ��ų ������ */
    /** ��ų ������ �� ȿ�� */
    public void Levelup(float _damage, int _count)
    {
        /** �Ű�������ŭ ������ ���� */
        this.damage = _damage;
        /** �Ű�������ŭ ���� ���� */
        this.count += _count;

        /** id�� 0�̸� */
        if (id == 0)
        {
            /** Batch�Լ� ȣ�� */
            Batch();
        }
        if (id == 2 || id == 3)
        {
            PlayerCtrl.BroadcastMessage("ApplyPassiveSkill", SendMessageOptions.DontRequireReceiver);
        }
    }

    /** TODO ## Weapon.cs ## ���� ���� */
    #region Near Weapon Batch
    void Batch()
    {
        /** ȸ���ϰ� �ִ� ������ ���ڸ�ŭ */
        for (int index = 0; index < count; index++)
        {
            Transform bullet;

            /** ���� index�� ������ �ڽĺ��� ������ */
            if (index < transform.childCount)
            {
                /** bullet�� �ڽ��� index������ �ȴ�. */
                bullet = transform.GetChild(index);
            }
            else
            {
                /** bullet�� PoolManager�� �ڽ����� �����´� */
                bullet = GameManager.GMInstance.PoolManagerRef.Get(prefabId).transform;
                /** bullet�� �θ�� �� ��ũ��Ʈ�� �����ϰ� �ִ� ������Ʈ�� �Ѵ�. */
                bullet.parent = transform;
            }

            /** bullet�� ��ġ���� Vector(0, 0, 0)���� �ٲ۴� */
            bullet.localPosition = Vector3.zero;
            /** bullet�� ȸ������  */
            bullet.localRotation = Quaternion.identity;

            /** ������ Weopon�� ��ġ��ų ���� ��� */
            Vector3 rotVec = Vector3.back * 360 * index / count;
            /** bullet�� ������ rotVec���� �Ѵ� */
            bullet.Rotate(rotVec);
            /** bullet�� ��ġ�� Space.World �������� up�������� 0.8��ŭ ���� ��ġ */
            bullet.Translate(bullet.up * 0.8f, Space.World);
            /** bullet�� scale�� 1.2�� ����� */
            bullet.transform.localScale = new Vector3(1f, 1f, 1f);
            /** bullet�� ��ũ��Ʈ���ִ� Init�Լ��� ���´�. */
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1 is Infinity per.
        }
    }
    #endregion

    /** TODO ## Weapon.cs ���Ÿ� ���� */
    #region Far Weapon Fire
    void Fire()
    {
        /** �÷��̾� ��Ʈ�ѷ����� ��ĵ�� ����� ���̾����� return */
        if (!PlayerCtrl.scanner.NearestTarget)
        {
            return;
        }

        /** bullet�� transform�� GameManager.GMInstance.PoolManagerRef.Get(prefabId).transform */
        Transform bullet = GameManager.GMInstance.PoolManagerRef.Get(prefabId).transform;
        Debug.Log(bullet.name);

        /**  bullet ������Ʈ �±װ� Bullet(���̾)�̶�� */
        if (bullet.CompareTag("Bullet"))
        {
            /** ȿ���� ��� */
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.FireBall);

            /** ����� ������ ��ġ */
            TargetPos = PlayerCtrl.scanner.NearestTarget.position;
            /** �÷��̾ ����� ���� ���� ���� */
            TargetDir = TargetPos - transform.position;
            /** TargetDir�� ����ȭ ���ش�(0, 1)*/
            TargetDir = TargetDir.normalized;

            /** bullet�� scale�� 1.2�� ����� */
            bullet.transform.localScale = new Vector2(0.8f, 0.8f);
            /** bullet�� ��ġ�� �� ��ũ��Ʈ�� ���� ������Ʈ�� ��ġ */
            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.down, TargetDir);

            bullet.GetComponent<Bullet>().Init(damage, per, TargetDir);
        }
        /** ���� bullet�±װ� IceArrow��� */
        else if (bullet.CompareTag("IceArrow"))
        {
            /** ȿ���� ��� */
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.IceArrow);

            /** ��ȯ���� ����� ������ ��ġ */
            TargetPos = GameManager.GMInstance.CreatureScannerRef.CreatureNearestTarget.position;
            /** �÷��̾ ����� ���� ���� ���� */
            TargetDir = TargetPos - GameManager.GMInstance.SkillManagerRef.IceArrow.transform.position;
            /** TargetDir�� ����ȭ ���ش�(0, 1)*/
            TargetDir = TargetDir.normalized;

            /** bullet�� scale�� 1.2�� ����� */
            // bullet.transform.localScale = new Vector2(0.06f, 0.06f);
            /** bullet�� ��ġ�� �� ��ũ��Ʈ�� ���� ������Ʈ�� ��ġ */
            bullet.position = GameManager.GMInstance.CreatureScannerRef.gameObject.transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.left, TargetDir);

            bullet.GetComponent<Bullet>().Init(damage, per, TargetDir);
        }
        /** �±װ� Arrow��� */
        else if (bullet.CompareTag("Arrow"))
        {
            /** ȿ���� ��� */
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.ArrowShoot);

            /** ����� ������ ��ġ */
            TargetPos = PlayerCtrl.scanner.NearestTarget.position;
            /** �÷��̾ ����� ���� ���� ���� */
            TargetDir = TargetPos - transform.position;
            /** TargetDir�� ����ȭ ���ش�(0, 1)*/
            TargetDir = TargetDir.normalized;

            /** bullet�� scale�� 1.2�� ����� */
            bullet.transform.localScale = new Vector2(0.7f, 0.7f);
            /** bullet�� ��ġ�� �� ��ũ��Ʈ�� ���� ������Ʈ�� ��ġ */
            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.down, TargetDir);

            bullet.GetComponent<Bullet>().Init(damage, per, TargetDir);
        }
        /** �±װ� WindSpiritAttack��� */
        else if (bullet.CompareTag("WindSpiritAttack"))
        {
            /** ȿ���� ��� */
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.ArrowShoot);

            /** �ٶ� ���ɰ� ����� ������ ��ġ */
            TargetPos = GameManager.GMInstance.CreatureScannerRef.CreatureNearestTarget.position;
            /** �ٶ� ������ ����� ���� ���� ���� */
            TargetDir = TargetPos - GameManager.GMInstance.SkillManagerRef.WindSpirit.transform.position;
            /** TargetDir�� ����ȭ ���ش�(0, 1)*/
            TargetDir = TargetDir.normalized;

            /** bullet�� scale�� 1.2�� ����� */
            bullet.transform.localScale = new Vector2(0.2f, 0.2f);
            /** bullet�� ��ġ�� �� ��ũ��Ʈ�� ���� ������Ʈ�� ��ġ */
            bullet.position = GameManager.GMInstance.SkillManagerRef.WindSpirit.transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.down, TargetDir);

            bullet.GetComponent<Bullet>().Init(damage, per, TargetDir);
        }
        /** �±װ� BombArrow��� */
        else if (bullet.CompareTag("BombArrow"))
        {
            /** ȿ���� ��� */
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.ArrowShoot);
            /** ����� ������ ��ġ */
            TargetPos = PlayerCtrl.scanner.NearestTarget.position;
            /** �÷��̾ ����� ���� ���� ���� */
            TargetDir = TargetPos - transform.position;
            /** TargetDir�� ����ȭ ���ش�(0, 1)*/
            TargetDir = TargetDir.normalized;

            /** bullet�� scale�� 1.2�� ����� */
            bullet.transform.localScale = new Vector2(1.2f, 1.2f);
            /** bullet�� ��ġ�� �� ��ũ��Ʈ�� ���� ������Ʈ�� ��ġ */
            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.down, TargetDir);

            bullet.GetComponent<Bullet>().Init(damage, per, TargetDir);
        }
    }
    #endregion
}
