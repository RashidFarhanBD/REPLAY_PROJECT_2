using UnityEngine;

public class SceneMover : MonoBehaviour
{


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

        return Mathf.Lerp(,targetScrollSpeed,t);

    }
}
