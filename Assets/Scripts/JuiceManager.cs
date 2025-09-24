using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using System;
public class JuiceManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

   [SerializeField] Material hitfxMat;

    [SerializeField] private Image flashImage;




    [SerializeField] private Image fadeImage; // full-screen white image
    [SerializeField] private float fadeDuration = 0.5f;

    [Header("Jump")]

    
    [Range(0f,3f)]
    public float shakeTime=.2f;
    [Range(0f, 5f)]
    public float shakeStrenght=1;
    [Range(0, 10)]
    public int  shakeVibration=1;
    [Range(0f, 90f)]
    public float  shakeRandomness=15;
    //(.2f, 1, 1, 15
    [SerializeField]
    private void Start()
    {
        // Start fully transparent
        fadeImage.color = new Color(1, 1, 1, 0);
    }

    public void FadeToWhite()
    {
        fadeImage.DOFade(1f, fadeDuration); // alpha → 1 (white)
    }

    public void FadeFromWhite()
    {
        fadeImage.DOFade(0f, fadeDuration); // alpha → 0 (clear)
    }

    public void FlashWhite(float holdTime = 0.2f)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(fadeImage.DOFade(1f, fadeDuration / 2f));
        seq.AppendInterval(holdTime);
        seq.Append(fadeImage.DOFade(0f, fadeDuration / 2f));
    }
    public void Flash(float duration = 0.3f)
    {
        flashImage.color = new Color(1, 1, 1, 0); // transparent
        
        flashImage.DOFade(1f, duration * 0.5f)    // fade in
            .OnComplete(() =>
            {
                DoCameraShakeForTrap(Camera.main);
                flashImage.DOFade(0f, duration * 0.5f); // fade out
            }).SetDelay(.5f);
    }
    public void DoCameraShakeForTrap(Camera cam)
    {

        Camera.main.DOShakePosition(.6f, 1.2f, 15, 120);

    }
    public void DoCameraShakeForJump(Transform player)
    {
        if (shakeTime <= 0) return;
        Camera.main.DOShakePosition(shakeTime, shakeStrenght, shakeVibration, shakeRandomness);

    }



    public void DoCameraShakeForSnake(Camera cam)
    {

        Camera.main.DOShakePosition(1, 1.2f, 15, 120);

    }

    public void DoHitFx(SpriteRenderer renderer)
    {
        //StartCoroutine(ShowTimeFX());
        var temp = renderer.material;
        renderer.material = hitfxMat;
        renderer.DOColor(Color.red, .5f);
        renderer.transform.DOShakeRotation(1.5f, 1, 15, 90);
        Flash();
        renderer.transform.DOScale(Vector3.zero, 1.5f).SetDelay(1f);
        
    }

    


    IEnumerator ShowTimeFX()
    {
        yield return new WaitForSeconds(1); //2 or .5
        //Debug.Log("time slowed");
        Time.timeScale = .3f;


        yield return new WaitForSeconds(.2f);
        //Debug.Log("time normal");
        Time.timeScale = 1;



    }
}


public class ShakeData
{
   public float duration;
    public float strenght;
    public int vibrato;
    public float randomness;




}
