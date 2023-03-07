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
        /** TODO ## ĳ���� ���� �� ������ GameManager�� �̵� */
        /** Player�� �迭�� ����� ĳ���͸� SelectScene���� ���õ� ĳ���� Ÿ������ ���� */
        Player = Instantiate(CharPrefabs[(int)GameManager.GMInstance.CurrentChar]);

        if (Player != null)
        {
            GameManager.GMInstance.Player = Player;
            GameManager.GMInstance.playerCtrl = Player.GetComponent<PlayerController>();
            
        } 
    }
}
