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
        float CharacterCriticalPercent = 0.15f;
        float CharacterCriticalDamage = 1.5f;
        float SkillDamageUp = 0.1f;

        [Header("-----Data-----")]
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
        public Spawner SpawnerRef;

        [Header("-----PlaySceneObject-----")]
        GameObject SceneSelectManager;

        [Header("-----CharacterType-----")]
        public ECharacterType CurrentChar;

        [Header("-----SceneType-----")]
        public ESceneType CurrentScene;

        [Header("-----DungeonType-----")]
        public ESelectDungeon SelectDungeonMode;

        [Header("-----GameObject-----")]
        public GameObject Player;
        public LevelUp UiLevelUp;
        public AcherLevelUp AcherLevelUpRef;

        [Header("-----AbilityRate-----")]
        public float SkillDamageUpRate;
        public float MaxHpLevelUpRate;
        public float CharSpeedLevelUpRate;
        public float CharCriticalPerUpRate;
        public float CharCriticalDamageUpRate;

        [Header("-----AbilityLevel-----")]
        /** �ɷ�ġ ���� */
        public int SkillDamageLevel = 1;
        public int MaxHpLevel = 1;
        public int CharSpeedLevel = 1;
        public int CharCriticalPerLevel = 1;
        public int CharCriticalDamageLevel = 1;


        /** public ���� ���� */
        [Header("-----WizardSkillBaseDamage-----")]
        public float FireBallBaseDamage;
        public float ElectricBallBaseDamage;
        public float MateoBaseDamage;
        public float IceArrowBaseDamage;
        public float IceAgeBaseDamage;
        public float TornadoBaseDamage;
        public float LightningBaseDamage;

        [Header("-----Component-----")]
        public PlayerController playerCtrl;

        /** ���ݷ� ���� ��ȯ�Լ� */
        //public int GetAttackAbilityLevel()
        //{
        //    return AttackAbilityLevel;
        //}

        ///** �ִ�ü������ ���� ��ȯ�Լ� */
        //public int GetMaxHpLevel()
        //{
        //    return MaxHpLevel;
        //}

        ///** ũ��Ƽ��Ȯ�� ���� ���� ��ȯ �Լ� */
        //public int GetCharCriticalPerLevel()
        //{
        //    return CharCriticalPerLevel;
        //}

        ///** ũ��Ƽ�õ����� ���� ���� ��ȯ �Լ� */
        //public int GetCharCriticalDamageLevel()
        //{
        //    return CharCriticalDamageLevel;
        //}

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

        public void SetPlayerSpeedInit(float Basespeed)
        {
            PlayerSpeed = Basespeed;
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
                /** ���� �� ���� ���� */
                SoundManagerRef.PlaySFX(SoundManager.SFX.LevelUp);

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

        /** ������ ��ų �ʱ� ������ ���� */
        #region SetWizardSkillBaseDamage
        public void SetFireBallBaseDamage(float basedamage)
        {
            FireBallBaseDamage = basedamage;
        }

        public void SetElectricBallBaseDamage(float basedamage)
        {
            ElectricBallBaseDamage = basedamage;
        }

        public void SetMateoBaseDamage(float basedamage)
        {
            MateoBaseDamage = basedamage;
        }

        public void SetIceArrowBaseDamage(float basedamage)
        {
            IceArrowBaseDamage = basedamage;
        }

        public void SetIceAgeBaseDamage(float basedamage)
        {
            IceAgeBaseDamage = basedamage;
        }

        public void SetTornadoBaseDamage(float basedamage)
        {
            TornadoBaseDamage = basedamage;
        }

        public void SetLightningBaseDamage(float basedamage)
        {
            LightningBaseDamage = basedamage;
        }
        #endregion

        /** ������ ��ų �ʱ� ������ ��ȯ */
        #region GetWizardSkillBaseDamage

        public float GetFireBallBaseDamage()
        {
            return FireBallBaseDamage;
        }

        public float GetElectricBallBaseDamage()
        {
            return ElectricBallBaseDamage;
        }

        public float GetMateoBaseDamage()
        {
            return MateoBaseDamage;
        }

        public float GetIceArrowBaseDamage()
        {
            return IceArrowBaseDamage;
        }

        public float GetIceAgeBaseDamage()
        {
            return IceAgeBaseDamage;
        }

        public float GetTornadoBaseDamage()
        {
            return TornadoBaseDamage;
        }

        public float GetLightningBaseDamage()
        {
            return LightningBaseDamage;
        }

        #endregion

        /** ũ��Ƽ�� ���� ��ȯ ���� �Լ� */
        #region Critical

        public void SetCriticalPercent(float percent)
        {
            CharacterCriticalPercent = percent;
        }

        public float GetCriticalPercent()
        {
            return CharacterCriticalPercent;
        }

        public void SetCriticaDamage(float damage)
        {
            CharacterCriticalDamage = damage;
        }

        public float GetCriticalDamage()
        {
            return CharacterCriticalDamage;
        }


        #endregion

        /** ��ų ������ ���� ��ȯ ���� �Լ� */
        #region SkillDamage
        public void SetSkillDamageUp(float rate)
        {
            SkillDamageUp = rate;
        }

        public float GetSkillDamageUp()
        {
            return SkillDamageUp;
        }

        #endregion
    }
}

