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
        rect = GetComponent<RectTransform>();
        skills = GetComponentsInChildren<Skill>(false);
    }

    // Update is called once per frame
    public void Show()
    {
        rect.localScale = Vector3.one;
        GameManager.GMInstance.PlaySceneManagerRef.TextInit();
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
    }

    //public void Select(int index)
    //{
    //    skills[index].OnClick();
    //}
}
