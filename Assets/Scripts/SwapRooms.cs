using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapRooms : MonoBehaviour
{
    bool BongoatRoom = false;

    [SerializeField] GameObject MainCanvas;
    [SerializeField] GameObject RoomCanvas;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            swapRoom();
    }

    void swapRoom()
    {
        if (!BongoatRoom)
        {
            BongoatRoom = true;
            RoomCanvas.SetActive(true);
            MainCanvas.SetActive(false);
        }
        else
        {
            BongoatRoom = false;
            RoomCanvas.SetActive(false);
            MainCanvas.SetActive(true);
        }
    }
}
