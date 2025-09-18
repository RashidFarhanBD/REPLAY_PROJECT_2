using UnityEngine;

public class RestrictMovement : MonoBehaviour
{

    Camera cam;
    float  Hbounds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
       Hbounds=(cam.orthographicSize * Screen.width / Screen.height);


    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, Camera.main.transform.position.x-Hbounds,Camera.main.transform.position.x + Hbounds), transform.position.y);
    }
}
