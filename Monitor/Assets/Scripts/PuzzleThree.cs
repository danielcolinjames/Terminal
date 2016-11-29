using UnityEngine;
using System.Collections;

public class PuzzleThree : MonoBehaviour
{

    // Global objects (accessed by other scripts)
    public static Transform puzzleTwoFallingBox;
    public static Transform key;

    // other stuff
    public bool puzzleTwoStarted = false;

    public float goalRange = 2f;

    Vector3 zeroVector = new Vector3(0, 0, 0);

    public bool redDone = false;
    public bool blueDone = false;
    public bool greenDone = false;
    public bool yellowDone = false;


    // Warehouse items
    public Light yellowLight;
    public int yellowCount;
    public Transform yellowGoal;
    public Transform yellowBoxOne;
    float yellowBoxOneDistance = 100;
    public Transform yellowBoxTwo;
    float yellowBoxTwoDistance = 100;
    public Transform yellowBoxThree;
    float yellowBoxThreeDistance = 100;


    public Light redLight;
    public int redCount;
    public Transform redGoal;
    public Transform redBoxOne;
    float redBoxOneDistance = 100;
    public Transform redBoxTwo;
    float redBoxTwoDistance = 100;
    public Transform redBoxThree;
    float redBoxThreeDistance = 100;

    public Light greenLight;
    public int greenCount;
    public Transform greenGoal;
    public Transform greenBoxOne;
    float greenBoxOneDistance = 100;
    public Transform greenBoxTwo;
    float greenBoxTwoDistance = 100;
    public Transform greenBoxThree;
    float greenBoxThreeDistance = 100;

    public Light blueLight;
    public int blueCount;
    public Transform blueGoal;
    public Transform blueBoxOne;
    float blueBoxOneDistance = 100;
    public Transform blueBoxTwo;
    float blueBoxTwoDistance = 100;
    public Transform blueBoxThree;
    float blueBoxThreeDistance = 100;

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

        redBoxOne = GameObject.FindGameObjectWithTag("RedBoxOne").transform;
        redBoxTwo = GameObject.FindGameObjectWithTag("RedBoxTwo").transform;
        redBoxThree = GameObject.FindGameObjectWithTag("RedBoxThree").transform;

        blueBoxOne = GameObject.FindGameObjectWithTag("BlueBoxOne").transform;
        blueBoxTwo = GameObject.FindGameObjectWithTag("BlueBoxTwo").transform;
        blueBoxThree = GameObject.FindGameObjectWithTag("BlueBoxThree").transform;

        greenBoxOne = GameObject.FindGameObjectWithTag("GreenBoxOne").transform;
        greenBoxTwo = GameObject.FindGameObjectWithTag("GreenBoxTwo").transform;
        greenBoxThree = GameObject.FindGameObjectWithTag("GreenBoxThree").transform;

        yellowBoxOne = GameObject.FindGameObjectWithTag("YellowBoxOne").transform;
        yellowBoxTwo = GameObject.FindGameObjectWithTag("YellowBoxTwo").transform;
        yellowBoxThree = GameObject.FindGameObjectWithTag("YellowBoxThree").transform;

        // public Transform flashlight = GameObject.FindGameObjectWithTag("Flashlight").GetComponent<Light>();
        yellowLight = GameObject.FindGameObjectWithTag("YellowLight").GetComponent<Light>();
        
        redLight = GameObject.FindGameObjectWithTag("RedLight").GetComponent<Light>();
        greenLight = GameObject.FindGameObjectWithTag("GreenLight").GetComponent<Light>();
        blueLight = GameObject.FindGameObjectWithTag("BlueLight").GetComponent<Light>();

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

            Vector3 puzzleTwoCameraPosition = new Vector3(-29f, 10f, 63f);

            float distance = Vector3.Distance(Global.monitorCamera.position, puzzleTwoCameraPosition);

            // pan camera across
            Global.monitorCamera.position = Vector3.MoveTowards(Global.monitorCamera.position, puzzleTwoCameraPosition, step);

            if (distance == 0) {
                // setting a flag when the camera has finished panning over
                // otherwise the player can move the puzzleTwoSelector around before puzzle one is finished
                puzzleTwoStarted = true;
            }

            // update box distances here (was doing it inside "if (monitorMode)" before...woops)
            redBoxOneDistance = Vector3.Distance(redBoxOne.position, redGoal.position);
            redBoxTwoDistance = Vector3.Distance(redBoxTwo.position, redGoal.position);
            redBoxThreeDistance = Vector3.Distance(redBoxThree.position, redGoal.position);

            blueBoxOneDistance = Vector3.Distance(blueBoxOne.position, blueGoal.position);
            blueBoxTwoDistance = Vector3.Distance(blueBoxTwo.position, blueGoal.position);
            blueBoxThreeDistance = Vector3.Distance(blueBoxThree.position, blueGoal.position);

            greenBoxOneDistance = Vector3.Distance(greenBoxOne.position, greenGoal.position);
            greenBoxTwoDistance = Vector3.Distance(greenBoxTwo.position, greenGoal.position);
            greenBoxThreeDistance = Vector3.Distance(greenBoxThree.position, greenGoal.position);

            yellowBoxOneDistance = Vector3.Distance(yellowBoxOne.position, yellowGoal.position);
            yellowBoxTwoDistance = Vector3.Distance(yellowBoxTwo.position, yellowGoal.position);
            yellowBoxThreeDistance = Vector3.Distance(yellowBoxThree.position, yellowGoal.position);

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

                if (puzzleTwoStarted) {
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


                    blueLight.enabled = (distanceFromBlueX < acceptableDistance && distanceFromBlueZ < acceptableDistance);
                    redLight.enabled = (distanceFromRedX < acceptableDistance && distanceFromRedZ < acceptableDistance);
                    greenLight.enabled = (distanceFromGreenX < acceptableDistance && distanceFromGreenZ < acceptableDistance);
                    yellowLight.enabled = (distanceFromYellowX < acceptableDistance && distanceFromYellowZ < acceptableDistance);
                }

            }
        


            if (blueLight.enabled) {
                blueCount = 0;
                if (blueBoxOneDistance < goalRange) blueCount++;
                if (blueBoxTwoDistance < goalRange) blueCount++;
                if (blueBoxThreeDistance < goalRange) blueCount++;
            }
            if (redLight.enabled) {
                redCount = 0;
                if (redBoxOneDistance < goalRange) redCount++;
                if (redBoxTwoDistance < goalRange) redCount++;
                if (redBoxThreeDistance < goalRange) redCount++;
            }
            if (greenLight.enabled) {
                greenCount = 0;
                if (greenBoxOneDistance < goalRange) greenCount++;
                if (greenBoxTwoDistance < goalRange) greenCount++;
                if (greenBoxThreeDistance < goalRange) greenCount++;
            }
            if (yellowLight.enabled) {
                yellowCount = 0;
                if (yellowBoxOneDistance < goalRange) yellowCount++;
                if (yellowBoxTwoDistance < goalRange) yellowCount++;
                if (yellowBoxThreeDistance < goalRange) yellowCount++;
            }

            //print("YC: " + yellowCount);
            //print("BC: " + blueCount);
            //print("GC: " + greenCount);
            //print("RC: " + redCount);

            // red
            if (redCount == 0){
                puzzleTwoRedBox.localScale = new Vector3(3.95f, 0.78f, 4.13f);
            } else if (redCount == 1) {
                puzzleTwoRedBox.localScale = new Vector3(3.16f, 0.78f, 3.2f);
            } else if (redCount == 2) {
                puzzleTwoRedBox.localScale = new Vector3(2.16f, 0.78f, 2.2f);
                redDone = false;
            } else if (redCount == 3) {
                puzzleTwoRedBox.localScale = new Vector3(0, 0, 0);
                redDone = true;
            }

            // blue
            if (blueCount == 0) {
                puzzleTwoBlueBox.localScale = new Vector3(3.95f, 0.78f, 4.13f);
            } else if (blueCount == 1) {
                puzzleTwoBlueBox.localScale = new Vector3(3.16f, 0.78f, 3.2f);
            } else if (blueCount == 2) {
                puzzleTwoBlueBox.localScale = new Vector3(2.16f, 0.78f, 2.2f);
                blueDone = false;
            } else if (blueCount == 3) {
                puzzleTwoBlueBox.localScale = new Vector3(0, 0, 0);
                blueDone = true;
            }

            // green
            if (greenCount == 0) {
                puzzleTwoGreenBox.localScale = new Vector3(3.95f, 0.78f, 4.13f);
            } else if (greenCount == 1) {
                puzzleTwoGreenBox.localScale = new Vector3(3.16f, 0.78f, 3.2f);
            } else if (greenCount == 2) {
                puzzleTwoGreenBox.localScale = new Vector3(2.16f, 0.78f, 2.2f);
                greenDone = false;
            } else if (greenCount == 3) {
                puzzleTwoGreenBox.localScale = new Vector3(0, 0, 0);
                greenDone = true;
            }

            // yellow
            if (yellowCount == 0) {
                puzzleTwoYellowBox.localScale = new Vector3(3.95f, 0.78f, 4.13f);
            } else if (yellowCount == 1) {
                puzzleTwoYellowBox.localScale = new Vector3(3.16f, 0.78f, 3.2f);
            } else if (yellowCount == 2) {
                puzzleTwoYellowBox.localScale = new Vector3(2.16f, 0.78f, 2.2f);
                yellowDone = false;
            } else if (yellowCount == 3) {
                puzzleTwoYellowBox.localScale = new Vector3(0, 0, 0);
                yellowDone = true;
            }

            if (redDone && blueDone && greenDone && yellowDone) {
                Global.currentPuzzle = 4;
                print("YOU WIN!");
            }
        }
    }
}