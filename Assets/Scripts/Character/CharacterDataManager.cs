using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My;
public class CharacterDataManager : MonoBehaviour
{

    /** 캐릭터 능력치 업 */
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
