using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Global global;
    public CharacterController controller;
    public float speed = 12f;
    
    void Update()
    {
        if (global.disabled)
            return;
        
                     //Input.GetAxis("Mouse ScrollWheel");
        float scroll = Input.mouseScrollDelta.y;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = (transform.right * x + transform.forward * z);
        
        if (transform.position.y > 10 && scroll > 0)
            move.y -= 10;
        else if (transform.position.y < 20 && scroll < 0)
            move.y += 10;
        
        controller.Move(move * (speed * Time.deltaTime));
    }
}