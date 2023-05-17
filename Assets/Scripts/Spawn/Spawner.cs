using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using My;
using static Define;

public class Spawner : MonoBehaviour
{
    public Transform[] SpawnPoint;
    public GameObject[] BossMonsters;


    /** 게임 시작 몬스터 종류 제한*/
    int SpawnLevel = 0;

    public int WaveLevelUpTime;
    public float BossSpawnTime;

    float m_timer;

    float m_PlayTime;
    bool bIsBossSpawn = false;
    bool bIsBossClear;

    /** 몬스터 능력 Init변수 */
    //public float Speed;
    //public float MaxHp;
    //public float CurrentSpeed;

    public void SetIsBossSpawn(bool bossspawn)
    {
        bIsBossSpawn = bossspawn;
        bIsBossClear = true;
    }

    /** 보스 격파 했는지? */
    public bool GetbIsBossClear()
    {
        return bIsBossClear;
    }

    private void Awake()
    {
        /** 배열로 생성된 Spawn포인트는 이 오브젝트의 자식들이 들어간다. */
        SpawnPoint = GetComponentsInChildren<Transform>();
    }

    void Start()
    {
        GameManager.GMInstance.SpawnerRef = this;

        /** 만약 PlayScene이면 Spawner 컴포넌트를 활성화 시켜준다. */
        if (GameManager.GMInstance.CurrentScene == Define.ESceneType.PlayScene)
        {
            this.GetComponent<Spawner>().enabled = true;
        }
        /** 만약 PlayScene이 아니면 Spawner 컴포넌트를 비활성화 시켜준다. */
        else if (GameManager.GMInstance.CurrentScene != Define.ESceneType.PlayScene)
        {
            this.GetComponent<Spawner>().enabled = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!GameManager.GMInstance.bIsLive)
        {
            return;
        }

        /** 게임 전체 시간 */
        m_PlayTime = GameManager.GMInstance.PlayTime;

        /** TODO ## Spawner.cs 몬스터 생성 로직 */
        /** m_timer는 업데이트 함수가 호출되는 프레임마다 증가 */
        m_timer += Time.deltaTime;

        /** m_timer가 1초가 넘어가면 */
        if (m_timer > GameManager.GMInstance.MonsterSpawnTime)
        {
            /** m_timer을 0으로 초기화 해주고 */
            m_timer = 0.0f;
            /** 몬스터 소환 함수 호출 */
            Spawn();
        }

        /** 함수 호출 */
        WaveLevelUp();

        if (m_PlayTime >= BossSpawnTime && bIsBossSpawn == false)
        {
            SpawnBoss();
            bIsBossSpawn = true;
            bIsBossClear = false;
        }
    }

    /** TODO ## Spawner.cs 몬스터 생성 함수 */
    void Spawn()
    {
        /** 초원 지역 몬스터 스폰 */
        if (GameManager.GMInstance.SelectDungeonMode == ESelectDungeon.GrassLand)
        {
            /** 게임 오브젝트인 몬스터를 PoolManager의 Get함수가 반환한 몬스터로 초기화 */
            GameObject Monster = GameManager.GMInstance.PoolManagerRef.Get(SpawnLevel);

            // TODO ## Spawner.cs 수정 필요 스폰위치 랜덤에서 정해지는걸로
            /** 생성되는 몬스터의 위치는 이 클래스의 자식들의 위치 중 한 곳으로 한다. */
            Monster.transform.position = SpawnPoint[Random.Range(1, SpawnPoint.Length)].position;

            Monster.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
        }
        /** 암석 지대 몬스터 스폰 */
        else if (GameManager.GMInstance.SelectDungeonMode == ESelectDungeon.RockLand)
        {
            /** 게임 오브젝트인 몬스터를 PoolManager의 Get함수가 반환한 몬스터로 초기화 */
            GameObject Monster = GameManager.GMInstance.PoolManagerRef.Get(SpawnLevel);

            // TODO ## Spawner.cs 수정 필요 스폰위치 랜덤에서 정해지는걸로
            /** 생성되는 몬스터의 위치는 이 클래스의 자식들의 위치 중 한 곳으로 한다. */
            Monster.transform.position = SpawnPoint[Random.Range(1, SpawnPoint.Length)].position;

            Monster.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
        }
        /** 암석 지대 몬스터 스폰 */
        else if (GameManager.GMInstance.SelectDungeonMode == ESelectDungeon.DeathLand)
        {
            /** 게임 오브젝트인 몬스터를 PoolManager의 Get함수가 반환한 몬스터로 초기화 */
            GameObject Monster = GameManager.GMInstance.PoolManagerRef.Get(SpawnLevel);

            // TODO ## Spawner.cs 수정 필요 스폰위치 랜덤에서 정해지는걸로
            /** 생성되는 몬스터의 위치는 이 클래스의 자식들의 위치 중 한 곳으로 한다. */
            Monster.transform.position = SpawnPoint[Random.Range(1, SpawnPoint.Length)].position;

            Monster.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
        }
    }

    void WaveLevelUp()
    {
        /** TODO ## Spawner.cs 몬스터 종류 스폰 제어 레벨디자인 필요 */
        if (m_PlayTime > WaveLevelUpTime)
        {
            /** 스폰단계가 저장되있는 인덱스보다 크면 리턴 */
            if (SpawnLevel >= 3)
            {
                return;
            }
            /** 스폰단계를 올린다. */
            SpawnLevel++;

            /** 
            다음 종류의 몬스터를 생성하기 위해서 시간을 추가한다.
            EX 10초마다 새로운 종류의 몬스터가 생성된다
            */
            WaveLevelUpTime += WaveLevelUpTime;
        }
    }

    void SpawnBoss()
    {
        /** 초원 지역 몬스터 스폰 */
        if (GameManager.GMInstance.SelectDungeonMode == ESelectDungeon.GrassLand)
        {
            /** 게임 오브젝트인 몬스터를 PoolManager의 Get함수가 반환한 몬스터로 초기화 */
            GameObject Boss = Instantiate(BossMonsters[0]);

            // TODO ## Spawner.cs 수정 필요 스폰위치 랜덤에서 정해지는걸로
            /** 생성되는 몬스터의 위치는 이 클래스의 자식들의 위치 중 한 곳으로 한다. */
            Boss.transform.position = SpawnPoint[Random.Range(1, SpawnPoint.Length)].position;

            Boss.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
        }
    }
}