using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentCoverNode : MonoBehaviour
{
    private string rectGrid = "RectGrid";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(rectGrid))
        {
            collision.GetComponent<RectGridCell>().SetInnerColor(Color.red);
        }
    }
}
