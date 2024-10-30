using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class InfoButtonHover : MonoBehaviour, ITooltip
{

    [SerializeField] float tooltipWidth;

    public void TooltipText()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<u>UPGRADES</u> <line-height=135%><br>");
        sb.Append("<line-height=100%>This is the upgrades tab where you can purchase upgrades to increase your GC per click.<line-height=135%><br>");
        sb.Append("<line-height=100%> <u>Note:</u> upgrade do not need to be equipped for their multipier to take affect.");

        gameObject.GetComponent<SpawnTooltip>().SetTooltipText(sb.ToString(), tooltipWidth);
    }
    
    public void OnCursorHover()
    {
        TooltipText();
    }

    //sets current gameobjects position to the mouse position  
}
