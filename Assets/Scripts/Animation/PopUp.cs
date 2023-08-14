using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    float width;
    float height;
    public float minSize;
    public float maxSize;
    public float timer;
    public Vector3 popSpeed;
    Vector3 currentSize;
    Rect rect;
    
    public Image popImage;
    public Fade fade;
    public bool pop;
    public bool wrong;

    [SerializeField]
    private bool popAtBeginning;

    [SerializeField]
    private GameObject rewardHintPop;

    
    // Start is called before the first frame update
    void Start()
    {
        pop = true;
        currentSize = popImage.rectTransform.localScale;

        rect = popImage.rectTransform.rect;
        width = rect.width;
        height = rect.height;

    }

    // Update is called once per frame
    void Update()
    {
        Pop();
    }

    void Pop()
    {
        if (pop)
        {
            if (!wrong && fade != null)
            {
                fade.darken = true;
            }
            
            //they do all of this just for an animation
            //check local scale and set the max size to be bigger than the gameobject
            if(popImage.rectTransform.localScale.x < maxSize)
            {
                popImage.rectTransform.localScale += popSpeed;

                if(popImage.rectTransform.localScale.x >= maxSize)
                {
                    pop = false;
                }
            }
        }
        else
        {
            if (popImage.rectTransform.localScale.x > minSize)
            {
                popImage.rectTransform.localScale -= popSpeed;

                if(popImage.rectTransform.localScale.x <= minSize && wrong)
                {
                    StartCoroutine(hidePopUp());
                }
            }
        }
    }

    IEnumerator hidePopUp()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    pop = true;
                    gameObject.SetActive(false);

                    yield return null;
                }
            }
        }
        yield return new WaitForSeconds(timer);
        pop = true;
        gameObject.SetActive(false);
    }

    public void hide()
    {
        if (fade.darken)
        {
            fade.darken = false;
        }

        pop = true;
        gameObject.SetActive(false);
    }

    public void Return()
    {
        FindObjectOfType<AudioManager>().Play("Click");

        pop = true;
        fade.darken = false;
        gameObject.SetActive(false);


        if (rewardHintPop)
        {
            rewardHintPop.SetActive(true);
        }
    }
}
