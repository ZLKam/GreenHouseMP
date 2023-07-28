using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageGroup : MonoBehaviour
{
    public List<IconTab> iconTabImages;
    public TheoryBook theoryBook;
    public Image imageLocation;

    public void Subscribe(IconTab icon)
    {
        if (iconTabImages == null)
        {
            iconTabImages = new List<IconTab>();
        }

        iconTabImages.Add(icon);
    }

    public void OnTabExit(IconTab image)
    {
        ResetTabs();
    }

    public void OnTabSelected(IconTab image)
    {
        ResetTabs();
        imageLocation.transform.parent.gameObject.SetActive(true);
        imageLocation.sprite = image.tabImage.sprite;
    }

    public void ResetTabs()
    {
        imageLocation.transform.parent.gameObject.SetActive(false);
        imageLocation.sprite = null;
    }

}
