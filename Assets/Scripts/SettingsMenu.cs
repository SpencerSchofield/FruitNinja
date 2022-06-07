using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetVolume(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("volume", volume);
    }

    public void NameChanged(string newName) 
    {
        PlayerPrefs.SetString("Username", newName);
        Debug.Log("username changed to " + newName);
    }

}
