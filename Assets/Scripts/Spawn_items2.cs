using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_items2 : MonoBehaviour
{
    public float spawnTime=1; //Spawn time
    public GameObject apple; //apple prefab
    //public GameObject bomb; //bomb prefab
    public float upForce = 750; //up force
    public float leftRightForce = 200; //left and right force
    public float maxX = -7; //max x spawn position
    public float minX = 7; //min x spawn position

    // Start is called before the first frame update
    void Start()
    {
        //start the spawn update
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        //Wait spawntime
        yield return new WaitForSeconds(spawnTime);        

        GameObject prefab = apple;

        // //if random is over 30
        // if(Random.Range(0,100) < 30)
        // {
        //     //spawn prefab is bomb
        //     prefab = bomb;
        // }

        //spawn prefab add random position
        GameObject go = Instantiate(prefab,new Vector3(Random.Range(minX ,maxX + 1),transform.position.y, 0f),Quaternion.Euler(0,0, Random.Range (- 90F, 90F))) as GameObject;

        //if x position is over 0 go left
        if(go.transform.position.x > 0)
        {
            go.GetComponent<Gravity>().applyForce(new Vector3(-leftRightForce, upForce, 0f));        
        }
        //else go right
        else
        {
            go.GetComponent<Gravity>().applyForce(new Vector3(leftRightForce, upForce, 0f));        
        }
        //start the spawn again
        StartCoroutine("Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}