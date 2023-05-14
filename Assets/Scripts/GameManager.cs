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
        public float maxGameTime;

        [Header("# Player Info")]
        public float Health;
        public float MaxHealth = 100;
        public int level;
        public int killcount;
        public int exp;
        public int[] nextExp = { 0, 1, 3, 5, 8, 13, 21, 44, 65, 109, 174 };
        public bool bIsLive;

        /** GameManager타입의 메모리를 미리 확보해 둔다. */
        public static GameManager GMInstance;
        [Header("-----InGameData-----")]
        public float PlayTime;

        [Header("-----PlayerData-----")]
        public float PlayerSpeed;
        float BasePlayerSpeed;
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
        public Scanner ScannerRef;
        public CreatureScanner CreatureScannerRef;
        public SoundManager SoundManagerRef;

        [Header("-----PlaySceneObject-----")]
        GameObject SceneSelectManager;

        [Header("-----CharacterType-----")]
        public ECharacterType CurrentChar;

        [Header("-----CharacterType-----")]
        public ESceneType CurrentScene;

        [Header("-----GameObject-----")]
        public GameObject Player;
        public LevelUp UiLevelUp;
        public AcherLevelUp AcherLevelUpRef;

        [Header("-----Component-----")]
        public PlayerController playerCtrl;

        public float GetPlayerBaseSpeed() 
        {
            return BasePlayerSpeed;
        }

        public void InfoInit()
        {
            level = 1;
            killcount = 0;
            exp = 0;
        }

        void Awake()
        {
            /** GMInstance는 이 클래스를 의미한다. */
            GMInstance = this;

            /** 캐릭터 기본이동속도 저장 */
            BasePlayerSpeed = PlayerSpeed;

            /** 화면이 바껴도 클래스 유지 */
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            /** 플레이 화면이 아니라면 */
            if (SceneManager.GetActiveScene().name != "PlayScene")
            {
                /** 게임 플레이 시간 0으로 */
                gameTime = 0;
                PlayTime = 0;
                return;
            }

            if (bIsLive == false)
            {
                return;
            }

            /** 플레이 시간 증가 */
            gameTime += Time.deltaTime;

            if (gameTime > maxGameTime)
            {
                gameTime = maxGameTime;
            }
        }

        /** 경험치 획득 함수 */
        public void GetExp()
        {
            /** EXP 증가 */
            exp++;

            /**
            만약 EXP가 다음 레벨 업 경험치를 다 획득했다면 
            Mathf.Min(level, nextExp.Length - 1) : 10레벨 이후의 경험치는 똑같음
            */
            if (exp == nextExp[Mathf.Min(level, nextExp.Length - 1)])
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

        public void PlaySceneInit(bool _bIsLive, float _gametime, float _maxHp)
        {
            bIsLive = _bIsLive;
            gameTime = _gametime;
            Health = _maxHp;
        }
    }
}

