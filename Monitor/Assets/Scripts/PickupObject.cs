using UnityEngine;
using System.Collections;

public class PickupObject : MonoBehaviour {
	GameObject mainCamera;
	bool carrying;
	GameObject carriedObject;
	float distance = 1.75f; //SAFE DISTANCE AWAY FROM FACE
	public float throwStrength = 6;
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
	}

	// Update is called once per frame
	void Update () 
	{
		if(carrying)
		{
			carry(carriedObject);
			checkDrop ();
			throwObject ();
			changeThrowStrength ();
		} else {
				pickup();
		}
	}

	void carry(GameObject o)
	{
		o.transform.position = mainCamera.transform.position + mainCamera.transform.forward * distance;
	}

	void pickup() {
		if(Input.GetKeyDown(KeyCode.E)) {
			int x = Screen.width / 2;
			int y = Screen.height / 2;

			Ray ray = Camera.main.ScreenPointToRay(new Vector3 (x, y));
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				Tagged4Pickup p = hit.collider.GetComponent<Tagged4Pickup>();
				if (p != null && (Physics.Raycast(ray, out hit, 1.75f))) {
					Debug.DrawLine (ray.origin, hit.point);
					carrying = true;
					carriedObject = p.gameObject;
					carriedObject.GetComponent<Rigidbody>().isKinematic = true;
				}
			}
		}
	}

	void changeThrowStrength(){
		if (Input.GetKeyDown (KeyCode.H) && throwStrength < 25) {
			throwStrength++;
			Debug.Log ("STRENGTH IS " + throwStrength);
		}
		if (Input.GetKeyDown(KeyCode.L) && throwStrength > 0){
			throwStrength = throwStrength - 1;
			Debug.Log ("STRENGTH IS " + throwStrength);
		}

	}

	void checkDrop() {
		if (Input.GetKeyDown(KeyCode.Q)) {
			dropObject ();
		}
	}

	void dropObject() {
		carrying = false;
		carriedObject.GetComponent<Rigidbody>().isKinematic = false;
		carriedObject = null;
	}

	void throwObject() {
		if (Input.GetMouseButtonDown(0)) {
			carriedObject.transform.position = transform.position + Camera.main.transform.forward * 2;
			Rigidbody rb = carriedObject.GetComponent<Rigidbody> ();
			rb.velocity = Camera.main.transform.forward * throwStrength;
			dropObject ();
		}
	}
}