using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] Movement movement;
    [SerializeField] MouseLook mouseLook;
    [SerializeField] Shoot shoot;

    PlayerControls controls;
    PlayerControls.GroundMovementActions groundMovement;

    Vector2 horizontalInput;
    Vector2 mouseInput;

    bool checkSwap = false;

    private void Awake ()
    {
        controls = new PlayerControls();
        groundMovement = controls.GroundMovement;

        groundMovement.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();

        groundMovement.Jump.performed += _ => movement.OnJumpPressed();
        groundMovement.Fire.performed += _ => shoot.OnFirePressed();
        groundMovement.CameraSwap.performed += _ => movement.OnTabPressed();
        groundMovement.Action1.performed += _ => shoot.On1Pressed();

        Cursor.visible = false;
        

        groundMovement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        groundMovement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();
    }

    private void Update ()
    {
        checkSwap = movement.ReturnSwap();
        movement.ReceiveInput(horizontalInput);
        mouseLook.ReceiveInput(mouseInput);
        if (checkSwap == true)
        {
            mouseLook.ResetCameraY();
        }
    }

    private void OnEnable ()
    {
        controls.Enable();
    }

    private void OnDestroy ()
    {
        controls.Disable();
    }
}
