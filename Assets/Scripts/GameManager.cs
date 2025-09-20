using UnityEngine;

public class GameManager : MonoBehaviour
{


    public static GameManager instance;
    bool isGameOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private void Awake()
    {
        if(instance==null) instance = this; 
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoShakeScreen()
    {


    }

    public void ShakerChar()
    {

    }
    public void SHakeEverything()
    {



    }
}
