using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SpawnTooltip : MonoBehaviour
{

    GameObject Tooltip;
    TextMeshProUGUI TooltipTMPro;
    RectTransform TooltipRT;

    string TooltipText;
    float widthOffset;

    private void Start()
    {
        Tooltip = GameObject.Find("Tooltip");
        TooltipRT = Tooltip.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        TooltipTMPro = Tooltip.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void ConfigureTooltip()
    {
        TooltipRT.sizeDelta = new Vector2(218 + widthOffset, TooltipRT.rect.height);
        TooltipTMPro.text = TooltipText;

        Tooltip.gameObject.transform.GetComponent<RectTransform>().position = new Vector2(gameObject.transform.GetComponent<RectTransform>().position.x - gameObject.transform.GetComponent<RectTransform>().rect.width / 2 - 8, gameObject.transform.GetComponent<RectTransform>().position.y);
    }

    public void OnCursorLeave()
    {
        Tooltip.GetComponent<RectTransform>().localPosition = new Vector2(28, 889);

        TooltipText = "No Text";
        //TooltipRT.sizeDelta = new Vector2(218, TooltipRT.rect.height);
    }

    public void SetTooltipText(string TooltipTextFromShopitem)
    {
        TooltipText = TooltipTextFromShopitem;
        widthOffset = 0;

        ConfigureTooltip();
    }

    public void SetTooltipText(string TooltipTextFromShopitem, float tooltipSizeOffset)
    {
        TooltipText = TooltipTextFromShopitem;
        widthOffset = tooltipSizeOffset;

        ConfigureTooltip();
    }
}
