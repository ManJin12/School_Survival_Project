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
        if (SceneManager.GetActiveScene().name == "PlayScene")
        {
            this.GetComponent<Spawner>().enabled = true;
        }
        else if (SceneManager.GetActiveScene().name != "PlayScene")
        {
            this.GetComponent<Spawner>().enabled = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        /** m_timer는 업데이트 함수가 호출되는 프레임마다 증가 */
        m_timer += Time.deltaTime;

        /** m_timer가 1초가 넘어가면 */
        if (m_timer > 1f)
        {
            /** m_timer을 0으로 초기화 해주고 */
            m_timer = 0.0f;
            /** 몬스터 소환 함수 호출 */
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject Monster = GameManager.GMInstance.PoolManagerRef.Get(Random.Range(0, 4));

        Monster.transform.position = SpawnPoint[Random.Range(1, SpawnPoint.Length)].position;
    }
}
