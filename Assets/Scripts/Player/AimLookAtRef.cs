using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimLookAtRef : MonoBehaviour
{
    private GameObject LookAtObject;

    // Start is called before the first frame update
    void Start()
    {
        LookAtObject = GameObject.Find("AimRef");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Directly set the position without checking ownership
        this.transform.position = LookAtObject.transform.position; 
    }
}
