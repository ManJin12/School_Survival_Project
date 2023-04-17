using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My;
using static Define;

public class CharacterSpawn : MonoBehaviour
{
    public GameObject[] CharPrefabs;
    GameObject Player;

    public ECharacterType CurrentCharType;

    // Start is called before the first frame update
    void Start()
    {
        /** TODO ## ChracterSpawn.cs 캐릭터 생성 및 데이터 GameManager로 이동 */
        /** Player는 배열에 저장된 캐릭터를 SelectScene에서 선택된 캐릭터 타입으로 생성 */
        Player = Instantiate(CharPrefabs[(int)GameManager.GMInstance.CurrentChar]);

        if (Player != null)
        {
            Player.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            GameManager.GMInstance.Player = Player;
            GameManager.GMInstance.playerCtrl = Player.GetComponent<PlayerController>();
            
        } 
    }
}
