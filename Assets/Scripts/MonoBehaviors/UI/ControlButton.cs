using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public DIRECTIONS Direction;
    private Image img;
    private Color originalColor;
    private void Start()
    {
        img = GetComponent<Image>();
        originalColor = img.color;

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        img.color = Color.gray;
        switch (Direction)
        {
            case DIRECTIONS.RIGHT:
                UIManager.Instance.Controls.HorizontalAxis = 1;
                break;
            case DIRECTIONS.DOWN:
                UIManager.Instance.Controls.VerticalAxis = -1;
                break;
            case DIRECTIONS.LEFT:
                UIManager.Instance.Controls.HorizontalAxis = -1;
                break;
            case DIRECTIONS.UP:
                UIManager.Instance.Controls.VerticalAxis = 1;
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        img.color = originalColor;
        switch (Direction)
        {
            case DIRECTIONS.RIGHT:
                UIManager.Instance.Controls.HorizontalAxis = 0;
                break;
            case DIRECTIONS.DOWN:
                UIManager.Instance.Controls.VerticalAxis = 0;
                break;
            case DIRECTIONS.LEFT:
                UIManager.Instance.Controls.HorizontalAxis = 0;
                break;
            case DIRECTIONS.UP:
                UIManager.Instance.Controls.VerticalAxis = 0;
                break;
        }
    }
    public enum DIRECTIONS { RIGHT, DOWN, LEFT, UP }
}

