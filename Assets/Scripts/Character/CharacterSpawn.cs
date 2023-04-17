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
        /** TODO ## ChracterSpawn.cs ĳ���� ���� �� ������ GameManager�� �̵� */
        /** Player�� �迭�� ����� ĳ���͸� SelectScene���� ���õ� ĳ���� Ÿ������ ���� */
        Player = Instantiate(CharPrefabs[(int)GameManager.GMInstance.CurrentChar]);

        if (Player != null)
        {
            Player.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            GameManager.GMInstance.Player = Player;
            GameManager.GMInstance.playerCtrl = Player.GetComponent<PlayerController>();
            
        } 
    }
}
