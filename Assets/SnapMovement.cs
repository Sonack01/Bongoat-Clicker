using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SnapMovement : MonoBehaviour
{
    public FurnitureScriptableObject SelectedFurniture;


    public Vector3 worldPosition;

    public float x;
    public float y;

    public GameObject DebugCursor;

    // Update is called once per frame
    void Update()
    {
        //moves furniture to mouse 
        gameObject.GetComponent<SpriteRenderer>().sprite = SelectedFurniture.sprite;

        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        DebugCursor.transform.position = new Vector2(worldPosition.x, worldPosition.y);

        if (SelectedFurniture.width % 2 != 0)
            x = (int)worldPosition.x + -0.5f;
        else
            x = (int)worldPosition.x;

        if (SelectedFurniture.height % 2 == 0)
            y = (int)worldPosition.y + -0.5f;
        else
            y = (int)worldPosition.y;

        transform.position = new Vector3(x + (SelectedFurniture.width % 2), y + SelectedFurniture.height, 0.1f);
    }
}