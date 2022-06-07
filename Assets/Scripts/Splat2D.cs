using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splat2D : MonoBehaviour
{
    private Color color; //Color
    public float destroySpeed = 0.2f; //Destroy speed

    // Start is called before the first frame update
    void Start()
    {
        //Set the color
        color = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        //set the mesh material colour
        GetComponent<SpriteRenderer>().color = new Color(color.r,color.g,color.b,color.a -= destroySpeed * Time.deltaTime);
        //If color a is 0
        if (color.a <= 0)
        {
            //Destroy
            Destroy(gameObject);
        }
    }
}
