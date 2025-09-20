using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxSource; // For one-shot sounds
    [SerializeField] private AudioSource bgmSource;  // For  background music

    [Header("Clips")]
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip doubleJumpClip;
    [SerializeField] private AudioClip landClip;
    [SerializeField] private AudioClip dashClip;
    [SerializeField] private AudioClip deathClip;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Keep between scenes
    }


    #region BGM
    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        if (bgmSource == null || clip == null) return;

        bgmSource.clip = clip;
        bgmSource.loop = loop;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        if (bgmSource != null)
            bgmSource.Stop();
    }

    public void PauseBGM()
    {
        if (bgmSource != null && bgmSource.isPlaying)
            bgmSource.Pause();
    }

    public void ResumeBGM()
    {
        if (bgmSource != null && !bgmSource.isPlaying)
            bgmSource.UnPause();
    }
    #endregion

    public void PlayJump()
    {
        PlaySFX(jumpClip);
    }

    public void PlayDoubleJump()
    {
        PlaySFX(doubleJumpClip);
    }

    public void PlayLand()
    {
        PlaySFX(landClip);
    }

    public void PlayDash()
    {
        PlaySFX(dashClip);
    }

    public void PlayDeath()
    {
        PlaySFX(deathClip);
    }

    private void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
            sfxSource.PlayOneShot(clip);
    }
}
