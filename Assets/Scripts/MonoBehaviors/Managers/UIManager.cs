public class UIManager : Manager<UIManager>
{
    public IntroUI IntroUI;
    public TopBar TopBar;
    public PauseMenu PauseMenu;
    public WinMenu WinMenu;
    public LoseMenu LoseMenu;
    public Controls Controls;
    private void Start()
    {
        IntroUI.Show();
    }
}
