using UnityEngine;
using UnityEngine.AI;
using System;
public class PlayerScreenPOSTracker : MonoBehaviour
{
    public static event Action<camerZone> OnCameraZoneChanged;
    [SerializeField]
    camerZone oldZone;
    [SerializeField]
    camerZone targetZone;
    

    [Header("CameraZone")]

    [SerializeField]
    [Range(.1f, .3f)]
    float LeftZoneLimitChecker = .3f;
    [SerializeField]
    [Range(.5f,.8f)]
    float MiddleZoneLimitChecker=.75f;
     


    [Header("TrackerTarget")]

    public Transform target;
    [Header("Cam")]
    public Camera cam;

    public camerZone TargetZone { get  => targetZone; set => targetZone = value; }

    public enum camerZone
    {
        LeftSide,
        Middle,
        Right,


    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(cam==null)
        cam = Camera.main;
        if (target == null) target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;
       var  getPlayerinScreen = cam.WorldToViewportPoint(target.position);
       // Debug.Log(getPlayerinScreen.x);

        if(getPlayerinScreen.x<= LeftZoneLimitChecker)
        {

            targetZone = camerZone.LeftSide;
        }
        else if(getPlayerinScreen.x<= MiddleZoneLimitChecker) 
        {


            targetZone = camerZone.Middle;

        }

        else
        {
            targetZone = camerZone.Right;



        }
        if(oldZone!= targetZone)
        {

            Debug.Log("zone changed");
            OnCameraZoneChanged?.Invoke(targetZone);
            oldZone = targetZone;
        }
        
       
    }

    private void OnDrawGizmos()
    {
        if (target == null|| cam==null) return;

        Gizmos.color = Color.red; 
      //  Gizmos.DrawLine()
        
    }
}
