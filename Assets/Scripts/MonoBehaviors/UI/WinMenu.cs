using UnityEngine;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{
    public Button NextButton;
    public Button ReplayButton;
    public Button HomeButton;

    public GridLayoutGroup StarsContainer;
    public GameObject StarPref;


    private void Start()
    {
        HomeButton.onClick.AddListener(OnHomeButtonClick);
        ReplayButton.onClick.AddListener(OnReplayButtonClick);
        NextButton.onClick.AddListener(OnNextButtonClick);
    }
    public void Show(int stars)
    {
        for (var i = 0; i < StarsContainer.transform.childCount; i++)
        {
            Destroy(StarsContainer.transform.GetChild(i).gameObject);
        }
        for (var i = 0; i < stars; i++)
        {
            var star = Instantiate(StarPref);
            star.transform.SetParent(StarsContainer.transform);
            star.transform.localScale = Vector3.one;
        }
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
        Hide();
    }
    void OnNextButtonClick()
    {
        SoundManager.Instance.PlayEffects("Click");

        GameManager.Instance.LevelNumber++;

        GameManager.Instance.LoadScene("Main");
        Hide();
    }
    void OnReplayButtonClick()
    {
        SoundManager.Instance.PlayEffects("Click");

        GameManager.Instance.LoadScene("Main");
        Hide();
    }


}
