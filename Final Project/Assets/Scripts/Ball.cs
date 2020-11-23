using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other){
        GameManager gM = GameObject.FindObjectOfType<GameManager>();
        if(other.tag == "LeftGoal"){
            gM.IncreaseScore("left");
        }
        if(other.tag == "RightGoal"){
            gM.IncreaseScore("right");
        }
    }
}
