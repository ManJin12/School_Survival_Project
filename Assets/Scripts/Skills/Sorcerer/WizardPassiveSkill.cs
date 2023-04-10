using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My;

public class WizardPassiveSkill : MonoBehaviour
{
    public SkillData.SkillType type;
    public float rate;

    /** 초기화 함수 */
    public void Init(SkillData data)
    {
        // Basic Set
        /** Gear + 스킬아이디 */
        name = "Gear " + data.Skill_Id;
        /** 함수의 부모는 플레이어 오브젝트로 한다 */
        transform.parent = GameManager.GMInstance.Player.transform;
        /** transform의 x,y,z는 0으로 해준다. */
        transform.localPosition = Vector3.zero;

        // Property Set
        type = data.skillType;
        rate = data.damageRates[0];
        ApplyPassiveSkill();
    }

    public void LevelUp(float _rate)
    {
        /** 이 클래스의 rate는 매개변수로 받은 _rate로 초기화한다. */
        this.rate = _rate;
        /** 함수 호출 */
        ApplyPassiveSkill();
    }

    void ApplyPassiveSkill()
    {
        switch(type)
        {
            /** Skill_02를 눌렸을 때 RateUp함수 호출 */
            case SkillData.SkillType.Skill_02:
                RateUp();
                break;
            /** Skill_03를 눌렀을 때 SpeedUp함수 호출 */
            case SkillData.SkillType.Skill_03:
                SpeedUp();
                break;
        }
    }

    void RateUp()
    {
        /** weapons는 부모 클래스에 있는 자식클래스의 weapon클래스를 가져온다. */
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        /** 찾은 weapons만큼 반복 */
        foreach(Weapon weapon in weapons)
        {
            /** weapon의 id를 조건문으로 */
            switch(weapon.id)
            {
                /** weapon.id가 0이라면(전기구체) */
                case 0:
                    /** 번개 구체 회전 속도를 증가시켜 준다. */
                    weapon.speed = 150 + (150 * rate);
                    break;
                /** weapon.id가 1이라면 (파이어볼) */
                case 1:
                    /** 발사 속도를 줄여준다. */
                    weapon.speed = 0.5f * (1f - rate);
                    break;

                default:
                    break;
            }
        }
    }
    
    void SpeedUp()
    {
        /** speed는 2 */
        float speed = 2;
        /** 플레이어 스피드는 2 + (2 * rate) */
        GameManager.GMInstance.PlayerSpeed = speed + speed * rate;
    }

}
