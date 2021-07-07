using System.Collections;
using UnityEngine;

public class LaserBetween : MonoBehaviour
{
    public Transform PosTwo;
    public LayerMask LaserDetectionLayer;
    public LineRenderer Line;
    public EdgeCollider2D LineCollider;


    private bool playerDetected = false;
    void Start()
    {
        GameManager.Instance.PlayerDetected.AddListener(OnPlayerDetected);
        GameManager.Instance.JewelryFound.AddListener(OnJewelryFound);

        LineCollider.offset = -transform.position;
        Line.SetPosition(0, transform.position);
        Line.SetPosition(1, PosTwo.position);
        LineCollider.points = new Vector2[] {
                Line.GetPosition(0),
                Line.GetPosition(1)
            };

        StartCoroutine(StartDetection());

    }
    IEnumerator StartDetection()
    {
        while (!playerDetected)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, PosTwo.position - transform.position,
                Vector2.Distance(transform.position, PosTwo.position), LaserDetectionLayer);

            if (hit)
            {
                Line.SetPosition(1, hit.point);
                LineCollider.points = new Vector2[] {
                Line.GetPosition(0),
                Line.GetPosition(1)
            };
                if (!playerDetected && hit.collider.gameObject.CompareTag("Player"))
                {
                    playerDetected = true;
                    GameManager.Instance.PlayerDetected.Invoke();
                }
            }
            else
            {
                Line.SetPosition(1, PosTwo.position);
                LineCollider.points = new Vector2[] {
                Line.GetPosition(0),
                Line.GetPosition(1)
            };
            }
            yield return new WaitForFixedUpdate();
        }

    }
    void OnPlayerDetected()
    {
        StopAllCoroutines();
    }
    void OnJewelryFound()
    {
        Line.gameObject.SetActive(false);
        LineCollider.gameObject.SetActive(false);
        StopAllCoroutines();
    }
}
