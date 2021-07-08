using UnityEngine;

public class MovingObject : MonoBehaviour
{
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.x > 0)
            rb.velocity = Vector2.zero;
        if (rb.velocity.x < 0)
            rb.velocity = Vector2.zero;
        if (rb.velocity.y > 0)
            rb.velocity = Vector2.zero;
        if (rb.velocity.y < 0)
            rb.velocity = Vector2.zero;
    }
}
