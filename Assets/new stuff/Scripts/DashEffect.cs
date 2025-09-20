using System.Collections;
using UnityEngine;

public class DashEffect : MonoBehaviour
{
    [SerializeField] private TrailRenderer dashTrail;
    [SerializeField] private TrailRenderer dashTrail2;
    [SerializeField] private TrailRenderer dashTrail3;
    [SerializeField] float dashtime;


    public void Init(float dashtime)
    {

        this.dashtime = dashtime;
        dashTrail.emitting = false;
        dashTrail2.emitting = false;
        dashTrail3.emitting = false;
    }
    public void Dash()
    {

        if (dashTrail != null) 
            dashTrail.emitting = true;   // turn on trail

        if (dashTrail2 != null)
            dashTrail2.emitting = true;
        if (dashTrail3 != null)
            dashTrail3.emitting = true;
        StartCoroutine(StopDashTrail()); // stop after dash ends
    }

    private IEnumerator StopDashTrail()
    {
        yield return new WaitForSeconds(.15f); // match your dash duration
        if (dashTrail != null)
            dashTrail.emitting = false;
        if (dashTrail2 != null)
            dashTrail2.emitting = false;
        if (dashTrail3 != null)
            dashTrail3.emitting = false;
    }
}
