using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;



[RequireComponent(typeof (BoxCollider2D))]
[System.Serializable]
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

    [Space(3)]
    [Header("Olzhas stuff")]

    [Tooltip("turn thiss on to speed cam for certain time")]
    public bool boltCamera;
     bool isBolting;
    public float boltCamSpeed=10;
    public float boltTransitionTime=.5f;
    public float boltWaitTime= 1;

    [HideInInspector]
    public float waitTimer=0;


    public bool IsBolting { get => isBolting; set => isBolting = value; }

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
    public void boltTranistionFinished()
    {
        isBolting = false;
        waitTimer = boltWaitTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      if(isDirty) return;
   
      if(collision.CompareTag("Player"))
        {

            isDirty = true;
            IsBolting = boltCamera;
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
