using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestingInputSystem : MonoBehaviour
{
    [SerializeField]
    private Rigidbody playerRigidbody;
    [SerializeField]
    private Rigidbody cameraRigidbody;
    private NewControls newControls;
    private PlayerInput playerInput;
    private bool controllerSwitch = true;
    private readonly float speed = 2f;
    private readonly float powerJump = 2f;

    void Awake()
    {
        //playerRigidbody = GetComponent<Rigidbody>();  
        playerInput = GetComponent<PlayerInput>();
        newControls = new NewControls();  

        newControls.Player.Enable();
        newControls.Player.Jump.performed += Jump;
        // newControls.Player.Movement.performed += Movement;
        // newControls.Camera.Enable();
        // newControls.Camera.Movement.performed += MovementCamera;
    }

    void Update()
    {
        if (Keyboard.current.cKey.wasPressedThisFrame)        
        {
            if (controllerSwitch)
            {
                newControls.Player.Disable();
                newControls.Camera.Enable();
                //playerInput.SwitchCurrentActionMap("Camera");       
                controllerSwitch = false;
            }
            else
            {
                newControls.Player.Enable();
                newControls.Camera.Disable();
                //playerInput.SwitchCurrentActionMap("Player");             
                controllerSwitch = true;
            }
        }
    }
    
    private void FixedUpdate()
    {
        Vector2 playerInputVector = newControls.Player.Movement.ReadValue<Vector2>();
        playerRigidbody.AddForce(new Vector3(playerInputVector.x, 0, playerInputVector.y) * speed, ForceMode.Force);

        Vector2 cameraInputVector = newControls.Camera.Movement.ReadValue<Vector2>();
        cameraRigidbody.AddForce(new Vector3(cameraInputVector.x, 0, cameraInputVector.y) * speed, ForceMode.Force);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Debug.Log(context);
            playerRigidbody.AddForce(Vector3.up * powerJump, ForceMode.Impulse);
        }  
    }

    // private void Movement(InputAction.CallbackContext context)
    // {
    //     //Debug.Log(context);
    //     Vector2 inputVector = context.ReadValue<Vector2>();
    //     float speed = 20f;
    //     playerRigidbody.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
    // }

    // public void MovementCamera(InputAction.CallbackContext context)
    // {
    //     //Debug.Log(context);
    //     Vector2 inputVector = context.ReadValue<Vector2>();
    //     float speed = 20f;
    //     cameraRigidbody.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
    // }
}
