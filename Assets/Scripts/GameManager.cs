using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /** GameManagerŸ���� �޸𸮸� �̸� Ȯ���� �д�. */
    public static GameManager GMInstance;

    /** PlayerControllerŸ���� ������ player�� ����Ƽ���� �޾ƿ� */
    public PlayerController player;

    void Awake()
    {
        /** GMInstance�� �� Ŭ������ �ǹ��Ѵ�. */
        GMInstance = this;
    }
}
