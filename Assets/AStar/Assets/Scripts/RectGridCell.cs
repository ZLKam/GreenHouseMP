using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The objective of this script is to be able to change the 
// visualisation of the cell.
public class RectGridCell : MonoBehaviour
{
    RectGrid rectGrid = new();

    [SerializeField]
    SpriteRenderer innerSprite;
    [SerializeField]
    SpriteRenderer outerSprite;

    public Vector2Int index = Vector2Int.zero;
    public bool isWalkable = true;

    public void SetInnerColor(Color col)
    {
        innerSprite.color = col;
    }

    public void SetOuterColor(Color col)
    {
        outerSprite.color = col;
    }

    public void SetWalkable()
    {
        isWalkable = true;
        SetInnerColor(rectGrid.WalkableColor);
    }

    public void SetNonWalkable()
    {
        isWalkable = false;
        SetInnerColor(rectGrid.NonWalkableColor);
    }
}
