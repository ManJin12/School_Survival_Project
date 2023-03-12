using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using My;

public class ImageChange : MonoBehaviour
{
    /** ĳ���� ��������Ʈ ������ �迭 ���� */
    public Sprite[] CharImage;
    /** ���� ĳ���� Ÿ�� Ȯ�ο� */
    public CharType ECurrentCharacter;

    /** Image�� ������ ���� ���� */
    public Image m_Image;
    // Start is called before the first frame update
    void Start()
    {
        /** m_Image�� Image�� ������ �� �ִ�. */
        m_Image = GetComponent<Image>();
               
    }

    // Update is called once per frame
    void Update()
    {
        /** ECurrentCharacter�� GameManager.GMInstance.CurrentChar�� ���ٸ� return */
        if (ECurrentCharacter == GameManager.GMInstance.CurrentChar)
        {
            return;
        }
        /**  ECurrentCharacter�� GameManager.GMInstance.CurrentChar�� �ٸ��� */
        else if (ECurrentCharacter != GameManager.GMInstance.CurrentChar)
        {
            /** ECurrentCharacter�� ���� ĳ����Ÿ������ �ٲ��ش�. */
            ECurrentCharacter = GameManager.GMInstance.CurrentChar;
            /** m_Image�� ��������Ʈ�� ����� �迭�� ���� GameManger�� ������ �ִ� ĳ���Ϳ� �°� ���´�. */
            m_Image.sprite = CharImage[(int)GameManager.GMInstance.CurrentChar];
        }
    }
}
