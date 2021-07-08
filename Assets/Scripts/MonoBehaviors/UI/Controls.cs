using UnityEngine;

public class Controls : MonoBehaviour
{
    internal float HorizontalAxis = 0;
    internal float VerticalAxis = 0;
    internal void Show()
    {
        gameObject.SetActive(true);
    }
    internal void Hide()
    {
        gameObject.SetActive(false);
    }

}
