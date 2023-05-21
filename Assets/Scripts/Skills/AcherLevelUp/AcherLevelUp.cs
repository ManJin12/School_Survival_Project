using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My;
using static Define;

public class AcherLevelUp : MonoBehaviour
{
    RectTransform rect;
    public AcherSkill[] skills;
    List<int> skillIndex;

    void Start()
    {
        /** ���� ���� ĳ���Ͱ� �����簡 �ƴ� �� */
        if (GameManager.GMInstance.CurrentChar != ECharacterType.AcherChar)
        {
            gameObject.SetActive(false);
            return;
        }

        GameManager.GMInstance.AcherLevelUpRef = this;
        rect = GetComponent<RectTransform>();
        skills = GetComponentsInChildren<AcherSkill>(true);

        /** ȭ�� �߻� ���� �Լ� ȣ�� */
        BaseAttack(0);

        /** TODO ## LevelUp.cs �⺻���� */
        rect.localScale = Vector3.zero;

        /** TODO ## LevelUp.cs �ΰ��� ������ �� ��ų ���� X */
            /** 0.2���ִٰ� next�Լ� ���� */
            // StartCoroutine(ShowBtn());


        /** ������Ʈ �̸��� WizardLevelUp�̰� ���� ĳ���� Ÿ���� �����簡 �ƴ� ��*/
        //else if (gameObject.name == "WizardLevelUp" && GameManager.GMInstance.CurrentChar != ECharacterType.AcherChar)
        //{
        //    /** ���� ĳ���Ͱ� �����簡 �ƴ� �� OFF */
        //    gameObject.SetActive(false);
        //}
    }

    public void BaseAttack(int index)
    {
        /** �ε����� �Է¹��� ���� ��ų�� �����Ų��. */
        skills[index].OnClick();
        // Debug.Log("0");
    }

    IEnumerator ShowBtn()
    {
        yield return 0.2f;

        Next();
    }


    public void Show()
    {
        /** Next�Լ� ȣ�� */
        Next();
        rect.localScale = Vector3.one;
        /** ȭ�鿡 ���� �� ���� �ؽ�Ʈ �ʱ�ȭ */
        GameManager.GMInstance.PlaySceneManagerRef.TextInit();
        /** �÷��� ȭ�� ���� */
        GameManager.GMInstance.PlayStop();
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        /** �÷��� ȭ�� �ٽ��ϱ� �Լ� ȣ�� */
        GameManager.GMInstance.PlayResume();
    }

    /** TODO ## ��ų ���ý� �������� ���� �Լ� �ٽ�!!!!!!!!! */
    public void Next()
    {
        // 1. ��� ������ ��Ȱ��ȭ
        foreach (AcherSkill skill in skills)
        {
            skill.gameObject.SetActive(false);
        }

        // 2. �� �߿��� ���� 3�� ������ Ȱ��ȭ
        int[] random = new int[3];

        /** �ݸ� */
        while (true)
        {
            /** ���� �迭�� �ε����� �����ų ������ ����ִ´�. */
            random[0] = Random.Range(0, skills.Length);
            random[1] = Random.Range(0, skills.Length);
            random[2] = Random.Range(0, skills.Length);

            /** ��ų���� ��ư�� �ߺ��� ���� �ʾҴٸ� �ݺ����� �������� */
            if (random[0] != random[1] && random[1] != random[2] && random[0] != random[2])
            {
                break;
            }
        }

        /** ������ų�� ���̸�ŭ �ݺ� */
        for (int index = 0; index < random.Length; index++)
        {
            /** ranSkill�� �������� ���� ��ų��ư */
            AcherSkill ranSkill = skills[random[index]];

            /** ���� �������� ���� ��ų�� ������ �ִ뷹���̶��? */
            if (ranSkill.level == ranSkill.data.damages.Length)
            {
                /** ���� �ݺ� */
                while (true)
                {
                    /** ���� �� ���� */
                    int randomIndex = Random.Range(0, skills.Length);

                    /** ������ ��ų ��Ȱ��ȭ */
                    ranSkill.gameObject.SetActive(false);

                    /** ������ ��ų�� �������� ���� ��ų�� �����ʰ� Ȱ��ȭ�� ������ �ʴٸ� */
                    if (ranSkill != skills[randomIndex] && skills[randomIndex].gameObject.activeSelf == false)
                    {
                        /** �������� ���� ��ư Ȱ��ȭ */
                        skills[randomIndex].gameObject.SetActive(true);
                        /** �ݺ��� �������� */
                        break;
                    }
                }
            }
            else
            {
                /** ��ų���� ��ư ������Ʈ Ȱ��ȯ */
                ranSkill.gameObject.SetActive(true);
            }

            ///** ��ų �迭�� ����� ���� �˻� */
            //for (int i = 0; i < skills.Length; i++)
            //{
            //    /** ���� ��ų�� ��Ȱ��ȭ ���ִٸ� */
            //    if (skills[i].gameObject.activeSelf == false)
            //    {
            //        skillIndex.Add(i);
            //        Debug.Log(skillIndex[0]);
            //    }

            //    skills[skillIndex[Random.Range(0, skillIndex.Count)]].gameObject.SetActive(true);
            //}


            // TODO ## LevelUp.cs �ִ뷹�� �� �� ��ü
            ///** ���� �������� ���� ��ų������ �ִ뷹���̶�� */
            //if (ranSkill.level == ranSkill.data.damages.Length)
            //{
            //    /** ��ų ���� ���� */
            //    skills[(Random.Range(3, 4))].gameObject.SetActive(true);
            //}
            //else
            //{
            //    /** ��ų���� ��ư ������Ʈ Ȱ��ȯ */
            //    ranSkill.gameObject.SetActive(true);
            //}
        }
    }
}
