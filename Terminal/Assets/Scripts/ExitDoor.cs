using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class ExitDoor : MonoBehaviour {
	float smooth = 2.0f;
	public float DoorOpenAngle = 90.0f;
	private bool open;
	private bool enter;
	public bool gameFinished;

	private Vector3 defaultRot;
	private Vector3 openRot;

    // audio stuff

    public AudioClip unlock;
    public AudioClip doorRattle;
    public AudioClip doorOpen;
	public AudioClip fail;

    bool doorWithoutKeyPlayed = false;

    void  Start (){
		defaultRot = transform.eulerAngles;
		openRot = new Vector3 (defaultRot.x, defaultRot.y + DoorOpenAngle, defaultRot.z);
        gameFinished = puzzle5ConveyorBelt.gameFinished;
        //print (haskey);
    }

	//Main function
	void  Update (){
        gameFinished = puzzle5ConveyorBelt.gameFinished;
        
        if (open) {
			//Open door
			transform.eulerAngles = Vector3.Slerp (transform.eulerAngles, openRot, Time.deltaTime * smooth);
        } else {
			//Close door
			transform.eulerAngles = Vector3.Slerp (transform.eulerAngles, defaultRot, Time.deltaTime * smooth);
		}
		if (((Input.GetKeyDown (KeyCode.E)) || (Global.prevState.Buttons.A == ButtonState.Released && Global.state.Buttons.A == ButtonState.Pressed)) && enter && !gameFinished) {
			Global.source.PlayOneShot (fail, Global.volumeMed);
		}
        if (((Input.GetKeyDown(KeyCode.E)) || (Global.prevState.Buttons.A == ButtonState.Released && Global.state.Buttons.A == ButtonState.Pressed)) && enter && gameFinished) {
            Global.source.PlayOneShot(unlock, Global.volumeMed);
            Global.source.PlayOneShot(doorOpen, Global.volumeMed);
            open = !open;
		}
        if (!doorWithoutKeyPlayed && ((Input.GetKeyDown(KeyCode.E)) || (Global.prevState.Buttons.A == ButtonState.Released && Global.state.Buttons.A == ButtonState.Pressed)) && enter && !gameFinished) {
            Global.currentCue = 3;
            doorWithoutKeyPlayed = true;
        }
        if (((Input.GetKeyDown(KeyCode.E)) || (Global.prevState.Buttons.A == ButtonState.Released && Global.state.Buttons.A == ButtonState.Pressed)) && enter && !gameFinished) {
            Global.source.PlayOneShot(doorRattle, Global.volumeMed);
        }
    }

	//Activate the Main function when player is near the door
	void  OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Player") {
			enter = true;
		}
	}

	//Deactivate the Main function when player goes away from door
	void  OnTriggerExit (Collider other) {
		if (other.gameObject.tag == "Player") {
			enter = false;
		}
	}
}