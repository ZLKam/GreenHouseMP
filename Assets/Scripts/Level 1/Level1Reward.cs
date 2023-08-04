using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Level1Reward : MonoBehaviour
{
    public TextAsset level1RewardText;
    public GameObject instantiatePoint;
    public Camera inspectCamera;
    public LayerMask layerToCheck;

    [SerializeField]
    private GameObject LoadedObject;
    public GameObject Panel;
    public GameObject cross;
    public TextMeshProUGUI description;
    private GameObject inspectPrefab;

    [SerializeField]
    private string[] tagArray;

    [SerializeField]
    private List<string> lvl1RewardList = new List<string>();
    [SerializeField]
    private List<string> descriptionList = new List<string>();

    private int descriptionListStart = 0;
    private int descriptionListEnd = 0;

    // Start is called before the first frame update
    void Start()
    {
        inspectCamera.gameObject.SetActive(false);

        string[] lines = level1RewardText.text.Split('\n');
        lvl1RewardList = lines.ToList();

        GetComponent<Level1Reward>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        InspectComponent();
    }

    private void InspectComponent()
    {
        Ray ray = new();
        if (Input.touchCount>0)
        {
            if (!inspectCamera.gameObject.activeSelf)
            {
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            }
            else
            {
                ray = inspectCamera.ScreenPointToRay(Input.GetTouch(0).position);
            }
        }

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerToCheck))
        {
            inspectCamera.gameObject.SetActive(true);

            if (hit.transform.tag.StartsWith("Component") && !LoadedObject) 
            {
                tagArray = hit.transform.tag.Split("/");
                LoadedObject = Resources.Load<GameObject>("Prefabs/Components/" + tagArray[1] + "_Inspect");
            }
            if (LoadedObject && instantiatePoint.transform.childCount < 1)
            {
                inspectPrefab = Instantiate(LoadedObject, instantiatePoint.transform.position, Quaternion.identity);
                inspectPrefab.GetComponentInChildren<Canvas>().worldCamera = inspectCamera;
                inspectPrefab.transform.parent = instantiatePoint.transform;
                inspectPrefab.transform.localPosition = Vector3.zero;
                foreach (Button btn in inspectPrefab.GetComponentsInChildren<Button>()) 
                {
                    btn.onClick.AddListener(delegate { DisplayDescription(); });
                }
                ComponentLinesAppend(tagArray[1], "End" + tagArray[1]);
                cross.SetActive(true);
                LoadedObject = null;
            }
        }

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                //gets the previous touch position and checks the difference between the new position before applying that value as the rotation of the object
                instantiatePoint.transform.Rotate(Vector3.up, Input.GetTouch(0).deltaPosition.x, Space.World);
            }
        }

    }

    private void ComponentLinesAppend(string startString, string endString)
    //changes which part of the text file to take based on the text that seperates the segments
    {
        for (int i = 0; i < lvl1RewardList.Count; i++)
        {
            if (lvl1RewardList[i].StartsWith(startString))
            {
                descriptionListStart = i + 1;
            }
            if (lvl1RewardList[i].StartsWith(endString))
            {
                descriptionListEnd = i;
            }
        }

        for (int i = descriptionListStart; i < descriptionListEnd; i++)
        {
            descriptionList.Add(lvl1RewardList[i]);
        }

    }

    public void DisplayDescription()
    {
        string ClickedBtnName = EventSystem.current.currentSelectedGameObject.name;
        if (!Panel.activeInHierarchy)
        {
            Panel.SetActive(true);
            for (int i = 0; i < descriptionList.Count; i++)
            {
                //Debug.Log(descriptionList[i].StartsWith("[" + ClickedBtnName + "]"));
                if (descriptionList[i].StartsWith("[" + ClickedBtnName + "]"))
                {
                    int index = descriptionList[i].IndexOf("]");
                    description.text = descriptionList[i].Substring(index + 1);
                    break;
                }
            }
        }
        else
        {
            Panel.SetActive(false);
        }
    }

    public void RemoveInspect() 
    {
        Destroy(inspectPrefab);
        Panel.SetActive(false);
        cross.SetActive(false);
        inspectCamera.gameObject.SetActive(false);
    }
}
