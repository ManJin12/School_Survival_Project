using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/** 캐릭터 타입 */
public enum CharType
{
    BoyChar,
    GirlChar,
    OldBoyChar,
    YoungChar,
}
public class SelectSceneManager : MonoBehaviour
{
    /** SelectScene에서 선택한 CurrentChar를 받기 위한 변수 */
    public CharType CurrentChar;

    /** 이 클래스를 받는 오브젝트 저장하기 위한 변수 */
    public GameObject SelectSceneMgr;

    void Awake()
    {
        /** 화면이 바껴도 클래스 유지 */
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
        /** 화면 전환 */
        SceneManager.LoadScene("PlayScene");
    }
}
