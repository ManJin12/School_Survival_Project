using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/** ĳ���� Ÿ�� */
public enum CharType
{
    BoyChar,
    GirlChar,
    OldBoyChar,
    YoungChar,
}
public class SelectSceneManager : MonoBehaviour
{
    /** SelectScene���� ������ CurrentChar�� �ޱ� ���� ���� */
    public CharType CurrentChar;

    /** �� Ŭ������ �޴� ������Ʈ �����ϱ� ���� ���� */
    public GameObject SelectSceneMgr;

    void Awake()
    {
        /** ȭ���� �ٲ��� Ŭ���� ���� */
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStart_Btn()
    {
        /** ȭ�� ��ȯ */
        SceneManager.LoadScene("PlayScene");
    }
}
