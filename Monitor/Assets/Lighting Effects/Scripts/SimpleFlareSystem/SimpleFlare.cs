//--------------------------------------------------------------
//
//                   Simple Flare System
//          Writed by AliyerEdon in fall 2016
//           Contact me : aliyeredon@gmail.com
//
//--------------------------------------------------------------

// This script used for night flare system
using UnityEngine;
using System.Collections;

public enum FlareType
{
	Negative,
	Positive
}

public enum Axis
{
	X,
	Y,
	XY
}

public class SimpleFlare : MonoBehaviour
{

	[Header("Simple Flare System")]
	[Space(3)]
	// Flare type
	public 	FlareType flareType = FlareType.Negative;
	public Axis axis;

	// Flare size multiplier + fade start distance
	public float multiplier  = 3f, distance = 1000f;

	// Raycast update rate   
	public float updateInterval = 0.3f;

	public bool Raycast = true;
	// Internal usage
	 bool canCompute, canFade;
	 float Dist;
	 Transform cam;
	GameObject target;
	 Vector3 temp;
	 bool positive;



	 MeshRenderer renderM;
	void Start ()
	{
		renderM = GetComponent<MeshRenderer> ();

		if(Raycast)
			StartCoroutine (RayCast ());



		if (!target)
			target = gameObject;
		
		if(target)
			temp = target.transform.localScale;

		if(!cam)
			cam = Camera.main.transform;

		if (flareType == FlareType.Negative)
			positive = false;
		else
			positive = true;

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!cam)
			return;


		// thistance from target(mainly camera)
		Dist = Vector3.Distance (transform.position, cam.position);


		// Start fading based on user defined distance
		if (Dist <= distance)
			canFade = true;
		else
			canFade = false;

		// Can compute for optimization
		if (canCompute)
			target.transform.LookAt (cam.position);

		// Fade flare based on distance from camera
		if (canFade) 
		{
			if (positive) {
				if (axis == Axis.X)
					target.transform.localScale = new Vector3 (temp.x * Dist / 100 * multiplier, temp.y, temp.z);
				if (axis == Axis.Y)
					target.transform.localScale = new Vector3 (temp.x, temp.y * Dist / 100 * multiplier, temp.z);

				if (axis == Axis.XY)
					target.transform.localScale = new Vector3 (temp.x * Dist / 100 * multiplier, temp.y * Dist / 100 * multiplier, temp.z);
				
			} else {
			
				if (axis == Axis.X)
					target.transform.localScale = new Vector3 (temp.x + Dist / 100 * multiplier, temp.y, temp.z);
				if (axis == Axis.Y)
					target.transform.localScale = new Vector3 (temp.x, temp.y + Dist / 100 * multiplier, temp.z);

				if (axis == Axis.XY)
					target.transform.localScale = new Vector3 (temp.x + Dist / 100 * multiplier, temp.y + Dist / 100 * multiplier, temp.z);
			}
		}
	}


	// For optimization
	void OnBecameVisible()
	{
		canCompute = true;

	}

	void OnBecameInvisible()
	{
		canCompute = false;
	}


	IEnumerator RayCast()
	{
		while (true) {
			yield return new WaitForSeconds (updateInterval);


				if (Physics.Linecast (transform.position, cam.transform.position))
					renderM.enabled = false;
				else
					renderM.enabled = true;
			



		}
	}
}
