using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TooltipOffset : MonoBehaviour
{
    // Start is called before the first frame update
    void LateUpdate()
    {
            gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-gameObject.GetComponent<RectTransform>().rect.width / 2, 0);
    }
}
