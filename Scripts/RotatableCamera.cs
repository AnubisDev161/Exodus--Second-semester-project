using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using System;

public class RotatableCamera : MonoBehaviour
{
    // recoil settings

    public float mouseSensitivity = 1.2f;
    public float actualMouseSensitivity;
    float xRotation = 0f;
    public Transform playerBody;
    private bool inTutorial;


    void OnEnable()
    {
        PlayerInputController.inTutorial += OnInTutorial;
    }

    void OnDisable()
    {
        PlayerInputController.inTutorial -= OnInTutorial;
    }

    public void OnInTutorial(bool inTutorial)
    {
        this.inTutorial = inTutorial;
    }

    private void Update()
    {
        if (!inTutorial)
        {
            LookAround();
        }
    }

    private void Start()
    {
        actualMouseSensitivity = mouseSensitivity * 200;
    }

    private void LookAround()
    {
        actualMouseSensitivity = mouseSensitivity * 200;
        float mouseX = Input.GetAxis("Mouse X") * actualMouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * actualMouseSensitivity * Time.deltaTime;


        xRotation -= mouseY;
        xRotation = math.clamp(xRotation, -90, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}

