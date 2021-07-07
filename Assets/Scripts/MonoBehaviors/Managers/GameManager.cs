using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Manager<GameManager>
{
    public GameObject[] SystemPrefabs;
    internal int LevelNumber { get; set; }
    internal MAINGAMESTATES MainGameState = MAINGAMESTATES.RUNNING;

    internal VoidEvent PlayerDetected { get; private set; } = new VoidEvent();
    internal VoidEvent JewelryFound { get; private set; } = new VoidEvent();


    void Start()
    {
        InstantiateSystemPrefabs();

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
            UIManager.Instance.IntroUI.Show();
        }
        else if (sceneName == "Main")
        {
            UIManager.Instance.TopBar.Show();
            UIManager.Instance.TopBar.StartTimer();
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

    public enum MAINGAMESTATES
    {
        RUNNING, PAUSED
    }
}

[System.Serializable] public class VoidEvent : UnityEvent { }