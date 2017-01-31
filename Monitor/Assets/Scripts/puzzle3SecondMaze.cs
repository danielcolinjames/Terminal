﻿using UnityEngine;
using System.Collections;

public class puzzle3SecondMaze : MonoBehaviour {

    GameObject[] redBoxes;
    GameObject[] blueBoxes;
    GameObject[] greenBoxes;
    GameObject[] yellowBoxes;

    // Global objects (accessed by other scripts)


    // other stuff
    bool puzzleThreeStarted = false;

    float goalRange = 2f;

    public static bool redDone = false;
    public static bool blueDone = false;
    public static bool greenDone = false;
    public static bool yellowDone = false;
    
    // Warehouse items

    public static Renderer redPlane;
    float distanceFromRedGoal = 999;
    public Light redLight;
    int redCount = 0;
    public Transform redGoal;

    public static Renderer bluePlane;
    float distanceFromBlueGoal = 999;
    public Light blueLight;
    int blueCount = 0;
    public Transform blueGoal;

    public static Renderer greenPlane;
    float distanceFromGreenGoal = 999;
    public Light greenLight;
    int greenCount = 0;
    public Transform greenGoal;

    public static Renderer yellowPlane;
    float distanceFromYellowGoal = 999;
    public Light yellowLight;
    int yellowCount = 0;
    public Transform yellowGoal;

    public Transform redPlaneProgress;
    public Transform bluePlaneProgress;
    public Transform greenPlaneProgress;
    public Transform yellowPlaneProgress;

    public float acceptableDistance = 0.5f;

    // super maze stuff
    public Transform superMazeRedGoal;
    public Transform superMazeBlueGoal;
    public Transform superMazeGreenGoal;
    public Transform superMazeYellowGoal;

    public Transform superMazeNavigator;
    
    float distanceFromSuperMazeRedGoal;
    float distanceFromSuperMazeBlueGoal;
    float distanceFromSuperMazeGreenGoal;
    float distanceFromSuperMazeYellowGoal;


    // public Transform puzzleTwoSelector;

    void Awake() {

        redBoxes = GameObject.FindGameObjectsWithTag("RedBox");
        blueBoxes = GameObject.FindGameObjectsWithTag("BlueBox");
        greenBoxes = GameObject.FindGameObjectsWithTag("GreenBox");
        yellowBoxes = GameObject.FindGameObjectsWithTag("YellowBox");

        redPlane = GameObject.FindGameObjectWithTag("RedPlane").GetComponent<Renderer>();
        bluePlane = GameObject.FindGameObjectWithTag("BluePlane").GetComponent<Renderer>();
        greenPlane = GameObject.FindGameObjectWithTag("GreenPlane").GetComponent<Renderer>();
        yellowPlane = GameObject.FindGameObjectWithTag("YellowPlane").GetComponent<Renderer>();

        redPlane.enabled = false;
        bluePlane.enabled = false;
        greenPlane.enabled = false;
        yellowPlane.enabled = false;
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Global.currentPuzzle == 3) {

            // move camera across from puzzle one
            float speed = 25f;
            float step = speed * Time.deltaTime;

            Vector3 superMazeCameraPosition = new Vector3(superMazeNavigator.position.x, superMazeNavigator.position.y + 10f, superMazeNavigator.position.z);

            float distance = Vector3.Distance(Global.monitorCamera.position, superMazeCameraPosition);

            // pan camera across
            Global.monitorCamera.position = Vector3.MoveTowards(Global.monitorCamera.position, superMazeCameraPosition, step);

            if (distance == 0) {
                // setting a flag when the camera has finished panning over
                // otherwise the player can move the puzzleTwoSelector around before puzzle one is finished
                puzzleThreeStarted = true;
            }
        }
        
        else if (Global.currentPuzzle == 4 || Global.currentPuzzle == 5 || Global.currentPuzzle == 6 || Global.currentPuzzle == 7) {

            foreach (GameObject redBox in redBoxes) {
                Transform redBoxT = redBox.transform;
                distanceFromRedGoal = Vector3.Distance(redBoxT.position, redGoal.position);
                if (distanceFromRedGoal < goalRange && redLight.enabled == true && redBox.activeSelf == true) {
                    redCount++;
                    redBox.SetActive(false);
                }
            }

            foreach (GameObject blueBox in blueBoxes) {
                Transform blueBoxT = blueBox.transform;
                distanceFromBlueGoal = Vector3.Distance(blueBoxT.position, blueGoal.position);
                if (distanceFromBlueGoal < goalRange && blueLight.enabled == true && blueBox.activeSelf == true) {
                    blueCount++;
                    blueBox.SetActive(false);
                }
            }

            foreach (GameObject greenBox in greenBoxes) {
                Transform greenBoxT = greenBox.transform;
                distanceFromGreenGoal = Vector3.Distance(greenBoxT.position, greenGoal.position);
                if (distanceFromGreenGoal < goalRange && greenLight.enabled == true && greenBox.activeSelf == true) {
                    greenCount++;
                    greenBox.SetActive(false);
                }
            }

            foreach (GameObject yellowBox in yellowBoxes) {
                Transform yellowBoxT = yellowBox.transform;
                distanceFromYellowGoal = Vector3.Distance(yellowBoxT.position, yellowGoal.position);
                if (distanceFromYellowGoal < goalRange && yellowLight.enabled == true && yellowBox.activeSelf == true) {
                    yellowCount++;
                    yellowBox.SetActive(false);
                }
            }
        }
    }

    void FixedUpdate() {
        if (Global.currentPuzzle == 3) {
            if (MonitorMode.monitorMode == true) {

                float movementSpeed = 0.005f;

                // locks rotation of light

                //puzzleTwoSelector.transform.rotation = Quaternion.Euler(puzzleTwoSelector.transform.rotation.eulerAngles.x, Global.lockPos, Global.lockPos);

                if (puzzleThreeStarted) {
                    // TODO: limits, eg: if (pos.x >) { ... }
                    
                    // up and down
                    superMazeNavigator.Translate(Vector3.back * Global.state.ThumbSticks.Left.Y * movementSpeed);
                    if (Input.GetKey(KeyCode.S)) superMazeNavigator.Translate(Vector3.forward * 2 * movementSpeed);

                    superMazeNavigator.Translate(Vector3.forward * -Global.state.ThumbSticks.Left.Y * movementSpeed);
                    if (Input.GetKey(KeyCode.W)) superMazeNavigator.Translate(Vector3.back * 2 * movementSpeed);

                    // left and right
                    superMazeNavigator.Translate(Vector3.right * -Global.state.ThumbSticks.Left.X * movementSpeed);
                    if (Input.GetKey(KeyCode.D)) superMazeNavigator.Translate(Vector3.left * 2 * movementSpeed);

                    superMazeNavigator.Translate(Vector3.left * Global.state.ThumbSticks.Left.X * movementSpeed);
                    if (Input.GetKey(KeyCode.A)) superMazeNavigator.Translate(Vector3.right * 2 * movementSpeed);

                    // distance calculation (inside super maze)
                    distanceFromSuperMazeRedGoal = Vector3.Distance(superMazeRedGoal.position, superMazeNavigator.position);
                    distanceFromSuperMazeBlueGoal = Vector3.Distance(superMazeBlueGoal.position, superMazeNavigator.position);
                    distanceFromSuperMazeGreenGoal = Vector3.Distance(superMazeGreenGoal.position, superMazeNavigator.position);
                    distanceFromSuperMazeYellowGoal = Vector3.Distance(superMazeYellowGoal.position, superMazeNavigator.position);

                    float superMazeGoalTolerance = 0.058f;

                    if (!redDone && distanceFromSuperMazeRedGoal < superMazeGoalTolerance) {
                        redPlane.enabled = true;
                        redLight.enabled = true;
                        
                        Global.currentPuzzle = 5;
                    } else {
                        redPlane.enabled = false;
                        redLight.enabled = false;
                    }

                    if (!blueDone && distanceFromSuperMazeBlueGoal < superMazeGoalTolerance) {
                        bluePlane.enabled = true;
                        blueLight.enabled = true;

                        Global.currentPuzzle = 5;
                    } else {
                        bluePlane.enabled = false;
                        blueLight.enabled = false;
                    }

                    if (!greenDone && distanceFromSuperMazeGreenGoal < superMazeGoalTolerance) {
                        greenPlane.enabled = true;
                        greenLight.enabled = true;

                        Global.currentPuzzle = 6;
                    } else {
                        greenPlane.enabled = false;
                        greenLight.enabled = false;
                    }

                    if (!yellowDone && distanceFromSuperMazeYellowGoal < superMazeGoalTolerance) {
                        yellowPlane.enabled = true;
                        yellowLight.enabled = true;

                        Global.currentPuzzle = 7;
                    } else {
                        yellowPlane.enabled = false;
                        yellowLight.enabled = false;
                    }
                }
            }
        }

        // if one of the sub puzzles has been activated, start displaying the progress with the consoles
        else if (Global.currentPuzzle == 3 || Global.currentPuzzle == 4 || Global.currentPuzzle == 5 || Global.currentPuzzle == 6 || Global.currentPuzzle == 7) {

            float defaultX = 0.061f;
            float defaultY = 0.1f;
            float defaultZ = 0.044f;

            if (redCount < 1) {
                // default size of the box on the screen
                redPlaneProgress.localScale = new Vector3(defaultX, defaultY, defaultZ);
            } else {
                // multiply the X and Z scale by 1 minus (redCount over redBoxes.length) to make the box on the monitor scale relative to how many red boxes there are in the scene
                redPlaneProgress.localScale = new Vector3(defaultX * (1 - ((float)redCount / (float)redBoxes.Length)), defaultY, defaultZ * (1 - ((float)redCount / (float)redBoxes.Length)));
            }

            if (blueCount < 1) {
                bluePlaneProgress.localScale = new Vector3(defaultX, defaultY, defaultZ);
            } else {
                bluePlaneProgress.localScale = new Vector3(defaultX * (1 - ((float)blueCount / (float)blueBoxes.Length)), defaultY, defaultZ * (1 - ((float)blueCount / (float)blueBoxes.Length)));
            }

            if (greenCount < 1) {
                greenPlaneProgress.localScale = new Vector3(defaultX, defaultY, defaultZ);
            } else {
                greenPlaneProgress.localScale = new Vector3(defaultX * (1 - ((float)greenCount / (float)greenBoxes.Length)), defaultY, defaultZ * (1 - ((float)greenCount / (float)greenBoxes.Length)));
            }

            if (yellowCount < 1) {
                yellowPlaneProgress.localScale = new Vector3(defaultX, defaultY, defaultZ);
            } else {
                yellowPlaneProgress.localScale = new Vector3(defaultX * (1 - ((float)yellowCount / (float)yellowBoxes.Length)), defaultY, defaultZ * (1 - ((float)yellowCount / (float)yellowBoxes.Length)));
            }

            if (redCount == redBoxes.Length) redDone = true;
            if (blueCount == blueBoxes.Length) blueDone = true;
            if (greenCount == greenBoxes.Length) greenDone = true;
            if (yellowCount == yellowBoxes.Length) yellowDone = true;

            if (redDone) {
                Global.currentPuzzle = 5;
                redPlane.enabled = false;
                redLight.enabled = false;
            }

            if (blueDone) {
                bluePlane.enabled = false;
                blueLight.enabled = false;
            }

            if (greenDone) {
                greenPlane.enabled = false;
                greenLight.enabled = false;
            }

            if (yellowDone) {
                yellowPlane.enabled = false;
                yellowLight.enabled = false;
            }

            if (redDone && blueDone && greenDone && yellowDone) {
                Global.currentPuzzle = 8;
                print("DONE");
            }
        }
    }
}