using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawn : MonoBehaviour
{
    public GameObject[] CharPrefabs;
    GameObject Player;

    public CharType CurrentCharType;

    // Start is called before the first frame update
    void Start()
    {
        /** TODO ## 캐릭터 생성 및 데이터 GameManager로 이동 */
        /** Player는 배열에 저장된 캐릭터를 SelectScene에서 선택된 캐릭터 타입으로 생성 */
        Player = Instantiate(CharPrefabs[(int)GameManager.GMInstance.CurrentChar]);

        if (Player != null)
        {
            Player.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            GameManager.GMInstance.Player = Player;
            GameManager.GMInstance.playerCtrl = Player.GetComponent<PlayerController>();
            
        } 
    }
}
