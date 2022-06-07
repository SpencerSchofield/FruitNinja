using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        //Loads scenes in order 
        SceneManager.LoadScene("GameHUD_gameModeScene");
    }

    public void PlayClassic()
    {
        PlayerPrefs.SetString("GameMode", "Classic");
        SceneManager.LoadScene("GameHUD_ClassicTutorial");
        
    }
    
    
    public void PlaySimonSays()
    {
        PlayerPrefs.SetString("GameMode", "SimonSays");
        SceneManager.LoadScene("GameHUD_3x3Tutorial");
    }

    public void OpenLeaderboard()
    {
        SceneManager.LoadScene("GameScene_highscoreTable");
    }

    public void PlayCardMatch(){
        SceneManager.LoadScene("GameHUD_MatchCardTutorial");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT"); //Game doesn't close in unity, checks button works
        Application.Quit();//Exit the game
    }

    public void continueClassic()
    {
        SceneManager.LoadScene("GameScene_classic");
    }

    public void continueSimonSays()
    {
        SceneManager.LoadScene("3x3");
    }
    public void continueMatchCard()
    {
        SceneManager.LoadScene("GameScene_cardGame");
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("GameHUD_menu");
    }
}
