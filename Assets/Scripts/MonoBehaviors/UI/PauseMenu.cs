using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button HomeButton;
    public Button ResumeButton;
    public Button QuitButton;

    private void Start()
    {
        HomeButton.onClick.AddListener(OnHomeButtonClick);
        ResumeButton.onClick.AddListener(OnResumeButtonClick);
        QuitButton.onClick.AddListener(OnQuitButtonClick);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    void OnHomeButtonClick()
    {
        SoundManager.Instance.PlayEffects("Click");
        GameManager.Instance.LoadScene("Boot");
        GameManager.Instance.TogglePause();
        Hide();
    }
    void OnResumeButtonClick()
    {
        SoundManager.Instance.PlayEffects("Click");
        GameManager.Instance.TogglePause();
        Hide();
    }
    void OnQuitButtonClick()
    {
        SoundManager.Instance.PlayEffects("Click");
        Application.Quit();
        GameManager.Instance.TogglePause();
        Hide();
    }
}
