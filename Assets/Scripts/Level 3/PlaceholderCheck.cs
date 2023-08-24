using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Level3
{
    public class PlaceholderCheck : MonoBehaviour
    {
        public Sprite greenBorder;

        // Update is called once per frame
        private void Update()
        {
            /// This is attached to placeholder to prevent any error happens in some very rare cases
            /// Usually, this will not be running
            
            if (transform.childCount > 0)
            {
                if ((!transform.GetChild(0).GetComponent<ComponentEvent>().holding && transform.GetChild(0).localPosition != Vector3.zero))
                {
                    Destroy(transform.GetChild(0).gameObject);
                }

                //if (transform.childCount > 1)
                //{
                //    transform.GetComponentsInChildren<ComponentEvent>().ToList().ForEach((child) =>
                //    {
                //        if (!child.enabled)
                //            Destroy(child.gameObject);
                //    });
                //}
            }
            else
            {
                if (!GetComponent<SpriteRenderer>().enabled)
                {
                    GetComponent<SpriteRenderer>().enabled = true;
                }
                if (GetComponent<SpriteRenderer>().sprite == null)
                {
                    GetComponent<SpriteRenderer>().sprite = greenBorder;
                }
            }
        }
    }
}