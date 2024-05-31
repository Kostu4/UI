using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [SerializeField] private float mouseSensetivity = 300f;

    float xRotation = 0f;
    float yRotation = 0f;

    private void Start()
    {
        // Lock the cursor on the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X")* mouseSensetivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensetivity * Time.deltaTime;

        //looking (up and down)
        yRotation -= mouseY;
        // Clamp up and down
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);
        //looking (left and right)
        xRotation += mouseX;
        //Apply rotations to our transform
        transform.localRotation = Quaternion.Euler(yRotation, xRotation, 0f);
    }
}
