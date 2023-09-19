using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 61;
    public bool timerIsRunning = false;
    public Text Timertxt;





    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);

        Timertxt.GetComponent<Text>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }



    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;               
                    Timertxt.text = "00:00";
                


                //GameplayController.instance.isGameOver = true;
                //GameplayController.instance.GameOver();
                //GameController.Instance.GameState = GameState.GameOver;
            }
        }
    }


    public void Start()
    {
        timerIsRunning = true;
    }
}
