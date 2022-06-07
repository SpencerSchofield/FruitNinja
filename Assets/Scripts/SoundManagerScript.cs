using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip explodeSound, sliceSound, spawnSound, fishSound, cardSound, cardMatch;
    static AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        explodeSound = Resources.Load<AudioClip>("BombSound");
        spawnSound = Resources.Load<AudioClip>("SpawnerSound");
        sliceSound = Resources.Load<AudioClip>("FruitSlice");
        fishSound = Resources.Load<AudioClip>("FishHit");
        cardSound = Resources.Load<AudioClip>("CardFlip");
        cardMatch = Resources.Load<AudioClip>("CardMatch");
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip) 
    {
        switch(clip) {
            case "BombSound":
            audioSource?.PlayOneShot(explodeSound);
            break;
            case "FruitSlice":
            audioSource?.PlayOneShot(sliceSound);
            break;
            case "SpawnerSound":
            audioSource?.PlayOneShot(spawnSound);
            break;
            case "FishHit":
            audioSource?.PlayOneShot(fishSound);
            break;
            case "CardFlip":
            audioSource?.PlayOneShot(cardSound);
            break;
            case "CardMatch":
            audioSource?.PlayOneShot(cardMatch);
            break;
        }
    }
}
