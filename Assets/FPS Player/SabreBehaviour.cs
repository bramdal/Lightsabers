using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SabreBehaviour : MonoBehaviour
{

    public bool controller;

    AimBehaviour aimBehaviourScript;
    public SabreBehaviourControl sabreController;
    public Animator sabreAnim;

    float xInput = 0f, yInput = 0f;
    public float mouseSensitivity = 0.1f;
    [HideInInspector] public bool defending = false;
    public bool attacking = false;
    bool allowedToAttack = false;
    bool triggerDown = false;

    public LightsaberTrailController trailController;
   

    // Start is called before the first frame update
    void Start()
    {
        aimBehaviourScript = GetComponent<AimBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") || Input.GetButton("Fire1") || Input.GetAxis("Left Trigger")>0){
            aimBehaviourScript.enabled = false;
            sabreAnim.SetBool("Defend Phase", true);
            defending = true;
            trailController.disableTrailRender = false;
        }

        if(Input.GetButtonUp("Fire1") || Input.GetAxis("Left Trigger")==0){
            aimBehaviourScript.enabled = true;
            sabreAnim.SetBool("Defend Phase", false);
            defending = false;
            sabreController.defending = defending;
            xInput = yInput = 0f;
            trailController.disableTrailRender = true; 

            
        }

        if(defending){
            GetJoystickInput();
            allowedToAttack = true;
            sabreController.defending = defending;
           
        }

        // if(sabreController.interrupted && attacking){
        //     sabreAnim.SetTrigger("Interrupted");
        // }
    }

    void GetJoystickInput(){
        
        if(triggerDown && Input.GetAxis("Right Trigger")<0.1)
            triggerDown = false;

        AttackInDirection(xInput, yInput);        
        if(Input.GetAxis("R Joystick Y")>0.1f){
            yInput += mouseSensitivity * Time.deltaTime;
        }
        else if(Input.GetAxis("R Joystick Y")< -0.1f){
            yInput -= mouseSensitivity * Time.deltaTime;
        }
        else
        {
            if(yInput<0){
                yInput += mouseSensitivity * Time.deltaTime;
                if(yInput<0.05 && yInput> -0.05)
                    yInput = 0f;
            }     
            else if(yInput>0){
                yInput -= mouseSensitivity * Time.deltaTime;
                if(yInput<0.05 && yInput> -0.05)
                    yInput = 0f;
            }       
        }

        if(Input.GetAxis("R Joystick X")>0.1f){
            xInput += mouseSensitivity * Time.deltaTime;
        }
        else if(Input.GetAxis("R Joystick X")< -0.1f){
            xInput -= mouseSensitivity * Time.deltaTime;
        }
         else
        {
            if(xInput<0){
                xInput += mouseSensitivity * Time.deltaTime;
                if(xInput<0.05 && xInput> -0.05)
                    xInput = 0f;
            }     
            else if(xInput>0){
                xInput -= mouseSensitivity * Time.deltaTime;
                if(xInput<0.05 && xInput> -0.05)
                    xInput = 0f;
            }       
        }
        

        yInput = Mathf.Clamp(yInput, -1f, +1f);
        xInput = Mathf.Clamp(xInput, -1f, +1f);    

        sabreAnim.SetFloat("Y_Input", yInput);
        sabreAnim.SetFloat("X_Input", xInput);

    }

    void AttackInDirection(float xInput, float yInput){
        if(defending && allowedToAttack && !triggerDown){
            if(yInput > 0.75f && xInput< 0.5f && xInput > -0.5f ){
                if(Input.GetAxis("Right Trigger") > 0.2){
                // if(Input.GetButtonDown("Fire2") && allowedToAttack){    
                    allowedToAttack = false;
                    sabreAnim.SetTrigger("Attack Top");
                    yInput = -1f;
                    xInput = 0f;
                    triggerDown = true;
                    //attacking = true;
                }
            }
            else if(yInput < -0.75f && xInput< 0.33f && xInput > -0.33f ){
                if(Input.GetAxis("Right Trigger") > 0.2){
                    allowedToAttack = false;
                    sabreAnim.SetTrigger("Attack Bottom");
                    yInput = 1f;
                    xInput = 0f;
                    triggerDown = true;
                    //attacking = true;
                }
            }
            else if(xInput > 0.75f && yInput< 0.5f && yInput > -0.5f ){
                if(Input.GetAxis("Right Trigger") > 0.2){
                    allowedToAttack = false;
                    sabreAnim.SetTrigger("Attack Right");
                    yInput = 0f;
                    xInput = -1f;
                    triggerDown = true;
                    //attacking = true;
                }
            }
            else if(xInput < -0.75f && yInput< 0.5f && yInput > -0.5f ){
                if(Input.GetAxis("Right Trigger") > 0.2){
                    allowedToAttack = false;
                    sabreAnim.SetTrigger("Attack Left");
                    yInput = 0f;
                    xInput = 1f;
                    triggerDown = true;
                    //attacking = true;
                }
            }
            else if(xInput >= 0.75f && yInput >= 0.5f ){
                if(Input.GetAxis("Right Trigger") > 0.2){
                    allowedToAttack = false;
                    sabreAnim.SetTrigger("Attack Top Right");
                    yInput = -1f;
                    xInput = -1f;
                    triggerDown = true;
                    //attacking = true;
                }
            }
            else if(xInput >= 0.75f && yInput <= -0.5f ){
                if(Input.GetAxis("Right Trigger") > 0.2){
                    allowedToAttack = false;
                    sabreAnim.SetTrigger("Attack Bottom Right");
                    yInput = 1f;
                    xInput = -1f;
                    triggerDown = true;
                    //attacking = true;
                }
            }
            else if(xInput <= -0.75f && yInput <= -0.5f ){
                if(Input.GetAxis("Right Trigger") > 0.2){
                    allowedToAttack = false;
                    sabreAnim.SetTrigger("Attack Bottom Left");
                    yInput = 1f;
                    xInput = 1f;
                    triggerDown = true;
                    //attacking = true;
                }
            }
            else if(xInput <= -0.75f && yInput >= 0.5f ){
                if(Input.GetAxis("Right Trigger") > 0.2){
                    allowedToAttack = false;
                    sabreAnim.SetTrigger("Attack Top Left");
                    yInput = -1f;
                    xInput = 1f;
                    triggerDown = true;
                    //attacking = true;
                }
            }
        }
    }

    public void AllowAttack(){
        allowedToAttack = true;
        attacking = false;
    }

    public void Attacking(){
        attacking = true;
    }

    
}
