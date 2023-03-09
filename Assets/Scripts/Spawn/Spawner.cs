using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Spawner : MonoBehaviour
{
    public Transform[] SpawnPoint;

    float m_timer;

    private void Awake()
    {
        /** 배열로 생성된 Spawn포인트는 이 오브젝트의 자식들이 들어간다. */
        SpawnPoint = GetComponentsInChildren<Transform>();
    }

    void Start()
    {
        /** 만약 PlayScene이면 Spawner 컴포넌트를 활성화 시켜준다. */
        if (SceneManager.GetActiveScene().name == "PlayScene")
        {
            this.GetComponent<Spawner>().enabled = true;
        }
        /** 만약 PlayScene이 아니면 Spawner 컴포넌트를 비활성화 시켜준다. */
        else if (SceneManager.GetActiveScene().name != "PlayScene")
        {
            this.GetComponent<Spawner>().enabled = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        /** TODO ## 몬스터 생성 로직 */
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
    }

    /** TODO ## 몬스터 생성 함수 */
    void Spawn()
    {
        /** 게임 오브젝트인 몬스터를 PoolManager의 Get함수가 반환한 몬스터로 초기화 */
        GameObject Monster = GameManager.GMInstance.PoolManagerRef.Get(Random.Range(0, 4));

        /** 생성되는 몬스터의 위치는 이 클래스의 자식들의 위치 중 한 곳으로 한다. */
        Monster.transform.position = SpawnPoint[Random.Range(1, SpawnPoint.Length)].position;

        Monster.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
}