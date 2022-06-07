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
    public Text livesLeft;
    public int playerLives = 3;//bomb hits til player loses
    public GameController gameController;//GameController reference
    string currentGameMode = "Classic";//Used to get the GameMode from player prefs

    // Start is called before the first frame update
    void Start()
    {
        //Set screen orientation to landscape
        Screen.orientation = ScreenOrientation.Landscape;
        //Set sleep timeout to never
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        currentGameMode = PlayerPrefs.GetString("GameMode");
        //gameController.UpdateHUD("Match");
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
        livesLeft.text = "Lives Left: " + playerLives;

    }

    /*
    *OnTriggerEnter2D(Collider2D other) When the player hits an object it first checks what gamemode is currently playing
    *Collider2D other --> the object the player hit
    */
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
    /*
    *objectTagChcker(Collider2D other) checks the tag of the object the player hits, then determines what should happen
    Collider2D other --> is the object the player hits i.e. apple or bomb.  
    */
    public void objectTagChecker(Collider2D other)
    {
        Debug.Log(currentGameMode);
            if (other.tag == "Apple" || other.tag == "Banana" || other.tag == "Orange") //increments score if object is fruit
            {
                if(currentGameMode == "SimonSays"){gameController.MatchFruitsSlice(other);}
                score = score + 10;
                Debug.Log(score);
            }
            else if (other.tag == "Fish") //decrements score by 2 if object is a fish
            {
                score = score - 2;
                Debug.Log(score);
                SoundManagerScript.PlaySound("FishHit");
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
                    score = score + 20;
                    Debug.Log(score);
                    other.GetComponent<Fruit2D>().Hit(other.gameObject); //calls hit method
                    SoundManagerScript.PlaySound("FruitSlice");

                }
            }

            if (other.tag != "Melon" && other.tag != "Bomb")
            {
                other.GetComponent<Fruit2D>().Hit(other.gameObject); //calls hit method
            }
            
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
    /*
    *scoreIncrease(int points) increases the score by a custom amount
    int points --> how many points you want the score to increase by
    */
    public void scoreIncrease(int points)
    {
        score = score + points;
    }

}