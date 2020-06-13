using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{

    public Transform[] playerPoints = new Transform[8];
    public Transform boltOrigin;
    public float targetSwitchTime;
    float currentTime;
    int i;
    Vector3 lookAtDirection;
    public GameObject boltPrefab;
    // Start is called before the first frame update
    void Start()
    {
        i=0;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTime<targetSwitchTime)
            currentTime += Time.deltaTime;   

        if(currentTime>targetSwitchTime){
            //i = Random.Range(0, 7);
            transform.LookAt(playerPoints[i]);
            i++;
            if(i==8)
                i=0;
           
            lookAtDirection = playerPoints[i].position - boltOrigin.position;
            currentTime = 0f;
            ShootBolt();
        }

        transform.LookAt(playerPoints[i]);

        Debug.DrawRay(boltOrigin.position, playerPoints[i].position - boltOrigin.position, Color.white, 0f);
        Debug.DrawRay(boltOrigin.position, transform.forward, Color.blue, 0f);

    }

    void ShootBolt(){
        GameObject bolt = Object.Instantiate(boltPrefab, boltOrigin.position, transform.rotation);
    }    
}
