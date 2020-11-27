using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCar : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float accelerateForce;
    [SerializeField] private float turningForce;
    [SerializeField] private float boostAccelerateForce;
    public float AccelerateForce{
        get{
            return accelerateForce;
        }
    }
    private float startAccelerateForce; 
    private float speed;
    private float turningAmount;
    private float direction;
    private Quaternion carStartDirection;
    private bool isCarBoosting;
    private float carBoostLevel;
    private float carBoostRate = 5;

    private Vector3 startPos;

    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        carStartDirection = transform.rotation;
        carBoostLevel = 0;
        isCarBoosting = false;
        startAccelerateForce = AccelerateForce;
    }
    public void FixedUpdate()
    {
        carController();
    }

    public void Update(){
        carBoost();
    }

    private void carController(){
        GameManager gM = GameObject.FindObjectOfType<GameManager>();
        if(gM.roundActive){
            turningAmount = - Input.GetAxis("2nd Player Horizontal");
            speed = Input.GetAxis("2nd Player Vertical") * accelerateForce;
            direction = Mathf.Sign(Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up)));
            rb.rotation += turningAmount * turningForce * rb.velocity.magnitude * direction;
            rb.AddRelativeForce(Vector2.up * speed);
            rb.AddRelativeForce(- Vector2.right * rb.velocity.magnitude * turningAmount / 2);
        }
    }

    private void carBoost(){
        if(Input.GetKey("left shift") && carBoostLevel > 0){
            isCarBoosting = true;
            if(isCarBoosting == true){
                accelerateForce = boostAccelerateForce;
                carBoostLevel -= carBoostRate * Time.deltaTime;
                Debug.Log(carBoostLevel);
            }
        }else{
            isCarBoosting = false;
            accelerateForce = startAccelerateForce;
        }
        if(carBoostLevel <= 0){
            carBoostLevel = 0;
            isCarBoosting = false;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other){
        GameManager gM = GameObject.FindObjectOfType<GameManager>();
        if(other.tag == "Boost"){
            carBoostLevel = 100;
        }
    }


    public void returnToStartPos(){
        transform.position = startPos;
        transform.rotation = carStartDirection;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        isCarBoosting = false;
        carBoostLevel = 0;
    }
}
