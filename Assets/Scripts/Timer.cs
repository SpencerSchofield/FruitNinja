using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float gameTimer = 60.0f;
    public Text timerText;
    public GameObject gameController;

    // Update is called once per frame
    void Update()
    {
        if (gameTimer > 0) //while greater than 0 update timer
        { 
            gameTimer -= Time.deltaTime;
            updateTimeHUD();
        }

        if(gameTimer <= 0) //once timer hits 0 
        { 
            gameController.GetComponent<GameController>().GameOver(); //ends game
            gameTimer = 0;
        }
    }

    /* 
    * updates the time HUD shown on screen 
    */
    public void updateTimeHUD()
    {
        double b = System.Math.Round (gameTimer, 2);   //rounds the time float to 2dp  
        timerText.text = "Time: " + b.ToString ();
    }
}
