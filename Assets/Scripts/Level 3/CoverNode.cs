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

        private void Update()
        {
            // If the gameobject is a component, and its ComponentEvent is disabled, enable it
            if (GetComponent<ComponentEvent>())
            {
                if (GetComponent<ComponentEvent>().enabled == false)
                    GetComponent<ComponentEvent>().enabled = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            CheckCoveringRectGridCell(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            RecoverRectGridCell(collision);
        }

        /// <summary>
        /// Set grid cell to non-walkable if it is covered
        /// </summary>
        /// <param name="collision"></param>
        private void CheckCoveringRectGridCell(Collider2D collision)
        {
            if (collision.CompareTag(rectGrid))
            {
                // for components, needs to know which cells are changed so that when the component is removed, the cells can be recovered
                if (CompareTag("Component") && collision.GetComponent<RectGridCell>().isWalkable)
                {
                    changedCells.Add(collision.transform);
                    collision.GetComponent<RectGridCell>().SetNonWalkable();
                    return;
                }
                collision.GetComponent<RectGridCell>().SetNonWalkable();
            }
        }

        /// <summary>
        /// Recover grid cell to walkable if it is no longer covered
        /// </summary>
        /// <param name="collision"></param>
        public void RecoverRectGridCell(Collider2D collision)
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
