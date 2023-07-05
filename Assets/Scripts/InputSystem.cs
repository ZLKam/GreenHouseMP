using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputSystem : MonoBehaviour
{
    public static InputSystem Instance;

    [SerializeField]
    private Dropdown dropdown;
    private List<string> levels = new();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            foreach (var scene in EditorBuildSettings.scenes)
            {
                levels.Add(scene.path.Substring(scene.path.LastIndexOf('/') + 1).Replace(".unity", ""));
            }
            if (dropdown == null)
            {
                dropdown = GameObject.Find("Canvas").transform.GetChild(-1).GetComponent<Dropdown>();
            }
            dropdown.gameObject.SetActive(true);
            dropdown.ClearOptions();
            dropdown.AddOptions(levels);
            dropdown.onValueChanged.AddListener(delegate { EnterLevel(dropdown.options[dropdown.value].text); });
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            foreach (var name in EditorBuildSettings.scenes)
            {
                levels.Add(scene.path.Substring(name.path.LastIndexOf('/') + 1).Replace(".unity", ""));
            }
            if (dropdown == null)
            {
                GameObject canvas = GameObject.Find("Canvas");
                dropdown = canvas.transform.GetChild(canvas.transform.childCount - 1).GetComponent<Dropdown>();
            }
            dropdown.gameObject.SetActive(true);
            dropdown.ClearOptions();
            dropdown.AddOptions(levels);
            dropdown.onValueChanged.AddListener(delegate { EnterLevel(dropdown.options[dropdown.value].text); });
        }
    }
#endif

    private void EnterLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    //Returns mouse Input left click
    //main purpose is to return the first touch detected and return it to be used
    public bool LeftClick()
    {
#if UNITY_STANDALONE
        return Input.GetMouseButtonDown(0);
#endif

#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                return true;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(0).phase == TouchPhase.Ended) 
            {
                return false;
            }
        }
        return false;

#endif
    }

    //public bool isStationaryTest(int touchCount, float initialDist, float deltaDist = 0) 
    //{
    //    if (initialDist - deltaDist > ) 
    //    {

    //    }
    //}

    public bool isStationary(int touchCount, float initialDist, float deltaDistance = 0f)
    {
        //Debug.Log(Mathf.Abs(initialDist - deltaDistance));
        if (/*(Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved) &&*/
                Mathf.Abs(initialDist - deltaDistance) < 1f)
        {
            return true;
        }
        return false;
        //int stationaryTouch = 0;
        //for (int i = 0; i < touchCount; i++)
        //{
        //    if (Input.GetTouch(i).phase == TouchPhase.Stationary)
        //    {
        //        stationaryTouch++;
        //    }
        //}
        //return stationaryTouch == touchCount;
    }

    //returns the right click for the mouse
    public bool RightClick()
    {
#if UNITY_STANDALONE
        return Input.GetMouseButtonDown(1);
#endif
#if UNITY_ANDROID
        return true;
#endif
    }
}

