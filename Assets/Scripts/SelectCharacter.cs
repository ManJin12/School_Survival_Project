using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCharacter : MonoBehaviour
{
    /** ĳ���� Ÿ�� ���� */
    public CharType Enum_character;
    /** ĳ���� ���� ���� */
    public SelectCharacter[] Characters;

    public SelectSceneManager SelectSceneManager;

    Animator m_anim;
    SpriteRenderer m_sprite;

   
             
// Start is called before the first frame update
void Start()
    {
        /** ���� Scene�̸��� CharacterSelect�� �ƴ϶�� */
        if (SceneManager.GetActiveScene().name != "CharacterSelect")
        {
            /** Start�Լ��� ����������. */
            return;
        }

        m_anim = GetComponent<Animator>();
        m_sprite = GetComponent<SpriteRenderer>();

        /** SelectMg.CurrentChar�� ĳ���� Ÿ���� Enum_character�� ���ٸ� */
        if (SelectSceneManager.CurrentChar == Enum_character)
        {
            /** OnSelect �Լ� ȣ��*/
            OnSelect();
        }
        /** SelectMg.CurrentChar�� ĳ���� Ÿ���� Enum_character�� �ٸ��� */
        else if (SelectSceneManager.CurrentChar != Enum_character)
        {
            /** OnDeSelect �Լ� ȣ��*/
            OnDeSelect();
        }
        
    }

    /* ���콺�� ������ ���� �� ȣ��Ǵ� �Լ� */
    void OnMouseUpAsButton()
    {
        /** ���� Scene�̸��� CharacterSelect�� �ƴ϶�� */
        if (SceneManager.GetActiveScene().name != "CharacterSelect")
        {
            /** OnMouseUpAsButton �Լ� ȣ�� �� �Լ��� ����������. */
            return;
        }

        /** CharSelectSceneManager ��ũ��Ʈ�� CurrentChar�� ���� Enum_character���� */
        SelectSceneManager.CurrentChar = Enum_character;

        /** OnSelect�Լ� ȣ�� */
        OnSelect();

        /** Characters�� �迭 ũ�⸸ŭ �ݺ� */
        for (int i = 0; i < Characters.Length; i++)
        {
            /** ���� ���õ� ĳ���Ͱ� �ƴ϶�� */
            if (Characters[i] != this)
            {
                /** �����̵��� ���� ĳ���ʹ� OnDeSelect�Լ� ȣ�� */
                Characters[i].OnDeSelect();
            }
        }
    }

    private void OnDeSelect()
    {
        /** ĳ������ �������� false�� �Ѵ�. */
        m_anim.SetBool("IsMove", false);
        /** ĳ������ ��⸦ ȸ������ ���ش�. */
        m_sprite.color = Color.gray;
    }

    /** ĳ���� ���� �� �Լ� ���� */
    void OnSelect()
    {
        /** ĳ������ �������� true�� �Ѵ�. */
        m_anim.SetBool("IsMove", true);
        /** ĳ������ ��⸦ �Ͼ�� ���ش�. */
        m_sprite.color = Color.white;
    }


}
