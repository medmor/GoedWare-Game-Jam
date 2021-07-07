using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class Alarm : MonoBehaviour
{
    public List<Light2D> lights;

    private float alarmDuration = 20;

    void Start()
    {
        GameManager.Instance.PlayerDetected.AddListener(OnPlayerDetected);

    }
    void OnPlayerDetected()
    {
        foreach (var light in lights)
        {
            light.intensity = 1;
            light.transform.Rotate(Vector3.forward * Random.value * 360);
            StartCoroutine(StartAlarm());
        }
    }

    IEnumerator StartAlarm()
    {

        UIManager.Instance.LoseMenu.Show();
        while (alarmDuration > 0)
        {
            foreach (var light in lights)
                light.transform.Rotate(Vector3.forward * 4);
            alarmDuration -= .01f;
            yield return new WaitForSeconds(.01f);
        }
        UIManager.Instance.LoseMenu.Hide();
        GameManager.Instance.LoadScene("Boot");
    }
}
