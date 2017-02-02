using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;

public class puzzle5ConveyerBelt : MonoBehaviour {

    public Transform goal1;
    public Transform goal2;
    public Transform goal3;
    public Transform goal4;

    // cubes
    public Transform s1c1;
    public Transform s1c2;
    public Transform s1c3;
    public Transform s1c4;

    //public Transform s2c1;
    //public Transform s2c2;
    //public Transform s2c3;
    //public Transform s2c4;

    //public Transform s3c1;
    //public Transform s3c2;
    //public Transform s3c3;
    //public Transform s3c4;

    //public Transform s4c1;
    //public Transform s4c2;
    //public Transform s4c3;
    //public Transform s4c4;

    //public Transform s5c1;
    //public Transform s5c2;
    //public Transform s5c3;
    //public Transform s5c4;
    // end cubes

    // stuff for this puzzle
    int currentStage = 1;

    public AudioClip successSound;
    public AudioClip failureSound;

    public Transform behindRedGoal;
    public Transform behindBlueGoal;
    public Transform behindGreenGoal;
    public Transform behindYellowGoal;

    float goalTolerance = 0.5f;

    public Transform player;
    public Transform conveyerBeltCheck;

    public Transform largeGoal;

    bool c1complete = false;

    GameObject[] s1cubes;
    GameObject[] s2cubes;
    GameObject[] s3cubes;
    GameObject[] s4cubes;
    GameObject[] s5cubes;

    public Renderer whitePlane;
    public Renderer redPlane;
    public Renderer greenPlane;

    float speed;
    float step;


    // stuff for the monitor camera
    public Canvas monitorCanvas;
    public Text monitorText;

    bool puzzle5Started = false;
    public Transform puzzle5Plane;
    Vector3 puzzle5CameraPosition;



    bool cubesReleased = false;


    void Start () {
        speed = 5f;
        step = speed * Time.deltaTime;

        puzzle5CameraPosition = new Vector3(puzzle5Plane.position.x, puzzle5Plane.position.y + 10f, puzzle5Plane.position.z);

        redPlane.enabled = false;
        greenPlane.enabled = false;

        s1cubes = GameObject.FindGameObjectsWithTag("s1cube");
    }

    // Update is called once per frame
    void Update () {

        

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

            if (c1complete == true) {
                moveBoxesIntoWall();
            }

            if (currentStage == 1) {
                monitorText.text = "PACKAGES NEED TO BE DELIVERED";

                s1c1.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                s1c2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                s1c3.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                s1c4.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

                if (cubesReleased == false) {

                    s1c1.position = behindRedGoal.position;
                    s1c1.position = s1c1.transform.position + new Vector3(1f, 0f, 0);
                    s1c1.GetComponent<Rigidbody>().velocity = new Vector3(14f, 0.25f, 0);

                    s1c2.position = behindGreenGoal.position;
                    s1c2.position = s1c2.transform.position + new Vector3(1f, 0f, 0);
                    s1c2.GetComponent<Rigidbody>().velocity = new Vector3(14f, 0.25f, 0);

                    s1c3.position = behindBlueGoal.position;
                    s1c3.position = s1c3.transform.position + new Vector3(-1f, 0f, 0);
                    s1c3.GetComponent<Rigidbody>().velocity = new Vector3(-14f, 0.25f, 0);

                    s1c4.position = behindYellowGoal.position;
                    s1c4.position = s1c4.transform.position + new Vector3(-1f, 0f, 0);
                    s1c4.GetComponent<Rigidbody>().velocity = new Vector3(-14f, 0.25f, 0);

                    cubesReleased = true;
                }
                checkPlacementOfBoxes();
            }
        }
	}

    void checkPlacementOfBoxes() {
        float distanceFromConveyerBelt = Vector3.Distance(player.position, conveyerBeltCheck.position);

        float c1d = 100;
        float c2d = 100;
        float c3d = 100;
        float c4d = 100;

        if(currentStage == 1) {
            c1d = Vector3.Distance(s1c1.position, goal1.position);
            c2d = Vector3.Distance(s1c2.position, goal2.position);
            c3d = Vector3.Distance(s1c3.position, goal3.position);
            c4d = Vector3.Distance(s1c4.position, goal4.position);
        }

        //else if (currentStage == 2) {
        //    c1d = Vector3.Distance(s2c1.position, goal1.position);
        //    c2d = Vector3.Distance(s2c2.position, goal2.position);
        //    c3d = Vector3.Distance(s2c3.position, goal3.position);
        //    c4d = Vector3.Distance(s2c4.position, goal4.position);
        //}
        
        //else if (currentStage == 3) {
        //    c1d = Vector3.Distance(s3c1.position, goal1.position);
        //    c2d = Vector3.Distance(s3c2.position, goal2.position);
        //    c3d = Vector3.Distance(s3c3.position, goal3.position);
        //    c4d = Vector3.Distance(s3c4.position, goal4.position);
        //}

        if (distanceFromConveyerBelt < 1.2f) {
            if (Input.GetKeyDown(KeyCode.E) || (Global.prevState.Buttons.A == ButtonState.Released && Global.state.Buttons.A == ButtonState.Pressed)) {

                if (c1d < goalTolerance && c2d < goalTolerance && c3d < goalTolerance && c4d < goalTolerance) {
                    Global.source.PlayOneShot(successSound, Global.volumeMed);

                    whitePlane.enabled = false;
                    redPlane.enabled = false;
                    greenPlane.enabled = true;

                    //TODO: lots

                    c1complete = true;


                } else {
                    Global.source.PlayOneShot(failureSound, Global.volumeMed);

                    greenPlane.enabled = false;
                    redPlane.enabled = true;
                    whitePlane.enabled = false;
                }
            }
        }
    }

    void moveBoxesIntoWall() {
        float speed = 2f;
        float step = speed * Time.deltaTime;

        if (currentStage == 1) {
            float distanceFromLargeGoal;

            for (int i = 0; i < s1cubes.Length; i++) {
                GameObject cube = s1cubes[i];

                Transform cubeT = cube.transform;
                distanceFromLargeGoal = Vector3.Distance(cubeT.position, largeGoal.position);

                cubeT.position = Vector3.MoveTowards(cubeT.position, largeGoal.position, step);

                float cubeDistance = Vector3.Distance(cubeT.position, largeGoal.position);

                if (cubeDistance < 0.5) {
                    cube.SetActive(false);
                }
            }
        } else if (currentStage == 2) {

        }
    }
}
