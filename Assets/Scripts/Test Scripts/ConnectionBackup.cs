using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConnectionBackup : MonoBehaviour
{
    public Material highlightMat;
    public Material selectionMat;
    public List<GameObject> points;

    public GameObject pipe;
    public GameObject entrance;
    public GameObject exit;
    public GameObject body;

    Transform point1;
    Transform point2;

    Material originalMat;
    Transform highlight;
    Transform selection;
    RaycastHit raycastHit;

    // Update is called once per frame
    void Update()
    {
        Highlight();
    }

    void Highlight()
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

        if (Input.GetKey(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject())
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
                if (selection.CompareTag("Connection"))
                {
                    selection.GetComponent<MeshRenderer>().material = selectionMat;

                    if (!points.Contains(selection.gameObject))
                    {
                        points.Add(selection.gameObject);

                        if (points.Count >= 2)
                            Connect();
                    }
                }
                else
                {
                    points.Remove(selection.gameObject);
                    selection = null;
                }
            }
        }
    }

    void Connect()
    {
        for (int i = 0; i < points.Count; i++)
        {
            point1 = points[0].transform;
            point2 = points[1].transform;
        }

        Quaternion rotation1 = point1.rotation;
        Quaternion rotation2 = point2.rotation;

        GameObject pipeMain = Instantiate(pipe, transform.position, Quaternion.identity);

        GameObject pipeEntrance = Instantiate(entrance, point1.transform.position, rotation1);
        pipeEntrance.transform.LookAt(point1.transform);

        Vector3 first = pipeEntrance.transform.GetChild(1).transform.position;

        GameObject pipeExit = Instantiate(exit, point2.transform.position, rotation2);
        pipeExit.transform.LookAt(point2.transform);

        Vector3 second = pipeExit.transform.GetChild(1).transform.position;

        Vector3 center = (first + second) / 2;

        float length = Vector3.Distance(first, second);

        GameObject pipeBody = Instantiate(body, center, Quaternion.identity);
        pipeBody.transform.localScale = new Vector3(pipeBody.transform.localScale.x, pipeBody.transform.localScale.y, length);
        pipeBody.transform.LookAt(first);

        pipeEntrance.transform.parent = pipeMain.transform;
        pipeExit.transform.parent = pipeMain.transform;
        pipeBody.transform.parent = pipeMain.transform;

        point1 = null;
        point2 = null;
        points.Clear();
    }
}
