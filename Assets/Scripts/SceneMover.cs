using UnityEngine;

public class SceneMover : MonoBehaviour
{
    [SerializeField]
    SpeedData leftSideSpeedData;
    [SerializeField]
    SpeedData midSideSpeedData;
    [SerializeField]
    SpeedData rightSideSpeedData;

    public float regularScrollSpeed;


    public float startScrollSpeed;
    public float targetScrollSpeed;

    public bool isLerping=false;
    public float elapsedTimeSinceLerping;
    public float currentScrollSpeed;
    public float maxTimeToLerp;
    
    //   public float 
    bool scrollOn =true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitCamToNewValue(.5f,6);
    }

    private void OnEnable()
    {
        PlayerScreenPOSTracker.OnCameraZoneChanged += PlayerScreenPOSTracker_OnCameraZoneChanged;
    }
    private void OnDisable()
    {
        PlayerScreenPOSTracker.OnCameraZoneChanged -= PlayerScreenPOSTracker_OnCameraZoneChanged;

    }

    private void PlayerScreenPOSTracker_OnCameraZoneChanged(PlayerScreenPOSTracker.camerZone obj)
    {
        switch (obj)
        {
            case PlayerScreenPOSTracker.camerZone.LeftSide:
                InitCamToNewValue(leftSideSpeedData.transitionTime, leftSideSpeedData.speed);
                break;
            case PlayerScreenPOSTracker.camerZone.Middle:
                InitCamToNewValue(midSideSpeedData.transitionTime, midSideSpeedData.speed);
                break;
            case PlayerScreenPOSTracker.camerZone.Right:
                InitCamToNewValue(rightSideSpeedData.transitionTime, rightSideSpeedData.speed);
                break;
            default:
                break;
        }
    }

    public void  switchCameraMove(bool condition)
    {

        scrollOn = condition;   
    }

    private void Update()
    {
        if (isLerping)
        {


            elapsedTimeSinceLerping += Time.deltaTime;

            currentScrollSpeed =  GetCurrentValueFromLerp( GetCamLerpValue());
            if(elapsedTimeSinceLerping>= maxTimeToLerp)
            {
                elapsedTimeSinceLerping = 0;
                isLerping = false;  


            }

        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!scrollOn) return;
        transform.position += Vector3.right * currentScrollSpeed * Time.deltaTime;
    }


    public float InitCamToNewValue( float maxTimeToLerp, float targetScrollSpeed )
    {
        isLerping = true;
        startScrollSpeed = currentScrollSpeed;
        this.maxTimeToLerp = maxTimeToLerp;
        this.targetScrollSpeed = targetScrollSpeed;

        return 0;
    } 

    public float GetCamLerpValue()
    {


        return (float) (elapsedTimeSinceLerping / maxTimeToLerp);
    }

    public float GetCurrentValueFromLerp(float t )
    {
        return Mathf.Lerp(startScrollSpeed,targetScrollSpeed,t);

    }

    [System.Serializable]

    public class SpeedData
    {
        public float speed;
        public float transitionTime;

    }
}
