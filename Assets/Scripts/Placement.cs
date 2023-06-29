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
    private bool allowDelete;
    private int layerMaskComponent = 1 << 6;

    GameObject detelableGameobject;

    // Update is called once per frame
    void Update()
    {
        //HighlightPlacement();
        //HighlightComponent();
        //not sure if needed
        if (!allowDelete && Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                allowDelete = true;
            }
        }
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
            //instatiates the prefab of the game object and sets the prefab the child of the selected box
            //turns off the selected box, renderer and box collider, not visible after the prefab is placed down
            allowDelete = false;
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
        //no components has been detected yet
        {
            Debug.Log("Please select a component first");
            //if there is no selected game object yet, it will set the new one based on the raycast hit, and sets the material
            if (selected == null)
            //brand new selected object(green squares)
            //since there is no previous selected, it will set the object hit by the raycast to become selectionMat, which is a darker green
            {
                selected = selectedTransform;
                selectedTransform.GetComponent<MeshRenderer>().material = selectionMat;
            }
            else
            //if there was a previous selected object
            //return the material back to the orignal material from selected mat
            //reapplies material to the new selected object
            {
                selected.GetComponent<MeshRenderer>().material = originalMat;
                selected = selectedTransform;
                selectedTransform.GetComponent<MeshRenderer>().material = selectionMat;
            }
        }
    }

    private void Highlight()
    //handles turning the selected object(green boxes) yellow(highlighted)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out RaycastHit hit))
        //constantly generates a raycast fromm the mouse position
        //checks if the mouse is over a game object and the returns the hit object using raycast
        {
            if (InputSystem.Instance.LeftClick())
            //if there is a left click or a touch detected
            {
                if (!hit.transform.CompareTag("Selection"))
                //the game object is not the green boxes it will run the delete function
#if UNITY_ANDROID
                {
                    Delete(hit.transform);
                    return;
                }
#endif          
                //if not it will run the select function
                Select(hit.transform);
                return;
            }
            //not sure if needed
            else if (Input.touchCount > 0 && allowDelete)
            {
                Delete(hit.transform);
                return;
            }
#if UNITY_STANDALONE
            if (InputSystem.Instance.RightClick())
            //right click the game object to delete the game object
            {
                Delete(hit.transform);
                return;
            }
#endif
            // Change color of the component to highlight material
            if (hit.transform.CompareTag("Selection"))
            //when the raycast returns the tag of the gameobject it hits and it matches the tag for the green boxes
            {
                if (hit.transform.GetComponent<MeshRenderer>().material != highlightMat && hit.transform != selected)
                //as long as the hit gameobject is not already the material for highlight or detected
                //it will change the material to the highlight material
                {
                    hit.transform.GetComponent<MeshRenderer>().material = highlightMat;
                }
            }
            else
            //handles returning all green boxes back to the original material except the highlighted one
            {
                GameObject[] selections = GameObject.FindGameObjectsWithTag("Selection");
                foreach (GameObject selection in selections)
                {
                    // if the selection is not in the selected array, change the material back to original material
                    if (selected == null || selection.transform != selected)
                    {
                        selection.GetComponent<MeshRenderer>().material = originalMat;
                    }
                }
            }
        }
        else
        //handles returning all green boxes back to the orignal mat as long as none of them are currently being hovered over by the mouse
        {
            if (InputSystem.Instance.LeftClick())
            //resets the variable that stores the currently hit green box to null
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
    //handles deletion of the component gameobjects
    {
        //null exception
        if (selectedTransform.gameObject == null)
            return;
#if UNITY_STANDALONE
        //gets the hit gamebject from the raycast(passed in through the parameter)
        //checks the tags to ensure its one of the components
        //component is a child to the green boxes
        //sets the green boxes to a visible(Renderer) and interactable(Box Collider) state
        //destroys the component
        if (selectedTransform.CompareTag("Component/Chiller") || selectedTransform.CompareTag("Component/AHU") ||
            selectedTransform.CompareTag("Component/CoolingTower") || selectedTransform.CompareTag("Component/CwpOpt") ||
            selectedTransform.CompareTag("Component/CwpOptElavated"))
        {
            selectedTransform.parent.GetComponent<Renderer>().enabled = true;
            selectedTransform.parent.GetComponent<BoxCollider>().enabled = true;
            Destroy(selectedTransform.gameObject);
        }
    
#endif
#if UNITY_ANDROID
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        //once the touch is detected
        {
            //returns the component which the gameobject matches the tag
            if (selectedTransform.CompareTag("Component/Chiller") || selectedTransform.CompareTag("Component/AHU") ||
            selectedTransform.CompareTag("Component/CoolingTower") || selectedTransform.CompareTag("Component/CwpOpt") ||
            selectedTransform.CompareTag("Component/CwpOptElavated"))
            {
                detelableGameobject = selectedTransform.gameObject;
            }
        }
        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        //once the finger starts moving
        {
            //Set the vector finger point from the screeen into a world point
            //set the gameobject we referenced to the world point
            Vector3 tempPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 50));
            detelableGameobject.transform.position = tempPos;

            Debug.Log("moving");
        }
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        //no longer detecting the touch
        {
            // check if dragged out, if out, delete
            //null error check
            if (detelableGameobject == null)
                return;
            //creates a raycast, ignores the component layer and checks if it does not hit any object with a collider
            //if there is no collider will delete the gameobject as it is outside the boundaries
            //if the raycast hits a collider, it will return the game object to its parent transform(green boxes)
            if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).position), out RaycastHit hit, Mathf.Infinity, ~layerMaskComponent))
            {
                detelableGameobject.transform.parent.GetComponent<Renderer>().enabled = true;
                detelableGameobject.transform.parent.GetComponent<BoxCollider>().enabled = true;
                Destroy(selectedTransform.gameObject);
            }
            else
            {
                selectedTransform.gameObject.transform.localPosition = Vector3.zero;
            }
            Debug.Log("stop");
        }
#endif
    }
}
