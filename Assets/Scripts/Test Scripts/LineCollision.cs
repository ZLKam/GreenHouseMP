using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LineCollision : MonoBehaviour
{
    internal Transform origin;
    internal Transform destination;

    [SerializeField]
    private GameObject line;

    [SerializeField]
    private Transform parent;
    private bool isTriggerring = false;
    private bool instantiatedLine = false;

    private void Start()
    {
        if (CompareTag("LineParent"))
        {
            parent = null;
        }
        else
        {
            parent = transform.parent;
        }
        StartCoroutine(DrawDelayCoroutine());
    }

    private IEnumerator DrawDelayCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        if (parent != null)
        {
            if (isTriggerring || parent.GetComponent<LineCollision>().isTriggerring)
            {
                // Find other path
                yield break;
            }
            if (!instantiatedLine)
            {
                GameObject line = Instantiate(this.line, transform.position, Quaternion.identity);
                line.transform.localScale = transform.localScale;
                line.transform.rotation = transform.rotation;
                line.name = name;
                line.transform.parent = transform.parent;
                instantiatedLine = true;
                Destroy(gameObject);
            }
        }
        else
        {
            yield break;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (parent != null)
        {
            if (transform.parent == collision.transform.parent || collision.transform == origin || collision.transform == destination)
            {
                return;
            }
            Debug.Log(name + " is trigger with " + collision.gameObject.name);
            isTriggerring = true;
            parent.GetComponent<LineCollision>().isTriggerring = true;
        }
    }
}
