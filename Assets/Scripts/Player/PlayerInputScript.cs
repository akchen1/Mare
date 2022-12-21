using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementScript))]
public class PlayerInputScript : MonoBehaviour
{
    private MovementScript mScript;
    private PlayerInputActions playerInput;


    private void Awake()
    {
        playerInput = new PlayerInputActions();
    }

    // Start is called before the first frame update
    void Start()
    {
        mScript = GetComponent<MovementScript>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void FixedUpdate()
    {
        mScript.Move(playerInput.Player.Movement.ReadValue<Vector2>());

    }
}
