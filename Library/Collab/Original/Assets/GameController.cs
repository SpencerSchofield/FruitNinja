using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Ninja_Player ninja_Player; //Ninja_Player reference
    public int sliceAmount = 3; //# of hits in objectives
    string fruitTarget = "Apple";//Fruit target to hit/avoid in objectives
    public Text hud;//Text reference --> used for Objectives
    [SerializeField] GameObject gameOverImage; //Game Over Image reference
    public Spawn_items spawn_Items; //Orignal spawner used in Classic
    private float timer = 0.0f;
    private bool gameover = false;
    private bool isFruit = false;
    private string matchFruitTo;
    private string currentGameMode;

    public void Update()
    {
        //if the game ends wait 3 seconds before running GameOver_Defer()
        if (gameover)
        {
            timer += Time.deltaTime;
            if (timer > 3.0f) GameOver_Defer();
        }
    }

    /*
    * GameOver() called when player loses objective. 
    *Display GameOver UI
    */
    public void GameOver()
    {
        if(spawn_Items){
            spawn_Items.stopCorutine("Spawn");
        }
        
        Debug.Log("Game Over");
        gameOverImage.SetActive(true);
        this.gameover = true;
        this.timer = 0;
    }

    /*called after 3 seconds
    *GameOver_Defer() resets values of gameover and timer, ready to be called again
    *Loads highscore table
    */
    public void GameOver_Defer()
    {

        //game over/leaderboard scene
        gameover = false;
        timer = 0;
        gameOverImage.SetActive(false);
        SceneManager.LoadScene("GameScene_highscoreTable");
    }

    /*
    *NextRound() called to start the next round
    */
    public void NextRound(int newSliceTarget)
    {
        if(spawn_Items){
            StartCoroutine("Spawn");
        }
        sliceAmount = newSliceTarget;
        Debug.Log("Next Round");
    }

    /*
    *SimonSays() picks different objectives the player needs to complete
    */
    public void SimonSays(string game)
    {

        //between slicing and avoiding and matching
        while(sliceAmount == 0)
        {
            string[] gameModes = new string[] {"MatchFruits","SliceFruits"};

            switch(game)
            {
                case("MatchFruits"):
                currentGameMode = "MatchFruits";
                break;
                case("SliceFruits"):
                currentGameMode = "SliceFruits";
                break;
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
        if(isFruit == true)
        {
            UpdateHUD();
            //An array of randomised fruits in order of hitting them
            int i = 0;
            string[] fruits = new string[] {"Apple", "Banana","Orange"};
            if (other.tag == fruits[i])
            {   
            sliceAmount--;
            Debug.Log("SliceTargetValue : " + sliceAmount);

            if (sliceAmount <= 0)
            {
                StopCoroutine("Spawn");//Stop Current Coroutine
                isFruit = false;
                updateFruitTarget();
                Debug.Log("Round Complete");
                NextRound(5);//Start next round / coroutine
                UpdateHUD();
            }
        } else if(isFruit == false){
            updateFruitTarget();
        }
        }
    }

    /*
    *Change the fruitTarget variable to randomise between all fruit types
    */
    public void updateFruitTarget()
    {
        if(isFruit == false){
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

    public void updateSliceRange()
    {

    }

    /*
    *SliceFruits Game --> Player must slice fruit target for slice amount of times.
    */
    public void SliceFruits(Collider2D other)
    {
        UpdateHUD();
        if (other.tag == fruitTarget)
        {
            sliceAmount--;
            Debug.Log("SliceTargetValue : " + sliceAmount);
            if (sliceAmount == 0)
            {
                StopCoroutine("Spawn");
                NextRound(5);
                Debug.Log("Trigger3");
            }

        }
    }

    public void AvoidFruits(Collider2D other)
    {
        UpdateHUD();
        if (other.tag == fruitTarget)
        {
            Debug.Log("You Hit the wrong fruit");
            GameOver();
        }
    
    }

    /*
    *updateHUD used to update HUD
    */
    public void UpdateHUD()
    {
        hud.text = "Slice " + fruitTarget + " " + sliceAmount + " times";
    }


}
