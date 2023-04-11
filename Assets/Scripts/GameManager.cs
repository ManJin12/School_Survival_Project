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


            //�ӽ� ��ũ��Ʈ (ù��° ĳ���� ����)
            // UiLevelUp.Select(2);
        }


        /** GameManagerŸ���� �޸𸮸� �̸� Ȯ���� �д�. */
        public static GameManager GMInstance;
        [Header("-----InGameData-----")]
        public float PlayTime;

        [Header("-----PlayerData-----")]
        public float PlayerSpeed;
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
            /** GMInstance�� �� Ŭ������ �ǹ��Ѵ�. */
            GMInstance = this;

            /** ȭ���� �ٲ��� Ŭ���� ���� */
            DontDestroyOnLoad(gameObject);
        }

        /** ����ġ ȹ�� �Լ� */
        public void GetExp()
        {
            /** EXP ���� */
            exp++;
            
            /** ���� EXP�� ���� ���� �� ����ġ�� �� ȹ���ߴٸ� */
            if(exp == nextExp[level])
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
    }
}

