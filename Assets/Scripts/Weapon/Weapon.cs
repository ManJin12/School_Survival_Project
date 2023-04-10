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


    PlayerController PlayerCtrl;

    float Timer;

    public Vector3 TargetPos;

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

        PlayerCtrl.BroadcastMessage("ApplyPassiveSkill", SendMessageOptions.DontRequireReceiver);
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
            bullet.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            /** bullet�� ��ũ��Ʈ���ִ� Init�Լ��� ����´�. */
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

        /** ����� ������ ��ġ */
        TargetPos = PlayerCtrl.scanner.NearestTarget.position;
        /** �÷��̾ ����� ���� ���� ���� */
        Vector3 TargetDir = TargetPos - transform.position;
        /** TargetDir�� ����ȭ ���ش�(0, 1)*/
        TargetDir = TargetDir.normalized;


        /** bullet�� transform�� GameManager.GMInstance.PoolManagerRef.Get(prefabId).transform */
        Transform bullet = GameManager.GMInstance.PoolManagerRef.Get(prefabId).transform;
        /** bullet�� scale�� 1.2�� ����� */
        bullet.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        /** bullet�� ��ġ�� �� ��ũ��Ʈ�� ���� ������Ʈ�� ��ġ */
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, TargetDir);

        bullet.GetComponent<Bullet>().Init(damage, per, TargetDir);
    }
    #endregion
}