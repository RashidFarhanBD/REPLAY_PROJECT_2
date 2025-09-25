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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mycollider= GetComponent<BoxCollider2D>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
