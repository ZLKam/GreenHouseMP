using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestLevelScroll : MonoBehaviour
{

    public RectTransform targetImage;
    private Animator targetImageAnim;
    public RectTransform[] swapObjects;
    public float moveSpeed = 200f;

    private RectTransform canvasRectTransform;
    private bool press;

    private void Start()
    {
        canvasRectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.anyKeyDown) 
        {
            press = !press;
        }

        if (press)
        {
            foreach (RectTransform image in swapObjects)
            {
                if (image.anchoredPosition == new Vector2(900, image.anchoredPosition.y) )
                {
                    CentreMove(new Vector2(-900, image.anchoredPosition.y), image);
                }
                if(image.anchoredPosition == new Vector2(-900, image.anchoredPosition.y)) 
                {
                    CentreMove(Vector2.zero, image);
                }
                if (image.anchoredPosition == Vector2.zero)
                {
                    CentreMove(new Vector2(900, image.anchoredPosition.y), image);
                }

            }
        }
    }
    

    private void CentreMove(Vector2 desiredPosition, RectTransform image)
    {
        Vector2 moveDirection = (desiredPosition - image.anchoredPosition).normalized;

        float distanceToCenter = Vector2.Distance(image.anchoredPosition, desiredPosition);
        //image.GetComponent<Animator>().SetTrigger("Selected");

        if (distanceToCenter <= moveSpeed * Time.deltaTime)
        {
            image.anchoredPosition = desiredPosition; // Snap to the center
            return;
        }

        // Move the target image towards the center
        image.anchoredPosition += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void MoveAndSwapObjects()
    {
        Vector2 targetPosition = new Vector2(900f, targetImage.anchoredPosition.y);

        foreach (RectTransform swapObject in swapObjects)
        {
            Vector2 swapPosition = new Vector2(-swapObject.anchoredPosition.x, swapObject.anchoredPosition.y);
            swapObject.anchoredPosition = swapPosition;
        }

        StartCoroutine(MoveObject(targetImage, targetPosition));
    }

    private IEnumerator MoveObject(RectTransform obj, Vector2 targetPosition)
    {
        while (obj.anchoredPosition.x < targetPosition.x)
        {
            obj.anchoredPosition += Vector2.right * moveSpeed * Time.deltaTime;
            yield return null;
        }

        obj.anchoredPosition = targetPosition;
    }
}



