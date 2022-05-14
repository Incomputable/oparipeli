using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] CharacterController controller;
    [SerializeField] CharacterController controller2;
    [SerializeField] float speed = 11f;
    Vector2 horizontalInput;

    [SerializeField] float jumpHeight = 3.5f;
    bool jump;
    [SerializeField] float gravity = -30f;
    Vector3 verticalVelocity = Vector3.zero;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    bool swap;
    bool swapLinger;

    public Camera cam1;
    public Camera cam2;
    public GameObject prefab;

    private void Update()
    {
        Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;

        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundMask);
        if (isGrounded)
        {
            verticalVelocity.y = 0;
        }

        verticalVelocity.y += gravity * Time.deltaTime;

        if (cam1.enabled)
        {

            controller.Move(horizontalVelocity * Time.deltaTime);

            if (jump)
            {
                if (isGrounded)
                {
                    verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
                }
                jump = false;
            }

            controller.Move(verticalVelocity * Time.deltaTime);

            
        }
        else
        {
            controller2.Move(horizontalVelocity * Time.deltaTime);

            if (jump)
            {
                Instantiate(prefab, 
                            controller2.transform.position, 
                            controller2.transform.rotation);
                jump = false;
            }
        }

        if (swap == false){
            swapLinger = false;
        }

        if (swap == true)
        {
            cam1.enabled = !cam1.enabled;
            cam2.enabled = !cam2.enabled;
            Cursor.visible = !Cursor.visible;
            swapLinger = true;
            

            if (cam1.enabled)
            {
                controller2.transform.position = controller.transform.position;
            }

            swap = false;
        }
    }

    public void ReceiveInput (Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }

    public void OnJumpPressed()
    {
        jump = true;
    }

    public void OnTabPressed()
    {
        swap = true;
    }

    public bool ReturnSwap()
    {
        return swapLinger;
        swapLinger = false;
    }
}
