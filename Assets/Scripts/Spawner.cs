using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public float spawnTime = 5; //Spawn time
    public GameObject bomb;
    public GameObject apple;
    public GameObject spawnFruit;
    private int spawnRange; //range between 0 & 100
    public float upForce = 750; //up force
    private Coroutine gameCorutine = null;
    public FruitManager fruitManager;

    public List<GameObject> gameObList = new List<GameObject>();
    public int x; //x coordinate used in finding GameObject to spawn from gameObList
    public int y; //y coordinate used in finding GameObject to spawn from gameObList
    public GameController gameController;


    // Start is called before the first frame update
    void Start()
    {
        //start the spawn update
        gameCorutine = StartCoroutine("Spawn");
        gameController.UpdateHUD("Match");
        gameController.updateFruitTarget();
    }

    IEnumerator Spawn()
    {

        //Wait spawntime
        yield return new WaitForSeconds(spawnTime);
        
        if(fruitManager.gameObList[x,y].Count == 0)
        {
            yield return null;
        }

        spawnFruit = fruitManager.gameObList[x,y].Dequeue();

        //spawn prefab add random position
        GameObject go = Instantiate(spawnFruit, transform.position, Quaternion.identity);

        //Throw fruit straight up 
        go.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, upForce));

        //start the spawn again
        StartCoroutine("Spawn");
    }

    // Update is called once per frame
    void Update()
    {

    }
}