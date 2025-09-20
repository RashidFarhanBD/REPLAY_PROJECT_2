using UnityEngine;

public class SceneMover : MonoBehaviour
{


    public float scrollSpeed;
    bool scrollOn =true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void  switchCameraMove(bool condition)
    {

        scrollOn = condition;   
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!scrollOn) return;
        transform.position += Vector3.right * scrollSpeed * Time.deltaTime;
    }
}
