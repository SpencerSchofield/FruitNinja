using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Ninja_Player ninja_Player; //Ninja_Player reference
    public int sliceAmount = 3; //# of hits in objectives
    string fruitTarget;//Fruit target to hit/avoid in objectives
    public Text hud;//Text reference --> used for Objectives
    [SerializeField] GameObject gameOverImage; //Game Over Image reference
    public Spawn_items spawn_Items; //Orignal spawner used in Classic
    private float waitTimer = 0.0f;
    private bool gameover = false;
    private bool isFruit = false;
    static int score;
    private string matchFruitTo;
    private string currentGameMode;
    private int iFruits = 0;
    private string[] fruits = new string[] { "Apple", "Banana", "Orange" };
    private int round = 0;
    private bool roundsComplete = false;

    public void Start()
    {
        updateFruitTarget();
    }
    public void Update()
    {
        //if the game ends wait 3 seconds before running GameOver_Defer()
        while (gameover)
        {
            waitTimer += Time.deltaTime;
            //Debug.Log("wt: " + waitTimer);
            if (waitTimer > 3.0f)
            {
                GameOver_Defer();
            }
        }
    }

    /*
    * GameOver() called when player loses objective. 
    *Display GameOver UI
    */
    public void GameOver()
    {
        if (spawn_Items)
        {
            spawn_Items.stopCorutine("Spawn");
        }

        Debug.Log("Game Over");
        gameOverImage.SetActive(true);
        this.gameover = true;
        this.waitTimer = 0;
        Debug.Log("wt: in game over");

    }

    /*called after 3 seconds
    *GameOver_Defer() resets values of gameover and waitTimer, ready to be called again
    *Loads highscore table
    */
    public void GameOver_Defer()
    {

        //game over/leaderboard scene
        gameover = false;
        waitTimer = 0;
        Debug.Log("wt: in game over defer");
        gameOverImage.SetActive(false);
        score = ninja_Player.score;
        PlayerPrefs.SetInt("PlayerScore", score);
        
        SceneManager.LoadScene("GameScene_highscoreTable");
        
    }

    /*
    *NextRound() called to start the next round
    */
    public void NextRound(int newSliceTarget)
    {
        if (spawn_Items)
        {
            StartCoroutine("Spawn");
        }
        sliceAmount = newSliceTarget;
        round++;
        Debug.Log("Next Round");
        Debug.Log("Round " + round);
        if(round > 2)
        {
            roundsComplete = true;
        }
    }

    /*
    *SimonSays() picks different objectives the player needs to complete
    */
    public void SimonSays(Collider2D other)
    {
        //between slicing and avoiding and matching
        int i = Random.Range(0, 1);
        if (isFruit == false && roundsComplete == true)
        {
            if (i == 0)
            {
                updateFruitTarget();
                SliceFruits(other);
            }
            else if (i == 1)
            {
                updateFruitTarget();
                MatchFruitsSlice(other);
            }
        }

    }

    /*
    *MatchFruits game --> player must hit fruit target (say banana) for sliceAmount of times (say 3), 
    *then trigger next round which changes fruit target and slicerAmount values.
    *If player hits anything other then fruit target they fail
    */
    public void MatchFruitsSlice(Collider2D other)
    {
        //Player hits fruit target (say banana) and fruit target changes (say to apple), if player hits anything other then fruit target they fail
        if (isFruit == true)
        {
            UpdateHUD("Match");
            //An array of randomised fruits in order of hitting them
            if (other.tag == fruits[iFruits])
            {
                sliceAmount--;
                iFruits++;
                Debug.Log("sliceAmount: " + sliceAmount);

                if (sliceAmount == 0)
                {
                    Debug.Log("oioioi");
                    StopCoroutine("Spawn");//Stop Current Coroutine
                    isFruit = false; //allows us to change fruit target
                    updateFruitTarget();
                    UpdateHUD("Match");
                    Debug.Log("Round Complete");
                    iFruits = 0;
                    ninja_Player.scoreIncrease(100);
                    Debug.Log("Matched All the Fruits");
                    updateMatchFruits();//change fruit order
                    NextRound(3);//Start next round / coroutine
                    SimonSays(other);
                }

            }
            else if (other.tag != fruits[iFruits])
            {
                Debug.Log("MatchGameOver");
                GameOver();
            }

        }
        else if (isFruit == false)
        {
            updateFruitTarget();
            MatchFruitsSlice(other);
        }
    }

    /*
    *Change the fruitTarget variable to randomise between all fruit types
    */
    public void updateFruitTarget()
    {
        if (isFruit == false)
        {
            int x = Random.Range(0, 2);
            if (x == 0)
            {
                fruitTarget = "Apple";
                isFruit = true;
            }
            else if (x == 1)
            {
                fruitTarget = "Banana";
                isFruit = true;
            }
            else if (x == 2)
            {
                fruitTarget = "Orange";
                isFruit = true;
            }
        }
    }


    public void updateMatchFruits()
    {
        int x = Random.Range(0,1);
        if(x == 0)
        {
            fruits = new string[] {"Banana", "Apple", "Orange"};
            UpdateHUD("Match");
        } 
        else if(x == 1)
        {
            fruits = new string[] {"Apple", "Apple", "Orange"};
            UpdateHUD("Match");
        }
    }

    /*
    *SliceFruits Game --> Player must slice fruit target for slice amount of times.
    */
    public void SliceFruits(Collider2D other)
    {
        if (other.tag == fruitTarget)
        {
            Debug.Log("Fruit hit: " + other.tag + " fruitTarget: " + fruitTarget);
            sliceAmount--;
            UpdateHUD("Slice");
            Debug.Log("SliceTargetValue : " + sliceAmount);
            if (sliceAmount == 0)
            {
                StopCoroutine("Spawn");
                ninja_Player.scoreIncrease(100);
               // updateFruitTarget();
                NextRound(5);
                Debug.Log("Trigger3");
                UpdateHUD("Slice");
            }

        }
    }


    /*
    *updateHUD used to update HUD
    */
    public void UpdateHUD(string Objective)
    {
        if(Objective == "Slice")
        {
        hud.text = "Slice " + fruitTarget + " " + sliceAmount + " times";
        } else if(Objective == "Match")
        {
        hud.text = "Slice " + fruits[0] + " " + fruits[1] + " " + fruits[2] + " in order";
        }

    }
}
