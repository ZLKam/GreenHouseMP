using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetScene : MonoBehaviour
{
    public void SetPreviousScene()
    {
        PlayerPrefs.SetString("previousScene", SceneManager.GetActiveScene().name);
    }

    public void ResetScene()
    {
        Instructions.Read = true;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    } 
}
