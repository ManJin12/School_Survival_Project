using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    /** 1. 프리펩들을 보관할 변수 */
    public GameObject[] MonsterPrefabs;

    /** 2. 풀 담당을 하는 리스트들 */
    List<GameObject>[] Pools;

    void Awake()
    {
        Pools = new List<GameObject>[MonsterPrefabs.Length];

        for (int i = 0; i < Pools.Length; i++)
        {
            Pools[i] = new List<GameObject>();
        }

    }

    void Start()
    {
        GameManager.GMInstance.PoolManagerRef = this;
    }

    public GameObject Get(int index)
    {
        GameObject m_Select = null;

        /** Pools[index]안의 index 배열에 접근 */
        foreach (GameObject Item in Pools[index])
        {
            /** 선택한 풀의 비활성화된 게임 오브젝트 접근 */
            if (Item.activeSelf == false)
            {
                /** 발견 하면 m_Select에 할당 */
                m_Select = Item;
                /** m_Select 활성화 */
                m_Select.SetActive(true);

                break;
            }
        }

        /** 못 찾았으면 새롭게 생성하고 m_Select에 할당 */
        if (m_Select == null)
        {
            /** 부모 오브젝트 밑으로 m_Select 생성된 몬스터프리펩 대입 */
            m_Select = Instantiate(MonsterPrefabs[index], transform);

            /** Pools list에 컨테이너에 m_Select를 저장한다 */
            Pools[index].Add(m_Select);
        }

        return m_Select;
    }
}
