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
    /** GameManagerŸ���� �޸𸮸� �̸� Ȯ���� �д�. */
    public static GameManager GMInstance;
    /** PlayerControllerŸ���� ������ player�� ����Ƽ���� �޾ƿ� */
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
        /** GMInstance�� �� Ŭ������ �ǹ��Ѵ�. */
        GMInstance = this;
    }

    void Start()
    {
        /** SceneSelectManager�� CharSelectManager��� �̸��� ������Ʈ�� ���� */
        SceneSelectManager = GameObject.Find("CharSelectManager");

        /** CurrentChar�� SceneSelectManager�� ������ �ִ� SelectSceneManager��ũ��Ʈ ���� ���� CurrentChar�� �����Ѵ�. */
        this.CurrentChar = SceneSelectManager.GetComponent<SelectSceneManager>().CurrentChar;

        /** Player�� �迭�� ����� ĳ���͸� SelectScene���� ���õ� ĳ���� Ÿ������ ���� */
        Player = Instantiate(CharPrefabs[(int)this.CurrentChar]);

        /** playerCtrl�� Player�� PlayerController��ũ��Ʈ ������Ʈ�� ���� */
        playerCtrl = Player.GetComponent<PlayerController>();
    }

}


