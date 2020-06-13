using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class SabreBehaviourControl : MonoBehaviour
{
    public SabreBehaviour sabreBehaviour;
    public bool interrupted;
    Animator saberAnim;
    [HideInInspector] public bool defending;

    bool flashLight = false;
    public float flashTime;
    float currentFlashTime;
    public Image flashImage;
    Color flashAlpha;

    public CinemachineVirtualCamera virtualCamera;
    CinemachineBasicMultiChannelPerlin virtualCameraNoise;
    public float shakeAmplitude = 1;
    public float shakeFrequency = 4;
    bool cameraNoise = false;
    public float shakeTime;
    float currentShakeTime;

    public AudioSource saberClash;
    public GameObject sparkFX;
    // Start is called before the first frame update
    
    private void Start() {
        saberAnim = GetComponent<Animator>();
        flashImage.enabled = false;
        flashAlpha.r = flashAlpha.g = flashAlpha.b = 255f;

        if(virtualCamera != null){
            virtualCameraNoise = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }
        //flash = GetComponent<Light>();
    }

    public void AllowSabreAttack(){
        sabreBehaviour.AllowAttack();
    }

    public void Attacking(){
        sabreBehaviour.Attacking(); 
    }

    private void Update() {
        if(flashLight){
            currentFlashTime -= Time.deltaTime;
            flashImage.color = flashAlpha;
            flashAlpha.a -= Time.deltaTime;
        }
        if(currentFlashTime<=0){
            flashLight = false;
            flashAlpha.a = 0f;
        }

        if(virtualCameraNoise != null){
            if(cameraNoise){
                currentShakeTime -= Time.deltaTime;
                virtualCameraNoise.m_AmplitudeGain = shakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = shakeFrequency;
            }
            if(currentShakeTime<=0){
                cameraNoise = false;
                virtualCameraNoise.m_AmplitudeGain = 0f;
                virtualCameraNoise.m_FrequencyGain = 0f;
            }
        }
    }
    
    // private void OnTriggerEnter(Collider other){
    //     if(other.tag == "Lightsaber" && sabreBehaviour.attacking){
    //         //if(saberAnim!=null)
    //         saberAnim.SetTrigger("Interrupted");
    //         sabreBehaviour.attacking = false;
    //         print("interrupted");    
    //     }    
    // }

    private void OnCollisionEnter(Collision other) {
        ContactPoint contact = other.contacts[0];
        Vector3 pos = contact.point;
        if( other.gameObject.tag == "Bolt"){
            FlashLightFunction(pos);
        }

        if(other.gameObject.tag == "Lightsaber" && sabreBehaviour.attacking){
            //if(other.gameObject.GetComponent<SabreBehaviourControl>().sabreBehaviour.defending){
                saberAnim.SetTrigger("Interrupted");
                sabreBehaviour.attacking = false;
                print("Interrupted");  
                FlashLightFunction(pos); 
                 
                cameraNoise = true;
                currentShakeTime = shakeTime;
                ContactPoint contact1 = other.contacts[0];
                Quaternion rot = Quaternion.FromToRotation(Vector3.up, -transform.forward);
                pos = contact1.point;
                GameObject tempFX = Instantiate(sparkFX, pos, rot);
                Destroy(tempFX, 2f);
                saberClash.Play();

                
            //}
        }     
    }

    void FlashLightFunction(Vector3 pos){
        flashLight = true;
        currentFlashTime = flashTime;
        flashImage.transform.position = Camera.main.WorldToScreenPoint(pos);
        flashImage.enabled = true;
        flashAlpha.a = 1f;
    }
   
}
