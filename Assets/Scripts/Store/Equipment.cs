using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

/** ����ȭ */
[System.Serializable]
public class Equipment
{
    /** ������ �̸� */
    public string itemName;
    /** ������ �̹��� */
    public Sprite itemImage;
    /** ������ ��� */
    public ItemGrade itemGrade;
    /** ����ġ ���� */
    public float weight;

    /** ������ �Լ� */
    public Equipment(Equipment equipment)
    {
        /** ������ �̸��� �Էµ� ������ �̸��� �����´�. */
        this.itemName = equipment.itemName;
        /** ������ �̹����� �Էµ� ������ �̹����� �����´�. */
        this.itemImage = equipment.itemImage;
        /** ������ ����� �Էµ� ������ ����� �����´�. */
        this.itemGrade = equipment.itemGrade;
        /** ������ ����ġ ���Դ� �Էµ� ������ ����ġ ���Ը� �����´�. */
        this.weight = equipment.weight;
    }
}
