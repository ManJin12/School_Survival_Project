using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    [Header("-----Data-----")]
    
    [Header("-----SelectScene_UI-----")]
    

    [Header("-----ManagerRef-----")]
    /** GameManager타입의 메모리를 미리 확보해 둔다. */
    public static GameManager GMInstance;
    /** PlayerController타입을 가지는 player를 유니티에서 받아옴 */
    public PlayerController playerCtrl;

    public SelectSceneManager SelectSceneMgr;

    [Header("-----PlaySceneObject-----")]
    GameObject SceneSelectManager;
    
    [Header("-----CharacterType-----")]
    public CharType CurrentChar;

    [Header("-----GameObject-----")]
    public VariableJoystick Joystick;
    public GameObject[] CharPrefabs;
    public GameObject Player;
    
    void Awake()
    {
        /** GMInstance는 이 클래스를 의미한다. */
        GMInstance = this;
    }

    void Start()
    {
        /** SceneSelectManager는 CharSelectManager라는 이름의 오브젝트를 대입 */
        SceneSelectManager = GameObject.Find("CharSelectManager");

        /** CurrentChar는 SceneSelectManager가 가지고 있는 SelectSceneManager스크립트 안의 값인 CurrentChar을 대입한다. */
        this.CurrentChar = SceneSelectManager.GetComponent<SelectSceneManager>().CurrentChar;

        /** Player는 배열에 저장된 캐릭터를 SelectScene에서 선택된 캐릭터 타입으로 생성 */
        Player = Instantiate(CharPrefabs[(int)this.CurrentChar]);

        /** playerCtrl는 Player의 PlayerController스크립트 컴포넌트를 대입 */
        playerCtrl = Player.GetComponent<PlayerController>();
    }

}


