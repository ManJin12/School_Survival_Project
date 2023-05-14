using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My;
using static Define;
using UnityEngine.UI;
using System;

public class SoundManager : MonoBehaviour
{
    /** 배경음악 관련 */
    [Header("---BGM---")]
    public AudioClip[] MainBGM;
    public AudioSource BGMPlayer;

    /** 효과음 관련 */
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
        /** 배경음 플레이어 초기화 */
        GameObject BGMObject = new GameObject("BGMPlayer");
        /** BGMObject의 부모클래스 이 스크립트를 가진 오브젝트로 한다. */
        BGMObject.transform.parent = transform;
        /** BGMPlayer는 BGMObject에 추가한 AudioSource를 가져온다. */
        BGMPlayer = BGMObject.AddComponent<AudioSource>();
        /** 배경음 재생 무한 반복 */
        BGMPlayer.loop = true;
        /** 배경음 플레이 */
        BGMPlayer.clip = MainBGM[0];
        BGMPlayer.Play();
        bIsBGMOn = true;

        /** 효과음 플레이어 초기화 */
        GameObject SFXObject = new GameObject("SFXPlayer");
        /** SFXObject의 부모클래스 이 스크립트를 가진 오브젝트로 한다. */
        SFXObject.transform.parent = transform;
        /** 채널의 개수만큼 효과음 재생기 생성 */
        SFXPlayers = new AudioSource[Channels];
        bIsSFXOn = true;


        /** 저장된 효과음 개수만큼 반복 */
        for (int i = 0; i < SFXPlayers.Length; i++)
        {
            SFXPlayers[i] = SFXObject.AddComponent<AudioSource>();
            /** 초기 재생 off */
            SFXPlayers[i].playOnAwake = false;
        }

    }

    void Start()
    {
        GameManager.GMInstance.SoundManagerRef = this;
    }

    /** SFX를 매개변수로 받는 효과음 재생 함수 정의 */
    public void PlaySFX(SFX sfx)
    {
        /** 저장된 Length값만큼 반복 */
        for (int i = 0; i < SFXPlayers.Length; i++)
        {
            int LoopIndex = (i + SFXChannelIndex) % SFXPlayers.Length;

            /** 만약 지금 효과음이 실행중이면? */
            if (SFXPlayers[LoopIndex].isPlaying)
            {
                /** 다시 반복문 초기부터 실행 */
                continue;
            }

            /** ChanelIndex를 LoopIndex값으로 바꿔준다. */
            SFXChannelIndex = LoopIndex;
            /** SFXPlayers의 0번째 Clip은 SFX Enum의 순서를 가져온다. */
            SFXPlayers[LoopIndex].clip = SFXClips[(int)sfx];
            /** 재생 */
            SFXPlayers[LoopIndex].Play();
            break;
        }
    }


}
