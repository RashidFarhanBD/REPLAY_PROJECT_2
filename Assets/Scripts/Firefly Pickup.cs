using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class FireflyPickup : MonoBehaviour, Ipickupable
{
    public static event Action OnPickUpFireFly; 


    public void PickUp()
    {
        OnPickUpFireFly?.Invoke();
        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {

            PickUp();

           
        }
    }

   
}
