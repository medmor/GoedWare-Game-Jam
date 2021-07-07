using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform Light;
    public float movementSpeed = 3.0f;
    Vector2 movement = new Vector2();
    Rigidbody2D rb2D;
    Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        UpdateState();
    }
    void FixedUpdate()
    {
        MoveCharacter();
    }
    private void MoveCharacter()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();
        rb2D.velocity = movement * movementSpeed;
        SetLightRotatoin(animator.GetFloat("XDir"), animator.GetFloat("YDir"));

    }
    private void UpdateState()
    {

        if (Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0))
        {
            animator.SetBool("IsWalking", false);
        }
        else
        {
            animator.SetBool("IsWalking", true);
            animator.SetFloat("XDir", movement.x);
            animator.SetFloat("YDir", movement.y);
        }
    }
    void SetLightRotatoin(float x, float y)
    {

        if (x == 0 && y == 0)
            Light.eulerAngles = new Vector3(0, 0, 180);
        else if (x > 0 && y == 0)
            Light.eulerAngles = new Vector3(0, 0, 270);
        else if (x > 0 && y > 0)
            Light.eulerAngles = new Vector3(0, 0, 315);
        else if (x == 0 && y > 0)
            Light.eulerAngles = new Vector3(0, 0, 0);
        else if (x < 0 && y > 0)
            Light.eulerAngles = new Vector3(0, 0, 45);
        else if (x < 0 && y == 0)
            Light.eulerAngles = new Vector3(0, 0, 90);
        else if (x < 0 && y < 0)
            Light.eulerAngles = new Vector3(0, 0, 135);
        else if (x == 0 && y < 0)
            Light.eulerAngles = new Vector3(0, 0, 180);
        else if (x < 0 && y > 0)
            Light.eulerAngles = new Vector3(0, 0, 225);
    }

}
