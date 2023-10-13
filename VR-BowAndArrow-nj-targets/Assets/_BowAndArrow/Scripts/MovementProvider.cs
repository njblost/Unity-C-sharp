using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class MovementProvider : LocomotionProvider
{
    public List<XRController> controllers = null;

    private CharacterController characterController = null;
    private GameObject head = null;

    protected override void Awake()
    {
        characterController = GetComponent<characterController>();
        head = GetComponent<XRRig>().cameraGameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void PositionController()
    {

    }

    private void CheckForInput()
    {

    }

    private void CheckForMovement(InputDevice device)
    {

    }

    private void ApplyGravity()
    {

    }
}
