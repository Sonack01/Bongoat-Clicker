using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] Camera Camera;
    [SerializeField] Rigidbody2D RB2D;

    [SerializeField] Vector3 Origin;
    [SerializeField] Vector3 Difference;
    [SerializeField] Vector3 ResetCamera;

    bool drag = false;

    private void Start() {
        ResetCamera = Camera.transform.position;
    }

    private void LateUpdate() {
        CameraDragMove();
    }

    void CameraDragMove()
    {
        if (Input.GetMouseButton(0))
        {
            Difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position;
            if (!drag)
            {
                drag = true;
                Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            drag = false;
        }

        if (drag)
        {
            Camera.main.transform.position = Origin - Difference;
        }

        if (Input.GetMouseButton(1))
            Camera.main.transform.position = ResetCamera;
        
    }
}
