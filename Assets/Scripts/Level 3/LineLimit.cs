using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineLimit : MonoBehaviour
{
    [SerializeField]
    private bool allow = true;

    /// <summary>
    /// Check if still can draw line from this connection point
    /// </summary>
    public bool AllowDrawLine
    {
        get { return allow; }
        set { allow = value; }
    }
}
