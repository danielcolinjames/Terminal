using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera1 : MonoBehaviour {

    float rotateSpeed = 5.0f;

    // Use this for initialization
    void Start () {
    
    }
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(30f, Mathf.PingPong(Time.time * rotateSpeed, 30.5f) - 45f, 0f);
    }
}
