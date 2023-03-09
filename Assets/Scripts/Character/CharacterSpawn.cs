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
            Player.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            GameManager.GMInstance.Player = Player;
            GameManager.GMInstance.playerCtrl = Player.GetComponent<PlayerController>();
            
        } 
    }
}
