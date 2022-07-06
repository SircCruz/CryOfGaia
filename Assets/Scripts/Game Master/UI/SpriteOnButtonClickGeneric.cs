using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteOnButtonClickGeneric : MonoBehaviour
{
    public Image[] genericButton;
    public Sprite[] buttonSprite;
    public Sprite[] buttonSpritePressed;

    public void Normal(int type)
    {
        for(int i = 0; i < genericButton.Length; i++)
        {
            if(i == type)
            {
                genericButton[i].sprite = buttonSprite[i];
            }
        }
    }
    public void Pressed(int type)
    {
        for (int i = 0; i < genericButton.Length; i++)
        {
            if (i == type)
            {
                genericButton[i].sprite = buttonSpritePressed[i];
            }
        }
    }
}
