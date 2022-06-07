using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_items : MonoBehaviour
{
    public float spawnTime=1; //Spawn time
    public GameObject apple; //apple prefab
    public GameObject banana; //banana prefab
    public GameObject orange; //orange prefab
    public GameObject fish; //fish prefab
    public GameObject melon; //melon prefab
    public GameObject bomb;
    public float upForce = 750; //up force
    public float leftRightForce = 200; //left and right force
    public float maxX = -7; //max x spawn position
    public float minX = 7; //min x spawn position
    private Coroutine gameCorutine = null;
    public Ninja_Player ninja_Player;
     public GameController gameController;
    


    // Start is called before the first frame update
    void Start()
    {
        //start the spawn update
        gameCorutine = StartCoroutine("Spawn");
        gameController.UpdateHUD();
        gameController.updateFruitTarget();
    }

    IEnumerator Spawn()
    {
        //Wait spawntime
        yield return new WaitForSeconds(spawnTime);        

        GameObject[] objectList = new GameObject[]{apple, banana, orange, melon, fish, bomb};
        GameObject prefab;

        int i = Random.Range(0, objectList.Length);
        prefab = objectList[i];

        //spawn prefab add random position
        GameObject go = Instantiate(prefab,new Vector3(Random.Range(minX ,maxX + 1),transform.position.y, 0f),Quaternion.Euler(0,0, Random.Range (- 90F, 90F))) as GameObject;
        SoundManagerScript.PlaySound("SpawnerSound");
        if (prefab == bomb) {
            Bomb2D script = go.GetComponent<Bomb2D>();
            script.ninja_Player = this.ninja_Player;
            script.gameController = this.gameController;
            script.Start_Defer();
        }

        //if x position is over 0 go left
        if(go.transform.position.x > 0)
        {
            go.GetComponent<Rigidbody2D>().AddForce(new Vector2(-leftRightForce,upForce));        
        }
        //else go right
        else
        {
            go.GetComponent<Rigidbody2D>().AddForce(new Vector2(leftRightForce,upForce));        
        }
        //start the spawn again
        StartCoroutine("Spawn");
    }

        public void playerDeath()
    {
        if (gameCorutine != null && ninja_Player.playerLives <= 0)
        {
            gameController.GameOver();
            Debug.Log("STOP");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void stopCorutine(string name)
    {
        StopCoroutine(name);
    }
}