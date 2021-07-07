using System.Collections;
using UnityEngine;


public class FixedLaser : MonoBehaviour
{
    public float RotationSpeed = 1.5f;
    public LayerMask LaserDetectionLayer;
    public LineRenderer Line;
    public EdgeCollider2D LineCollider;


    private bool playerDetected = false;
    void Start()
    {
        GameManager.Instance.PlayerDetected.AddListener(OnPlayerDetected);
        GameManager.Instance.JewelryFound.AddListener(OnJewelryFound);

        transform.Rotate(new Vector3(0, 0, Random.value * 360));
        LineCollider.offset = -transform.position;
        Line.SetPosition(0, transform.position);

        StartCoroutine(StartDetection());

        if (Random.value < .5) RotationSpeed = -RotationSpeed;
    }
    IEnumerator StartDetection()
    {
        while (!playerDetected)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 100, LaserDetectionLayer);

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
                Line.SetPosition(1, Vector3.zero);
            }

            transform.Rotate(Vector3.forward * RotationSpeed);
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

