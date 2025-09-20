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
