using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour {

    float smooth = 2.0f;
    public float rotateAngle = 60.0f;
    private Vector3 defaultRot;
    private Vector3 openRot;
    private Vector3 rotCheck1;
    private Vector3 rotCheck2;
    bool isLeft = true;
    bool isRight = false;

    // Use this for initialization
    void Start ()
    {

    defaultRot = transform.eulerAngles;
	openRot = new Vector3(defaultRot.x, defaultRot.y + rotateAngle, defaultRot.z);
    rotCheck1 = new Vector3 (0, 345, 0);
    rotCheck2 = new Vector3(0, 285, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.eulerAngles.y == rotCheck2.y)
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, openRot, Time.deltaTime * smooth);
            print(transform.eulerAngles.y);
            //System.Threading.Thread.Sleep(500);

        }
        if (transform.eulerAngles.y == rotCheck1.y)
            {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, defaultRot, Time.deltaTime * smooth);
            print(transform.eulerAngles.y);
            // System.Threading.Thread.Sleep(500);
        }
    }
}
