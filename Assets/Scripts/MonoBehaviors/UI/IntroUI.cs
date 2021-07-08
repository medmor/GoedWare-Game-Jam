using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroUI : MonoBehaviour
{
    public Button QuitButton;
    public GridLayoutGroup LevelsContainer;
    public GameObject LevelButtonPref;
    public GameObject StarPref;
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
        InitLevelsButtons();
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
    private void InitLevelsButtons()
    {
        for (var i = 0; i < LevelsContainer.transform.childCount; i++)
        {
            Destroy(LevelsContainer.transform.GetChild(i).gameObject);
        }
        for (var i = 1; i <= GameManager.Instance.AllLevelsNumber; i++)
        {
            var levelButtonObject = Instantiate(LevelButtonPref);
            levelButtonObject.transform.SetParent(LevelsContainer.transform);


            var text = levelButtonObject.GetComponentInChildren<TextMeshProUGUI>();
            text.text = ". " + i + " -";
            levelButtonObject.transform.localScale = Vector3.one;

            var isLevelLocked = GameManager.Instance.IsLevelLocked(i);
            var button = levelButtonObject.GetComponent<Button>();
            levelButtonObject.transform.GetChild(1).GetComponent<Image>().enabled = isLevelLocked;
            if (isLevelLocked)
            {
                button.enabled = false;
            }
            else
            {
                var levelNumber = int.Parse(text.text.Substring(2, 1));

                var starsContainerTransform = levelButtonObject.transform.GetChild(2);

                for (var j = 0; j < starsContainerTransform.childCount; j++)
                    Destroy(starsContainerTransform.GetChild(j).gameObject);

                for (var j = 0; j < GameManager.Instance.GetLevelStarsProgress(levelNumber); j++)
                {
                    var star = Instantiate(StarPref).transform;
                    star.SetParent(starsContainerTransform);
                    star.transform.localScale = Vector3.one;
                }
                button.onClick.AddListener(() =>
                {
                    OnLevelButtonClick(levelNumber);
                });
            }
        }
    }
}
