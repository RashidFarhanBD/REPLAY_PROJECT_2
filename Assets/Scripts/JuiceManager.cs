using UnityEngine;
using DG.Tweening;
using UnityEditor.Rendering;
using UnityEngine.UI;
public class JuiceManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

   [SerializeField] Material hitfxMat;

    [SerializeField] private Image flashImage;

    public void Flash(float duration = 0.3f)
    {
        flashImage.color = new Color(1, 1, 1, 0); // transparent
        
        flashImage.DOFade(1f, duration * 0.5f)    // fade in
            .OnComplete(() =>
            {
                flashImage.DOFade(0f, duration * 0.5f); // fade out
            }).SetDelay(.5f);
    }
    public void DoCameraShakeForTrap(Camera cam)
    {

        Camera.main.DOShakePosition(.6f, 1.2f, 15, 120);

    }

    public void DoHitFx(SpriteRenderer renderer)
    {
        var temp = renderer.material;
        renderer.material = hitfxMat;
        renderer.DOColor(Color.red, .5f);
        renderer.transform.DOShakeRotation(1.5f, 1, 15, 90);
        Flash();
        
    }
}


public class ShakeData
{
   public float duration;
    public float strenght;
    public int vibrato;
    public float randomness;




}
