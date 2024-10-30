using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, ISaveLoad
{

    [SerializeField] Vector2 Origin;

    public int XIndex;
    public int YIndex;

    public string[,] Tile;
    public int width;
    public int height;

    FurnitureScriptableObject[] avaliableTIles;

    [SerializeField] GameObject TilePrefab;

    [SerializeField] SnapMovement SnapMovementScript;

    SaveLoad saveload;

    // Start is called before the first frame update
    public void Start()
    {
        

        SnapMovementScript = GameObject.Find("GhostBuild").GetComponent<SnapMovement>();
        saveload = gameObject.GetComponent<SaveLoad>();
        saveload.LoadGame();

        Tile = new string[width, height];

        for (int h = 0; h < height; h++)
        {
            XIndex = 0;
            for (int w = 0; w < width; w++)
            {
                Instantiate(TilePrefab, new Vector2(Origin.x + w, Origin.y + h), Quaternion.identity);
                XIndex++;
            }
            YIndex++;
        }

        avaliableTIles = Resources.LoadAll<FurnitureScriptableObject>("Furniture");
    }

    public void PlaceFurniture(int xPos, int yPos, string furniture)
    {
        /*
        //a loop to check if the furniture will overlap with other furniture or bounds
        for (int w = 0; w < furniture.width; w++)
        {
            for (int h = 0; h < furniture.height; h++)
            {

            }
        }
        //place the furniture and place its "Obstructed" tile in the places it takes up
        */
        Tile[xPos, yPos] = furniture;
    }

    public void LoadData()
    {

    }

    public void SaveData()
    {
        saveload.tiles = Tile;

        saveload.SaveGame();
    }


}
