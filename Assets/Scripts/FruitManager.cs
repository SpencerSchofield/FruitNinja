using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{

    public GameObject apple; //apple prefab
    public GameObject banana; //banana prefab
    public GameObject orange; //orange prefab
    public GameObject bomb;
    public Queue<GameObject>[,] gameObList = new Queue<GameObject>[3, 3];

    public Ninja_Player ninja_Player;
    public GameController gameController;


    public void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                gameObList[i, j] = new Queue<GameObject>();
            }
        }

        fruitRound();

    }

    /*
    *AddRoundOfFruit(string objectToSpawn) adds the next round of fruit to spawn.
    *string objectToSpawn is string of objects in order to spawn them in. String is spilt into individual words
    */
    public void AddRoundOfFruit(string objectToSpawn)
    {
        GameObject[,] exa = new GameObject[3, 3];
        string[] lines = objectToSpawn.Split('\n');
        for (int i = 0; i < 3; ++i)
        {
            string[] items = lines[i].Trim().Split(' ');
            exa[i, 0] = lookupItem(items[0].Trim());
            exa[i, 1] = lookupItem(items[1].Trim());
            exa[i, 2] = lookupItem(items[2].Trim());
        }
        AddRoundOfFruitHelper(exa);

    }

    /*
    *lookupItem takes in single word string and coverts them into correct gameobject to spawn. i.e. "apple" = apple.
    *string item
    */
    public GameObject lookupItem(string item)
    {
        GameObject go = null;
        switch (item)
        {
            case "apple":
                go = apple;
                break;
            case "bomb":
                go = bomb;
                Bomb2D script = go.GetComponent<Bomb2D>();
                script.ninja_Player = this.ninja_Player;
                script.gameController = this.gameController;
                script.Start_Defer();
                break;
            case "fruit":
                GameObject[] fruits = new GameObject[] { apple, banana, orange };
                int x = Random.Range(0, fruits.Length);
                go = fruits[x];
                break;
            case "null":
            default:
                break;
        }
        return go;
    }

    /*
    *AddRoundOfFruitHelper(GameObject[,] round) creates array full of gameobjects( different fruit / bombs)
    *GameObject[,] round --> This is the array of gameobjects we got from fruitRound();
    */
    public void AddRoundOfFruitHelper(GameObject[,] round)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                gameObList[i, j].Enqueue(round[j, i]);
            }
        }
    }

    /*
    *fruitRound() chooses a random 3x3 spawn shape or fruits/bombs
    */
    public void fruitRound()
    {
        int spawnRange = Random.Range(0, 100);
        if (spawnRange >= 0 && spawnRange <= 10)
        {
            AddRoundOfFruit(@"bomb fruit bomb
                        bomb fruit bomb
                        bomb fruit bomb");
        }
        else if (spawnRange >= 11 && spawnRange <= 20)
        {
            AddRoundOfFruit(@"fruit bomb fruit
                        bomb fruit bomb
                        fruit bomb fruit");
        }
        else if (spawnRange >= 21 && spawnRange <= 30)
        {
            AddRoundOfFruit(@"bomb bomb bomb
                        bomb fruit bomb
                        bomb bomb bomb");
        }
        else if (spawnRange >= 31 && spawnRange <= 40)
        {
            AddRoundOfFruit(@"fruit fruit fruit
                        fruit Bomb fruit
                        fruit fruit fruit");
        }
        else if (spawnRange >= 41 && spawnRange <= 50)
        {
            AddRoundOfFruit(@"bomb fruit fruit
                        bomb fruit fruit
                        bomb fruit fruit");
        }
        else if (spawnRange >= 51 && spawnRange <= 60)
        {
            AddRoundOfFruit(@"fruit fruit bomb
                        fruit fruit bomb
                        fruit fruit bomb");
        }
        else if (spawnRange >= 61 && spawnRange <= 70)
        {
            AddRoundOfFruit(@"fruit fruit fruit
                        bomb bomb bomb
                        bomb bomb bomb");
        }
        else if (spawnRange >= 71 && spawnRange <= 80)
        {
            AddRoundOfFruit(@"bomb bomb bomb
                        bomb bomb bomb
                        fruit fruit fruit");
        }
        else if (spawnRange >= 81 && spawnRange <= 90)
        {
            AddRoundOfFruit(@"bomb fruit bomb
                        fruit fruit fruit
                        bomb fruit bomb");
        }
        else if (spawnRange >= 91 && spawnRange <= 100)
        {
            AddRoundOfFruit(@"fruit fruit fruit
                        fruit fruit fruit
                        fruit fruit fruit");
        }
    }


    public void Update()
    {
        fruitRound();//constantly changes the round of fruit that will spawn
    }

}
