//--------------------------------------------------------------
//
//                   Simple Flare System
//          Writed by AliyerEdon in fall 2016
//           Contact me : aliyeredon@gmail.com
//
//--------------------------------------------------------------

// This script used for light shaft system


using UnityEngine;
using System.Collections;

public class ShaftVolume : MonoBehaviour
{

	// Target is main camera
	Transform target;

	// Shaft material instance
	Material mat;

	// Default color
	Color mainColor;

	// Alpha multiplier
	public float multiplier = 100f;

	// Max distance to start fade in\out
	public float MaxDistance = 1000f;

	void Start ()
	{
			target = Camera.main.transform;
			mat = GetComponent<MeshRenderer> ().material;

		// Store material starting color
		mainColor = mat.GetColor ("_TintColor");
	}
	

	float distance;
	Vector3 eulerAngleOffset;

	void Update ()
	{
		if (!target || !mat)
			return;


		if (canCompute) {

			distance = Vector3.Distance (transform.position, target.position);

			mat.SetColor ("_TintColor", new Color (mainColor.r, mainColor.g, mainColor.b, distance / multiplier));

		}
	}


	public bool canCompute;

	void OnBecameVisible ()
	{
		canCompute = true;
	}

	void OnBecameInvisible ()
	{
		canCompute = false;
	}
}
