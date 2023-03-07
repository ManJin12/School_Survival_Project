using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/** TODO ## GamaManager Script */
public class GameManager : MonoBehaviour
{
    [Header("-----this-----")]
    /** GameManagerŸ���� �޸𸮸� �̸� Ȯ���� �д�. */
    public static GameManager GMInstance;

    [Header("-----PlayerData-----")]
    public float PlayerSpeed;

    [Header("-----ManagerRef-----")]
    public PoolManager PoolManagerRef;

    [Header("-----PlaySceneObject-----")]
    GameObject SceneSelectManager;
    
    [Header("-----CharacterType-----")]
    public CharType CurrentChar;

    [Header("-----GameObject-----")]
    public GameObject Player;

    [Header("-----Component-----")]
    public PlayerController playerCtrl;

    void Awake()
    {
        /** GMInstance�� �� Ŭ������ �ǹ��Ѵ�. */
        GMInstance = this;

        /** ȭ���� �ٲ��� Ŭ���� ���� */
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    private void Update()
    {
        
    }
}


