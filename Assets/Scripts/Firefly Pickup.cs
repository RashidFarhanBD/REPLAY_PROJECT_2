using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

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
        Debug.Log(collision.gameObject.name);
        if (collision.CompareTag("Player"))
        {

            PickUp();


        }
    }

   
}
