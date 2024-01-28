using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class ItemRequestUI : MonoBehaviour
{
    public Image image;

    public void Setup(FoodItem item)
    {
        image.sprite = item.Icon;
    }
}
