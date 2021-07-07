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

        StartCoroutine(FadeOut());

        GameManager.Instance.JewelryFound.AddListener(SwitchOn);
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
            light2D.intensity -= .01f;
            yield return new WaitForSeconds(.05f);
        }
    }
    IEnumerator FlickerGreen()
    {

        var dr = flickerDuration / 2;
        light2D.color = Color.green;
        while (flickerDuration > 0)
        {
            if (light2D.intensity > .6)
                light2D.intensity = .5f;
            else
                light2D.intensity = 1;
            flickerDuration -= .1f;
            if (flickerDuration < dr)
            {
                int stars = 3;
                if (UIManager.Instance.TopBar.IsTimeDone())
                    stars--;
                if (UIManager.Instance.TopBar.GetLightsNumber() == 0)
                    stars--;
                UIManager.Instance.WinMenu.Show(stars);
            }
            yield return new WaitForSeconds(.1f);
        }
    }
}
