using UnityEngine;

public class SpikeAudio : MonoBehaviour
{
    public static SpikeAudio Instance { get; private set; }
    private AudioSource audioSource; 

    private void Awake() {
        Instance = this; 
        audioSource = GetComponent<AudioSource>(); 
    }

    internal void PlaySpikeAudio()
    {
        audioSource.Play(); 
    }

    internal void StopSpikeAudio()
    {
        audioSource.Stop(); 
    }
}
