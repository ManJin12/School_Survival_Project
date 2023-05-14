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

        /** GameManagerŸ���� �޸𸮸� �̸� Ȯ���� �д�. */
        public static GameManager GMInstance;
        [Header("-----InGameData-----")]
        public float PlayTime;

        [Header("-----PlayerData-----")]
        public float PlayerSpeed;
        float BasePlayerSpeed;
        public float DashSpeed;

        [Header("-----MonsterData-----")]
        /** ���� �����ð� */
        public float MonsterSpawnTime;
        /** ���� �̵��ӵ� */
        public float MonsterSpeed;
        /** TODO ## GameManager ���� Init�Լ� */
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
            /** GMInstance�� �� Ŭ������ �ǹ��Ѵ�. */
            GMInstance = this;

            /** ĳ���� �⺻�̵��ӵ� ���� */
            BasePlayerSpeed = PlayerSpeed;

            /** ȭ���� �ٲ��� Ŭ���� ���� */
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            /** �÷��� ȭ���� �ƴ϶�� */
            if (SceneManager.GetActiveScene().name != "PlayScene")
            {
                /** ���� �÷��� �ð� 0���� */
                gameTime = 0;
                PlayTime = 0;
                return;
            }

            if (bIsLive == false)
            {
                return;
            }

            /** �÷��� �ð� ���� */
            gameTime += Time.deltaTime;

            if (gameTime > maxGameTime)
            {
                gameTime = maxGameTime;
            }
        }

        /** ����ġ ȹ�� �Լ� */
        public void GetExp()
        {
            /** EXP ���� */
            exp++;

            /**
            ���� EXP�� ���� ���� �� ����ġ�� �� ȹ���ߴٸ� 
            Mathf.Min(level, nextExp.Length - 1) : 10���� ������ ����ġ�� �Ȱ���
            */
            if (exp == nextExp[Mathf.Min(level, nextExp.Length - 1)])
            {
                /** ���� ���� */
                level++;

                /** ����ġ �� �ʱ�ȭ */
                exp = 0;

                /** ��ų ����â ���� */
                UiLevelUp.Show();
            }
        }

        /** �÷���ȭ�� ���� */
        public void PlayStop()
        {
            bIsLive = false;

            Time.timeScale = 0;
        }

        /** �÷���ȭ�� �ٽý��� */
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

