using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSFX : MonoBehaviour
{
    public void Click(string name)
    {
        // Calls the Play() function within the AudioManager instance within the scene to play
        // the specific sound with the correct name
        AudioManager.instance.Play(name);
    }
}