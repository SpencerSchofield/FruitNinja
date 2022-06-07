using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb2D : MonoBehaviour
{
    public GameObject splat; //Splat reference
    public Ninja_Player ninja_Player;//Ninja_Player reference
    public GameController gameController;//GameController reference

    public void Start_Defer()
    {
        ninja_Player.GetComponent<Ninja_Player>().returnPlayerLives();
    }
    /*
    *Hit(GameObject other) used to explode the bomb
    *GameObject other is the bomb we hit
    */
    public void Hit(GameObject other)
    {
        if (other.tag == "Bomb")
        {
            //find x position on screen
            float posX = transform.position.x;
            //find y position on screen
            float posY = transform.position.y;
            explode();

            //UI End game
        }

        //Destroy
        Destroy(other);
    }

/*
*explode() checks if the player still has lives left, if no then end the game.
*/
    public void explode()
    {
        int lives = ninja_Player.returnPlayerLives();
        //sprite explode
        SoundManagerScript.PlaySound("BombSound");
        //particle effect on explode
        if(lives <= 0)
        {
            gameController.GameOver();
        }
    }

}
