using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    /** �浹������ �߻��ϴ� �̺�Ʈ */
    void OnTriggerExit2D(Collider2D collision)
    {
        /** �±� �̸��� Area�� �ƴϸ� */
        if (!collision.CompareTag("Area"))
        {
            /** �Լ��� ����������.*/
            return;
        }

        /** ĳ������ ��ġ �� */
        Vector3 PlayerPos =
            GameManager.GMInstance.player.transform.position;

        /** �� Ŭ������ ���� ������Ʈ�� ��ġ �� */
        Vector3 MyPos = transform.position;

        /** �÷��̾�� Ŭ������ ���� ������Ʈ�� x�Ÿ����� */
        float diff_X = Mathf.Abs(PlayerPos.x - MyPos.x);

        /** �÷��̾�� Ŭ������ ���� ������Ʈ�� y�Ÿ����� */
        float diff_Y = Mathf.Abs(PlayerPos.y - MyPos.y);

        /** �÷��̾ �Է¹��� �� */
        Vector3 PlayerDir = GameManager.GMInstance.player.m_InputVec;

        /** ���� �����ڷ� �÷��̾��� x���� ũ��� ������ ���Ѵ�. */
        float DirX = PlayerDir.x < 0 ? -1 : 1 ;
        /** ���� �����ڷ� �÷��̾��� y���� ũ��� ������ ���Ѵ�. */
        float DirY = PlayerDir.y < 0 ? -1 : 1;


        switch (transform.tag)
        {
            case "Ground":
                /** �� ������Ʈ�� �Ÿ� ���̰� x���� �� ũ�ٸ� */
                if (diff_X > diff_Y)
                {
                    /** ������Ʈ�� ������ �������� 40��ŭ �̵��Ѵ�.  */
                    transform.Translate(Vector3.right * DirX * 40);
                    
                }
                /** �� ������Ʈ�� �Ÿ����̰� y���� �� ũ�ٸ� */
                else if (diff_X < diff_Y)
                {
                    /** ������Ʈ�� �� �������� 40��ŭ �̵��Ѵ�.  */
                    transform.Translate(Vector3.up * DirY * 40);
                    
                }
                break;

            case "Enemy":

                break;
        }
    }
}