using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int leftPlayerScore;
    private int rightPlayerScore;
    private int roundTime = 20;
    private float startTime = 0;
    private bool roundActive = false;
    [SerializeField] private TextMeshProUGUI leftScoreText;
    [SerializeField] private TextMeshProUGUI rightScoreText;
    [SerializeField] private TextMeshProUGUI roundTimeText;
    [SerializeField] private TextMeshProUGUI leftWinText;
    [SerializeField] private TextMeshProUGUI rightWinText;
    public int LeftPlayerScore{
        get{
            return leftPlayerScore;
        }
        set{
            
            this.leftPlayerScore = value;
            this.leftScoreText.text = value.ToString();
        }
    }
    public int RightPlayerScore{
        get{
            return rightPlayerScore;
        }
        set{
            
            this.rightPlayerScore = value;
            this.rightScoreText.text = value.ToString();
        }
    }
    
    void Start(){
        leftPlayerScore = 0;
        rightPlayerScore = 0;
        startTime = Time.time;
        setTimeDisplay(roundTime);
        roundActive = true;
    }

    void Update(){
        if(Time.time - startTime < roundTime){
            float timeElapsed = Time.time - startTime;
            setTimeDisplay(roundTime - timeElapsed);
        }
        else{
            setTimeDisplay(0);
            if(LeftPlayerScore > RightPlayerScore){
                leftWinText.enabled = true;
            }else{
                rightWinText.enabled = true;
            }
            roundActive = false;
        }
    }

    private void setTimeDisplay(float timeDisplay){
        roundTimeText.text = roundTimeDisplay(timeDisplay);
    }

    private string roundTimeDisplay(float time){
        int secondsToShow = Mathf.CeilToInt(time);
        int seconds = secondsToShow % 60;
        string secondsDisplay = (seconds < 10) ? "0" + seconds.ToString() : seconds.ToString();
        int minutes = (secondsToShow - seconds)/60;
        return minutes.ToString() + " : " + seconds.ToString();
    }

    public void IncreaseScore(string goal){
        if(roundActive){
            if(goal.Equals("left")){
                LeftPlayerScore++;
            }
            if(goal.Equals("right")){
                RightPlayerScore++;
            }
        }
    }


}
