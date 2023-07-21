using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Connection : MonoBehaviour
{
    public bool tutorial;
    public Level2AnswerSheet level2AnswerSheet;
    public SelectedComponent selectedComponent;
    public ReturnValue valueReturnBtn;
    public ToggleMultiConnect multiConnectToggle;
    public CameraMovement cameraMovement;

    public GameObject particle;

    public Material highlightMat;
    public Material selectionMat;

    public List<GameObject> points;
    public List<GameObject> multiPoints;
    public SelectedComponent[] componentArray;

    public int multiConnectLimit;
    public bool multiConnect;

    public List<GameObject> pipes;

    public GameObject pipe;
    public GameObject entrance;
    public GameObject exit;
    public GameObject body;

    Transform point1;
    Transform point2;

    [HideInInspector]
    public List<Transform> multiplePoints;
    [HideInInspector]
    public List<Vector3> pipeConnection;
    [HideInInspector]
    public List<Vector3> centerPoints;
    [HideInInspector]
    public List<float> lengths;

    Material originalMat;
    Transform highlight;
    Transform selection;
    RaycastHit raycastHit;

    Renderer[] renderers;

    bool allMatch;
    bool anomalyFound;
    public bool pipeWarning;
    public GameObject pipeWarningPanel;
    public Fade fade;
    public GameObject uiParent;

    private void Awake()
    {
        Camera.main.transform.parent.GetComponent<CameraMovement>().zoomStopDistance = 30f;
        componentArray = FindObjectsOfType<SelectedComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!fade.PauseCheck)
        {
            CheckUndoPipes();

            if (multiConnect)
            {
                MultiHighlight();
            }
            else
            {
                Highlight();
            }

            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void CheckUndoPipes()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (pipes.Count > 0)
                {
                    if (pipes.Contains(pipes[pipes.Count - 1]))
                    {
                        GameObject Clone = pipes[pipes.Count - 1];
                        pipes.Remove(pipes[pipes.Count - 1]);
                        Destroy(Clone);
                    }
                }
            }
        }
    }

    void Highlight()
    {
        if (!pipe && !entrance && !exit && !body)
        {
            pipeWarning = true;
        }
        else 
        {
            pipeWarning = false;
        }
        // checks if there is an object being highlighted
        // if so, remove highlight by resetting the object's material to it's original material
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#if UNITY_STANDALONE
        if(highlight != null)
        {
            highlight.GetComponent<MeshRenderer>().material = originalMat;
            highlight = null;
        }


        // checks if the raycast being drawn from the mouse hits an object
        // if so, check if the tag of the highlight is called "Connection" before setting the colour of the object's material
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {
            highlight = raycastHit.transform;
            if(highlight.CompareTag("Connection") && highlight != selection)
            {
                if(highlight.GetComponent<MeshRenderer>().material != highlightMat)
                {
                    originalMat = highlight.GetComponent<MeshRenderer>().material;
                    highlight.GetComponent<MeshRenderer>().material = highlightMat;
                }
            }
            else
            {
                highlight = null;
            }
        }
#endif
        if (InputSystem.Instance.LeftClick())
        {
            if (EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject.layer == 5)
                return;
            if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
            {
                selection = raycastHit.transform;
                if (selection.GetComponent<SelectedComponent>())
                {
                    //Debug.Log("Reached two");
                    selectedComponent = selection.GetComponent<SelectedComponent>();
                    //selectedComponent.valueReturn.selectedComponentBtn = selectedComponent;
                    //valueReturnBtn.selectedComponentBtn = selectedComponent;

                    foreach (SelectedComponent component in componentArray) 
                    {
                        if (selectedComponent != component)
                        {
                            component.RemoveUI();
                        }
                        else 
                        {
                            cameraMovement.LookAtComponent(selectedComponent.transform);
                            component.ShowUI(Camera.main.WorldToScreenPoint(selectedComponent.transform.position));
                        }
                    }
                }
                else 
                {
                    selectedComponent.RemoveUI();
                    cameraMovement.zooming = false;
                }
            }
            else
            {
                if (selectedComponent) 
                {
                    selectedComponent.RemoveUI();
                    cameraMovement.zooming = false;
                }
            }
        }
        if (valueReturnBtn.pressedBtn)
        {
            if (selectedComponent.IndexReturn() != null)
            {
                if (selectedComponent.IndexReturn().GetComponent<MeshRenderer>().sharedMaterial == selectionMat)
                {
                    selectedComponent.IndexReturn().GetComponent<MeshRenderer>().sharedMaterial = originalMat;
                    points.Remove(selection.gameObject);
                    valueReturnBtn.pressedBtn = false;
                    return;
                }
                originalMat = selectedComponent.IndexReturn().GetComponent<MeshRenderer>().material;
                selectedComponent.IndexReturn().GetComponent<MeshRenderer>().sharedMaterial = selectionMat;
            }

            if (!points.Contains(selectedComponent.IndexReturn()) && selectedComponent.IndexReturn() != null)
            {
                points.Add(selectedComponent.IndexReturn().gameObject);
                

                if (points.Count >= 2)
                {
                    Connect();

                    if (FindObjectOfType<Tutorial>() != null)
                    {
                        FindObjectOfType<Tutorial>().CheckConnection();
                    }
                }
            }
            valueReturnBtn.pressedBtn = false;
        }
    }

        void Connect()
        {
            if (!pipe && !entrance && !exit && !body)
            {
                return;
            }
        for (int i = 0; i < points.Count; i++)
            {
                point1 = points[0].transform;
                point2 = points[1].transform;
            }

            Debug.Log(pipe.transform.name);

            Quaternion rotation1 = point1.rotation;
            Quaternion rotation2 = point2.rotation;

            GameObject pipeMain = Instantiate(pipe, transform.position, Quaternion.identity);
            //Instantiates an empty gameobject to be the parent of the pipes generated by this function

            //Instantiates both the pipes int
            //
            //to be the beginning of the system and the end
            //sets the point where they instantiate based on the selected points from the list
            //changes the rotation to make sure the pipes faces the points transform
            GameObject pipeEntrance = Instantiate(entrance, point1.transform.position, rotation1);
            pipeEntrance.transform.LookAt(point1.transform);

            Vector3 first = pipeEntrance.transform.GetChild(1).position;

            GameObject pipeExit = Instantiate(exit, point2.transform.position, rotation2);
            pipeExit.transform.LookAt(point2.transform);

            //used later.
            Vector3 second = pipeExit.transform.GetChild(1).position;

            Vector3 center = (first + second) / 2;

            float length = Vector3.Distance(first, second);

            GameObject pipeBody = Instantiate(body, center, Quaternion.identity);
            pipeBody.transform.localScale = new Vector3(pipeBody.transform.localScale.x, pipeBody.transform.localScale.y, length);
            pipeBody.transform.LookAt(first);

            pipeEntrance.transform.parent = pipeMain.transform;
            pipeExit.transform.parent = pipeMain.transform;
            pipeBody.transform.parent = pipeMain.transform;

            pipes.Add(pipeMain);

            ColourPipe(pipeMain);
            point1 = null;
            point2 = null;
            points.Clear();
        }

        void MultiHighlight()
        {
        if (!pipe && !entrance && !exit && !body)
        {
            pipeWarning = true;
        }
        else 
        {
            pipeWarning = false;
        }
            // checks if there is an object being highlighted
            // if so, remove highlight by resetting the object's material to it's original material
            if (highlight != null)
            {
                highlight.GetComponent<MeshRenderer>().material = originalMat;
                highlight = null;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#if UNITY_STANDALONE
        // checks if the raycast being drawn from the mouse hits an object
        // if so, check if the tag of the highlight is called "Connection" before setting the colour of the object's material
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
            {
                highlight = raycastHit.transform;
                if (highlight.CompareTag("Connection") && highlight != selection)
                {
                    if (highlight.GetComponent<MeshRenderer>().material != highlightMat)
                    {
                        originalMat = highlight.GetComponent<MeshRenderer>().material;
                        highlight.GetComponent<MeshRenderer>().material = highlightMat;
                    }
                }
                else
                {
                    highlight = null;
                }
            }
#endif

        if (InputSystem.Instance.LeftClick() && !EventSystem.current.IsPointerOverGameObject())
        {
            // checks if the raycast being drawn from the mouse hits an object
            // if so, check if the tag of the selection is called "Connection" before setting the colour of the object's material
            if (EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject.layer == 5)
                return;
            if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
            {
                selection = raycastHit.transform;
                if (selection.GetComponent<SelectedComponent>())
                {
                    selectedComponent = selection.GetComponent<SelectedComponent>();
                    valueReturnBtn.selectedComponentBtn = selection.GetComponent<SelectedComponent>();

                    foreach (SelectedComponent component in componentArray)
                    {
                        if (selectedComponent != component)
                        {
                            component.RemoveUI();
                            multiConnectToggle.UpdateText();
                        }
                        else
                        {
                            cameraMovement.LookAtComponent(selectedComponent.transform);
                            component.ShowUI(Camera.main.WorldToScreenPoint(selectedComponent.transform.position));

                        }
                    }
                }
                else
                {
                    selectedComponent.RemoveUI();
                    cameraMovement.zooming = false;
                }
            }
            else
            {
                    selectedComponent.RemoveUI();
                cameraMovement.zooming = false;
            }
             
        }
            if (valueReturnBtn.pressedBtn)
            {
                if (selectedComponent.IndexReturn() != null)
                {
                    if (selectedComponent.IndexReturn().GetComponent<MeshRenderer>().sharedMaterial == selectionMat)
                    {
                        selectedComponent.IndexReturn().GetComponent<MeshRenderer>().sharedMaterial = originalMat;
                        points.Remove(selection.gameObject);
                        valueReturnBtn.pressedBtn = false;
                        return;
                    }
                    originalMat = selectedComponent.IndexReturn().GetComponent<MeshRenderer>().material;
                    selectedComponent.IndexReturn().GetComponent<MeshRenderer>().sharedMaterial = selectionMat;
                }


                if (!multiPoints.Contains(selectedComponent.IndexReturn()) && selectedComponent.IndexReturn())
                {
                    multiPoints.Add(selectedComponent.IndexReturn().gameObject);
                    multiConnectToggle.UpdateText();

                    if (multiPoints.Count >= multiConnectLimit)
                    {
                        MultiConnect();
                    }

                }
            valueReturnBtn.pressedBtn = false;  
        }

        }
            
        

        void MultiConnect()
        {
        if (!pipe && !entrance && !exit && !body)
        {
            return;
        }
        for (int i = 0; i < multiPoints.Count; i++)
            {
                multiplePoints.Add(multiPoints[i].transform);
            }

            GameObject pipeMain = Instantiate(pipe, transform.position, Quaternion.identity);

            for (int i = 0; i < multiPoints.Count; i++)
            {
                GameObject pipeEntry = Instantiate(entrance, multiPoints[i].transform.position, multiPoints[i].transform.rotation);
                pipeEntry.transform.LookAt(multiPoints[i].transform);
                pipeEntry.transform.parent = pipeMain.transform;

                pipeConnection.Add(pipeEntry.transform.GetChild(1).transform.position);
            }

            for (int i = 0; i < pipeConnection.Count - 1; i++)
            {
                centerPoints.Add((pipeConnection[i] + pipeConnection[i + 1]) / 2);
            }

            for (int i = 0; i < centerPoints.Count; i++)
            {
                lengths.Add(Vector3.Distance(pipeConnection[i], pipeConnection[i + 1]));
            }

            for (int i = 0; i < centerPoints.Count; i++)
            {
                GameObject pipeBody = Instantiate(body, centerPoints[i], Quaternion.identity);
                pipeBody.transform.localScale = new Vector3(pipeBody.transform.localScale.x, pipeBody.transform.localScale.y, lengths[i]);
                pipeBody.transform.LookAt(pipeConnection[i]);
                pipeBody.transform.parent = pipeMain.transform;
            }

            pipes.Add(pipeMain);

            //pipeMain.AddComponent<ParticleFlow>();

            ColourPipe(pipeMain);
            multiPoints.Clear();
            multiplePoints.Clear();
            pipeConnection.Clear();
            centerPoints.Clear();
            lengths.Clear();
            multiConnectToggle.UpdateText();
        }

        void ColourPipe(GameObject pipeMain)
        {
            string name = "";

            if (level2AnswerSheet.ListComparison(multiPoints) || level2AnswerSheet.ListComparison(points))
            {
                if (multiConnect)
                {
                    for (int i = 0; i < multiPoints.Count; i++)
                    {
                        for (int j = 0; j < multiPoints.Count; j++)
                        {
                            if (multiPoints[i].name.Equals(multiPoints[j].name))
                            {
                                allMatch = true;
                                name = multiPoints[i].name;
                            }
                            else if (!multiPoints[i].name.Equals(multiPoints[j].name))
                            {
                                anomalyFound = true;
                            }
                        }
                    }
                }
                else
                {
                    if (point1.name.Equals(point2.name))
                    {
                        allMatch = true;
                        name = point1.name;
                    }
                    else if (!point1.name.Equals(point2.name))
                    {
                        anomalyFound = true;
                    }
                }


                if (allMatch && !anomalyFound)
                {
                    Debug.Log("everything matches");
                    renderers = pipeMain.GetComponentsInChildren<Renderer>();

                    if (name == "Connection Point 1")
                    {
                        pipeMain.name = "Connection 1";
                        foreach (Renderer renderer in renderers)
                        {
                            renderer.material.color = new Color32(0, 0, 255, 175);

                        }
                    }

                    if (name == "Connection Point 2")
                    {
                        pipeMain.name = "Connection 2";
                        foreach (Renderer renderer in renderers)
                        {
                            renderer.material.color = new Color32(0, 255, 255, 175);
                        }
                    }

                    if (name == "Connection Point 3")
                    {
                        pipeMain.name = "Connection 3";
                        foreach (Renderer renderer in renderers)
                        {
                            renderer.material.color = new Color32(255, 0, 0, 175);
                        }
                    }

                    if (name == "Connection Point 4")
                    {
                        pipeMain.name = "Connection 4";
                        foreach (Renderer renderer in renderers)
                        {
                            renderer.material.color = new Color32(232, 0, 254, 175);
                        }
                    }

                    pipeMain.AddComponent<ParticleFlow>();

                    Debug.Log("matches");
                }
                else if (anomalyFound)
                {
                    Debug.Log("anomaly present");
                    Debug.Log("no match");
                }
            }
        }
    }
