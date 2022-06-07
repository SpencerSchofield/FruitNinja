using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    void Awake () {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
        if (objs.Length > 1) { //removes duplicate music objects
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
