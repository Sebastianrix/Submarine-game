using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Controller : MonoBehaviour
{
Rigidbody m_Rigidbody;
public InputActionAsset InputActions;
InputActionMap gameplayActionMap;

InputAction accelerationInputAction;
InputAction pitchInputAction;
InputAction turnInputAction;

private float TurnInput;
private float SpeedInput;
private float RiseInput;
private float m_Turn;
private float m_Speed;
private float m_Rise;
private float RotationFix;

private float SpeedSensitivity;
private float RiseSensitivity;
private float TurnSensitivity;

//change

    // Start is called before the first frame update
    void Awake()
    {
  //Action Map 
        gameplayActionMap = InputActions.FindActionMap("Gameplay");
  //Forward Controls
      accelerationInputAction = gameplayActionMap.FindAction("Acceleration");
      accelerationInputAction.performed += GetAcceleration;
      accelerationInputAction.canceled  += GetAcceleration;
  //Pitch Controls
      pitchInputAction = gameplayActionMap.FindAction("pitch");
      pitchInputAction.performed += GetPitch;
      pitchInputAction.canceled  += GetPitch;
  //Turn Controls
      turnInputAction = gameplayActionMap.FindAction("turn");
      turnInputAction.performed += GetTurn;
      turnInputAction.canceled  += GetTurn;
    }
    private void OnEnable(){
        accelerationInputAction.Enable();
        pitchInputAction.Enable();
        turnInputAction.Enable();
    }
    private void onDisable(){
        accelerationInputAction.Disable();
        pitchInputAction.Disable();
        turnInputAction.Disable();
    }


    void Start(){
          m_Rigidbody = GetComponent<Rigidbody>();
            
    }

    void GetAcceleration(InputAction.CallbackContext context){
        SpeedInput = context.ReadValue<float>();
    }

    void GetPitch(InputAction.CallbackContext context){
        RiseInput = context.ReadValue<float>();
    }

    void GetTurn(InputAction.CallbackContext context){
        TurnInput = context.ReadValue<float>();
      
    }

    // Update is called once per frame

    void InputToTransform(){
        m_Speed = SpeedInput;
        m_Rise  =  RiseInput;
        m_Turn  =  TurnInput;
    }
   
  
    void ForwardTransform(){
        m_Rigidbody.AddForce(transform.forward * m_Speed);
        
    }
    void TurnTransform(){
   
        m_Rigidbody.AddTorque(transform.up * m_Turn);
     
    }
    void RiseTransform(){

        m_Rigidbody.AddForce(transform.up * m_Rise);
    }

    
    void Update(){
    InputToTransform();
    
    }


    void FixedUpdate()
    {  
     ForwardTransform();
     TurnTransform();
    RiseTransform();

    }
}



 //m_Rigidbody.AddTorque(transform.right*m_Pitch);
        //m_Rigidbody.AddTorque(transform.forward * RotationFix);
        // Debug.Log(this.transform.rotation.z);
         //Debug.Log("This is the RotationFix : "+RotationFix);
      
         //RotationFix = this.transform.rotation.z *-1;
         /*
         if(this.transform.rotation.z>0){
             Debug.Log("Now I should rotate to OneSIDE"); 
         RotationFix = -1f;

           }

 

       if(this.transform.rotation.z<0){    
           Debug.Log("Now I should rotate to secondSIDE");     
            RotationFix = 1f;
       }
       */