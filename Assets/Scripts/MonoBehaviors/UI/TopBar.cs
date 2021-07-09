using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopBar : MonoBehaviour
{
    public Button PauseButton;
    public Button LightsButton;
    public TextMeshProUGUI LightsNumberText;
    public TextMeshProUGUI TimerText;

    private Player player;//needed for lights button; simple way to do it, I think.
    private float neededTime;
    private bool isRed;

    void Start()
    {
        PauseButton.onClick.AddListener(OnPauseButtonClick);
        LightsButton.onClick.AddListener(OnLightsButtonClick);
        GameManager.Instance.PlayerDetected.AddListener(OnGameEnded);
        GameManager.Instance.JewelryFound.AddListener(OnGameEnded);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
        PauseButton.enabled = true;
        LightsButton.enabled = true;
    }
    public void SetLightsNumber(int number)
    {
        LightsNumberText.text = number.ToString();
    }
    public int GetLightsNumber()
    {
        return int.Parse(LightsNumberText.text);
    }
    void OnPauseButtonClick()
    {
        SoundManager.Instance.PlayEffects("Click");

        GameManager.Instance.TogglePause();
        UIManager.Instance.PauseMenu.Show();
    }
    void OnLightsButtonClick()
    {

        if (player == null && GameObject.Find("Player(Clone)"))
            player = GameObject.Find("Player(Clone)").GetComponent<Player>();
        if (player)
        {
            SoundManager.Instance.PlayEffects("Click");
            player.StartLight();
        }
    }
    public bool IsTimeDone()
    {
        return neededTime <= 0;
    }
    public void StartTimer()
    {
        neededTime = 180;
        isRed = false;
        StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        while (neededTime >= 0)
        {
            System.TimeSpan t = System.TimeSpan.FromSeconds(neededTime);

            string answer = string.Format("{0:D2}.{1:D2}",
                            t.Minutes,
                            t.Seconds);
            neededTime -= 1f;
            TimerText.text = answer;
            if (neededTime < 20 && !isRed)
            {
                isRed = true;
                TimerText.color = Color.red;
            }
            yield return new WaitForSeconds(1f);
        }
    }
    void OnGameEnded()
    {
        PauseButton.enabled = false;
        LightsButton.enabled = false;
        StopAllCoroutines();
    }
}
