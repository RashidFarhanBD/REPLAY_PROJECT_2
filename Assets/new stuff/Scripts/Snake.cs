using System;
using UnityEngine;

public class Snake : MonoBehaviour
{

    public static event Action OnSnakeHit;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {


            if(!GameManager.instance.GetIsPlayerDead())
            {




                OnSnakeHit?.Invoke();




            }

        }
    }
}
