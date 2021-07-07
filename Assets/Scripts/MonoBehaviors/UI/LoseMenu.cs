using UnityEngine;
using UnityEngine.UI;

public class LoseMenu : MonoBehaviour
{
    public Button HomeButton;
    public Button ReplayButton;
    void Start()
    {
        HomeButton.onClick.AddListener(OnHomeButtonClick);
        ReplayButton.onClick.AddListener(OnReplayButtonClick);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }

    void OnHomeButtonClick()
    {
        SoundManager.Instance.PlayEffects("Click");
        Hide();
        GameManager.Instance.LoadScene("Boot");
    }
    void OnReplayButtonClick()
    {
        SoundManager.Instance.PlayEffects("Click");
        Hide();
        GameManager.Instance.LoadScene("Main");
    }
}
