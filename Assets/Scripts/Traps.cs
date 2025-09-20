using UnityEngine;
using System;
public class Traps : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public static event Action<Traps> OnHitTrap;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            OnHitTrap?.Invoke(this);

        }
    }


}
