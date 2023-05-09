using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRandomSelect : MonoBehaviour
{
    /** �������� ���� ������ ���� ���� */
    public List<Equipment> Items = new List<Equipment>();
    /** ����ġ ���Ը� �ջ��� ���� */
    public float total = 0;
    /** test */
    public int[] result;

    /** �������� ��ȯ ���� �Լ� */
    public int GetRandomItem()
    {
        /** ������ ���ڸ� ���� ���� */
        int SelectNum = 0;
        /** ���� ����ġ */
        float weight = 0;

        /** 0.0f ~ 1.0f ������ �Ǽ� ���� �����´�. */
        SelectNum = Mathf.RoundToInt(total * Random.Range(0.0f, 1.0f));
        
        /** ����� ������ �� ��ŭ �ݺ� */
        for (int i = 0; i < Items.Count; i++)
        {
            weight += Items[i].weight;

            Debug.Log("SelectNum : " + SelectNum + " / weight : " + weight);

            /** ���� ���õ� SelectNum weight���� �۴ٸ� */
            if (SelectNum <= weight)
            {
                /** �ش�Ǵ� �������� ��ȯ�� �ش�. */
                //Equipment temp = new Equipment(Items[i]);
                //return temp;

                if (Items[i].weight == 0.2f)
                {
                    return Random.Range(0, 5);
                }
                else if (Items[i].weight == 0.9f)
                {
                    return Random.Range(5, 15);
                }
                else if (Items[i].weight == 6)
                {
                    return Random.Range(15, 30);
                }
            }
        }

        /** ���� �� null ��ȯ */
        //return null;
        return 0;
    }

    void Start()
    {
      
    }

    public void OnClickTenGacha()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            /** �������� ����ġ ���Ը�ŭ total�� �� �� �ش�. */
            total += Items[i].weight;
        }

        for (int i = 0; i < 10; i++)
        {
            result[GetRandomItem()]++;
        }

        for (int i = 0; i < result.Length; i++)
        {
            Debug.Log("result[" + i + "] : " + result[i]);
        }
    }

    public void OnClickCancel()
    {
        total = 0;
    }

}
