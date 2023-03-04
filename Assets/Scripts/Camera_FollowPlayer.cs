using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera_FollowPlayer : MonoBehaviour
{
    public GameObject Player = null;
    public Transform FollowTarget;

    private CinemachineVirtualCamera VCam;

    // Start is called before the first frame update
    void Start()
    {
        /** CinemachineVirtualCamera 컴포넌트에 접근할 수 있게해준다. */
        VCam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        /** 만약 Player값이 null이라면 */
        if (Player == null)
        {
            /** Player는 PlayerCharacter태그를 가진 오브젝트가 된다. */
            Player = GameObject.FindWithTag("PlayerCharacter");
            /** Player가 잘 연결 됬다면 */
            if (Player != null)
            {
                /** 위치값인 FollowTarget Player의 위치값을 따라가고 */
                FollowTarget = Player.transform;
                /** CinemachineVirtualCamera컴포넌트의 Follow기능을 지니고 있는 VCam은 FollowTarget을 적용시킨다. */
                VCam.Follow = FollowTarget;
            }
        }
    }
}
