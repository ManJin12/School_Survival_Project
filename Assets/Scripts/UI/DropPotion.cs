using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My;


public class DropPotion : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        /** 만약 오브젝트 태그이름이 Potion이라면 */
        if (collision.gameObject.CompareTag("PlayerCharacter"))
        {
            /** 캐릭터 체력 10퍼 회복 */
            GameManager.GMInstance.Health += GameManager.GMInstance.MaxHealth * 0.1f;

            /** 물약오브젝트 파괴 */
            Destroy(gameObject);
        }
    }
}
