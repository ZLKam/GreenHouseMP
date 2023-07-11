using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineLimit : MonoBehaviour
{
    [SerializeField]
    private bool allow = true;

    public bool AllowDrawLine
    {
        get { return allow; }
        set { allow = value; }
    }
}
