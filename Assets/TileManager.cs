using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] int XIndex;
    [SerializeField] int YIndex;

    public string tile;

    Vector3 CameraStartPos = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        XIndex = gameManager.XIndex;
        YIndex = gameManager.YIndex;
    }

    void Start()
    {
        if (gameManager.Tile[XIndex, YIndex] != null)
        {
            tile = gameManager.Tile[XIndex, YIndex];
        }
    }

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0))
            CameraStartPos = Camera.main.transform.position;

        if (Input.GetMouseButtonUp(0) && CameraStartPos == Camera.main.transform.position)
        {
            Debug.Log(XIndex + " : " + YIndex);
            gameManager.PlaceFurniture(XIndex, YIndex, GameObject.Find("GhostBuild").GetComponent<SnapMovement>().SelectedFurniture.name);
        }
        
    }
}
