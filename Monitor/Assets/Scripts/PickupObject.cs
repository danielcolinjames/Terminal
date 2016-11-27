using UnityEngine;
using System.Collections;

public class PickupObject : MonoBehaviour {
	GameObject mainCamera;
	bool carrying; 
	GameObject carriedObject;
	public float distance;
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindWithTag ("MainCamera"); 
	}
	
	// Update is called once per frame
	void Update () {
		if (carrying) {
			carry(carriedObject);
		} else {
			pickup();
		}
	}

	void carry(GameObject o) {
		o.transform.position = mainCamera.transform.position + mainCamera.transform.forward * distance;
	}

	void pickup() {
		if (Input.GetKeyDown (KeyCode.E)) {
			int x = Screen.width / 2;
			int y = Screen.height / 2;

			Ray ray = Camera.main.ScreenPointToRay (new Vector3 (x,y));
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				Pickupable p = hit.collider.GetComponent<Pickupable>();
				if (p != null) {
					carrying = true;
					carriedObject = p.gameObject;
					carriedObject.GetComponent<Rigidbody> ().isKinematic = true;
				}
			} 
		}
	}

	void putdown() {
		if (Input.GetKeyDown (KeyCode.E)) {
			dropObject ();
		}
	}

	void dropObject() {
		carrying = false;
		carriedObject = null;
		carriedObject.GetComponent<Rigidbody> ().isKinematic = false;
	}
}