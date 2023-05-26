using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My;


public class DropPotion : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        /** ���� ������Ʈ �±��̸��� Potion�̶�� */
        if (collision.gameObject.CompareTag("PlayerCharacter"))
        {
            /** ĳ���� ü�� 10�� ȸ�� */
            GameManager.GMInstance.Health += GameManager.GMInstance.MaxHealth * 0.1f;

            /** ���������Ʈ �ı� */
            Destroy(gameObject);
        }
    }
}
