using UnityEngine;
using UnityEngine.UI;

public class ComponentWheelController : MonoBehaviour
{
    public Animator anim;
    private bool componentWheelSelected = false;
    public Image selectedItem;
    public Sprite noImage;
    public static int componentID;
    //public GameObject Text;
    public GameObject AHU;
    //public GameObject Cube;

    public Transform movePosition;

    public GameObject wheel;
    Vector3 startPosition;
    Vector3 endPosition;
    float elapsedTime;
    float desiredDuration = 3f;

    public bool ahuCheck = false;

    private void Start()
    {
        startPosition = wheel.transform.position;
        endPosition = movePosition.transform.position;

        //Text = GameObject.Find("Item Selected");
        //Text.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) 
        {
            //Text.SetActive(true);
            componentWheelSelected = !componentWheelSelected;
        }

        if (componentWheelSelected)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / desiredDuration;

            wheel.transform.position = Vector3.Lerp(startPosition, endPosition, percentageComplete);
            Mathf.Round(wheel.transform.position.x);
            //anim.SetBool("OpenWheel", true);
        }
        if(!componentWheelSelected && wheel.transform.position != startPosition)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / desiredDuration;

            wheel.transform.position = Vector3.Lerp(endPosition, startPosition, percentageComplete);
            Mathf.Round(wheel.transform.position.x);
            //anim.SetBool("OpenWheel", false);
        }

        switch (componentID)
        {
            case 0: // nothing selected
                selectedItem.sprite = noImage;
                break;
            case 1: // AHU                
                if (!ahuCheck)
                {
                    Debug.Log("AHU SPAWNED");
                    AHU.transform.localScale = new Vector3(100, 100, 100);
                    //Cube.gameObject.SetActive(false);
                    //Instantiate(AHU, Cube.transform.position, Quaternion.Euler(0f, 90f, 0f));
                    ahuCheck = true;
                }
                break;
        }
    }
}
