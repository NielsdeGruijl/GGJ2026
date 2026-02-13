using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource SFXAudioSource;
    [SerializeField] private AudioSource ambientAudioSource;
    [SerializeField] private AudioSource coinAudioSource;

    private WaitForSeconds waitForCoinSFXCooldown;

    private bool canPlayCoinSFX = true;
    private bool canPlayDamageSFX = true;
    
    private void Awake()
    {
        if(instance && instance != this)
            Destroy(this);
        else
            instance = this;
        
        DontDestroyOnLoad(gameObject);

        waitForCoinSFXCooldown = new WaitForSeconds(0.05f);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (!canPlayDamageSFX)
            return;
        
        SFXAudioSource.PlayOneShot(clip);

        StartCoroutine(DamageSFXCooldownCo());
    }

    public void PlayCoinSound(AudioClip clip, float pitch)
    {
        if (!canPlayCoinSFX)
            return;
        
        coinAudioSource.pitch = pitch;
        coinAudioSource.PlayOneShot(clip);
        
        StartCoroutine(CoinSFXCooldownCo());
    }

    public void PlayAmbient(AudioClip clip)
    {
        ambientAudioSource.clip = clip;
        ambientAudioSource.Play();
    }

    private IEnumerator CoinSFXCooldownCo()
    {
        canPlayCoinSFX = false;
        yield return waitForCoinSFXCooldown;
        canPlayCoinSFX = true;
    }
    
    private IEnumerator DamageSFXCooldownCo()
    {
        canPlayDamageSFX = false;
        yield return waitForCoinSFXCooldown;
        canPlayDamageSFX = true;
    }
}
