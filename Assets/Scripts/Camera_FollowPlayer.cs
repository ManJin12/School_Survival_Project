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
        /** CinemachineVirtualCamera ������Ʈ�� ������ �� �ְ����ش�. */
        VCam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        /** ���� Player���� null�̶�� */
        if (Player == null)
        {
            /** Player�� PlayerCharacter�±׸� ���� ������Ʈ�� �ȴ�. */
            Player = GameObject.FindWithTag("PlayerCharacter");
            /** Player�� �� ���� ��ٸ� */
            if (Player != null)
            {
                /** ��ġ���� FollowTarget Player�� ��ġ���� ���󰡰� */
                FollowTarget = Player.transform;
                /** CinemachineVirtualCamera������Ʈ�� Follow����� ���ϰ� �ִ� VCam�� FollowTarget�� �����Ų��. */
                VCam.Follow = FollowTarget;
            }
        }
    }
}
