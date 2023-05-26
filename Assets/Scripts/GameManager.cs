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
        public float MaxHealth;
        public int level;
        public int killcount;
        public int exp;
        public int[] nextExp = { 0, 1, 3, 5, 8, 13, 21, 44, 65, 109, 174 };
        public bool bIsLive;

        /** GameManagerŸ���� �޸𸮸� �̸� Ȯ���� �д�. */
        public static GameManager GMInstance;
        [Header("-----InGameData-----")]
        public float PlayTime;
        public int MagicStone;
        public int Diamond;
        public int SkillTicket;

        [Header("-----PlayerData-----")]
        public float PlayerSpeed;
        float BasePlayerSpeed;
        public float DashSpeed;
        public float CharacterCriticalPercent = 0.15f;
        public float CharacterCriticalDamage = 1.5f;
        public float SkillDamageUp = 0.1f;

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
        public UpGradeManager UpGradeManagerRef;
        public CoinManager CoinManagerRef;

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
        float FireBallBaseDamage;
        float ElectricBallBaseDamage;
        float MateoBaseDamage;
        float IceArrowBaseDamage;
        float IceAgeBaseDamage;
        float TornadoBaseDamage;
        float LightningBaseDamage;

        /** public ���� ���� */
        [Header("-----AcherSkillBaseDamage-----")]
        public float ArrowBaseDamage;
        public float VortexBaseDamage;
        public float HuricaneBaseDamage;
        public float WindSpiritBaseDamage;
        public float TrapBaseDamage;
        public float ArrowRainBaseDamage;
        public float BombArrowBaseDamage;

        [Header("-----Advertisements-----")]
        public RewardedAdsButton RewardedAdsButtonRef;

        [Header("-----Component-----")]
        public PlayerController playerCtrl;
        public EndGameAdsPanel EndGameAdsPanelRef;
        public EndGameAdsPanel GameFailedAdsPanelRef;

        [Header("-----UpGradeData-----")]
        public int UpPriceRate;
        /** ��ų ������ ���� ���� */
        public float SkillDamageUpSum;
        public int SkillDamageUpPrice;
        public int SkillDamageUpLevel;
        /** �ִ�ü�� ���� ���� */
        public float MaxHpUpSum;
        public int MaxHpUpPrice;
        public int MaxHpUpLevel;
        /** �̵��ӵ� ���� ���� */
        public float SpeedUpSum;
        public int SpeedUpPrice;
        public int SpeedUpLevel;
        /** ũ��Ƽ�� Ȯ�� ���� ���� */
        public float CriticalUpSum;
        public int CriticalUpPrice;
        public int CriticalUpLevel;
        /** ũ��Ƽ�� ������ ���� ���� */
        public float CriticalDamageUpSum;
        public int CriticalDamageUpPrice;
        public int CriticalDamageUpLevel;

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

        void Start()
        {
            InitData();
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

                if (CurrentChar == ECharacterType.WizardChar)
                {
                    /** ��ų ����â ���� */
                    UiLevelUp.Show();
                }
                else if (CurrentChar == ECharacterType.AcherChar)
                {
                    AcherLevelUpRef.Show();
                }

                /** ���� ��ư Ȱ��ȭ */
                RewardedAdsButtonRef._showSkillReSelectAdButton.interactable = true;
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

        /** �ü� ��ų �ʱ� ������ ���� */
        #region
        /** �ʱ� ���ؽ� ������ ���� */
        public void SetArrowBaseDamage(float basedamage)
        {
            ArrowBaseDamage = basedamage;
        }

        /** ���ؽ� �⺻ ������ ���� */
        public void SetVortexBaseDamage(float basedamage)
        {
            VortexBaseDamage = basedamage;
        }

        /** �㸮���� �⺻ ������ ���� */
        public void SetHuricaneBaseDamage(float basedamage)
        {
            HuricaneBaseDamage = basedamage;
        }

        /** �ٶ����� �⺻ ������ ���� */
        public void SetWindSpiritBaseDamage(float basedamage)
        {
            WindSpiritBaseDamage = basedamage;
        }

        /** Ʈ�� �⺻ ������ ���� */
        public void SetTrapBaseDamage(float basedamage)
        {
            TrapBaseDamage = basedamage;
        }

        /** ȭ��� �⺻ ������ ���� */
        public void SetArrowRainBaseDamage(float basedamage)
        {
            ArrowRainBaseDamage = basedamage;
        }

        /** ��źȭ�� �⺻ ������ ���� */
        public void SetBombArrowBaseDamage(float basedamage)
        {
            BombArrowBaseDamage = basedamage;
        }
        #endregion

        /** �ü� ��ų �ʱ� ������ ��ȯ */
        #region
        /** ȭ�� �⺻ ������ ��ȯ */
        public float GetArrowBaseDamage()
        {
            return ArrowBaseDamage;
        }
        /** ���ؽ� �⺻ ������ ��ȯ */
        public float GetVortexBaseDamage()
        {
            return VortexBaseDamage;
        }

        /** �㸮���� �⺻ ������ ��ȯ */
        public float GetHuricaneBaseDamage()
        {
            return HuricaneBaseDamage;
        }

        /** �ٶ����� �⺻ ������ ��ȯ */
        public float GetWindSpiritBaseDamage()
        {
            return WindSpiritBaseDamage;
        }

        /** �ٶ����� �⺻ ������ ��ȯ */
        public float GetTrapBaseDamage()
        {
            return TrapBaseDamage;
        }

        /** ȭ��� �⺻ ������ ��ȯ */
        public float GetArrowRainBaseDamage()
        {
            return ArrowRainBaseDamage;
        }

        /** ��źȭ�� �⺻ ������ ��ȯ */
        public float GetBombArrowBaseDamage()
        {
            return BombArrowBaseDamage;
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
            return SkillDamageUpSum;
        }

        #endregion

        void InitData()
        {
            /** ������ ���� ����ŭ ���������� */
            MaxHealth += MaxHpUpSum;
            PlayerSpeed += SpeedUpSum;
            CharacterCriticalPercent += CriticalUpSum;
            CharacterCriticalDamage += CriticalDamageUpSum;
        }
    }
}

