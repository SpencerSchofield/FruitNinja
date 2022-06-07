using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ninja_Player : MonoBehaviour
{
    public GameObject spawner;
    private Vector3 pos; //position
    public int score = 0; //Score 
    public Text scoreText;
    public int playerLives = 3;//bomb hits til player loses
    public GameController gameController;//GameController reference
    string currentGameMode;//Used to get the GameMode from player prefs

    // Start is called before the first frame update
    void Start()
    {
        //Set screen orientation to landscape
        Screen.orientation = ScreenOrientation.Landscape;
        //Set sleep timeout to never
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        currentGameMode = PlayerPrefs.GetString("GameMode");
    }

    // Update is called once per frame
    void Update()
    {
        //If the game is running on an iPhone device
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            //If we are hitting the screen
            if (Input.touchCount == 1)
            {
                //Find screen touch position, by transforming position from screen space into game world space. 
                pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 1));
                //Set position of the player object
                transform.position = new Vector3(pos.x, pos.y, 3);
                //Set collider to true 
                GetComponent<Collider2D>().enabled = true;
                return;
            }
            //Set collider to false
            GetComponent<Collider2D>().enabled = false;
        }
        //If the game is not running on an iPhone device
        else
        {
            pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            //Set position
            transform.position = new Vector3(pos.x, pos.y, 3);
        }

        scoreText.text = "Score: " + score;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (currentGameMode == "Classic")//If gamemode is classic
        {
            objectTagChecker(other);
        }
        else if (currentGameMode == "SimonSays")//if gamemode is SimonSays
        {
            
            objectTagChecker(other);
        }
    }
    public void objectTagChecker(Collider2D other)
    {
        Debug.Log(currentGameMode);
            if (other.tag == "Fruit" || other.tag == "Apple" || other.tag == "Banana" || other.tag == "Orange") //increments score if object is fruit
            {
                gameController.SimonSays(other);
                score++;
                Debug.Log(score);
            }
            else if (other.tag == "Fish") //decrements score by 2 if object is a fish
            {
                score = score - 2;
                Debug.Log(score);
            }
            else if (other.tag == "Bomb") //decrements score by 2 if object is a bomb
            {
                //ToDo: Remove a life from player
                score = score - 2;
                reducePlayerHealth();
                other.GetComponent<Bomb2D>().Hit(other.gameObject);
                Debug.Log(score);
            }
            else if (other.tag == "Melon")
            {
                other.GetComponent<Melon2D>().reduceHealth();
                if (other.GetComponent<Melon2D>().health <= 0) //??
                {
                    score = score + 2;
                    Debug.Log(score);
                    other.GetComponent<Fruit2D>().Hit(other.gameObject); //calls hit method
                }
            }

            if (other.tag != "Melon")
            {
                other.GetComponent<Fruit2D>().Hit(other.gameObject); //calls hit method
            }
            gameController.SimonSays(other);
    }

    /*
    *setDifficulty() used to change the amount of lives the player has
    */
    public void setDifficulty()
    {
        //switch case
    }

/*
*reducePlayerHealth() remove -1 from player lives
*/
    public void reducePlayerHealth()
    {
        playerLives--;
    }

    /*
    *returnPlayerLives() returns int playerLives. Used when checking if game is GameOver 
    */
    public int returnPlayerLives()
    {
        return playerLives;
    }

}