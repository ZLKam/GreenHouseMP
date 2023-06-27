using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Placement : MonoBehaviour
{
    public Material highlightMat;
    public Material selectionMat;

    [HideInInspector]
    public GameObject selectedPrefab;
    Material originalMat;
    Transform highlight;
    Transform selection;
    RaycastHit raycastHit;

    public bool tutorial;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HighlightPlacement();
        HighlightComponent();
    }

    void HighlightPlacement()
    {
        // checks if there is an object being highlighted
        // if so, remove highlight by resetting the object's material to it's original material
        if (highlight != null)
        {
            highlight.GetComponent<MeshRenderer>().material = originalMat;
            highlight = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // checks if the raycast being drawn from the mouse hits an object
        // if so, check if the tag of the highlight is called "Connection" before setting the colour of the object's material
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {
            highlight = raycastHit.transform;
            if (highlight.CompareTag("Selection") && highlight != selection)
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

        if (InputSystem.Instance.LeftClick() && !EventSystem.current.IsPointerOverGameObject())
        {
            // checks if there is an object being selected
            // if so, remove selection by resetting the object's material to it's original material
            if (selection != null)
            {
                selection.GetComponent<MeshRenderer>().material = originalMat;
                selection = null;
            }

            // checks if the raycast being drawn from the mouse hits an object
            // if so, check if the tag of the selection is called "Connection" before setting the colour of the object's material
            if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
            {
                selection = raycastHit.transform;
                if (selection.CompareTag("Selection"))
                {
                    selection.GetComponent<MeshRenderer>().material = selectionMat;

                    if (selectedPrefab != null)
                    {
                        GameObject component = Instantiate(selectedPrefab, selection.transform.position, selectedPrefab.transform.rotation);
                        component.transform.parent = selection;

                        selection.GetComponent<Renderer>().enabled = false;
                        selection.GetComponent<BoxCollider>().enabled = false;

                        if (tutorial)
                        {
                            FindObjectOfType<Tutorial>().CheckPlacement();
                        }
                    }
                    else
                    {
                        Debug.Log("Please Select A Component First");
                    }

                }
                else
                {
                    selection = null;
                }
            }
        }
    }

    void HighlightComponent()
    {
        // checks if there is an object being highlighted
        // if so, remove highlight by resetting the object's material to it's original material
        if (highlight != null)
        {
            highlight.GetComponent<MeshRenderer>().material = originalMat;
            highlight = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // checks if the raycast being drawn from the mouse hits an object
        // if so, check if the tag of the highlight is called "Connection" before setting the colour of the object's material
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {
            highlight = raycastHit.transform;
            if (highlight.CompareTag("Selection") && highlight != selection)
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

        if (InputSystem.Instance.RightCLick() && !EventSystem.current.IsPointerOverGameObject())
        {
            // checks if there is an object being selected
            // if so, remove selection by resetting the object's material to it's original material
            if (selection != null)
            {
                selection.GetComponent<MeshRenderer>().material = originalMat;
                selection = null;
            }

            // checks if the raycast being drawn from the mouse hits an object
            // if so, check if the tag of the selection is called "Connection" before setting the colour of the object's material
            if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
            {
                selection = raycastHit.transform;
                DeleteSelection();
            }
        }
    }

    void DeleteSelection()
    {
#if UNITY_STANDALONE
        if (selection.CompareTag("Component/Chiller") || selection.CompareTag("Component/AHU") || selection.CompareTag("Component/CoolingTower") || selection.CompareTag("Component/CwpOpt") || selection.CompareTag("Component/CwpOptElavated"))
        {
            selection.transform.parent.GetComponent<Renderer>().enabled = true;
            selection.transform.parent.GetComponent<BoxCollider>().enabled = true;
            Destroy(selection.gameObject);

            /*
            selection.GetComponent<MeshRenderer>().material = selectionMat;

            if (selectedPrefab != null)
            {
                GameObject component = Instantiate(selectedPrefab, selection.transform.position, selectedPrefab.transform.rotation);
                component.transform.parent = selection;
            }
            else
            {
                Debug.Log("Please Select A Component First");
            }
            */
        }
        else
        {
            selection = null;
        }
#endif
#if UNITY_ANDROID
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (selection.CompareTag("Component/Chiller") || selection.CompareTag("Component/AHU") || selection.CompareTag("Component/CoolingTower") || selection.CompareTag("Component/CwpOpt") || selection.CompareTag("Component/CwpOptElavated"))
            {
                Debug.Log("drw out");
            }
        }
        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            // gameobject follow position
            Debug.Log("moving");
        }
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            // check if dragged out, if out, delete
            Debug.Log("stop");
        }
#endif
    }
}
