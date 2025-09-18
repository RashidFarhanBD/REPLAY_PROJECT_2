using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;
    public float lerp =0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame

    private void LateUpdate()
    {
        transform.position = Vector3.Slerp(transform.position, new Vector3(target.position.x,target.position.y,transform.position.z), lerp);
    }
   
}
