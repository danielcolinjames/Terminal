using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class OpenDoor : MonoBehaviour {
	float smooth = 2.0f;
	public float DoorOpenAngle = 90.0f;
	private bool open;
	private bool enter;
	public bool hasKey;

	private Vector3 defaultRot;
	private Vector3 openRot;

	void  Start (){
		defaultRot = transform.eulerAngles;
		openRot = new Vector3 (defaultRot.x, defaultRot.y + DoorOpenAngle, defaultRot.z);
        hasKey = PickupObject.hasKey;
        //print (haskey);
    }

	//Main function
	void  Update (){
        hasKey = PickupObject.hasKey;
        //print("OpenDoor.haskey = " + hasKey);

		if (open) {
			//Open door
			transform.eulerAngles = Vector3.Slerp (transform.eulerAngles, openRot, Time.deltaTime * smooth);
		} else {
			//Close door
			transform.eulerAngles = Vector3.Slerp (transform.eulerAngles, defaultRot, Time.deltaTime * smooth);
		}


		if (((Input.GetKeyDown(KeyCode.E)) || (Global.prevState.Buttons.A == ButtonState.Released && Global.state.Buttons.A == ButtonState.Pressed)) && enter && hasKey) {
			open = !open;
		}
	}

	//Activate the Main function when player is near the door
	void  OnTriggerEnter ( Collider other  ){
		if (other.gameObject.tag == "Player") {
			enter = true;
		}
	}

	//Deactivate the Main function when player is go away from door
	void  OnTriggerExit ( Collider other  ) {
		if (other.gameObject.tag == "Player") {
			enter = false;
		}
	}
}