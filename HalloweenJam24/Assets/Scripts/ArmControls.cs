using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using static InputManager;

enum ArmState { Left, Right }

public class CharacterControls : MonoBehaviour
{
    
    [SerializeField]
    private GameObject Arm;
    
    [SerializeField]
    private GameObject ForeArm;

    private Vector3 ArmOriginalRot, ForeArmOriginalRot;

    [SerializeField]
    private Vector3 NewArmRot, NewForeArmRot;

    [SerializeField]
    private bool ArmUp;

    private ArmState ArmSt;


    private void Awake()
    {
        Arm = this.gameObject;
        ForeArm = this.transform.GetChild(0).gameObject;

        if(Arm.CompareTag("RightArm"))
        {
            ArmSt = ArmState.Right;
        }
        else if(Arm.CompareTag("LeftArm"))
        {
            ArmSt = ArmState.Left;
        }

        switch (ArmSt)
        {
            case ArmState.Left:
                InputManager.InputActions.PlayerArms.LeftArm.performed += ActivateLeftArm;
                break;
            case ArmState.Right:
                InputManager.InputActions.PlayerArms.RightArm.performed += ActivateRightArm;
                break;
        }


        
    }

    private void OnDisable()
    {
        InputManager.InputActions.PlayerArms.LeftArm.performed -= ActivateLeftArm;
        InputManager.InputActions.PlayerArms.RightArm.performed -= ActivateRightArm;
    }
    // Start is called before the first frame update
    void Start()
    {
        ArmOriginalRot = Arm.transform.rotation.eulerAngles;
        ForeArmOriginalRot = ForeArm.transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ActivateLeftArm(InputAction.CallbackContext callback)
    {
        ArmMovement();
    }

    private void ActivateRightArm(InputAction.CallbackContext callback)
    {
        ArmMovement();
    }

    private void ArmMovement()
    {
        if (!ArmUp)
        {
            ArmUp = true;
            Debug.Log(Arm + " is active");
            Arm.transform.Rotate(NewArmRot);
            ForeArm.transform.Rotate(NewForeArmRot);
            StartCoroutine(WaitingR(Arm, ForeArm, ArmOriginalRot, ForeArmOriginalRot));
        }
    }

    IEnumerator WaitingR(GameObject arm, GameObject forearm, Vector3 armRot, Vector3 forearmRot)
    {
        yield return new WaitForSeconds(1);
        arm.transform.rotation = Quaternion.Euler(armRot);
        forearm.transform.rotation = Quaternion.Euler(forearmRot);
        ArmUp = false;
        StopAllCoroutines();       
    }


   
    

    
}
