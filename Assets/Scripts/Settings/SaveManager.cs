using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance { set; get; }
    public SaveState state;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
        instance = this;
        Load();
    }

    // Update is called once per frame
    void Update()
    {}

    //Writes a file with a key called save?
    public void Save()
    {
        PlayerPrefs.SetString("save", Helper.Serialize<SaveState>(state));
    }

    //loads the file with the key "save"?
    //if there isnt one loads up a new save state to save? not too sure
    public void Load()
    {
        if (PlayerPrefs.HasKey("save"))
        {
            state = Helper.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
        }
        else
        {
            state = new SaveState();
            Save();
        }
    }
}
