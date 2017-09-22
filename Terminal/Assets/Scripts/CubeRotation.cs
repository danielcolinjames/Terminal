using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotation : MonoBehaviour {
    public Transform rotatingCube;
    bool rotatingPuzzleStarted = false;

    float movementSpeed = 0.5f;
    


    // pan camera across
    
    
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        //       float speed = 5f;
        //       float step = speed * Time.deltaTime;

        //       if (Global.currentPuzzle == 6 && rotatingPuzzleStarted == false) {
        //           Vector3 aboveRotatingCube = new Vector3(rotatingCube.position.x, rotatingCube.position.y + 10f, rotatingCube.position.z);

        //           Global.monitorCamera.position = Vector3.MoveTowards(Global.monitorCamera.position, aboveRotatingCube, step);

        //           // move camera across from puzzle one
        //           float distance = Vector3.Distance(Global.monitorCamera.position, aboveRotatingCube);

        //           if (distance == 0) {
        //               rotatingPuzzleStarted = true;
        //           }
        //       }

        //       if (Global.currentPuzzle == 6 && rotatingPuzzleStarted) {
        //           if (MonitorMode.monitorMode == true) {
        //               // up and down
        //               //rotatingCube.Rotate(Vector3.back * Global.state.ThumbSticks.Left.Y * movementSpeed);
        //               if (Input.GetKey(KeyCode.W)) rotatingCube.Rotate(Vector3.forward * 150 * Time.deltaTime);

        //               //rotatingCube.Rotate(Vector3.forward * -Global.state.ThumbSticks.Left.Y * movementSpeed);
        //               if (Input.GetKey(KeyCode.S)) rotatingCube.Rotate(Vector3.back * 150 * Time.deltaTime);

        //               // left and right
        //               //rotatingCube.Rotate(Vector3.right * -Global.state.ThumbSticks.Left.X * movementSpeed);
        //               //if (Input.GetKey(KeyCode.D)) rotatingCube.Rotate(new Vector3(rotatingCube.rotation.x + 1f, rotatingCube.rotation.y, rotatingCube.rotation.z) * 2 * movementSpeed);
        //               if (Input.GetKey(KeyCode.A)) rotatingCube.Rotate(Vector3.up * 150 * Time.deltaTime);

        //               //rotatingCube.Rotate(Vector3.left * Global.state.ThumbSticks.Left.X * movementSpeed);
        //               //if (Input.GetKey(KeyCode.A)) rotatingCube.Rotate(new Vector3(rotatingCube.rotation.x - 1f, rotatingCube.rotation.y, rotatingCube.rotation.z) * 2 * movementSpeed);
        //               if (Input.GetKey(KeyCode.D)) rotatingCube.Rotate(Vector3.down * 150 * Time.deltaTime);
        //           }
        //       }
    }
}
