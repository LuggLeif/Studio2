using System;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Global global;
    public Transform playerBody;
    
    public float mouseSensitivity = 250f;

    private float xRotation, yRotation;

    private void Awake()
    {
        xRotation = Camera.main.transform.rotation.x;
    }

    void Update()
    {
        if (global.disabled)
            return;
        
        if (Input.GetKey(KeyCode.Mouse2))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, 0f, 90f);
            
            yRotation = mouseX * mouseSensitivity * Time.deltaTime;
            
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            
            if (playerBody.rotation.y > 0.9f && mouseX < 0f) // Decrease
                playerBody.Rotate(Vector3.up * yRotation);
            if (playerBody.rotation.y < 0.99f && mouseX > 0f) // Increase
                playerBody.Rotate(Vector3.up * yRotation);
        }
            
        if (Input.GetMouseButtonDown(2))
        {
            Cursor.lockState = CursorLockMode.Locked;
            global.busy = true;
        }
        else if (Input.GetMouseButtonUp(2))
        {
            Cursor.lockState = CursorLockMode.None;
            global.busy = false;
        }
    }
}
