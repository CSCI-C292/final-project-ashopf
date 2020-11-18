using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCarMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float accelerateForce;
    [SerializeField] private float turningForce;
    private float speed;
    private float turningAmount;
    private float direction;
    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        carController();
    }

    private void carController(){
        turningAmount = - Input.GetAxis("2nd Player Horizontal");
        speed = Input.GetAxis("2nd Player Vertical") * accelerateForce;
        direction = Mathf.Sign(Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up)));
        rb.rotation += turningAmount * turningForce * rb.velocity.magnitude * direction;
        rb.AddRelativeForce(Vector2.up * speed);
        rb.AddRelativeForce(- Vector2.right * rb.velocity.magnitude * turningAmount / 2);
    }
}
