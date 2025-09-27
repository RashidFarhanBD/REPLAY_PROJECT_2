using UnityEngine;

public class DustTrail : MonoBehaviour
{

    [SerializeField] ParticleSystem particles;
    Rigidbody2D rb;
    [SerializeField] PlayerMovement mov;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mov = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!particles) return;
        
        if (rb.linearVelocityY <= 0f && !mov.IsDashing)
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