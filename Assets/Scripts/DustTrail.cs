using UnityEngine;

public class DustTrail : MonoBehaviour
{

    [SerializeField] ParticleSystem particles;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!particles) return;
        if (rb.linearVelocityY <= .1f)
        {
            if (!particles.isPlaying)
                particles.Play();

        }
        else
        {
            if (particles.isPlaying)
                particles.Stop();

        }
    }
}