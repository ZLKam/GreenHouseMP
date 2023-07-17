using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverNode : MonoBehaviour
{
    private string rectGrid = "RectGrid";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCoveringRectGridCell(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        RecoverRectGridCell(collision);
    }

    private void CheckCoveringRectGridCell(Collider2D collision)
    {
        if (collision.CompareTag(rectGrid))
        {
            collision.GetComponent<RectGridCell>().SetNonWalkable();
        }
    }

    private void RecoverRectGridCell(Collider2D collision)
    {
        if (collision.CompareTag(rectGrid))
        {
            collision.GetComponent<RectGridCell>().SetWalkable();
        }
    }
}
