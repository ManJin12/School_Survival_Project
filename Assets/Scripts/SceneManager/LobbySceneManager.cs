using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbySceneManager : MonoBehaviour
{
    public GameObject MenuPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickMenuBtn()
    {
        MenuPanel.SetActive(true);
    }

    public void OnClickMenuCloseBtn()
    {
        /** MenuPanel 비활성화 */
        MenuPanel.SetActive(false);
    }

    public void OnClickSelectCharacterBtn()
    {
        /** 화면 전환 */
        SceneManager.LoadScene("CharacterSelect");
    }

    public void OnClickGameplayBtn()
    {
        /** 화면 전환 */
        SceneManager.LoadScene("PlayScene");
    }
}
