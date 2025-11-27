
using UnityEngine;

public class BGMAudioPlayer : MonoBehaviour
{

    public static BGMAudioPlayer Instance; 
    private AudioSource audioSource;

    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("BGMAudioPlayer is missing an AudioSource component.");
                return;
            }

            
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
           
            Destroy(this.gameObject);
        }
    }
}