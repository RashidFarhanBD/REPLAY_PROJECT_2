using System.Collections;
using UnityEngine;

public class DashEffect : MonoBehaviour
{
    [SerializeField] private TrailRenderer dashTrail;
    [SerializeField] float dashtime;


    public void Init(float dashtime)
    {

        this.dashtime = dashtime;
        dashTrail.emitting = false;
    }
    public void Dash()
    {

        Debug.Log("dasggg");
        if (dashTrail != null) 
            dashTrail.emitting = true;   // turn on trail
           
        StartCoroutine(StopDashTrail()); // stop after dash ends
    }

    private IEnumerator StopDashTrail()
    {
        yield return new WaitForSeconds(1); // match your dash duration
        if (dashTrail != null)
            dashTrail.emitting = false;
    }
}
