using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using My;

[System.Serializable]
public struct SaveEconomy
{
    public int MagicStone;
    public int Diamond;
    public int SkillTicket;
    public int EnterTicket;
}

public class CoinManager : MonoBehaviour
{
    string path;

    void Start()
    {
        GameManager.GMInstance.CoinManagerRef = this;

        /** ���� �̸��� ��� ���� */
        path = Path.Combine(Application.dataPath, "Economy.json");
        /** ������ �ҷ����� */
        JsonLoad();
    }

    /** ������ �ҷ����� */
    public void JsonLoad()
    {
        SaveEconomy saveEconomy = new SaveEconomy();

        /** path�� �������� �ʴ´ٸ� */
        if (!File.Exists(path))
        {
            /** ������ �ʱ�ȭ */
            GameManager.GMInstance.MagicStone = 10000;
            /** ���̾Ƹ�� �ʱ�ȭ */
            GameManager.GMInstance.Diamond = 10000;
            /** ��ų �缳�� Ƽ�� �ʱ�ȭ */
            GameManager.GMInstance.SkillTicket = 5;
            /** ���� Ƽ�� �ʱ�ȭ */
            GameManager.GMInstance.EnterTicket = 20;

            JsonSave();
        }
        /** path�� �����Ѵٸ� */
        else if (File.Exists(path))
        {
            /** path�� ��� text�� ���� */
            string loadJson = File.ReadAllText(path);
            /** saveEconomy�� loadJson�� ����� json���κ��� SaveEconomy�� �����͸� �ҷ��´� */
            saveEconomy = JsonUtility.FromJson<SaveEconomy>(loadJson);

            /** ����Ǿ� �ִ� �������� �����´�. */
            GameManager.GMInstance.MagicStone = saveEconomy.MagicStone;
            /** ����Ǿ� �ִ� ���̾Ƹ�带 �ҷ��´�. */
            GameManager.GMInstance.Diamond = saveEconomy.Diamond;
            /** ����Ǿ� �ִ� ��ų �缳�� Ƽ���� �ҷ��´�. */
            GameManager.GMInstance.SkillTicket = saveEconomy.SkillTicket;
            /** ����Ǿ� �ִ� ���� Ƽ���� �ҷ��´�. */
            GameManager.GMInstance.EnterTicket = saveEconomy.EnterTicket;
        }
    }

    public void JsonSave()
    {
        SaveEconomy saveEconomy = new SaveEconomy();

        /** ������ �������� GameManager�� ����� ���������� �ʱ�ȭ�Ѵ�. */
        saveEconomy.MagicStone = GameManager.GMInstance.MagicStone;
        /** ������ ���̾ƴ� GameManager�� ����� ���̾Ƹ�� �������� �ʱ�ȭ�Ѵ�. */
        saveEconomy.Diamond = GameManager.GMInstance.Diamond;
        /** ������ ��ų �缳������ GameManager�� ����� Ƽ�� �������� �ʱ�ȭ�Ѵ�. */
        saveEconomy.SkillTicket = GameManager.GMInstance.SkillTicket;
        /** ������ ����� GameManager�� ����� Ƽ�� �������� �ʱ�ȭ�Ѵ�. */
        saveEconomy.EnterTicket = GameManager.GMInstance.EnterTicket;

        /** json ���ڿ��� saveEconomy�� ����� �������� ���̾Ƹ�带 �����Ѵ�. */
        string json = JsonUtility.ToJson(saveEconomy, true);

        /** path�� ����� json�����͸� �д´�. */
        File.WriteAllText(path, json);
    }
}
