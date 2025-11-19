using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    public Transform arm;

    [Header("Settings")]
    public float mouseSensitivity;
    [SerializeField] private float xRotation;
    [SerializeField] private float yRotation;
    public float topClam;
    public float bottomClam;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x * mouseSensitivity;
        float mouseY = input.y * mouseSensitivity;

        //calculate camera rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, topClam, bottomClam);
        yRotation += mouseX;

        //apply to camera transform
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    public Camera GetCam()
    {
        return Camera.main;
    }
}
