using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterBoltBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 travelAlong; 
    public float velocity;
    public float timeToDestroy;

    public float scaleLimit = 2.0f;    
    public float z = 10f;

    AudioSource boltFire;
    public GameObject sparkFX;

    void Start()
    {
        travelAlong = transform.forward;
        boltFire = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
        transform.position += travelAlong * Time.deltaTime * velocity;
        Debug.DrawRay(transform.position, transform.forward, Color.blue, 0f);
        timeToDestroy -= Time.deltaTime;
        if(timeToDestroy<0f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other){
        if(other.tag == "Lightsaber"){
            if(other.gameObject.GetComponent<SabreBehaviourControl>().defending){
                print("Hit Saber");
                int probability = Random.Range(1, 100);
                if(probability>75)
                    travelAlong = -travelAlong;
                else{
                    transform.forward = travelAlong = ShootRay();
                }
                boltFire.Play();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other){
       if(other.gameObject.tag == "Lightsaber"){
            if(other.gameObject.GetComponent<SabreBehaviourControl>().defending){
                int probability = Random.Range(1, 100);
                if(probability>75)
                    travelAlong = -travelAlong;
                else{
                    transform.forward = travelAlong = ShootRay();
                }
                boltFire.Play();
            }

        }
        else if(other.gameObject.tag == "Player"){
            print("Hit player");
            Destroy(gameObject);
        }
        else
        {
            print("destroyed");
            Destroy(gameObject);
        } 

        ContactPoint contact = other.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, -transform.forward);
        Vector3 pos = contact.point;
        GameObject tempFX = Instantiate(sparkFX, pos, rot);
        Destroy(tempFX, 2f);
    }

     Vector3 ShootRay(){
        Vector3 direction = Random.insideUnitCircle * scaleLimit;
        direction.z = -z; // circle is at Z units 
        direction = transform.TransformDirection( direction.normalized );    
        Ray r = new Ray( transform.position, direction );
        RaycastHit hit;     
        if( Physics.Raycast( r, out hit ) ) {
          Debug.DrawLine( transform.position, hit.point ); 
        } 
        return direction;
     }
}
