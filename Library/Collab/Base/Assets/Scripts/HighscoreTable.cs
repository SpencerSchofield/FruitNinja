using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;
    
    /*
    * Awake is called when the script instance is being loaded.
    */
    
    private void Awake() {
        if (!PlayerPrefs.HasKey("highscoreTable")) { //checks if table exists yet
            //prefill table with default values
            AddHighscoreEntry(10, "LRM");
            AddHighscoreEntry(9, "LRM");
            AddHighscoreEntry(8, "LRM");
            AddHighscoreEntry(7, "LRM");
            AddHighscoreEntry(6, "LRM");
            AddHighscoreEntry(5, "LRM");
            AddHighscoreEntry(4, "LRM");
            AddHighscoreEntry(3, "LRM");
            AddHighscoreEntry(2, "LRM");
            AddHighscoreEntry(2, "LRM");
        }
        entryContainer = GameObject.Find("highscoreEntryContainer").transform;
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false); //hides default template

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        // sort entry list by score
        highscores.highscoreEntryList.Sort((x,y) => y.score.CompareTo(x.score));

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList) {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }
    /*
    *
    */
    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList) {
        float templateHeight = 30f;
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

        transformList.Add(entryTransform);
    }

    /*
    *
    */
    private void AddHighscoreEntry(int score, string name) {
        //create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };
        //load saved highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        bool scoreAdded = false;

        for(int i = 0; i < highscores.highscoreEntryList.Count; i++) {
            if(highscoreEntry.score > highscores.highscoreEntryList[i].score) {
                highscores.highscoreEntryList.Insert(i, highscoreEntry);
                scoreAdded = true;
                break;
            }
        }
        int maxScoreBoardEntries = 10;
        
        if (!scoreAdded && highscores.highscoreEntryList.Count < maxScoreBoardEntries) { //checks to see if theres less than 10 entries
            // add new entry to highscores
            highscores.highscoreEntryList.Add(highscoreEntry);
        }

        if (highscores.highscoreEntryList.Count > maxScoreBoardEntries) {
            highscores.highscoreEntryList.RemoveRange(maxScoreBoardEntries, highscores.highscoreEntryList.Count - maxScoreBoardEntries);
        }

        //save updated highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    /*
    *
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
}