using System;
using System.Runtime.CompilerServices;
using UnityEngine;



[RequireComponent(typeof (BoxCollider2D))]
public class StaticCameraZones : MonoBehaviour
{

    bool isDirty = false;
    BoxCollider2D mycollider;
    public static event Action<StaticCameraZones> OnCollisionEnter;
    public static event Action<StaticCameraZones> OnCollistionExit;
    SpriteRenderer rend;
    [Range(0,15)]
    public float overrideSpeed=1;
    [Range(0, 3)]
    public float transitionTime=2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mycollider= GetComponent<BoxCollider2D>();  
        rend = GetComponent<SpriteRenderer>();
        if(rend != null) rend.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      if(isDirty) return;
   
      if(collision.CompareTag("Player"))
        {

            isDirty = true;
            OnCollisionEnter?.Invoke(this);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellowNice;
        Gizmos.DrawWireCube(transform.position, transform.localScale);

        if (isDirty)
        {

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, transform.localScale);


        }


        
    }


    private void OnTriggerExit2D(Collider2D collision)
    {

        if (!isDirty) return;
        if (collision.CompareTag("Player")) 
        {
            mycollider.enabled = false;
            OnCollistionExit?.Invoke(this);
        }
            
    }
}
