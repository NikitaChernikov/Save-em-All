using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInit : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] ShopUI shopUI;
    [SerializeField] int amountOfGold = 10;
    private string _gameId;

    private string intId = "Interstitial_Android";
    private string rewId = "Rewarded_Android";

    void Awake()
    {
        InitializeAds();
        //DontDestroyOnLoad(this.gameObject);
    }

    public void ShowAd()
    {
        Advertisement.Show(intId, this);
    }
    public void ShowAdWithBonus()
    {
        Advertisement.Show(rewId, this);
    }

    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, false, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (placementId.Equals(intId))
        {
            Debug.Log("IntAdsLoaded");
        }
        if (placementId.Equals(rewId))
        {
            Debug.Log("RewAdsLoaded");
        }
    }
    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        if (placementId.Equals(intId))
        {
            Debug.Log($"Unity IntAds Load Failed: {error.ToString()} - {message}");
        }
        if (placementId.Equals(rewId))
        {
            Debug.Log($"Unity RewAds Load Failed: {error.ToString()} - {message}");
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        Advertisement.Load(intId, this);
        Advertisement.Load(rewId, this);
    }
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) { }
    public void OnUnityAdsShowStart(string placementId)
    {
        if (placementId == rewId)
        {
            Advertisement.Load(rewId, this);
        }
        else
        {
            Advertisement.Load(intId, this);
        }
    }
    public void OnUnityAdsShowClick(string placementId) { }
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId == rewId)
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) + amountOfGold);
            shopUI.ChangeMoneyText();
            Advertisement.Load(rewId, this);
        }
        else
        {
            Advertisement.Load(intId, this);
        }
    }
}
