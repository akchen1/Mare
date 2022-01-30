using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuController : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showDiamond()
    {
        diamond.enabled = true;
        img.sprite = textHovor;
    }

    public void hideDiamond()
    {
        diamond.enabled = false;
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
