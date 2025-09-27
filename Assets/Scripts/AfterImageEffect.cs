using UnityEngine;
using DG.Tweening;

public class AfterImageEffect : MonoBehaviour
{
    [Header("After Image Settings")]
    [SerializeField] private GameObject ghostPrefab;   // Prefab with SpriteRenderer
    [SerializeField] private float spawnInterval = 0.05f;
    [SerializeField] private float fadeDuration = 0.3f;
    [SerializeField] private Color ghostColor = new Color(1f, 1f, 1f, 0.5f);
    [SerializeField] SpriteRenderer spriteRenderer;
    private bool isSpawning = false;
    private float spawnTimer;

    void Update()
    {
        if (!isSpawning) return;

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            SpawnOne();
            spawnTimer = spawnInterval;
        }
    }

    public void StartTrail()
    {
        isSpawning = true;
        spawnTimer = 0f; // force immediate spawn
    }

    public void StopTrail()
    {
        isSpawning = false;
    }

    private void SpawnOne()
    {
        if (ghostPrefab == null) return;

        GameObject ghost = Instantiate(ghostPrefab, transform.position, transform.rotation);
        ghost.transform.localScale = transform.localScale;

        SpriteRenderer sr = ghost.GetComponent<SpriteRenderer>();
        sr.sprite = spriteRenderer.sprite;
        if (sr != null)
        {
            sr.color = ghostColor;

            // Tween alpha to 0 over fadeDuration
            
            sr.transform.DOScale(Vector3.one*1.1f, fadeDuration / 2).SetEase(Ease.Linear).OnComplete(() => { sr.transform.DOScale(Vector3.zero, fadeDuration / 2); });
            sr.DOFade(0f, fadeDuration).SetEase(Ease.Linear)
                .OnComplete(() => Destroy(ghost));
        }
    }
}
