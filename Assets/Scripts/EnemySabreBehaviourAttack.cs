using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySabreBehaviourAttack : MonoBehaviour
{
   
        public bool controller;

    AimBehaviour aimBehaviourScript;
    public SabreBehaviourControl sabreController;
    public Animator sabreAnim;

    [HideInInspector] public bool defending = false;
    public bool attacking = false;
    bool allowedToAttack = false;
   
    public LightsaberTrailController trailController;

    struct AttackType{
        public float x;
        public float y;
        public string attackDirection;
    }

    AttackType newAttack = new AttackType();

    public bool[] attackMode = new bool[8];

    public float attackRate;
    float currentTime = 0f;

    int i=0;
    bool switch1;
    bool switch2;

    string[] attacks = { "Attack Top", "Attack Bottom", "Attack Left", "Attack Right"};  
    // Start is called before the first frame update

    void Start()
    {
        i=0;
        newAttack.x = 0;
        newAttack.y = 1;
        newAttack.attackDirection = "Attack Top";
        sabreAnim.SetBool("Defend Phase", true);
    }

    // Update is called once per frame
    void Update(){
        defending = true;
        sabreController.defending = defending;
        currentTime += Time.deltaTime;
        if(switch1)
            newAttack.x += Time.deltaTime;
        else
            newAttack.x -= Time.deltaTime;
        if(switch2)    
            newAttack.y += Time.deltaTime;
        else 
            newAttack.y -= Time.deltaTime;
        sabreAnim.SetFloat("Y_Input", newAttack.y);
        sabreAnim.SetFloat("X_Input", newAttack.x);
        if(currentTime > attackRate){
            switch1 = randomBool(0.5f);
            switch2 = randomBool(0.5f);
            currentTime = 0f;
            sabreAnim.SetTrigger(attacks[Random.Range(0,3)]);
        }
        
    }

    bool randomBool (float chanceOfSuccess){
        return Random.value < chanceOfSuccess;
    }

    public void AllowAttack(){
        allowedToAttack = true;
        attacking = false;
    }

    public void Attacking(){
        attacking = true;
    }
}
