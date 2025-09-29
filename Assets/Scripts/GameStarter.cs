using System;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public bool startPressed;
    public static event Action OnStartPressed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startPressed) return;
        if (Input.anyKeyDown)
        {

            startPressed = true;
            OnStartPressed?.Invoke();
            this.enabled = false;

        }
    }
}
