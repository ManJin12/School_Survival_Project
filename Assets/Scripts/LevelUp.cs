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
        /** 화면에 보일 때 마다 텍스트 초기화 */
        GameManager.GMInstance.PlaySceneManagerRef.TextInit();
        /** 플레이 화면 멈춤 */
        GameManager.GMInstance.PlayStop();
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        /** 플레이 화면 다시하기 함수 호출 */
        GameManager.GMInstance.PlayResume();
    }

    //public void Select(int index)
    //{
    //    skills[index].OnClick();
    //}
}
