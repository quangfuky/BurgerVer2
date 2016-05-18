using System;
using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class LiveManager : MonoSingleton<LiveManager>
{
    public int MaxLive = 10;
    public int Live;
    public double LastTime;
    public double CurrentTime;
    public float Wait;

    public int Minute;
    public float Second;

    public Text Health;
    public Text TimeHealth;

    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        Live = PlayerPrefs.GetInt("Live", MaxLive);
        LastTime = PlayerPrefs.GetFloat("LastTime", (float)CurrentTimeInSecond());
        Wait = PlayerPrefs.GetFloat("Wait", 240);
        CurrentTime = CurrentTimeInSecond();
        double totalSecond = CurrentTime - LastTime;
        Wait -= (float)totalSecond % 240;
        if (Wait < 0)
        {
            Wait = 240 + Wait;
            Live ++;
        }
        else
        {
            Wait = Wait;
        }
        Second = (int) totalSecond;
    }

    void Start()
    {
        //DontDestroyOnLoad(gameObject);

    }

    void Update()
    {
        if (Live <= MaxLive)
        {
            CalculateLive();
        }
    }

    void OnDisable()
    {
        LastTime = CurrentTimeInSecond();
        PlayerPrefs.SetFloat("LastTime", (float)LastTime);
        PlayerPrefs.SetInt("Live", Live);
        PlayerPrefs.SetFloat("Wait", Wait);
    }

    void OnApplicationQuit()
    {
        Debug.Log("quit");
        LastTime = CurrentTimeInSecond();
        PlayerPrefs.SetFloat("LastTime", (float)LastTime);
        PlayerPrefs.SetInt("Live", Live);
        PlayerPrefs.SetFloat("Wait", Wait);
    }

    public void SavePref()
    {
        PlayerPrefs.SetInt("Live", Live);
    }

    public double CurrentTimeInSecond()
    {
        TimeSpan span = DateTime.UtcNow.Subtract(new DateTime(2016, 1, 1, 0, 0, 0, DateTimeKind.Utc));
        return span.TotalSeconds;
    }

    public void CalculateLive()
    {
        while (Second>240)
        {
            if (Live < MaxLive)
            {
                Live++;
                Second -= 240;
            }
            else
            {
                Second = 0;
            }
        }
        if (Wait <= 0)
        {
            Wait = 240;
            if (Live < MaxLive)
            {
                Live++;
                if (Health != null)
                {
                    Health.text = Live.ToString();
                }
            }
        }
        Wait -= Time.deltaTime;
        if (TimeHealth != null)
        {
            TimeHealth.text = ((int)Wait / 60).ToString() + ":" + ((int)Wait % 60).ToString();
        }
        if (Health != null)
        {
            Health.text = Live.ToString();
        }
    }
}
