using UnityEngine;

public class Jewelry : MonoBehaviour
{
    public float DetectionRadius = 1;
    private void Start()
    {
        GetComponent<CircleCollider2D>().radius = DetectionRadius;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.JewelryFound.Invoke();
            transform.position = collision.transform.position + Vector3.down * .25f;

        }
    }


}
