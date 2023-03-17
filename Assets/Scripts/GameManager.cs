using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static Define;

namespace My
{
    /** TODO ## GamaManager Script */
    public class GameManager : MonoBehaviour
    {
        /** GameManager타입의 메모리를 미리 확보해 둔다. */
        public static GameManager GMInstance;
        [Header("-----InGameData-----")]
        public float PlayTime;

        [Header("-----PlayerData-----")]
        public float PlayerSpeed;
        public float DashSpeed;

        [Header("-----MonsterData-----")]
        /** 몬스터 스폰시간 */
        public float MonsterSpawnTime;
        /** 몬스터 이동속도 */
        public float MonsterSpeed;

        [Header("-----ManagerRef-----")]
        public PoolManager PoolManagerRef;
        public PlaySceneManager PlaySceneManagerRef;
        public ScrollManager ScrollManagerRef;
        public ImageChange ImageChangeRef;

        [Header("-----PlaySceneObject-----")]
        GameObject SceneSelectManager;

        [Header("-----CharacterType-----")]
        public ECharacterType CurrentChar;

        [Header("-----CharacterType-----")]
        public ESceneType CurrentScene;

        [Header("-----GameObject-----")]
        public GameObject Player;

        [Header("-----Component-----")]
        public PlayerController playerCtrl;

        void Awake()
        {
            /** GMInstance는 이 클래스를 의미한다. */
            GMInstance = this;

            /** 화면이 바껴도 클래스 유지 */
            DontDestroyOnLoad(gameObject);
        }
    }
}

