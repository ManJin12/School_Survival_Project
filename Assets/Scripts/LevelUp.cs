using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    public Skill[] skills;

    void Start()
    {
        GameManager.GMInstance.UiLevelUp = this;
        rect = GetComponent<RectTransform>();
        skills = GetComponentsInChildren<Skill>(false);
    }

    // Update is called once per frame
    public void Show()
    {
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

    //public void Select(int index)
    //{
    //    skills[index].OnClick();
    //}
}
