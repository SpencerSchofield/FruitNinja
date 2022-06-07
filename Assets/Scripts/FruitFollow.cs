using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitFollow : MonoBehaviour
{
    GameObject player; //The player cursor
    Vector3 tF; //Transform forward
    Vector3 fD; //Transform direction


    // Start is called before the first frame update
    void Start()
    {
        tF = this.transform.up; //Transform forward facing up at Y axis
        player = GameObject.FindWithTag("Player"); //set player variable to be cursor
    }

    // Update is called once per frame
    void Update()
    {
        
        fD = player.transform.position - this.transform.position; //get the vector from fruit to player
        float angle = Vector2.Angle(tF, fD);//use Vector2 to avoid Z in the calculation

        Debug.DrawRay(this.transform.position, tF*2, Color.green); //make green line appear on screen representing transform forward
        Debug.DrawRay(this.transform.position, fD, Color.red); //make red line appear aiming at the player using transform direction
        
        Vector3 crossP = Vector3.Cross(tF, fD); //Cross product of angle
        int clockwise = 1; //assume its clockwise to start with
        if (crossP.z < 0) { //if cross product z less than 0 make counter clockwise
            clockwise = -1;
        }
        
        this.transform.rotation = Quaternion.Euler(0, 0, angle*clockwise); //calculate which direction to rotate
        this.transform.Translate(this.transform.up * Time.deltaTime); //make apple travel towards player
    }
}