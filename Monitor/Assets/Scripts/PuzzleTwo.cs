using UnityEngine;
using System.Collections;

public class PuzzleTwo : MonoBehaviour
{

    // Global objects (accessed by other scripts)
    public static Transform puzzleTwoFallingBox;


    // Warehouse items
    public Light yellowLight;
    public bool yellowFlag;
    public Transform yellowBoxOne;
    public Transform yellowBoxTwo;
    public Transform yellowBoxThree;

    public Light redLight;
    public bool redFlag;
    public Transform redBoxOne;
    public Transform redBoxTwo;
    public Transform redBoxThree;

    public Light greenLight;
    public bool greenFlag;
    public Transform greenBoxOne;
    public Transform greenBoxTwo;
    public Transform greenBoxThree;

    public Light blueLight;
    public bool blueFlag;
    public Transform blueBoxOne;
    public Transform blueBoxTwo;
    public Transform blueBoxThree;


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


    public Transform puzzleTwoSelector;

    void Awake() {
        // need to be in Awake() instead of Start() or else Unity has tons of errors
        // puzzle two objects
        puzzleTwoFallingBox = GameObject.FindGameObjectWithTag("FallingBox").transform;

        puzzleTwoRedBox = GameObject.FindGameObjectWithTag("PuzzleTwoRedBox").transform;
        puzzleTwoBlueBox = GameObject.FindGameObjectWithTag("PuzzleTwoBlueBox").transform;
        puzzleTwoGreenBox = GameObject.FindGameObjectWithTag("PuzzleTwoGreenBox").transform;
        puzzleTwoYellowBox = GameObject.FindGameObjectWithTag("PuzzleTwoYellowBox").transform;

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
        if (Global.currentPuzzle == 2) {

            float speed = 25f;
            float step = speed * Time.deltaTime;

            Vector3 puzzleTwoCameraPosition = new Vector3(-29f, 10f, 63f);

            Global.monitorCamera.position = Vector3.MoveTowards(Global.monitorCamera.position, puzzleTwoCameraPosition, step);
        }
        if (MonitorMode.monitorMode == true) {
            if (Global.currentPuzzle == 2) {
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

                // locks rotation of box
                puzzleTwoSelector.transform.rotation = Quaternion.Euler(puzzleTwoSelector.transform.rotation.eulerAngles.x, Global.lockPos, Global.lockPos);

                // up and down
                puzzleTwoSelector.Translate(Vector3.down * Global.state.ThumbSticks.Left.Y * movementSpeed);
                puzzleTwoSelector.Translate(Vector3.up * -Global.state.ThumbSticks.Left.Y * movementSpeed);

                // left and right
                puzzleTwoSelector.Translate(Vector3.right * -Global.state.ThumbSticks.Left.X * movementSpeed);
                puzzleTwoSelector.Translate(Vector3.left * Global.state.ThumbSticks.Left.X * movementSpeed);

                blueLight.enabled = (distanceFromBlueX < 0.5 && distanceFromBlueZ < 0.5);
                redLight.enabled = (distanceFromRedX < 0.5 && distanceFromRedZ < 0.5);
                greenLight.enabled = (distanceFromGreenX < 0.5 && distanceFromGreenZ < 0.5);
                yellowLight.enabled = (distanceFromYellowX < 0.5 && distanceFromYellowZ < 0.5);

                // 
                yellowFlag = false;
                blueFlag = false;
                redFlag = false;
                greenFlag = false;

                // TODO box distance to goal
                //puzzleOneDistanceToCompletion = Vector3.Distance(puzzleOneBox.position, puzzleOneComplete.position);

                //if ()

                // TODO MonitorRedBox.scale *= 0.8 * numberOfRedBoxesInSpotlight; //or something like that

            }
        }
    }
}
