using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    public static GameManager instance;
    public PlayerMovement playerMovement;
    public SceneMover sceneMover;   
    bool isGameOver;
    private bool isPlayerDead;
    public JuiceManager juiceManager;
    [Header("Designer look here")]
    [Tooltip ("time to restart after player dies")]
    public float gameRestartTime = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created



    private void OnEnable()
    {
        Traps.OnHitTrap += Traps_OnHitTrap;
    }

    private void Traps_OnHitTrap(Traps obj)
    {
        if (isPlayerDead) return;
        //stop player, stop camera 
        isPlayerDead = true;
        playerMovement.enabled = false;

        playerMovement.RB.linearVelocity = Vector2.zero;
        playerMovement.RB.bodyType = RigidbodyType2D.Kinematic;
        sceneMover.switchCameraMove(false);
        Camera.main.DOShakePosition(.6f, 1.2f,15,120);
        juiceManager.DoCameraShakeForTrap(Camera.main);
        juiceManager.DoHitFx(playerMovement.GetComponentInChildren<SpriteRenderer>());
        juiceManager.Flash();
        StartCoroutine(RestartLevel());
        OnDeathFX();
    }


    public void OnDeathFX()
    {




    }


    IEnumerator RestartLevel()
    {


        yield return new WaitForSeconds(gameRestartTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);

    }
    private void OnDisable()
    {
        Traps.OnHitTrap-= Traps_OnHitTrap;
    }
    private void Awake()
    {
        if(instance==null) instance = this;

        if (sceneMover == null) sceneMover =
            Camera.main.GetComponent<SceneMover>();
        if (playerMovement == null) playerMovement =
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        if (juiceManager == null)
            juiceManager = gameObject.GetComponent<JuiceManager>();

    }

   
 

   

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetIsPlayerDead()
    {

        return isPlayerDead;    
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
