using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    /** 1. ��������� ������ ���� */
    public GameObject[] MonsterPrefabs;

    /** 2. Ǯ ����� �ϴ� ����Ʈ�� */
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

        /** Pools[index]���� index �迭�� ���� */
        foreach (GameObject Item in Pools[index])
        {
            /** ������ Ǯ�� ��Ȱ��ȭ�� ���� ������Ʈ ���� */
            if (Item.activeSelf == false)
            {
                /** �߰� �ϸ� m_Select�� �Ҵ� */
                m_Select = Item;
                /** m_Select Ȱ��ȭ */
                m_Select.SetActive(true);

                break;
            }
        }

        /** �� ã������ ���Ӱ� �����ϰ� m_Select�� �Ҵ� */
        if (m_Select == null)
        {
            /** �θ� ������Ʈ ������ m_Select ������ ���������� ���� */
            m_Select = Instantiate(MonsterPrefabs[index], transform);

            /** Pools list�� �����̳ʿ� m_Select�� �����Ѵ� */
            Pools[index].Add(m_Select);
        }

        return m_Select;
    }
}
