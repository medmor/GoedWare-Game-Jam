using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Player : MonoBehaviour
{

    public Light2D light2D;

    private int lightsNumber = 3;
    private bool gameEnded = false;
    void Start()
    {
        GameManager.Instance.PlayerDetected.AddListener(OnGameEnded);

        GameManager.Instance.JewelryFound.AddListener(OnGameEnded);

        UIManager.Instance.TopBar.SetLightsNumber(lightsNumber);
    }

    void OnGameEnded()
    {
        gameEnded = true;
        StopAllCoroutines();
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<PlayerController>().enabled = false;
        var animator = GetComponent<Animator>();
        animator.SetBool("IsWalking", false);
        animator.SetFloat("XDir", 0);
        animator.SetFloat("YDir", 0);
        light2D.intensity = 0;
    }

    public void StartLight()
    {
        if (gameEnded || light2D.intensity > .1f) return;
        if (lightsNumber > 0)
        {
            light2D.intensity = 1;
            StartCoroutine(FadeOut());
            lightsNumber--;
            UIManager.Instance.TopBar.SetLightsNumber(lightsNumber);
        }
    }

    IEnumerator FadeOut()
    {
        while (light2D.intensity > 0)
        {
            light2D.intensity -= .005f;
            yield return new WaitForSeconds(.05f);
        }
    }
}
