using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Manager<GameManager>
{
    public GameObject[] SystemPrefabs;
    internal readonly int AllLevelsNumber = 4;
    internal int LevelNumber { get; set; }
    internal MAINGAMESTATES MainGameState = MAINGAMESTATES.RUNNING;

    internal VoidEvent PlayerDetected { get; private set; } = new VoidEvent();
    internal VoidEvent JewelryFound { get; private set; } = new VoidEvent();



    void Start()
    {
        InstantiateSystemPrefabs();
        InitProgress();
    }
    void InstantiateSystemPrefabs()
    {
        for (int i = 0; i < SystemPrefabs.Length; ++i)
        {
            Instantiate(SystemPrefabs[i]);
        }
    }
    public void LoadScene(string sceneName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to load level " + sceneName);
            return;
        }

        ao.completed += OnLoadOperationComplete;
    }
    void OnLoadOperationComplete(AsyncOperation ao)
    {
        var sceneName = SceneManager.GetActiveScene().name;
        SoundManager.Instance.StopMusicAudioSource();
        SoundManager.Instance.StopEffectsAudioSource();

        if (sceneName == "Boot")
        {
            UIManager.Instance.TopBar.Hide();
            UIManager.Instance.Controls.Hide();
            UIManager.Instance.IntroUI.Show();
        }
        else if (sceneName == "Main")
        {
            UIManager.Instance.TopBar.Show();
            UIManager.Instance.TopBar.StartTimer();
            if (Platform.IsMobileBrowser())
            {
                UIManager.Instance.Controls.Show();
            }
            //things are gettin messy like always
            //on webgl build, the WinMenu dont hide after next button clicked
            if (UIManager.Instance.WinMenu.gameObject.activeSelf)
                UIManager.Instance.WinMenu.Hide();
            SoundManager.Instance.PlayMusic(0);

            //Destroy any exsting level
            var currentLevel = GameObject.Find(LevelNumber.ToString());
            if (currentLevel)
                Destroy(currentLevel);

            //Instantiate current level and confine the camera
            var level = Instantiate(Resources.Load("Levels/" + LevelNumber)) as GameObject;
            var vCam = Camera.main.transform.GetChild(0);
            vCam.GetComponent<CinemachineConfiner>().m_BoundingShape2D
                = level.transform.Find("CamConfiner").GetComponent<PolygonCollider2D>();

            //Instantiate the player
            var player = Instantiate(Resources.Load("Player")) as GameObject;
            player.transform.position = GameObject.Find(LevelNumber + "(Clone)/StartPos").transform.position;

            //Set cam to follow player
            vCam.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
            //vCam.GetComponent<CinemachineVirtualCamera>().LookAt = player.transform;

        }
    }
    public void TogglePause()
    {
        if (MainGameState == MAINGAMESTATES.RUNNING)
        {
            MainGameState = MAINGAMESTATES.PAUSED;
            SoundManager.Instance.TogglePauseMusic();
            Time.timeScale = 0;
        }
        else
        {
            MainGameState = MAINGAMESTATES.RUNNING;
            SoundManager.Instance.TogglePauseMusic();
            Time.timeScale = 1;
        }
    }

    #region Progress logic
    private string playerLevelProgressString = "ExtremeLevelProgress";
    private int playerLevelProgress = 1;
    private string playerStarsProgressString = "ExtremeStarsProgress";
    private string[] playerStarsProgress;

    void InitProgress()
    {
        if (PlayerPrefs.HasKey(playerLevelProgressString))
            playerLevelProgress = PlayerPrefs.GetInt(playerLevelProgressString);
        if (PlayerPrefs.HasKey(playerStarsProgressString))
            playerStarsProgress = PlayerPrefs.GetString(playerStarsProgressString).Split(',');
        else
        {
            playerStarsProgress = new string[playerLevelProgress];
            for (var i = 0; i < playerLevelProgress; i++)
                playerStarsProgress[i] = "0";
        }
    }
    public int GetLevelProgress()
    {
        return playerLevelProgress;
    }
    public void SetLevelProgress()
    {

        var nextLevel = LevelNumber + 1;
        if (nextLevel > playerLevelProgress && nextLevel <= AllLevelsNumber)
        {
            playerLevelProgress = nextLevel;
            PlayerPrefs.SetInt(playerLevelProgressString, nextLevel);
            PlayerPrefs.Save();
        }
    }
    public bool IsLevelLocked(int level)
    {
        return level > playerLevelProgress;
    }
    public void SetLevelStarsProgress(int stars)
    {
        if (stars > GetLevelStarsProgress(LevelNumber))
        {
            playerStarsProgress[LevelNumber - 1] = stars.ToString();
            PlayerPrefs.SetString(playerStarsProgressString, string.Join(",", playerStarsProgress));
            PlayerPrefs.Save();
        }
    }
    public int GetLevelStarsProgress(int level)
    {
        return int.Parse(playerStarsProgress[level - 1]);
    }
    #endregion

    public enum MAINGAMESTATES
    {
        RUNNING, PAUSED
    }
}

[System.Serializable] public class VoidEvent : UnityEvent { }