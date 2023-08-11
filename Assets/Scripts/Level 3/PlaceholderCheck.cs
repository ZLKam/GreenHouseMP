using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level3
{
    public class PlaceholderCheck : MonoBehaviour
    {
        public Sprite greenBorder;

        // Update is called once per frame
        void Update()
        {
            if (transform.childCount > 0)
            {
                if (!transform.GetChild(0).GetComponent<ComponentEvent>().holding || !transform.GetChild(0).GetComponent<ComponentEvent>().enabled)
                {
                    if (transform.GetChild(0).transform.localPosition != Vector3.zero)
                    {
                        Destroy(transform.GetChild(0).gameObject);
                    }
                }
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