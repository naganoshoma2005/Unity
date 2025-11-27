
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public static VolumeManager Instance;

    [Header("Mixer Settings")]
    public AudioMixer masterMixer;

    // AudioMixerでExposeしたパラメータ名に合わせる
    private const string BGM_PARAM = "MyBGMVolume"; 
    private const string SE_PARAM = "MySEVolume";

    // PlayerPrefsに保存するキー
    private const string BGM_KEY = "BGMVolume";
    private const string SE_KEY = "SEVolume";

    void Awake()
    {
        // 🚨 シングルトンパターンによる重複防止と永続化 🚨
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // 既に存在する場合、新しく作られた自分自身は破棄
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        // PlayerPrefsに保存された値を読み込み、AudioMixerに設定
        float bgmValue = PlayerPrefs.GetFloat(BGM_KEY, 0.7f);
        SetBGMVolume(bgmValue);

        float seValue = PlayerPrefs.GetFloat(SE_KEY, 0.7f);
        SetSEVolume(seValue);
    }

    // BGMスライダーのOnValueChangedイベントに登録する関数
    public void SetBGMVolume(float sliderValue)
    {
    
        float volume = Mathf.Log10(Mathf.Max(sliderValue, 0.0001f)) * 20;
        masterMixer.SetFloat(BGM_PARAM, volume);

        PlayerPrefs.SetFloat(BGM_KEY, sliderValue);
        PlayerPrefs.Save();
    }

    // SEスライダーのOnValueChangedイベントに登録する関数
    public void SetSEVolume(float sliderValue)
    {
        float volume = Mathf.Log10(Mathf.Max(sliderValue, 0.0001f)) * 20;
        masterMixer.SetFloat(SE_PARAM, volume);

        PlayerPrefs.SetFloat(SE_KEY, sliderValue);
        PlayerPrefs.Save();
    }
}