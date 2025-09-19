using UnityEngine;

public class SceneMover : MonoBehaviour
{


    public float scrollSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position += Vector3.right * scrollSpeed * Time.deltaTime;
    }
}
