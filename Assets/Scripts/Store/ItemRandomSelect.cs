using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRandomSelect : MonoBehaviour
{
    /** 아이템을 만들어서 저장할 변수 선언 */
    public List<Equipment> Items = new List<Equipment>();
    /** 가중치 무게를 합산할 변수 */
    public float total = 0;
    /** test */
    public int[] result;

    /** 아이템을 반환 받을 함수 */
    public int GetRandomItem()
    {
        /** 랜덤한 숫자를 받을 변수 */
        int SelectNum = 0;
        /** 무게 가중치 */
        float weight = 0;

        /** 0.0f ~ 1.0f 사이의 실수 값을 가져온다. */
        SelectNum = Mathf.RoundToInt(total * Random.Range(0.0f, 1.0f));
        
        /** 저장된 아이템 수 만큼 반복 */
        for (int i = 0; i < Items.Count; i++)
        {
            weight += Items[i].weight;

            Debug.Log("SelectNum : " + SelectNum + " / weight : " + weight);

            /** 만약 선택된 SelectNum weight보다 작다면 */
            if (SelectNum <= weight)
            {
                /** 해당되는 아이템을 반환해 준다. */
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

        /** 실패 시 null 반환 */
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
            /** 아이템의 가중치 무게만큼 total을 더 해 준다. */
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
