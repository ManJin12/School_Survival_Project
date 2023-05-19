using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using My;
using static Define;

public class ImageChange : MonoBehaviour
{
    /** 캐릭터 스프라이트 저장할 배열 변수 */
    public Image[] CharAnimImage;

    public Text CharacterName;

    /** 현재 캐릭터 타입 확인용 */
    public ECharacterType ECurrentCharacter;

    /** Image에 접근할 변수 선언 */
    // public Image m_Image;

    /** 크기 조절을 위한 변수 선언 */
    // RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        /** m_Image는 Image에 접근할 수 있다. */
        //m_Image = GetComponent<Image>();

        //rect = m_Image.rectTransform;
    }

    // Update is called once per frame
    void Update()
    {
        /** ECurrentCharacter가 GameManager.GMInstance.CurrentChar와 같다면 return */
        if (ECurrentCharacter == GameManager.GMInstance.CurrentChar)
        {
            return;
        }

        /**  ECurrentCharacter가 GameManager.GMInstance.CurrentChar와 다르면 */
        else if (ECurrentCharacter != GameManager.GMInstance.CurrentChar)
        {
            /** ECurrentCharacter를 현재 캐릭터타입으로 바꿔준다. */
            ECurrentCharacter = GameManager.GMInstance.CurrentChar;

            /** 현재 캐릭터가 마법사라면 */
            if (ECurrentCharacter == ECharacterType.WizardChar)
            {
                /** 마법사 캐릭터 활성화 */
                CharAnimImage[0].gameObject.SetActive(true);
                /** 직업 종류 */
                CharacterName.text = "마법사";
            }
            /** 현재 캐릭터가 마법사가 아니라면 */
            else
            {
                /** 마법사 캐릭터 비활성화 */
                CharAnimImage[0].gameObject.SetActive(false);
            }

            /** 현재 캐릭터가 궁수라면 */
            if (ECurrentCharacter == ECharacterType.AcherChar)
            {
                /** 궁수 캐릭터 활성화 */
                CharAnimImage[1].gameObject.SetActive(true);
                /** 직업 종류 */
                CharacterName.text = "궁수";

            }
            /** 현재 캐릭터가 궁수가 아니라면 */
            else
            {
                /** 궁수 캐릭터 비활성화 */
                CharAnimImage[1].gameObject.SetActive(false);
            }

            /** 현재 캐릭터가 전사라면 */
            if (ECurrentCharacter == ECharacterType.WorriorChar)
            {
                /** 전사 캐릭터 활성화 */
                CharAnimImage[2].gameObject.SetActive(true);
                /** 직업 종류 */
                CharacterName.text = "전사";
            }
            /** 현재 캐릭터가 전사가 아니라면 */
            else
            {
                /** 전사 캐릭터 비활성화 */
                CharAnimImage[2].gameObject.SetActive(false);
            }

            /** m_Image의 스프라이트를 저장된 배열과 현재 GameManger가 가지고 있는 캐릭터에 맞게 나온다. */
            // m_Image.sprite = CharImage[(int)GameManager.GMInstance.CurrentChar];

           

        }
    }
}
