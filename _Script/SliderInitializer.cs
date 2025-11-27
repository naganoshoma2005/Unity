/*
using UnityEngine;
using UnityEngine.UI;

public class SliderInitializer : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider seSlider;

    private const string BGM_KEY = "BGMVolume";
    private const string SE_KEY = "SEVolume";

    void Start()
    {
        if (VolumeManager.Instance == null) return; // VolumeManagerがない場合は処理を中断

        // 🚨 1. PlayerPrefsから保存された値を読み込み、スライダーのValueに設定する（見た目の連動） 🚨
        if (bgmSlider != null)
        {
            bgmSlider.value = PlayerPrefs.GetFloat(BGM_KEY, 0.7f);
            
            // 🚨 2. 永続化されたVolumeManagerの関数をイベントに再登録 🚨
            bgmSlider.onValueChanged.RemoveAllListeners(); // 二重登録を防ぐため、一度クリア
            bgmSlider.onValueChanged.AddListener(VolumeManager.Instance.SetBGMVolume);
        }
        
        if (seSlider != null)
        {
            seSlider.value = PlayerPrefs.GetFloat(SE_KEY, 0.7f);
            
            seSlider.onValueChanged.RemoveAllListeners(); // 二重登録を防ぐため、一度クリア
            seSlider.onValueChanged.AddListener(VolumeManager.Instance.SetSEVolume);
        }
    }
}*/
using UnityEngine;
using UnityEngine.UI;

public class SliderInitializer : MonoBehaviour
{
    [Header("UI References")]
    public Slider bgmSlider;
    public Slider seSlider;

    private const string BGM_KEY = "BGMVolume";
    private const string SE_KEY = "SEVolume";

    void Start()
    {
        // VolumeManagerがシーンを跨いで存在していることを確認
        if (VolumeManager.Instance == null) return; 

        // 🚨 1. PlayerPrefsから保存された値を読み込み、スライダーのValueに設定する（見た目の連動） 🚨
        if (bgmSlider != null)
        {
            bgmSlider.value = PlayerPrefs.GetFloat(BGM_KEY, 0.7f);
            
            // 🚨 2. 永続化されたVolumeManagerの関数をイベントに再登録 🚨
            // シーンをロードするたびに再登録することで、イベントの参照切れを防ぐ
            bgmSlider.onValueChanged.RemoveAllListeners(); 
            bgmSlider.onValueChanged.AddListener(VolumeManager.Instance.SetBGMVolume);
        }
        
        if (seSlider != null)
        {
            seSlider.value = PlayerPrefs.GetFloat(SE_KEY, 0.7f);
            
            seSlider.onValueChanged.RemoveAllListeners(); 
            seSlider.onValueChanged.AddListener(VolumeManager.Instance.SetSEVolume);
        }
    }
}