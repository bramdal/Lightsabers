using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller class which uses the LightsaberTrail class to show a trail when we move the lightsaber
/// </summary>
[RequireComponent(typeof(LightsaberTrail))]
public class LightsaberTrailController : MonoBehaviour {

    LightsaberTrail lightsaberTrail;
    public bool disableTrailRender;

    Quaternion previousRotation;

    void Start()
    {
        lightsaberTrail = GetComponent<LightsaberTrail>();
    }

    void Update()
    {

        if(!disableTrailRender && transform.rotation != previousRotation)
            lightsaberTrail.Iterate(Time.time);
        lightsaberTrail.UpdateTrail(Time.time, 0f);

        previousRotation = transform.rotation;
    }

}
