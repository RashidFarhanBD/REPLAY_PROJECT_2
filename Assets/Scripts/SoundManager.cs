using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private Tween bgmTween;
    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxSource; // For one-shot sounds
    [SerializeField] private AudioSource bgmSource;  // For  background music
    [SerializeField] private AudioSource snakeSOundsrc;  // For  background music

    [Header("Clips")]
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip doubleJumpClip;
    [SerializeField] private AudioClip landClip;
    [SerializeField] private AudioClip dashClip;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] public AudioClip bgmCLip;
    [SerializeField] public AudioClip pickUPclip;

    public float BGMVolume;
    [SerializeField] public AudioClip clip1;
    [SerializeField] public AudioClip clip2;
    [SerializeField] public AudioClip rockbreak;
    [SerializeField] private float minPitch = 0.75f;
    [SerializeField] private float maxPitch = 1f;
    [SerializeField] private float minDelay= 10;
    [SerializeField] private float maxDelay= 30;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        BGMVolume = bgmSource.volume;
        // Keep between scenes
    }

    private void Start()
    {
        StartCoroutine(PlayRandomClips());
    }

    #region --- BGM with Fade ---
    public void PlayBGM(AudioClip clip , float fadeDuration = 1f, bool loop = true)
    {
        if (bgmSource == null || clip == null) return;

        // Stop previous tween if any
        bgmTween?.Kill();

        // If already playing something, fade out first then fade in new track
        if (bgmSource.isPlaying)
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(bgmSource.DOFade(0f, fadeDuration / 2f))
               .AppendCallback(() =>
               {
                   bgmSource.clip = clip;
                   bgmSource.loop = loop;
                   bgmSource.Play();
               })
               .Append(bgmSource.DOFade(BGMVolume, fadeDuration / 2f));
            bgmTween = seq;
        }
        else
        {
            bgmSource.clip = clip;
            bgmSource.loop = loop;
            bgmSource.volume = 0f;
            bgmSource.Play();
            bgmTween = bgmSource.DOFade(BGMVolume, fadeDuration);
        }
    }

    public void StopBGM(float fadeDuration = 1f)
    {
        if (bgmSource != null && bgmSource.isPlaying)
        {
            bgmTween?.Kill();
            bgmTween = bgmSource.DOFade(0f, fadeDuration)
                .OnComplete(() => bgmSource.Stop());
        }
    }

    public void PauseBGM(float fadeDuration = 0.5f)
    {
        if (bgmSource != null && bgmSource.isPlaying)
        {
            bgmTween?.Kill();
            bgmTween = bgmSource.DOFade(0f, fadeDuration)
                .OnComplete(() => bgmSource.Pause());
        }
    }

    public void ResumeBGM(float fadeDuration = 0.5f)
    {
        if (bgmSource != null && !bgmSource.isPlaying && bgmSource.clip != null)
        {
            bgmSource.UnPause();
            bgmSource.volume = 0f;
            bgmTween?.Kill();
            bgmTween = bgmSource.DOFade(BGMVolume, fadeDuration);
        }
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
    public void PlayPickup()
    {

        PlaySFX(pickUPclip);
    }



    private System.Collections.IEnumerator PlayRandomClips()
    {
        while (true)
        {
            // wait a random delay
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);
            PlayRandomMonsterSound();

        }
    }

    public void PlayRandomMonsterSound()
    {

        AudioClip chosenClip = (Random.value > 0.5f) ? clip1 : clip2;
        snakeSOundsrc.pitch = Random.Range(minPitch, maxPitch);

        // play the sound
        snakeSOundsrc.PlayOneShot(chosenClip);
      
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
            sfxSource.PlayOneShot(clip);
    }
}
