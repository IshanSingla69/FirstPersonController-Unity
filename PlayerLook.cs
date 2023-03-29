using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    float xRotatation = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotatation -= mouseY;

        xRotatation = Mathf.Clamp(xRotatation, -90f, 90);

        transform.localRotation = Quaternion.Euler(xRotatation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
