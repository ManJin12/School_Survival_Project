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
        /** �迭�� ������ Spawn����Ʈ�� �� ������Ʈ�� �ڽĵ��� ����. */
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
        /** m_timer�� ������Ʈ �Լ��� ȣ��Ǵ� �����Ӹ��� ���� */
        m_timer += Time.deltaTime;

        /** m_timer�� 1�ʰ� �Ѿ�� */
        if (m_timer > 1f)
        {
            /** m_timer�� 0���� �ʱ�ȭ ���ְ� */
            m_timer = 0.0f;
            /** ���� ��ȯ �Լ� ȣ�� */
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject Monster = GameManager.GMInstance.PoolManagerRef.Get(Random.Range(0, 4));

        Monster.transform.position = SpawnPoint[Random.Range(1, SpawnPoint.Length)].position;
    }
}
