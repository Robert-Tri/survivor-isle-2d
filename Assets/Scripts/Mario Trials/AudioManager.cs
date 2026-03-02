using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundAudioSource;
    [SerializeField] private AudioSource effectAudioSource;

    [SerializeField] private AudioClip backgroundClip;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip coinClip;

    void Start()
    {
        PlayBackgroundMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundAudioSource != null && backgroundClip != null)
        {
            backgroundAudioSource.clip = backgroundClip;
            backgroundAudioSource.loop = true;
            backgroundAudioSource.Play();
        }
    }
    
    public void PlayJumpSound()
    {
        if (effectAudioSource != null && jumpClip != null)
        {
            effectAudioSource.PlayOneShot(jumpClip);
        }
    }

    public void PlayCoinSound() 
    { 
        if (effectAudioSource != null && coinClip != null)
        {
            effectAudioSource.PlayOneShot(coinClip);
        }
    }
}
