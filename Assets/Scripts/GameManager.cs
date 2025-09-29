using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerDeathEffect fx;  
    public  float snakeMoveDuration = 2f;
    public static GameManager instance;
    public PlayerMovement playerMovement;
    public SceneMover sceneMover;
    bool isGameOn;
    bool isGameOver;
    private bool isPlayerDead;
    public JuiceManager juiceManager;
    [Header("Designer look here")]
    [Tooltip ("time to restart after player dies")]
    public float gameRestartTime = 2f;

    [Header("Snake stuff")]
    Vector2 snakeStartingPos = new Vector3 (-20,0,20);
   
    [SerializeField]Vector3 snakeOffPos = new Vector3 (-35,0,17.5f);
   
    public GameObject snakeObject;
    public float snakeDelayTime;
    private Sequence snakeSeq;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject UI;
    IEnumerator InitSnake()
    {
        snakeSeq?.Kill();
        snakeSeq = DOTween.Sequence();

        SoundManager.Instance.PlayBGM(SoundManager.Instance.bgmCLip,.2f,true);
        yield return new WaitForSeconds(snakeDelayTime);
       // SoundManager.Instance.PauseBGM();


        
        snakeObject.transform.DOLocalMoveX(snakeStartingPos.x, snakeMoveDuration).OnComplete(() =>
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.rockbreak);
            // juiceManager.DoCameraShakeForSnake(Camera.main);
            juiceManager.Flash(.3f);
           // SoundManager.Instance.ResumeBGM();

        }
        );



    }

    public void InitUI()
    {
        UI.SetActive(true);
    }
    public void StopUI()
    {

        UI.SetActive(false);


    }

    public void DoMildShakeCam()
    {
        juiceManager.DoMildCamShake();
    }

    public void SHakeFromLand()
    {

      //  juiceManager.DoCameraShakeForJump(transform);

    }
    public void ShakeFromLand2(float t,float i)
    {
        juiceManager.DoCameraShakeForJump(t,i);

    }

    public void Start()
    {

        Application.targetFrameRate = 60; // Lock to 60 FPS
        QualitySettings.vSyncCount = 0;   // Make sure VSync doesn’t override
        InitUI();
        //StartCoroutine(InitSnake());    
    }


    private void OnEnable()
    {
        Traps.OnHitTrap += Traps_OnHitTrap;
        Snake.OnSnakeHit += Snake_OnSnakeHit;
        GameStarter.OnStartPressed += GameStarter_OnStartPressed;
    }

    private void GameStarter_OnStartPressed()
    {
        isGameOn = true;
        StopUI();

        Camera.main.DOShakePosition(1, new Vector3(0, .88F, 0)).SetEase(Ease.InFlash);
        StartCoroutine(InitSnake());

    }




    private void Snake_OnSnakeHit()
    {
        if (isPlayerDead) return;
        SoundManager.Instance.StopBGM(0);

        //stop player, stop camera 
        isPlayerDead = true;
        playerMovement.enabled = false;

        playerMovement.RB.linearVelocity = Vector2.zero;
        playerMovement.RB.bodyType = RigidbodyType2D.Kinematic;
        sceneMover.switchCameraMove(false);
        // fx.Die();
        SoundManager.Instance.PlayDeath();
        juiceManager.DoCameraShakeForTrap(Camera.main);
        juiceManager.DoHitFx(playerMovement.GetComponentInChildren<SpriteRenderer>());
        
        StartCoroutine(RestartLevel());
    }



    private void Traps_OnHitTrap(Traps obj)
    {
        if (isPlayerDead) return;
        SoundManager.Instance.StopBGM(0);

        //stop player, stop camera 
        isPlayerDead = true;
        playerMovement.enabled = false;

        playerMovement.RB.linearVelocity = Vector2.zero;
        playerMovement.RB.bodyType = RigidbodyType2D.Kinematic;
        sceneMover.switchCameraMove(false);
        Camera.main.DOShakePosition(.6f, 1.2f,15,120);
        juiceManager.DoCameraShakeForTrap(Camera.main);
        juiceManager.DoHitFx(playerMovement.GetComponentInChildren<SpriteRenderer>());
        SoundManager.Instance.PlayDeath();
        StartCoroutine(RestartLevel());
    }


    IEnumerator RestartLevel()
    {
       
      

        yield return new WaitForSeconds(gameRestartTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);

    }
    private void OnDisable()
    {
        Traps.OnHitTrap-= Traps_OnHitTrap;
        Snake.OnSnakeHit -= Snake_OnSnakeHit;
        GameStarter.OnStartPressed -= GameStarter_OnStartPressed;

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
        if (snakeObject == null)
            snakeObject = GameObject.FindGameObjectWithTag("Snake");
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

    internal void GAMEWIN()
    {
        if (!isPlayerDead)
        {

            snakeObject.SetActive(false);
            playerMovement.enabled = false;
            isGameOver = true;
            playerMovement.RB.linearVelocity = Vector2.zero;
            playerMovement.RB.bodyType = RigidbodyType2D.Kinematic;
            sceneMover.switchCameraMove(false);
            juiceManager.FadeToWhite();
            SoundManager.Instance.StopBGM();    
           StartCoroutine( RestartLevel());
        }
    }
}
