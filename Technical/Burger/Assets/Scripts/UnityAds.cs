using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class UnityAds : MonoSingleton<UnityAds>
{
    public GameObject plusLife;
    public GameObject AddLife;
    public string gameId;
    private GameObject addLife;
	void Start () {
	    if (Instance != null && Instance != this)
	    {
            Destroy(gameObject);
	    }
        DontDestroyOnLoad(this.gameObject);
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId);
        }
    }
	
	void Update () {
	
	}

    public void ShowRewardAds()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            Time.timeScale = 0;
            ShowOptions options = new ShowOptions();
            options.resultCallback = HandleRewardVideo;
            Advertisement.Show("rewardedVideo", options);
        }
    }

    public void HandleRewardVideo(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Time.timeScale = 1;
                if (plusLife != null)
                {
                    plusLife.SetActive(true);
                }
                Debug.Log("Video completed.");
                LiveManager.Instance.Live += 2;
                if (LiveManager.Instance.Live > 20)
                {
                    LiveManager.Instance.Live = 20;
                }
                if (AddLife != null)
                {
                    AddLife.SetActive(false);
                }
                addLife = GameObject.FindWithTag("ads");
                if (addLife != null)
                {
                    addLife.SetActive(false);
                }
                LiveManager.Instance.SavePref();
                break;
            case ShowResult.Skipped:
                Time.timeScale = 1;
                Debug.LogWarning("Video was skipped.");
                break;
            case ShowResult.Failed:
                Time.timeScale = 1;
                Debug.LogError("Video failed to show.");
                break;
        }
    }
}
