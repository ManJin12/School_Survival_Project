using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /** GameManager타입의 메모리를 미리 확보해 둔다. */
    public static GameManager GMInstance;

    /** PlayerController타입을 가지는 player를 유니티에서 받아옴 */
    public PlayerController player;

    void Awake()
    {
        /** GMInstance는 이 클래스를 의미한다. */
        GMInstance = this;
    }
}
