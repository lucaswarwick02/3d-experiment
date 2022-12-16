using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public static PlayerLook INSTANCE;
    
    float mouseSensitivity = 450f;
    public Transform root;

    float xRotation = 0f;

    private void Awake() {
        INSTANCE = this;
    }

    private void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the mouse input
        float mouseInputX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * zoomMultiplier() * Time.deltaTime;
        float mouseInputY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * zoomMultiplier() * Time.deltaTime;

        xRotation -= mouseInputY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        root.Rotate(Vector3.up * mouseInputX);
    }

    float zoomMultiplier () {
        return PlayerCombat.INSTANCE.isZoom ? 0.5f : 1.0f;
    }
}
