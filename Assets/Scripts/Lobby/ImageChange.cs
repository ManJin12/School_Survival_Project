using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using My;

public class ImageChange : MonoBehaviour
{
    /** 캐릭터 스프라이트 저장할 배열 변수 */
    public Sprite[] CharImage;
    /** 현재 캐릭터 타입 확인용 */
    public CharType ECurrentCharacter;

    /** Image에 접근할 변수 선언 */
    public Image m_Image;
    // Start is called before the first frame update
    void Start()
    {
        /** m_Image는 Image에 접근할 수 있다. */
        m_Image = GetComponent<Image>();
               
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
            /** m_Image의 스프라이트를 저장된 배열과 현재 GameManger가 가지고 있는 캐릭터에 맞게 나온다. */
            m_Image.sprite = CharImage[(int)GameManager.GMInstance.CurrentChar];
        }
    }
}
