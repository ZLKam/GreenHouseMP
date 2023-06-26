using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingTest : MonoBehaviour
{
    byte fadeAmount;
    Material originalSkybox;
    public Material otherskybox;
    bool change;

    // Start is called before the first frame update
    void Start()
    {
        originalSkybox = RenderSettings.skybox;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("hi");
            change = !change;
        }

        if (change)
        {
            RenderSettings.skybox = otherskybox;
        }
        else
        {
            RenderSettings.skybox = originalSkybox;
        }
    }
}
