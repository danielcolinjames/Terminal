using UnityEngine;
using System.Collections;

public class PuzzleThree : MonoBehaviour
{

    public GameObject[] redBoxes;
    public GameObject[] blueBoxes;
    public GameObject[] greenBoxes;
    public GameObject[] yellowBoxes;

    // Global objects (accessed by other scripts)
    public static Transform puzzleTwoFallingBox;
    public static Transform key;

    // other stuff
    public bool puzzleThreeStarted = false;

    public float goalRange = 2f;

    public bool redDone = false;
    public bool blueDone = false;
    public bool greenDone = false;
    public bool yellowDone = false;
    
    // Warehouse items

    public static Renderer redPlane;
    public float distanceFromRedGoal = 999;
    public Light redLight;
    public int redCount = 0;
    public Transform redGoal;

    public static Renderer bluePlane;
    public float distanceFromBlueGoal = 999;
    public Light blueLight;
    public int blueCount = 0;
    public Transform blueGoal;

    public static Renderer greenPlane;
    public float distanceFromGreenGoal = 999;
    public Light greenLight;
    public int greenCount = 0;
    public Transform greenGoal;

    public static Renderer yellowPlane;
    public float distanceFromYellowGoal = 999;
    public Light yellowLight;
    public int yellowCount = 0;
    public Transform yellowGoal;

    // planes (on consoles)
    public Transform redPlaneProgress;
    public Transform bluePlaneProgress;
    public Transform greenPlaneProgress;
    public Transform yellowPlaneProgress;


    // Monitor items
    public Transform puzzleTwoRedBox;
    public Transform puzzleTwoBlueBox;
    public Transform puzzleTwoGreenBox;
    public Transform puzzleTwoYellowBox;

    public float distanceFromYellowX;
    public float distanceFromYellowZ;

    public float distanceFromRedX;
    public float distanceFromRedZ;

    public float distanceFromGreenX;
    public float distanceFromGreenZ;

    public float distanceFromBlueX;
    public float distanceFromBlueZ;

    public float acceptableDistance = 0.5f;

    public Transform puzzleTwoSelector;

    void Awake() {

        redBoxes = GameObject.FindGameObjectsWithTag("RedBox");
        blueBoxes = GameObject.FindGameObjectsWithTag("BlueBox");
        greenBoxes = GameObject.FindGameObjectsWithTag("GreenBox");
        yellowBoxes = GameObject.FindGameObjectsWithTag("YellowBox");

        redPlane = GameObject.FindGameObjectWithTag("RedPlane").GetComponent<Renderer>();
        bluePlane = GameObject.FindGameObjectWithTag("BluePlane").GetComponent<Renderer>();
        greenPlane = GameObject.FindGameObjectWithTag("GreenPlane").GetComponent<Renderer>();
        yellowPlane = GameObject.FindGameObjectWithTag("YellowPlane").GetComponent<Renderer>();

        redPlaneProgress = GameObject.FindGameObjectWithTag("RedPlane").transform;
        bluePlaneProgress = GameObject.FindGameObjectWithTag("BluePlane").transform;
        greenPlaneProgress = GameObject.FindGameObjectWithTag("GreenPlane").transform;
        yellowPlaneProgress = GameObject.FindGameObjectWithTag("YellowPlane").transform;

        redPlane.enabled = false;
        bluePlane.enabled = false;
        greenPlane.enabled = false;
        yellowPlane.enabled = false;

        // need to be in Awake() instead of Start() or else Unity has tons of errors
        // puzzle two objects
        puzzleTwoFallingBox = GameObject.FindGameObjectWithTag("FallingBox").transform;
        key = GameObject.FindGameObjectWithTag("isKey").transform;

        puzzleTwoRedBox = GameObject.FindGameObjectWithTag("PuzzleTwoRedBox").transform;
        puzzleTwoBlueBox = GameObject.FindGameObjectWithTag("PuzzleTwoBlueBox").transform;
        puzzleTwoGreenBox = GameObject.FindGameObjectWithTag("PuzzleTwoGreenBox").transform;
        puzzleTwoYellowBox = GameObject.FindGameObjectWithTag("PuzzleTwoYellowBox").transform;

        redGoal = GameObject.FindGameObjectWithTag("RedGoal").transform;
        blueGoal = GameObject.FindGameObjectWithTag("BlueGoal").transform;
        greenGoal = GameObject.FindGameObjectWithTag("GreenGoal").transform;
        yellowGoal = GameObject.FindGameObjectWithTag("YellowGoal").transform;
        
        // public Transform flashlight = GameObject.FindGameObjectWithTag("Flashlight").GetComponent<Light>();

        redLight = GameObject.FindGameObjectWithTag("RedLight").GetComponent<Light>();
        blueLight = GameObject.FindGameObjectWithTag("BlueLight").GetComponent<Light>();
        greenLight = GameObject.FindGameObjectWithTag("GreenLight").GetComponent<Light>();
        yellowLight = GameObject.FindGameObjectWithTag("YellowLight").GetComponent<Light>();

        puzzleTwoSelector = GameObject.FindGameObjectWithTag("PuzzleTwoSelector").transform;
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

            Vector3 puzzleTwoCameraPosition = new Vector3(-2.02f, 7.88f, 59.44f);

            float distance = Vector3.Distance(Global.monitorCamera.position, puzzleTwoCameraPosition);

            // pan camera across
            Global.monitorCamera.position = Vector3.MoveTowards(Global.monitorCamera.position, puzzleTwoCameraPosition, step);

            if (distance == 0) {
                // setting a flag when the camera has finished panning over
                // otherwise the player can move the puzzleTwoSelector around before puzzle one is finished
                puzzleThreeStarted = true;
            }

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

        if (MonitorMode.monitorMode == true) {
            if (Global.currentPuzzle == 3) {
                distanceFromBlueX = Mathf.Abs(puzzleTwoSelector.position.x - puzzleTwoBlueBox.position.x);
                distanceFromBlueZ = Mathf.Abs(puzzleTwoSelector.position.z - puzzleTwoBlueBox.position.z);

                distanceFromRedX = Mathf.Abs(puzzleTwoSelector.position.x - puzzleTwoRedBox.position.x);
                distanceFromRedZ = Mathf.Abs(puzzleTwoSelector.position.z - puzzleTwoRedBox.position.z);

                distanceFromGreenX = Mathf.Abs(puzzleTwoSelector.position.x - puzzleTwoGreenBox.position.x);
                distanceFromGreenZ = Mathf.Abs(puzzleTwoSelector.position.z - puzzleTwoGreenBox.position.z);

                distanceFromYellowX = Mathf.Abs(puzzleTwoSelector.position.x - puzzleTwoYellowBox.position.x);
                distanceFromYellowZ = Mathf.Abs(puzzleTwoSelector.position.z - puzzleTwoYellowBox.position.z);
            }
        }
    }

    void FixedUpdate() {
        if (Global.currentPuzzle == 3) {
            if (MonitorMode.monitorMode == true) {

                float movementSpeed = 0.05f;

                // locks rotation of light
                puzzleTwoSelector.transform.rotation = Quaternion.Euler(puzzleTwoSelector.transform.rotation.eulerAngles.x, Global.lockPos, Global.lockPos);

                if (puzzleThreeStarted) {
                    // TODO: limits, eg: if (pos.x >) { ... }
                    // up and down
                    puzzleTwoSelector.Translate(Vector3.down * Global.state.ThumbSticks.Left.Y * movementSpeed);
                    if (Input.GetKey(KeyCode.S)) puzzleTwoSelector.Translate(Vector3.up * 2 * movementSpeed);

                    puzzleTwoSelector.Translate(Vector3.up * -Global.state.ThumbSticks.Left.Y * movementSpeed);
                    if (Input.GetKey(KeyCode.W)) puzzleTwoSelector.Translate(Vector3.down * 2 * movementSpeed);

                    // left and right
                    puzzleTwoSelector.Translate(Vector3.right * -Global.state.ThumbSticks.Left.X * movementSpeed);
                    if (Input.GetKey(KeyCode.D)) puzzleTwoSelector.Translate(Vector3.left * 2 * movementSpeed);

                    puzzleTwoSelector.Translate(Vector3.left * Global.state.ThumbSticks.Left.X * movementSpeed);
                    if (Input.GetKey(KeyCode.A)) puzzleTwoSelector.Translate(Vector3.right * 2 * movementSpeed);


                    if (distanceFromBlueX < acceptableDistance && distanceFromBlueZ < acceptableDistance) {
                        bluePlane.enabled = true;
                        blueLight.enabled = true;
                    } else {
                        bluePlane.enabled = false;
                        blueLight.enabled = false;
                    }
                    
                    if (distanceFromRedX < acceptableDistance && distanceFromRedZ < acceptableDistance) {
                        redPlane.enabled = true;
                        redLight.enabled = true;
                    } else {
                        redPlane.enabled = false;
                        redLight.enabled = false;
                    }

                    if (distanceFromGreenX < acceptableDistance && distanceFromGreenZ < acceptableDistance) {
                        greenPlane.enabled = true;
                        greenLight.enabled = true;
                    } else {
                        greenPlane.enabled = false;
                        greenLight.enabled = false;
                    }

                    if (distanceFromYellowX < acceptableDistance && distanceFromYellowZ < acceptableDistance) {
                        yellowLight.enabled = true;
                        yellowPlane.enabled = true;
                    } else {
                        yellowLight.enabled = false;
                        yellowPlane.enabled = false;
                    }
                }
            }


            if (redCount < 1) {
                // default size of the box on the screen
                redPlaneProgress.localScale = new Vector3(0.077f, 0.1f, 0.059f);
            } else {
                // multiply the X and Z scale by 1 minus (redCount over redBoxes.length) to make the box on the monitor scale relative to how many red boxes there are in the scene
                redPlaneProgress.localScale = new Vector3(0.077f * (1 - ((float)redCount / (float)redBoxes.Length)), 0.1f, 0.059f * (1 - ((float)redCount / (float)redBoxes.Length)));
            }

            if (blueCount < 1) {
                bluePlaneProgress.localScale = new Vector3(0.077f, 0.1f, 0.059f);
            } else {
                bluePlaneProgress.localScale = new Vector3(0.077f * (1 - ((float)blueCount / (float)blueBoxes.Length)), 0.1f, 0.059f * (1 - ((float)blueCount / (float)blueBoxes.Length)));
            }

            if (greenCount < 1) {
                greenPlaneProgress.localScale = new Vector3(0.077f, 0.1f, 0.059f);
            } else {
                greenPlaneProgress.localScale = new Vector3(0.077f * (1 - ((float)greenCount / (float)greenBoxes.Length)), 0.1f, 0.059f * (1 - ((float)greenCount / (float)greenBoxes.Length)));
            }

            if (yellowCount < 1) {
                yellowPlaneProgress.localScale = new Vector3(0.077f, 0.1f, 0.059f);
            } else {
                yellowPlaneProgress.localScale = new Vector3(0.077f * (1 - ((float)yellowCount / (float)yellowBoxes.Length)), 0.1f, 0.059f * (1 - ((float)yellowCount / (float)yellowBoxes.Length)));
            }


            if (redCount == redBoxes.Length) redDone = true;
            if (blueCount == blueBoxes.Length) blueDone = true;
            if (greenCount == greenBoxes.Length) greenDone = true;
            if (yellowCount == yellowBoxes.Length) yellowDone = true;
            

            if (redDone && blueDone && greenDone && yellowDone) {
                Global.currentPuzzle = 4;
                print("YOU WIN!");
            }
        }
    }
}