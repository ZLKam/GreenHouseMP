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
    public Material originalMat;
    Transform highlight;
    Transform selection;
    RaycastHit raycastHit;
    Transform selected;

    public bool tutorial;

    GameObject temp;

    // Update is called once per frame
    void Update()
    {
        //HighlightPlacement();
        //HighlightComponent();
        Highlight();
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

        if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject())
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

        if (Input.GetKeyDown(KeyCode.Mouse1) && !EventSystem.current.IsPointerOverGameObject())
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
            }
        }
    }

    private void Select(Transform selectedTransform)
    {
        if (selectedPrefab != null)
        {
            GameObject component = Instantiate(selectedPrefab, selectedTransform.position, selectedPrefab.transform.rotation);
            component.transform.parent = selectedTransform;

            selectedTransform.GetComponent<Renderer>().enabled = false;
            selectedTransform.GetComponent<BoxCollider>().enabled = false;

            if (tutorial)
            {
                FindObjectOfType<Tutorial>().CheckPlacement();
            }
        }
        else
        {
            Debug.Log("Please select a component first");
            if (selected == null)
            {
                selected = selectedTransform;
                selectedTransform.GetComponent<MeshRenderer>().material = selectionMat;
            }
            else
            {
                selected.GetComponent<MeshRenderer>().material = originalMat;
                selected = selectedTransform;
                selectedTransform.GetComponent<MeshRenderer>().material = selectionMat;
            }
        }
    }

    private void Highlight()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out RaycastHit hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!hit.transform.CompareTag("Selection"))
                    return;
                Select(hit.transform);
                return;
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Debug.Log("right click");
                Delete(hit.transform);
                return;
            }
            // Change color of the component to highlight material
            if (hit.transform.CompareTag("Selection"))
            {
                if (hit.transform.GetComponent<MeshRenderer>().material != highlightMat && hit.transform != selected)
                {
                    hit.transform.GetComponent<MeshRenderer>().material = highlightMat;
                }
            }
            else
            {
                GameObject[] selections = GameObject.FindGameObjectsWithTag("Selection");
                foreach (GameObject selection in selections)
                {
                    // if the selection is not in the selected list, change the material back to original material
                    if (selected == null || selection.transform != selected)
                    {
                        selection.GetComponent<MeshRenderer>().material = originalMat;
                    }
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                selected = null;
            }
            if (originalMat != null)
            {
                GameObject[] selections = GameObject.FindGameObjectsWithTag("Selection");
                foreach (GameObject selection in selections)
                {
                    // if the selection is not in the selected list, change the material back to original material
                    if (selected == null || selection.transform != selected)
                    {
                        selection.GetComponent<MeshRenderer>().material = originalMat;
                    }
                }
            }
        }
    }

    private void Delete(Transform selectedTransform)
    {
#if UNITY_STANDALONE
        if (selectedTransform.CompareTag("Component/Chiller") || selectedTransform.CompareTag("Component/AHU") ||
            selectedTransform.CompareTag("Component/CoolingTower") || selectedTransform.CompareTag("Component/CwpOpt") ||
            selectedTransform.CompareTag("Component/CwpOptElavated"))
        {
            selectedTransform.parent.GetComponent<Renderer>().enabled = true;
            selectedTransform.parent.GetComponent<BoxCollider>().enabled = true;
            Destroy(selectedTransform.gameObject);
        }
    }
#endif
#if UNITY_ANDROID
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (selectedTransform.CompareTag("Component/Chiller") || selectedTransform.CompareTag("Component/AHU") ||
            selectedTransform.CompareTag("Component/CoolingTower") || selectedTransform.CompareTag("Component/CwpOpt") ||
            selectedTransform.CompareTag("Component/CwpOptElavated"))
            {
                //selectedTransform.parent.GetComponent<Renderer>().enabled = true;
                //selectedTransform.parent.GetComponent<BoxCollider>().enabled = true;
                //Destroy(selectedTransform.gameObject);
                temp = selectedTransform.gameObject;
            }
        }
        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            // gameobject follow position
            //gameobject displayed on canvas?
            //get game object to display in front of screen
            Vector3 tempPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 50));
            temp.transform.position = tempPos;
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
