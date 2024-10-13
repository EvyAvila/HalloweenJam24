using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using static InputManager;

public class CharacterMovement : Entity
{
    InputAction Move;
    InputAction Sprint;

    private void OnEnable()
    {
        Move = InputManager.InputActions.Player.Movement;
        Move.Enable();

        Sprint = InputManager.InputActions.Player.Sprint;
        Sprint.Enable();
    }

    private void OnDisable()
    {
        Move.Disable();
        Sprint.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        Speed = 1;

        if(Rotate == 0) { Rotate = 45; }

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();        
    }

    private void MovePlayer()
    {
        SprintPlayer();

        var m = Move.ReadValue<Vector3>();
        Direction.z = m.z;

        var r = m.x;

        transform.Translate(Direction * Speed * Time.deltaTime);

        transform.Rotate(0, r * Rotate * Time.deltaTime, 0);
    }

    private void SprintPlayer()
    {
        if(Sprint.IsPressed())
        {
            Speed = 3;
        }
        else
        {
            Speed = 1;
        }
    }
}

public abstract class Entity : MonoBehaviour 
{
    public Vector3 Direction;
    public float Rotate;
    public float Speed;
}

public static class InputManager //code from David (old teach)
{
    public static PlayerControls InputActions
    {
        get
        {
            if(inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.Enable();
            }
            return inputActions;
        }
    }

    private static PlayerControls inputActions;

}

