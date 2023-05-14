using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using My;
using static Define;

public class SelectCharacter : MonoBehaviour
{
    /** 캐릭터 타입 관련 */
    public ECharacterType Enum_character;

    [HideInInspector]
    /** 캐릭터 저장 공간 */
    public SelectCharacter[] Characters;

    // public SelectSceneManager SelectSceneManager;

    Animator m_anim;
    SpriteRenderer m_sprite;



    // Start is called before the first frame update
    void Start()
    {
        /** 만약 Scene이름이 CharacterSelect이 아니라면 */
        if (SceneManager.GetActiveScene().name != "Lobby")
        {
            /** Start함수를 빠져나간다. */
            return;
        }

        m_anim = GetComponent<Animator>();
        m_sprite = GetComponent<SpriteRenderer>();

        /** SelectMg.CurrentChar의 캐릭터 타입이 Enum_character과 같다면 */
        if (GameManager.GMInstance.CurrentChar == Enum_character)
        {
            /** OnSelect 함수 호출*/
            OnSelect();
        }
        /** SelectMg.CurrentChar의 캐릭터 타입이 Enum_character과 다르면 */
        else if (GameManager.GMInstance.CurrentChar != Enum_character)
        {
            /** OnDeSelect 함수 호출*/
            OnDeSelect();
        }
    }

    /* 마우스를 눌렀다 뗏을 때 호출되는 함수 */
    void OnMouseUpAsButton()
    {
        /** 만약 Scene이름이 CharacterSelect이 아니라면 */
        if (SceneManager.GetActiveScene().name != "Lobby")
        {
            /** OnMouseUpAsButton 함수 호출 시 함수를 빠져나간다. */
            return;
        }

        /** CharSelectSceneManager 스크립트의 CurrentChar에 현재 Enum_character대입 */
        GameManager.GMInstance.CurrentChar = Enum_character;

        /** OnSelect함수 호출 */
        OnSelect();

        /** Characters의 배열 크기만큼 반복 */
        for (int i = 0; i < Characters.Length; i++)
        {
            /** 만약 선택된 캐릭터가 아니라면 */
            if (Characters[i] != this)
            {
                /** 선택이되지 않은 캐릭터는 OnDeSelect함수 호출 */
                Characters[i].OnDeSelect();
            }
        }
    }

    private void OnDeSelect()
    {
        /** 캐릭터의 움직임을 false로 한다. */
        m_anim.SetBool("IsMove", false);
        /** 캐릭터의 밝기를 회색으로 해준다. */
        m_sprite.color = Color.gray;
    }

    /** 캐릭터 선택 시 함수 정의 */
    void OnSelect()
    {
        /** 캐릭터의 움직임을 true로 한다. */
        m_anim.SetBool("IsMove", true);
        /** 캐릭터의 밝기를 하얗게 해준다. */
        m_sprite.color = Color.white;
    }
}