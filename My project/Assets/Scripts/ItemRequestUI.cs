using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class ItemRequestUI : MonoBehaviour
{
    public Image image;

    public Image fillImage;

    public TMPro.TMP_Text Text;

    public void Setup(FoodItem item)
    {
        image.sprite = item.Icon;
        Text.SetText(item.name);
    }

    public void SetProgress(float percentage)
    {
        fillImage.fillAmount = percentage;
    }
}
