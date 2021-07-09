using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GlobalLight : MonoBehaviour
{
    Light2D light2D;

    private float flickerDuration = 5;

    void Start()
    {
        light2D = GetComponent<Light2D>();

        GameManager.Instance.JewelryFound.AddListener(SwitchOn);
        GameManager.Instance.PlayerDetected.AddListener(OnPlayerDetected);
    }
    public void StartFadingOut()
    {
        StartCoroutine(FadeOut());
    }

    void SwitchOn()
    {
        StopAllCoroutines();
        light2D.intensity = 1;
        StartCoroutine(FlickerGreen());
    }
    IEnumerator FadeOut()
    {
        while (light2D.intensity > 0)
        {
            light2D.intensity -= .015f;
            yield return new WaitForSeconds(.05f);
        }
    }
    IEnumerator FlickerGreen()
    {

        light2D.color = Color.green;
        while (flickerDuration > 0)
        {
            if (light2D.intensity > .6)
                light2D.intensity = .5f;
            else
                light2D.intensity = 1;
            flickerDuration -= .1f;
            yield return new WaitForSeconds(.1f);
        }
    }
    void OnPlayerDetected()
    {
        StopAllCoroutines();
        light2D.intensity = .5f;
        light2D.color = Color.red;
    }
}
