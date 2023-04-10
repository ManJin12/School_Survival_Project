using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My;

public class WizardPassiveSkill : MonoBehaviour
{
    public SkillData.SkillType type;
    public float rate;

    /** �ʱ�ȭ �Լ� */
    public void Init(SkillData data)
    {
        // Basic Set
        /** Gear + ��ų���̵� */
        name = "Gear " + data.Skill_Id;
        /** �Լ��� �θ�� �÷��̾� ������Ʈ�� �Ѵ� */
        transform.parent = GameManager.GMInstance.Player.transform;
        /** transform�� x,y,z�� 0���� ���ش�. */
        transform.localPosition = Vector3.zero;

        // Property Set
        type = data.skillType;
        rate = data.damageRates[0];
        ApplyPassiveSkill();
    }

    public void LevelUp(float _rate)
    {
        /** �� Ŭ������ rate�� �Ű������� ���� _rate�� �ʱ�ȭ�Ѵ�. */
        this.rate = _rate;
        /** �Լ� ȣ�� */
        ApplyPassiveSkill();
    }

    void ApplyPassiveSkill()
    {
        switch(type)
        {
            /** Skill_02�� ������ �� RateUp�Լ� ȣ�� */
            case SkillData.SkillType.Skill_02:
                RateUp();
                break;
            /** Skill_03�� ������ �� SpeedUp�Լ� ȣ�� */
            case SkillData.SkillType.Skill_03:
                SpeedUp();
                break;
        }
    }

    void RateUp()
    {
        /** weapons�� �θ� Ŭ������ �ִ� �ڽ�Ŭ������ weaponŬ������ �����´�. */
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        /** ã�� weapons��ŭ �ݺ� */
        foreach(Weapon weapon in weapons)
        {
            /** weapon�� id�� ���ǹ����� */
            switch(weapon.id)
            {
                /** weapon.id�� 0�̶��(���ⱸü) */
                case 0:
                    /** ���� ��ü ȸ�� �ӵ��� �������� �ش�. */
                    weapon.speed = 150 + (150 * rate);
                    break;
                /** weapon.id�� 1�̶�� (���̾) */
                case 1:
                    /** �߻� �ӵ��� �ٿ��ش�. */
                    weapon.speed = 0.5f * (1f - rate);
                    break;

                default:
                    break;
            }
        }
    }
    
    void SpeedUp()
    {
        /** speed�� 2 */
        float speed = 2;
        /** �÷��̾� ���ǵ�� 2 + (2 * rate) */
        GameManager.GMInstance.PlayerSpeed = speed + speed * rate;
    }

}
