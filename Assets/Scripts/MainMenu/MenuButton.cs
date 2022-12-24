using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Image diamond;
    public Sprite textNormal;
    public Sprite textHovor;

    Image img;

    void Start()
    {
        img = GetComponent<Image>();
    }


    public void showDiamond()
    {
        diamond.enabled = true;
        if (textHovor != null)
            img.sprite = textHovor;
    }

    public void hideDiamond()
    {
        diamond.enabled = false;
        if (textHovor != null)
            img.sprite = textNormal;
    }

    public void showCredits()
    {
        diamond.enabled = true;
    }

    public void hideCredits()
    {
        diamond.enabled = false;
    }

}
