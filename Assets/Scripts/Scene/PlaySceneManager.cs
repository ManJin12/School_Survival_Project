using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using My;

public class PlaySceneManager : MonoBehaviour
{
    public float PlayTime;
    public Text SkillSelectPaneltext;
    public Image GameOverImage;
    // float fadeImage = 1;
    public bool bIsFirstStart;
    public GameObject GameClearPanel;
    public GameObject GameOverPanel;
    public LevelUp WizardLevelUp;

    void Start()
    {
        /** ��ų ���� �ؽ�ƮUI �Լ� ȣ�� */
        TextInit();
        GameManager.GMInstance.CurrentScene = Define.ESceneType.PlayScene;
        GameManager.GMInstance.PlaySceneManagerRef = this;

        /** ���� �÷��� ���� �ʱ�ȭ */
        GameManager.GMInstance.PlaySceneInit(false, 0, GameManager.GMInstance.MaxHealth);

        /** ���� ���� Ȱ��ȭ */
        GameManager.GMInstance.bIsLive = true;

        /** ó�� �����ϴ��� Ȯ���ϱ� ���� ���� */
        bIsFirstStart = true;
    }

    void Update()
    {
        if (GameManager.GMInstance.bIsLive == false)
        {
            return;
        }

        PlayTime += Time.deltaTime;
        GameManager.GMInstance.PlayTime = PlayTime;

        GameClear();
    }

    public void OnClickBack()
    {
        SceneManager.LoadScene("DungeonSelect");
    }
    public void TextInit()
    {
        if (GameManager.GMInstance.level == 1)
        {
            SkillSelectPaneltext.text = "��ų�� ������ �ּ���!";
        }
        else if (GameManager.GMInstance.level != 1)
        {
            SkillSelectPaneltext.text = "�����մϴ�!\n ���� ��!";
        }
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        /** �÷��̾� ���� �Լ� off */
        GameManager.GMInstance.bIsLive = false;

        /** GameOverImage ���� */
        GameOverImage.gameObject.SetActive(true);

        /** 1�� �� */
        yield return new WaitForSeconds(1.0f);

        /** GameOverPanel On */
        GameOverPanel.SetActive(true);

        StartCoroutine(GameOverTimeRoutine());
        
    }

    IEnumerator GameOverTimeRoutine()
    {
        /** 0.5�� �� */
        yield return new WaitForSeconds(1.0f);

        /** PlayStop�Լ� ȣ�� */
        GameManager.GMInstance.PlayStop();
    }

    /** ���� Ŭ���� �Լ� */
    void GameClear()
    {
        /** �ð��� �� �Ǿ��ٸ� */
        if (GameManager.GMInstance.gameTime == GameManager.GMInstance.maxGameTime)
        {
            /** ���� Ŭ���� �ǳ� Active On */
            GameClearPanel.SetActive(true);
            /** ���� �ð� ���� */
            Time.timeScale = 0;
        }
    }

    /** �κ�ȭ�� �̵� �Լ� */
    public void OnClickLobby()
    {
        /** ���� �ð��� ���󺹱� */
        Time.timeScale = 1.0f;
        /** �κ� ������ ��ȯ�Ǳ� ������ ���� ������ ���� false�� ���ش�. */
        GameManager.GMInstance.bIsLive = false;

        /** �κ�ȭ�� �̵� */
        SceneManager.LoadScene("Lobby");
    }

    /** �κ�ȭ�� �̵� �Լ� */
    public void OnClickReStart()
    {
        /** ���� �ð��� ���󺹱� */
        Time.timeScale = 1.0f;

        /** �÷��� �ð� �ʱ�ȭ */
        GameManager.GMInstance.PlayTime = 0.0f;

        /** �÷��̾� ���ǵ�� ������ ���̽� �̵��ӵ��� �����´�. */
        GameManager.GMInstance.PlayerSpeed = GameManager.GMInstance.GetPlayerBaseSpeed();

        /** �κ� ������ ��ȯ�Ǳ� ������ ���� ������ ���� false�� ���ش�. */
        GameManager.GMInstance.bIsLive = false;

        /** �κ�ȭ�� �̵� */
        SceneManager.LoadScene("PlayScene");
    }


}
