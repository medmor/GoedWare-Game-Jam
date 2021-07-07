using UnityEngine;
using UnityEngine.UI;

public class IntroUI : MonoBehaviour
{
    public Button QuitButton;
    private void Start()
    {
        QuitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void OnLevelButtonClick(int levelNumber)
    {
        SoundManager.Instance.PlayEffects("Click");
        GameManager.Instance.LevelNumber = levelNumber;
        GameManager.Instance.LoadScene("Main");
        Hide();
    }
}
