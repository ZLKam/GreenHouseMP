using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Level3
{
    public class CoverNode : MonoBehaviour
    {
        private string rectGrid = "RectGrid";

        [SerializeField]
        private List<Transform> changedCells = new();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            CheckCoveringRectGridCell(collision);
        }

        private void OnTriggerStay2D(Collider2D collision)
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
                if (CompareTag("Component") && collision.GetComponent<RectGridCell>().isWalkable)
                {
                    changedCells.Add(collision.transform);
                    collision.GetComponent<RectGridCell>().SetNonWalkable();
                    return;
                }
                collision.GetComponent<RectGridCell>().SetNonWalkable();
            }
        }

        private void RecoverRectGridCell(Collider2D collision)
        {
            if (collision.CompareTag(rectGrid))
            {
                if (CompareTag("Component"))
                {
                    foreach (Transform cell in changedCells)
                    {
                        cell.GetComponent<RectGridCell>().SetWalkable();
                    }
                    changedCells.Clear();
                }
            }
        }
    }
}
