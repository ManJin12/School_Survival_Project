using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My;
public class CharacterDataManager : MonoBehaviour
{

    /** ĳ���� �ɷ�ġ �� */
    public float CharacterHp_Up;
    public float CharacterDamage_Up;
    public float CharacterDefense_Up;
    public float CharacterSpeed_Up;

    void Start()
    {
        GameManager.GMInstance.CharacterDataManagerRef = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
