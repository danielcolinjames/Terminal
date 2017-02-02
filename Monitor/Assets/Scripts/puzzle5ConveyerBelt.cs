using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class puzzle5ConveyerBelt : MonoBehaviour {

    public Transform goal1;
    public Transform goal2;
    public Transform goal3;
    public Transform goal4;

    public Canvas monitorCanvas;
    public Text monitorText;

    bool puzzle5Started = false;

    public Transform puzzle5Plane;

    Vector3 puzzle5CameraPosition;

    void Start () {
        puzzle5CameraPosition = new Vector3(puzzle5Plane.position.x, puzzle5Plane.position.y + 10f, puzzle5Plane.position.z);
    }

    // Update is called once per frame
    void Update () {

        float speed = 5f;
        float step = speed * Time.deltaTime;

        if (Global.currentPuzzle == 5 && puzzle5Started == false) {

            float distance = Vector3.Distance(Global.monitorCamera.position, puzzle5CameraPosition);

            Global.monitorCamera.position = Vector3.MoveTowards(Global.monitorCamera.position, puzzle5CameraPosition, step);

            if (distance == 0) {
                // setting a flag when the camera has finished panning over
                // otherwise the player can move things around before the camera is finished panning
                puzzle5Started = true;
            }
        }


        if (Global.currentPuzzle == 5 && puzzle5Started) {
            // turn on the text on the screen
            monitorCanvas.enabled = true;

            monitorText.text = "TESTING WORDS WORDS WORDS CITIES CITIES CITIES";

            
        }
	}
}
