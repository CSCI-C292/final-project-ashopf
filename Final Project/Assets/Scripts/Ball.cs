using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        GameManager gM = GameObject.FindObjectOfType<GameManager>();
        if(!gM.roundActive){
            returnToStartPos();
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        GameManager gM = GameObject.FindObjectOfType<GameManager>();
        if(other.tag == "LeftGoal"){
            gM.IncreaseScore("right");
        }
        if(other.tag == "RightGoal"){
            gM.IncreaseScore("left");
        }
    }

    public void returnToStartPos(){
        transform.position = startPos;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
}
