using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ComponentSelectBackgroundAnim : MonoBehaviour
{
    RectTransform rectTransform;

    [SerializeField]
    private float speed = 20f;

    [SerializeField]
    private float resetOffset = 80f;
    [SerializeField]
    private float resetDuration = 5f;

    private float originalWidth;
    private Vector3 initialPosition;

    private bool isResetting = false;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalWidth = GetComponent<Image>().sprite.rect.width;
        initialPosition = rectTransform.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isResetting)
        {
            Vector3 newPosition = rectTransform.anchoredPosition + new Vector2(1, 1) * speed * Time.deltaTime;
            if (Mathf.Abs(newPosition.x - initialPosition.x) > resetOffset || Mathf.Abs(newPosition.y - initialPosition.y) > resetOffset)
            {
                // Reset the position to the start position to create an endless loop
                //rectTransform.anchoredPosition = initialPosition;
                StartCoroutine(SmoothResetPosition());
            }
            else
            {
                // Apply the new position to the UI image
                rectTransform.anchoredPosition = newPosition;
            }
        }
    }

    private IEnumerator SmoothResetPosition()
    {
        // Set the flag to indicate that the resetting process has started
        isResetting = true;

        // Store the current position before resetting
        Vector3 currentPosition = rectTransform.anchoredPosition;

        // Calculate the target position (start position)
        Vector3 targetPosition = initialPosition;

        // Calculate the step size for the reset
        float stepSize = 0f;

        // Perform the smooth reset over the specified duration
        while (stepSize < 1f)
        {
            stepSize += Time.deltaTime / resetDuration;
            rectTransform.anchoredPosition = Vector3.Lerp(currentPosition, targetPosition, stepSize);
            yield return null;
        }

        // Resetting process is complete, set the flag back to false
        isResetting = false;
    }
}
