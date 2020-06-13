using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class EnemyBehaviour : MonoBehaviour
{
    public SabreBehaviourControl sabreController;
    public Animator sabreAnim;

    struct AttackType{
        public float x;
        public float y;
        public string attackDirection;
    }

    AttackType newAttack = new AttackType();

    public bool[] attackMode = new bool[8];

    public float attackRate;
    float currentTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        newAttack.x = 0;
        newAttack.y = -1;
        newAttack.attackDirection = "Attack Right";
        sabreAnim.SetBool("Defend Phase", true);
        sabreAnim.SetFloat("Y_Input", newAttack.y);
        sabreAnim.SetFloat("X_Input", newAttack.x);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime>attackRate){
            sabreAnim.SetTrigger(newAttack.attackDirection);
            currentTime = 0f;
        }
    }

    // int selected;

    // public void OnGUI()
    // {
    //     string[] options = new string[] { "  version 1", "  version 2" };
    //     selected = GUILayout.SelectionGrid(selected, options, 1, EditorStyles.radioButton);
    //     if (selected == 0)
    //             {
    //                 Debug.Log("Version 1 is selected");
    //             }
    //     else if (selected == 1)
    //             {
    //                 Debug.Log("Version 2 is selected");
    //             }
    // }
}
