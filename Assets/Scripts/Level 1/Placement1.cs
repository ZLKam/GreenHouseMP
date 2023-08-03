using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Placement1 : MonoBehaviour
{

    public TextMeshProUGUI testingText;

    public CameraMovement cameraMovement;
    public Level1AnswerSheet1 answerSheet1;
    public HoverGroup hoverGroup;

    public Sprite highlightSprite;
    public Sprite selectionSprite;
    public Sprite originalSprite;

    public GameObject component;
    [HideInInspector]
    public GameObject selectedPrefab;
    Transform highlightedPlacement;
    GameObject[] selections;
    public GameObject[] duplicationPoints;

    private bool allowDelete;

    private GameObject deletableGameobject;
    public LayerMask layerToIgnore;
    public bool deletingObject;

    public GameObject objectToCheck;

    // Start is called before the first frame update
    void Start()
    {
        selections = GameObject.FindGameObjectsWithTag("Selection");
        allowDelete = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Highlight();
        //AutoPlace(objectToCheck.transform);

    }

    private void Highlight()
    {
        Ray ray = new();
        if (Input.touchCount != 0)
        {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        }

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~layerToIgnore))
        {
            //Debug.Log(hit.transform.name);
            DeleteComponent(hit.transform);

            foreach (GameObject selectPoints in selections)
            {
                if (selectPoints == hit.collider.gameObject)
                {
                    selectPoints.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = highlightSprite;
                }
                else
                {
                    selectPoints.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = originalSprite;
                }
                continue;
            }


            if (hit.transform.CompareTag("Selection") && hit.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite == highlightSprite)
            {
                highlightedPlacement = hit.collider.transform;
                allowDelete = false;
            }
            else
            {
                //highlightedPlacement = null;
                allowDelete = true;
            }

        }
        else
        {
            if (Input.touchCount == 0)
            {
                if (highlightedPlacement)
                {
                    foreach (GameObject dupepoint in duplicationPoints)
                    {
                        //Debug.Log(highlightedPlacement.gameObject.name + "&" + dupepoint);
                        if (dupepoint.name == highlightedPlacement.gameObject.name)
                        {
                            AutoPlace(dupepoint.transform , false);
                            highlightedPlacement = null;
                            return;
                        }
                        else
                        {
                            PlaceComponent(highlightedPlacement);
                        }
                    }
                }
                else
                {
                    DeleteComponent();
                }
            }
        }


    }

    private void AutoPlace(Transform objectToCheck, bool delete) 
    {
        if (!objectToCheck)
            return;

        Debug.Log(objectToCheck.childCount > 1);
        if (objectToCheck.childCount > 1 && objectToCheck.GetChild(1).gameObject && !delete)
        {
            foreach (GameObject dupepoint in duplicationPoints)
            {
                if (dupepoint.transform.childCount < 2)
                {
                    GameObject temp = Instantiate(selectedPrefab, dupepoint.transform.position, objectToCheck.GetChild(1).rotation);
                    temp.transform.parent = dupepoint.transform;
                    temp.transform.localScale = objectToCheck.GetChild(1).localScale;
                    temp.transform.localPosition = Vector3.zero;
                    temp.GetComponent<Collider>().enabled = false;

                    dupepoint.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = originalSprite;
                    dupepoint.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                    dupepoint.transform.GetComponent<BoxCollider>().enabled = false;
                    dupepoint.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                    dupepoint.transform.GetChild(0).GetComponent<Animator>().SetBool("ObjectPlaced", true);
                }

            }
        }
        else if(delete)
        {
            foreach (GameObject dupepoint in duplicationPoints) 
            {
                if (dupepoint.transform.childCount > 1)
                {
                    Destroy(dupepoint.transform.GetChild(1).gameObject);
                    dupepoint.GetComponent<BoxCollider>().enabled = true;
                    dupepoint.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                    dupepoint.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                    dupepoint.transform.GetChild(0).GetComponent<Animator>().SetBool("ObjectPlaced", false);
                }
            }
        }
    }

    private void PlaceComponent(Transform selectedTransform) 
    {
        if (!highlightedPlacement)
            return;

        if (!component) 
        {
            component = Instantiate(selectedPrefab, selectedTransform.position, selectedPrefab.transform.rotation);
        }

        component.transform.parent = selectedTransform;
        component.transform.localPosition = Vector3.zero;
        component.GetComponent<Collider>().enabled = true;

        selectedTransform.GetChild(0).GetComponent<SpriteRenderer>().sprite = originalSprite;
        selectedTransform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        selectedTransform.GetComponent<BoxCollider>().enabled = false;
        selectedTransform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        selectedTransform.GetChild(0).GetComponent<Animator>().SetBool("ObjectPlaced", true);

        if (component.CompareTag("Component/CwpOpt") && selectedTransform.name.Equals("Selection Point (17)"))
        {
            Vector3 scaleTemp = component.transform.localScale;
            scaleTemp.x *= -1;
            component.transform.localScale = scaleTemp;
        }
        else if (component.CompareTag("Component/CwpOptElavated") && selectedTransform.name.Equals("Selection Point (16)"))
        {
            Vector3 scaleTemp = component.transform.localScale;
            scaleTemp.z *= -1;
            component.transform.localScale = scaleTemp;
        }

    }

    private void DeleteComponent(Transform selectedTransform = null) 
    {
        if (!selectedTransform && allowDelete && component && !deletableGameobject)
        {
            Destroy(component);
            return;
        }

        if (selectedTransform && !deletableGameobject && selectedTransform.tag.StartsWith("Component"))
        {
            deletableGameobject = selectedTransform.gameObject;
        }

        if (Input.touchCount > 0 && deletableGameobject)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                deletableGameobject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, Camera.main.nearClipPlane + 150));
                cameraMovement.allowRotation = false;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
            //no longer detecting the touch
            {
                if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).position), out RaycastHit hit, Mathf.Infinity, ~(1 << 6)))
                {
                    deletableGameobject.transform.parent.GetComponent<BoxCollider>().enabled = true;
                    deletableGameobject.transform.parent.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                    deletableGameobject.transform.parent.GetChild(0).GetChild(0).gameObject.SetActive(true);
                    deletableGameobject.transform.parent.GetChild(0).GetComponent<Animator>().SetBool("ObjectPlaced", false);
                    Destroy(selectedTransform.gameObject);
                    highlightedPlacement = null;
                    AutoPlace(deletableGameobject.transform.parent, true);
                }
                else
                {
                    deletableGameobject.gameObject.transform.localPosition = Vector3.zero;
                }
            }
        }

    }
}
