using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using My;

[System.Serializable]
public struct UpGradeData
{
    /** ��ų ������ : ������, ����, ���� */
    public float SkillDamageUpSum;
    public int SkillDamageUpLevel;
    public int SkillDamageUpPrice;

    /** �ִ� ü�� : ������, ����, ���� */
    public float MaxHpUpSum;
    public int MaxHpUpLevel;
    public int MaxHpUpPrice;

    /** �̵��ӵ�  : ������, ����, ���� */
    public float SpeedUpSum;
    public int SpeedUpLevel;
    public int SpeedUpPrice;

    /** ũ��Ƽ�� Ȯ�� : ������, ����, ���� */
    public float CriticalUpSum;
    public int CriticalUpLevel;
    public int CriticalUpPrice;

    /** ũ��Ƽ�� ������ : ������, ����, ���� */
    public float CriticalDamageUpSum;
    public int CriticalDamageUpLevel;
    public int CriticalDamageUpPrice;
}

public class UpGradeManager : MonoBehaviour
{
    string UpGradepath;

    void Start()
    {
        GameManager.GMInstance.UpGradeManagerRef = this;

        /** ���� �̸��� ��� ���� */
        UpGradepath = Path.Combine(Application.dataPath, "UpGrade.json");
        /** ������ �ҷ����� */
        JsonLoad();
    }

    /** ������ �ҷ����� */
    public void JsonLoad()
    {
        UpGradeData saveUpGrade = new UpGradeData();

        /** path�� �������� �ʴ´ٸ� */
        if (!File.Exists(UpGradepath))
        {
            Debug.Log(0);

            /** --- ��ų ������ ���׷��̵� ���� �ʱ�ȭ --- */
            GameManager.GMInstance.SkillDamageUpLevel = 1;
            GameManager.GMInstance.SkillDamageUpSum = 0.0f;
            GameManager.GMInstance.SkillDamageUpPrice = 100;

            /** --- �ִ� ü�� ���׷��̵� ���� �ʱ�ȭ --- */
            GameManager.GMInstance.MaxHpUpLevel = 1;
            GameManager.GMInstance.MaxHpUpSum = 0.0f;
            GameManager.GMInstance.MaxHpUpPrice = 100;

            /** --- �̵��ӵ� ���׷��̵� ���� �ʱ�ȭ --- */
            GameManager.GMInstance.SpeedUpLevel = 1;
            GameManager.GMInstance.SpeedUpSum = 0.0f;
            GameManager.GMInstance.SpeedUpPrice = 100;

            /** --- ũ��Ƽ�� Ȯ�� ���׷��̵� ���� �ʱ�ȭ --- */
            GameManager.GMInstance.CriticalUpLevel = 1;
            GameManager.GMInstance.CriticalUpSum = 0.0f;
            GameManager.GMInstance.CriticalUpPrice = 100;

            /** --- ��ų ������ ���׷��̵� ���� �ʱ�ȭ --- */
            GameManager.GMInstance.CriticalDamageUpLevel = 1;
            GameManager.GMInstance.CriticalDamageUpSum = 0.0f;
            GameManager.GMInstance.CriticalDamageUpPrice = 100;

            JsonSave();
        }
        /** path�� �����Ѵٸ� */
        else if (File.Exists(UpGradepath))
        {
            Debug.Log(1);

            /** path�� ��� text�� ���� */
            string loadJson = File.ReadAllText(UpGradepath);
            /** saveEconomy�� loadJson�� ����� json���κ��� SaveEconomy�� �����͸� �ҷ��´� */
            saveUpGrade = JsonUtility.FromJson<UpGradeData>(loadJson);

            /** ��ų ������ ���� �ʱ�ȭ */
            GameManager.GMInstance.SkillDamageUpSum = saveUpGrade.SkillDamageUpSum;
            GameManager.GMInstance.SkillDamageUpLevel = saveUpGrade.SkillDamageUpLevel;
            GameManager.GMInstance.SkillDamageUpPrice = saveUpGrade.SkillDamageUpPrice;

            /** �ִ� ü�� ���� ���� �ʱ�ȭ */
            GameManager.GMInstance.MaxHpUpSum = saveUpGrade.MaxHpUpSum;
            GameManager.GMInstance.MaxHpLevel = saveUpGrade.MaxHpUpLevel;
            GameManager.GMInstance.MaxHpUpPrice = saveUpGrade.MaxHpUpPrice;

            /** �̵��ӵ� ���� ���� �ʱ�ȭ */
            GameManager.GMInstance.SpeedUpSum = saveUpGrade.SpeedUpSum;
            GameManager.GMInstance.SpeedUpLevel = saveUpGrade.SpeedUpLevel;
            GameManager.GMInstance.SpeedUpPrice = saveUpGrade.SpeedUpPrice;

            /** ũ��Ƽ�� Ȯ�� ���� ���� �ʱ�ȭ*/
            GameManager.GMInstance.CriticalUpSum = saveUpGrade.CriticalUpSum;
            GameManager.GMInstance.CriticalUpLevel = saveUpGrade.CriticalUpLevel;
            GameManager.GMInstance.CriticalUpPrice = saveUpGrade.CriticalUpPrice;

            /** ũ��Ƽ�� ������ ���� ���� �ʱ�ȭ */
            GameManager.GMInstance.CriticalDamageUpSum = saveUpGrade.CriticalDamageUpSum;
            GameManager.GMInstance.CriticalDamageUpLevel = saveUpGrade.CriticalDamageUpLevel;
            GameManager.GMInstance.CriticalDamageUpPrice = saveUpGrade.CriticalDamageUpPrice;
        }
    }

    public void JsonSave()
    {
        UpGradeData saveUpGrade = new UpGradeData();

        /** ��ų ������ ���� �ʱ�ȭ */
        saveUpGrade.SkillDamageUpSum = GameManager.GMInstance.SkillDamageUpSum;
        saveUpGrade.SkillDamageUpLevel = GameManager.GMInstance.SkillDamageUpLevel;
        saveUpGrade.SkillDamageUpPrice = GameManager.GMInstance.SkillDamageUpPrice;

        /** �ִ� ü�� ���� ���� �ʱ�ȭ */
        saveUpGrade.MaxHpUpSum = GameManager.GMInstance.MaxHpUpSum;
        saveUpGrade.MaxHpUpLevel = GameManager.GMInstance.MaxHpLevel;
        saveUpGrade.MaxHpUpPrice = GameManager.GMInstance.MaxHpUpPrice;

        /** �̵��ӵ� ���� ���� �ʱ�ȭ */
        saveUpGrade.SpeedUpSum = GameManager.GMInstance.SpeedUpSum;
        saveUpGrade.SpeedUpLevel = GameManager.GMInstance.SpeedUpLevel;
        saveUpGrade.SpeedUpPrice = GameManager.GMInstance.SpeedUpPrice;

        /** ũ��Ƽ�� Ȯ�� ���� ���� �ʱ�ȭ*/
        saveUpGrade.CriticalUpSum = GameManager.GMInstance.CriticalUpSum;
        saveUpGrade.CriticalUpLevel = GameManager.GMInstance.CriticalUpLevel;
        saveUpGrade.CriticalUpPrice = GameManager.GMInstance.CriticalUpPrice;

        /** ũ��Ƽ�� ������ ���� ���� �ʱ�ȭ */
        saveUpGrade.CriticalDamageUpSum = GameManager.GMInstance.CriticalDamageUpSum;
        saveUpGrade.CriticalDamageUpLevel = GameManager.GMInstance.CriticalDamageUpLevel;
        saveUpGrade.CriticalDamageUpPrice = GameManager.GMInstance.CriticalDamageUpPrice;

        /** json ���ڿ��� saveEconomy�� ����� �������� ���̾Ƹ�带 �����Ѵ�. */
        string json = JsonUtility.ToJson(saveUpGrade, true);

        /** path�� ����� json�����͸� �д´�. */
        File.WriteAllText(UpGradepath, json);
    }
}
