using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melon2D : MonoBehaviour
{
    public float health = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void reduceHealth()
    {
        health--;
    }
    // Update is called once per frame
    void Update()
    {      
        //if(health <=0)
        //{
        //    Ninja_player.score +2;

        //}
    }
}
