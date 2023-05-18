using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using My;
using static Define;

public class AdInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    [SerializeField] RewardedAdsButton rewardedGoldAdsButton;
    [SerializeField] RewardedAdsButton rewardedTicketAdsButton;
    [SerializeField] RewardedAdsButton SkillReSelectButton;
    [SerializeField] RewardedAdsButton GetClearGoldButton;

    void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
#if UNITY_IOS
            _gameId = _iOSGameId;
#elif UNITY_ANDROID
        _gameId = _androidGameId;
#elif UNITY_EDITOR
            _gameId = _androidGameId; //Only for testing the functionality in the Editor
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }

    void Start()
    {
        OnInitializationComplete();
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        if (GameManager.GMInstance.CurrentScene == ESceneType.LobbyScene)
        {
            rewardedGoldAdsButton.LoadAd();
            rewardedTicketAdsButton.LoadAd();
        }
        else if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene)
        {
            SkillReSelectButton.LoadAd();
            GetClearGoldButton.LoadAd();
        }
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}