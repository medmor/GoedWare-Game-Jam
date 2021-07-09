using UnityEngine;

public class IntroCamTimeLine : MonoBehaviour
{
    public GlobalLight GlobalLight;
    public void OnCamIntroEnded()
    {
        gameObject.SetActive(false);
        GameManager.Instance.SpawnPlayer();
        GlobalLight.StartFadingOut();
    }
}
