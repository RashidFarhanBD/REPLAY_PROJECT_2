using UnityEngine;
using DG.Tweening;

public class PlayerDeathEffect : MonoBehaviour
{
    public float moveDuration = 1f;   // how long the move lasts
    public float scaleMultiplier = 3f; // how much bigger when it hits screen
    public float fadeDuration = 0.5f;  // fade out after

    private bool isDead = false;
    private Camera mainCam;
    private SpriteRenderer sr;

    void Start()
    {
        mainCam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        transform.DOKill(); // kill any running tweens

        // Get the world position of the screen center (at player's current depth)
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f,
                                           mainCam.WorldToScreenPoint(transform.position).z);
        Vector3 targetPos = mainCam.ScreenToWorldPoint(screenCenter);

        // Create sequence
        Sequence deathSeq = DOTween.Sequence();

        // Move player to center of screen
        deathSeq.Append(transform.DOMove(targetPos, moveDuration).SetEase(Ease.InCubic));

        // Scale up while moving
        deathSeq.Join(transform.DOScale(scaleMultiplier, moveDuration).SetEase(Ease.OutQuad));

        // Fade out at the end
        if (sr != null)
        {
            deathSeq.Append(sr.DOFade(0f, fadeDuration));
        }

        // Destroy at the end
        deathSeq.OnComplete(() => Destroy(gameObject));
    }
}
