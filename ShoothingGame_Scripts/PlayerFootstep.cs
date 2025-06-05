using UnityEngine;

public class PlayerFootstep : MonoBehaviour
{
    public AudioSource footstepAudio; // 足音用のオーディオソース

    public void FootStep()
    {
        if (footstepAudio != null)
        {
            footstepAudio.Play(); // 足音を再生
        }
        else
        {
            Debug.LogWarning("Footstep AudioSource is not assigned.");
        }
    }
}
