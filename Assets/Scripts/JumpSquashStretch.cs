using DG.Tweening;
using UnityEngine;

public class JumpSquashStretch : MonoBehaviour
{
    [SerializeField] private Transform playerVisual; // The child sprite/mesh, not the root with Rigidbody
    [SerializeField] private float squashX = 1.2f;
    [SerializeField] private float squashY = 0.8f;
    [SerializeField] private float stretchX = 0.8f;
    [SerializeField] private float stretchY = 1.2f;
    [SerializeField] private float squashDuration = 0.08f;
    [SerializeField] private float stretchDuration = 0.12f;
    [SerializeField] private float recoverDuration = 0.1f;

    private Sequence _jumpSeq;

    public void PlayJumpEffect()
    {
        // Kill existing animation if one is running
        _jumpSeq?.Kill();

        _jumpSeq = DOTween.Sequence();

        _jumpSeq
            // Squash before takeoff
            .Append(playerVisual.DOScale(new Vector3(squashX, squashY, 1f), squashDuration).SetEase(Ease.OutQuad))
            // Stretch as you go up
            .Append(playerVisual.DOScale(new Vector3(stretchX, stretchY, 1f), stretchDuration).SetEase(Ease.OutQuad))
            // Recover to normal
            .Append(playerVisual.DOScale(Vector3.one, recoverDuration).SetEase(Ease.InOutQuad));
    }
}
