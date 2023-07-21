using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetScene : MonoBehaviour
{
    private Scene scene1;
    public void SetPreviousScene()
    {
        PlayerPrefs.SetString("previousScene", SceneManager.GetActiveScene().name);
    }

    public void ResetScene()
    {
        scene1 = SceneManager.GetActiveScene();
        if (scene1.name == "Level 1")
        {
            Instructions.Readlvl1 = true;
        }
        else if (scene1.name == "Level 2")
        {
            Instructions.Readlvl2 = true;
        }
        else if(scene1.name == "Level 3") { Instructions.Readlvl3 = true; }
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    } 
}
