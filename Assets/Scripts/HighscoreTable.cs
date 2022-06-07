using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;
    [SerializeField] GameObject gameController;
    public int score;
    public string name;
    
    /*
    * Awake is called when the script instance is being loaded.
    */
    private void Awake() {
        if (!PlayerPrefs.HasKey("highscoreTable")) //checks if table exists yet
        { 
            //prefill table with default value
            AddHighscoreEntry(10, "LRM");
        }
        
        score = PlayerPrefs.GetInt("PlayerScore");
        name = PlayerPrefs.GetString("Username");
        AddHighscoreEntry(score, name);

        entryContainer = GameObject.Find("highscoreEntryContainer").transform;
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false); //hides default template

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        // sort entry list by score
        highscores.highscoreEntryList.Sort((x,y) => y.score.CompareTo(x.score));

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList) 
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }


    /*
    * creates the container template for the table
    */
    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList) {
        float templateHeight = 40f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank) {
            default: rankString = rank + "TH"; break;
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }
        entryTransform.Find("posText").GetComponent<Text>().text = rankString;

        int score = highscoreEntry.score;
        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("nameText").GetComponent<Text>().text = name;

        //set background visible odds evens for easier reading
        entryTransform.Find("background").gameObject.SetActive(rank % 2 == 1);

        transformList.Add(entryTransform);
    }

    /*
    * method that controls the table entries
    */
    public void AddHighscoreEntry(int score, string name) {
        //create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };
        //load saved highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        if (jsonString == "") jsonString = "{}"; //if empty make vaild empty json
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        bool scoreAdded = false;

        for(int i = 0; i < highscores.highscoreEntryList.Count; i++) { //finds correct place in list to add the score
            if(highscoreEntry.score > highscores.highscoreEntryList[i].score) {
                highscores.highscoreEntryList.Insert(i, highscoreEntry);
                scoreAdded = true;
                break;
            }
        }

        int maxScoreBoardEntries = 10; //max entries you want to show on the board
        
        if (!scoreAdded && highscores.highscoreEntryList.Count < maxScoreBoardEntries) { //checks to see if theres less than 10 entries
            // add new entry to highscores
            highscores.highscoreEntryList.Add(highscoreEntry);
        }

        if (highscores.highscoreEntryList.Count > maxScoreBoardEntries) { // removes extra scores from board
            highscores.highscoreEntryList.RemoveRange(maxScoreBoardEntries, highscores.highscoreEntryList.Count - maxScoreBoardEntries);
        }

        //save updated highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();

        Debug.Log(score + " " + name + " end of add method");
    }

    /*
    * creates list of highscore entries
    */
    private class Highscores {
        public List<HighscoreEntry> highscoreEntryList;
    }

    /*
    * Represents a single highscore entry
    * Serialization is the automatic process of transforming data structures or object states into a format that Unity can store and reconstruct later
    */
    [System.Serializable]
    private class HighscoreEntry {
        public int score;
        public string name;
    }

    /* 
    * menu method for use with button, takes to home screen.
    */
    public void Menu()
    {
        SceneManager.LoadScene("GameHUD_menu");
    }
}