//--------------------------------------------------------------
//
//                    Simple Flare System
//          Writed by AliyerEdon in fall 2016
//           Contact me : aliyeredon@gmail.com
//
//--------------------------------------------------------------

// This script used for face the flare to camera
using UnityEngine;
using System.Collections;

public class FlareLookAt : MonoBehaviour
{


	// Used for optimazation
	bool canCompute;

	void OnBecameVisible ()
	{
		canCompute = true;
	}

	Transform cam;

	void Start ()
	{
		cam = Camera.main.transform;
	}

	void Update ()
	{
		if (canCompute) {
			transform.LookAt (cam.position);
		}
	}

	void OnBecameInVisible ()
	{
		canCompute = false;
	}
}
