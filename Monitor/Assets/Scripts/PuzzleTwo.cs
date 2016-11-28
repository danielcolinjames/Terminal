using UnityEngine;
using System.Collections;

public class PuzzleTwo : MonoBehaviour
{

    // Global objects (accessed by other scripts)
    public static Transform puzzleTwoFallingBox;


    // Warehouse items
    public Light yellowLight;
    public int yellowCount;
    public Transform yellowGoal;
    public Transform yellowBoxOne;
    float yellowBoxOneDistance;
    public Transform yellowBoxTwo;
    float yellowBoxTwoDistance;
    public Transform yellowBoxThree;
    float yellowBoxThreeDistance;

    public Light redLight;
    public int redCount;
    public Transform redGoal;
    public Transform redBoxOne;
    float redBoxOneDistance;
    public Transform redBoxTwo;
    float redBoxTwoDistance;
    public Transform redBoxThree;
    float redBoxThreeDistance;

    public Light greenLight;
    public int greenCount;
    public Transform greenGoal;
    public Transform greenBoxOne;
    float greenBoxOneDistance;
    public Transform greenBoxTwo;
    float greenBoxTwoDistance;
    public Transform greenBoxThree;
    float greenBoxThreeDistance;

    public Light blueLight;
    public int blueCount;
    public Transform blueGoal;
    public Transform blueBoxOne;
    float blueBoxOneDistance;
    public Transform blueBoxTwo;
    float blueBoxTwoDistance;
    public Transform blueBoxThree;
    float blueBoxThreeDistance;

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

    void Awake()
    {
        // need to be in Awake() instead of Start() or else Unity has tons of errors
        // puzzle two objects
        puzzleTwoFallingBox = GameObject.FindGameObjectWithTag("FallingBox").transform;

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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Global.currentPuzzle == 2)
        {

            float speed = 25f;
            float step = speed * Time.deltaTime;

            Vector3 puzzleTwoCameraPosition = new Vector3(-29f, 10f, 63f);

            Global.monitorCamera.position = Vector3.MoveTowards(Global.monitorCamera.position, puzzleTwoCameraPosition, step);

        }
        if (MonitorMode.monitorMode == true)
        {
            if (Global.currentPuzzle == 2)
            {
                distanceFromBlueX = Mathf.Abs(puzzleTwoSelector.position.x - puzzleTwoBlueBox.position.x);
                distanceFromBlueZ = Mathf.Abs(puzzleTwoSelector.position.z - puzzleTwoBlueBox.position.z);
                //print("BLUE X: " + distanceFromBlueX);
                //                print("BLUE Z: " + distanceFromBlueZ);

                distanceFromRedX = Mathf.Abs(puzzleTwoSelector.position.x - puzzleTwoRedBox.position.x);
                distanceFromRedZ = Mathf.Abs(puzzleTwoSelector.position.z - puzzleTwoRedBox.position.z);

                distanceFromGreenX = Mathf.Abs(puzzleTwoSelector.position.x - puzzleTwoGreenBox.position.x);
                distanceFromGreenZ = Mathf.Abs(puzzleTwoSelector.position.z - puzzleTwoGreenBox.position.z);

                distanceFromYellowX = Mathf.Abs(puzzleTwoSelector.position.x - puzzleTwoYellowBox.position.x);
                distanceFromYellowZ = Mathf.Abs(puzzleTwoSelector.position.z - puzzleTwoYellowBox.position.z);
            }
        }
    }

    void FixedUpdate()
    {
        if (MonitorMode.monitorMode == true)
        {
            if (Global.currentPuzzle == 2)
            {
                float movementSpeed = 0.05f;

                // locks rotation of light
                puzzleTwoSelector.transform.rotation = Quaternion.Euler(puzzleTwoSelector.transform.rotation.eulerAngles.x, Global.lockPos, Global.lockPos);

                // TODO: limits, eg: if (pos.x >) { ... }
                // up and down
                puzzleTwoSelector.Translate(Vector3.down * Global.state.ThumbSticks.Left.Y * movementSpeed);
                if (Input.GetKey(KeyCode.S)) puzzleTwoSelector.Translate(Vector3.up * 1 * movementSpeed);

                puzzleTwoSelector.Translate(Vector3.up * -Global.state.ThumbSticks.Left.Y * movementSpeed);
                if (Input.GetKey(KeyCode.W)) puzzleTwoSelector.Translate(Vector3.down * 1 * movementSpeed);

                // left and right
                puzzleTwoSelector.Translate(Vector3.right * -Global.state.ThumbSticks.Left.X * movementSpeed);
                if (Input.GetKey(KeyCode.D)) puzzleTwoSelector.Translate(Vector3.left * 1 * movementSpeed);

                puzzleTwoSelector.Translate(Vector3.left * Global.state.ThumbSticks.Left.X * movementSpeed);
                if (Input.GetKey(KeyCode.A)) puzzleTwoSelector.Translate(Vector3.right * 1 * movementSpeed);



                blueLight.enabled = (distanceFromBlueX < acceptableDistance && distanceFromBlueZ < acceptableDistance);
                //     true; blueFlag = true;
                //} else {
                //    blueLight.enabled = false; blueFlag = false;
                //}

                redLight.enabled = (distanceFromRedX < acceptableDistance && distanceFromRedZ < acceptableDistance);
                //    redLight.enabled = true; redFlag = true;
                //} else {
                //    redLight.enabled = false; redFlag = false;
                //}

                greenLight.enabled = (distanceFromGreenX < acceptableDistance && distanceFromGreenZ < acceptableDistance);
                //    greenLight.enabled = true; greenFlag = true;
                //} else {
                //    greenLight.enabled = false; greenFlag = false;
                //}

                yellowLight.enabled = (distanceFromYellowX < acceptableDistance && distanceFromYellowZ < acceptableDistance);
                //    yellowLight.enabled = true; yellowFlag = true;
                //}
                //else {
                //    yellowLight.enabled = false; yellowFlag = false;
                //}


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

                print(yellowBoxTwo.position);

                if (blueLight.enabled)
                {
                    blueCount = 0;
                    if (blueBoxOneDistance < 0.5) blueCount++;
                    if (blueBoxTwoDistance < 0.5) blueCount++;
                    if (blueBoxThreeDistance < 0.5) blueCount++;
                }
                if (redLight.enabled)
                {
                    redCount = 0;
                    if (redBoxOneDistance < 0.5) redCount++;
                    if (redBoxTwoDistance < 0.5) redCount++;
                    if (redBoxThreeDistance < 0.5) redCount++;
                }
                if (greenLight.enabled)
                {
                    greenCount = 0;
                    if (greenBoxOneDistance < 0.5) greenCount++;
                    if (greenBoxTwoDistance < 0.5) greenCount++;
                    if (greenBoxThreeDistance < 0.5) greenCount++;
                }
                if (yellowLight.enabled)
                {
                    yellowCount = 0;
                    if (yellowBoxOneDistance < 0.5) yellowCount++;
                    if (yellowBoxTwoDistance < 0.5) yellowCount++;
                    if (yellowBoxThreeDistance < 0.5) yellowCount++;
                }

                //puzzleTwoYellowBox.localScale -= new Vector3(0.5f * yellowCount, 0.5f * yellowCount, 0.5f * yellowCount);
                //print("Yellow = " + yellowCount);
                //print("BOX TWO DIST: " + yellowBoxTwoDistance);
                //puzzleTwoYellowBox.localScale -= new Vector3(6, 8, 9);

                // TODO box distance to goal
                //puzzleOneDistanceToCompletion = Vector3.Distance(puzzleOneBox.position, puzzleOneComplete.position);

                //if ()

                // TODO MonitorRedBox.scale *= 0.8 * numberOfRedBoxesInSpotlight; //or something like that

            }
        }
    }
}