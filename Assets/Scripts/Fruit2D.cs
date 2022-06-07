using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit2D : MonoBehaviour
{
    private Vector2 posX; //position x
    private Vector2 posY; //position y
    private bool canBeDead; //if we can destroy the object
    private Vector3 screen; //position on the screen
    public Splat2D splat;

    // Update is called once per frame
    void Update()
    {
        //set screen position
        screen = Camera.main.WorldToScreenPoint(transform.position);

        //if we can die and are not on the screen
        if(canBeDead && screen.y < -30)
        {
            //Destroy
            Destroy(gameObject);
        }

        //if we cant die and are on the screen
        //for the fruit to appear on the screen for the first time
        else if(!canBeDead && screen.y > -10)
        {
            //we can die
            canBeDead = true;
        }
    }
    /*
    *Hit(GameObject other) used to instantiate splash behind destroyed fruit
    *GameObject other is the fruit we've hit
    */
    public void Hit(GameObject other) 
    {
        if(other.tag == "Fruit" || other.tag == "Apple" || other.tag == "Banana" || other.tag == "Orange")
        {
            //find x position on screen
            float posX = transform.position.x;
            //find y position on screen
            float posY = transform.position.y;
            //spawn splat at fruit position
            Instantiate(splat, new Vector2(posX, posY), Quaternion.identity);
            SoundManagerScript.PlaySound("FruitSlice");
        }

        //Destroy
        Destroy(other);
    }
}
