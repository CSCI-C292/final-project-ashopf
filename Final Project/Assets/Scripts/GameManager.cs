using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int leftPlayerScore;
    private int rightPlayerScore;
    [SerializeField] private int roundTime;
    private float startTime = 0;
    public bool roundActive = false;
    [SerializeField] private TextMeshProUGUI leftScoreText;
    [SerializeField] private TextMeshProUGUI rightScoreText;
    [SerializeField] private TextMeshProUGUI roundTimeText;
    [SerializeField] private GameObject leftWinText;
    [SerializeField] private GameObject rightWinText;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject tiedGameText;
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
        leftWinText.SetActive(false);
        rightWinText.SetActive(false);
        restartButton.SetActive(false);
        tiedGameText.SetActive(false);

    }

    void Update(){
        if(Time.time - startTime < roundTime){
            float timeElapsed = Time.time - startTime;
            setTimeDisplay(roundTime - timeElapsed);
        }
        else{
            setTimeDisplay(0);
            if(LeftPlayerScore > RightPlayerScore){
                leftWinText.SetActive(true);
            }
            if(RightPlayerScore > LeftPlayerScore){
                rightWinText.SetActive(true);
            }
            if(LeftPlayerScore == RightPlayerScore){
                tiedGameText.SetActive(true);
            }
            roundActive = false;
            restartButton.SetActive(true);
        }
    }

    private void setTimeDisplay(float timeDisplay){
        roundTimeText.text = roundTimeDisplay(timeDisplay);
    }

    private string roundTimeDisplay(float time){
        int secondsToShow = Mathf.CeilToInt(time);
        int seconds = secondsToShow % 60;
        string secondsDisplay = "0";
        if(seconds < 10){
            secondsDisplay = "0" + seconds.ToString();
        }else{
             secondsDisplay = seconds.ToString();
        }
        int minutes = (secondsToShow - seconds)/60;
        return minutes.ToString() + " : " + secondsDisplay;
    }

    public void IncreaseScore(string goal){
        BlueCar blueCar = GameObject.FindObjectOfType<BlueCar>();
        RedCar redCar = GameObject.FindObjectOfType<RedCar>();
        Ball ball = GameObject.FindObjectOfType<Ball>();
        if(roundActive){
            if(goal.Equals("left")){
                LeftPlayerScore++;
            }
            if(goal.Equals("right")){
                RightPlayerScore++;
            }
            blueCar.returnToStartPos();
            redCar.returnToStartPos();
            ball.returnToStartPos();
        }
    }

    public void NewGame(){
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
