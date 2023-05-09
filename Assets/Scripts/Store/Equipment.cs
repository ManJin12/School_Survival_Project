using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

/** 직렬화 */
[System.Serializable]
public class Equipment
{
    /** 아이템 이름 */
    public string itemName;
    /** 아이템 이미지 */
    public Sprite itemImage;
    /** 아이템 등급 */
    public ItemGrade itemGrade;
    /** 가중치 무게 */
    public float weight;

    /** 생성자 함수 */
    public Equipment(Equipment equipment)
    {
        /** 아이템 이름은 입력된 아이템 이름을 가져온다. */
        this.itemName = equipment.itemName;
        /** 아이템 이미지는 입력된 아이템 이미지를 가져온다. */
        this.itemImage = equipment.itemImage;
        /** 아이템 등급은 입력된 아이템 등급을 가져온다. */
        this.itemGrade = equipment.itemGrade;
        /** 아이템 가중치 무게는 입력된 아이템 가중치 무게를 가져온다. */
        this.weight = equipment.weight;
    }
}
