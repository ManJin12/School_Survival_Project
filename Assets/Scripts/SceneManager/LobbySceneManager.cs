using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbySceneManager : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject[] SelectCharacterPrefabs;
    public GameObject MyCharacter;

    // Start is called before the first frame update
    void Start()
    {
        /** MyCharacter에 GameManager가 저장하고 있는 캐릭터를 소환 */
        // MyCharacter = Instantiate(SelectCharacterPrefabs[(int)GameManager.GMInstance.CurrentChar]);
        /** MyCharacter크기를 3x3x3으로 바꿔준다. */
        //MyCharacter.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void OnClickMenuBtn()
    {
        /** MenuPanel 활성화 */
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
        SceneManager.LoadScene("DungeonSelect");
    }

    public void OnClickGameplayBtn()
    {
        /** 화면 전환 */
        SceneManager.LoadScene("PlayScene");
    }


}
