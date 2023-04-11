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
        public float gameTime;
        public float maxGameTime = 2 * 10f;


        [Header("# Player Info")]
        public int Health;
        public int MaxHealth = 100;
        public int level;
        public int killcount;
        public int exp;
        public int[] nextExp = { 0, 1, 3, 5, 8, 13, 21, 44, 65, 109, 174 };
        public bool bIsLive;

        void Update()
        {
            gameTime += Time.deltaTime;

            if (gameTime > maxGameTime)
            {
                gameTime = maxGameTime;
            }
        }
        void Start()
        {
            Health = MaxHealth;


            //임시 스크립트 (첫번째 캐릭터 선택)
            // UiLevelUp.Select(2);
        }


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
        /** TODO ## GameManager 몬스터 Init함수 */
        public float MonsterMaxHp;
        public float MonsterCurrentSpeed;

        [Header("-----ManagerRef-----")]
        public PoolManager PoolManagerRef;
        public PlaySceneManager PlaySceneManagerRef;
        public ScrollManager ScrollManagerRef;
        public ImageChange ImageChangeRef;
        public CharacterDataManager CharacterDataManagerRef;
        public SkillManager SkillManagerRef;
        public MonsterMove MonsterMoveRef;

        [Header("-----PlaySceneObject-----")]
        GameObject SceneSelectManager;

        [Header("-----CharacterType-----")]
        public ECharacterType CurrentChar;

        [Header("-----CharacterType-----")]
        public ESceneType CurrentScene;

        [Header("-----GameObject-----")]
        public GameObject Player;
        public LevelUp UiLevelUp;

        [Header("-----Component-----")]
        public PlayerController playerCtrl;

        void Awake()
        {
            /** GMInstance는 이 클래스를 의미한다. */
            GMInstance = this;

            /** 화면이 바껴도 클래스 유지 */
            DontDestroyOnLoad(gameObject);
        }

        /** 경험치 획득 함수 */
        public void GetExp()
        {
            /** EXP 증가 */
            exp++;
            
            /** 만약 EXP가 다음 레벨 업 경험치를 다 획득했다면 */
            if(exp == nextExp[level])
            {
                /** 레벨 증가 */
                level++;
                /** 경험치 량 초기화 */
                exp = 0;
                /** 스킬 선택창 오픈 */
                UiLevelUp.Show();
            }
        }

        /** 플레이화면 정지 */
        public void PlayStop()
        {
            bIsLive = false;
            Time.timeScale = 0;
        }

        /** 플레이화면 다시시작 */
        public void PlayResume()
        {
            bIsLive = true;
            Time.timeScale = 1;
        }
    }
}

