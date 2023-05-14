using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My;
using static Define;
using UnityEngine.UI;
using System;

public class SoundManager : MonoBehaviour
{
    /** ������� ���� */
    [Header("---BGM---")]
    public AudioClip[] MainBGM;
    public AudioSource BGMPlayer;

    /** ȿ���� ���� */
    [Header("---SFX---")]
    public AudioClip[] SFXClips;
    public int Channels;
    public AudioSource[] SFXPlayers;
    int SFXChannelIndex;

    public bool bIsSFXOn;
    public bool bIsBGMOn;

    public enum SFX
    {
        Dead,
        Hit,
        LevelUp = 3,
        Lose,
        Melee, 
        Range = 7,
        Select,
        Win,
        FireBall,
        IceArrow,
        Lightning,
        Tornado,
    }

    void Awake()
    {
        Init();
    }

    void Init()
    {
        /** ����� �÷��̾� �ʱ�ȭ */
        GameObject BGMObject = new GameObject("BGMPlayer");
        /** BGMObject�� �θ�Ŭ���� �� ��ũ��Ʈ�� ���� ������Ʈ�� �Ѵ�. */
        BGMObject.transform.parent = transform;
        /** BGMPlayer�� BGMObject�� �߰��� AudioSource�� �����´�. */
        BGMPlayer = BGMObject.AddComponent<AudioSource>();
        /** ����� ��� ���� �ݺ� */
        BGMPlayer.loop = true;
        /** ����� �÷��� */
        BGMPlayer.clip = MainBGM[0];
        BGMPlayer.Play();
        bIsBGMOn = true;

        /** ȿ���� �÷��̾� �ʱ�ȭ */
        GameObject SFXObject = new GameObject("SFXPlayer");
        /** SFXObject�� �θ�Ŭ���� �� ��ũ��Ʈ�� ���� ������Ʈ�� �Ѵ�. */
        SFXObject.transform.parent = transform;
        /** ä���� ������ŭ ȿ���� ����� ���� */
        SFXPlayers = new AudioSource[Channels];
        bIsSFXOn = true;


        /** ����� ȿ���� ������ŭ �ݺ� */
        for (int i = 0; i < SFXPlayers.Length; i++)
        {
            SFXPlayers[i] = SFXObject.AddComponent<AudioSource>();
            /** �ʱ� ��� off */
            SFXPlayers[i].playOnAwake = false;
        }

    }

    void Start()
    {
        GameManager.GMInstance.SoundManagerRef = this;
    }

    /** SFX�� �Ű������� �޴� ȿ���� ��� �Լ� ���� */
    public void PlaySFX(SFX sfx)
    {
        /** ����� Length����ŭ �ݺ� */
        for (int i = 0; i < SFXPlayers.Length; i++)
        {
            int LoopIndex = (i + SFXChannelIndex) % SFXPlayers.Length;

            /** ���� ���� ȿ������ �������̸�? */
            if (SFXPlayers[LoopIndex].isPlaying)
            {
                /** �ٽ� �ݺ��� �ʱ���� ���� */
                continue;
            }

            /** ChanelIndex�� LoopIndex������ �ٲ��ش�. */
            SFXChannelIndex = LoopIndex;
            /** SFXPlayers�� 0��° Clip�� SFX Enum�� ������ �����´�. */
            SFXPlayers[LoopIndex].clip = SFXClips[(int)sfx];
            /** ��� */
            SFXPlayers[LoopIndex].Play();
            break;
        }
    }


}
