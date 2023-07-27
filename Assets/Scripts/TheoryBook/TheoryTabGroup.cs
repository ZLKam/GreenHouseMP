using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheoryTabGroup : MonoBehaviour
{
    public List<TheoryTab> tabButtons;

    public Sprite selectedSprite, unselectedSprite;

    public TheoryBook theoryBook;

    public void Subscribe(TheoryTab button) 
    {
        if (tabButtons == null) 
        {
            tabButtons = new List<TheoryTab>();
        }

        tabButtons.Add(button);
    }

    public void OnTabExit(TheoryTab button) 
    {
        ResetTabs();
    }

    public void OnTabSelected(TheoryTab button) 
    {
        ResetTabs();
        button.tabImage.sprite = selectedSprite;
        theoryBook.Initialize();
    }

    public void ResetTabs() 
    {
        foreach (TheoryTab button in tabButtons) 
        {
            button.tabImage.sprite = unselectedSprite;
        }
    }
}
