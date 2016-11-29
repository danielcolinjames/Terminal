using UnityEngine;
using System.Collections;

public class PuzzleOne : MonoBehaviour {

    // puzzle one objects
    public static Transform puzzleOneBox;
    public Transform puzzleOneComplete;
    float puzzleOneDistanceToCompletion;

    public static Light mainLightOne;
    public static Light mainLightTwo;
    public static Light mainLightThree;

    public static Light screenLight;

    public static Light backupLightOne;
    public static Light backupLightTwo;
    public static Light backupLightThree;

    // Use this for initialization
    void Start () {

        // puzzle one objects
        puzzleOneBox = GameObject.FindGameObjectWithTag("PuzzleOneCube").transform;
        puzzleOneBox.position = new Vector3(puzzleOneBox.position.x, puzzleOneBox.position.y, puzzleOneBox.position.z);

        puzzleOneComplete = GameObject.FindGameObjectWithTag("PuzzleOneComplete").transform;
        puzzleOneDistanceToCompletion = 0;

        mainLightOne = GameObject.FindGameObjectWithTag("MainLightOne").GetComponent<Light>();
        mainLightTwo = GameObject.FindGameObjectWithTag("MainLightTwo").GetComponent<Light>();
        mainLightThree = GameObject.FindGameObjectWithTag("MainLightThree").GetComponent<Light>();

        screenLight = GameObject.FindGameObjectWithTag("ScreenLight").GetComponent<Light>();

        backupLightOne = GameObject.FindGameObjectWithTag("BackupLightOne").GetComponent<Light>();
        backupLightTwo = GameObject.FindGameObjectWithTag("BackupLightTwo").GetComponent<Light>();
        backupLightThree = GameObject.FindGameObjectWithTag("BackupLightThree").GetComponent<Light>();

    }

    // Update is called once per frame
    void Update () {
        puzzleOneDistanceToCompletion = Vector3.Distance(puzzleOneBox.position, puzzleOneComplete.position);

        if (Global.currentPuzzle == 1) {
            Global.monitorCamera.position = new Vector3(puzzleOneBox.position.x, puzzleOneBox.position.y + 10f, puzzleOneBox.position.z);
        }
    }

    void FixedUpdate()
    {
        if (MonitorMode.monitorMode == true) {

            if (Global.currentPuzzle == 1) {
                // have to do all the physics changes inside FixedUpdate or else the box jerks around

                // how quickly the box will translate
                float movementSpeed = 0.05f;

                // locks rotation of box
                puzzleOneBox.transform.rotation = Quaternion.Euler(puzzleOneBox.transform.rotation.eulerAngles.x, Global.lockPos, Global.lockPos);

                // up and down
                puzzleOneBox.Translate(Vector3.back * Global.state.ThumbSticks.Left.Y * movementSpeed);
                if (Input.GetKey(KeyCode.W)) puzzleOneBox.Translate(Vector3.back * 2 * movementSpeed);

                puzzleOneBox.Translate(Vector3.forward * -Global.state.ThumbSticks.Left.Y * movementSpeed);
                if (Input.GetKey(KeyCode.S)) puzzleOneBox.Translate(Vector3.forward * 2 * movementSpeed);

                // left and right
                puzzleOneBox.Translate(Vector3.right * -Global.state.ThumbSticks.Left.X * movementSpeed);
                if (Input.GetKey(KeyCode.D)) puzzleOneBox.Translate(Vector3.left * 2 * movementSpeed);

                puzzleOneBox.Translate(Vector3.left * Global.state.ThumbSticks.Left.X * movementSpeed);
                if (Input.GetKey(KeyCode.A)) puzzleOneBox.Translate(Vector3.right * 2 * movementSpeed);

                // puzzleOneDistanceToCompletion = Vector3.Distance(player.position, monitor.position);

                //print(puzzleOneDistanceToCompletion);

                //print("BOX: " + puzzleOneBox.position);
                //print("COMPLETE: " + puzzleOneComplete.position);

                // test for completion
                if (puzzleOneDistanceToCompletion < 1) {
                    Global.currentPuzzle = 2;

                    //MonitorMode.monitorMode = false;

                    Global.monitorCamera.position = new Vector3(-14.99f, Global.monitorCamera.position.y, Global.monitorCamera.position.z);

                    mainLightOne.enabled = false;
                    mainLightTwo.enabled = false;
                    mainLightThree.enabled = false;

                    screenLight.enabled = false;

                    backupLightOne.enabled = true;
                    backupLightTwo.enabled = true;
                    backupLightThree.enabled = true;

                    PuzzleThree.puzzleTwoFallingBox.position = new Vector3(PuzzleThree.puzzleTwoFallingBox.position.x, PuzzleThree.puzzleTwoFallingBox.position.y - 1f, PuzzleThree.puzzleTwoFallingBox.position.z);
                    PuzzleThree.key.position = new Vector3(PuzzleThree.puzzleTwoFallingBox.position.x, PuzzleThree.puzzleTwoFallingBox.position.y + 0.01f, PuzzleThree.puzzleTwoFallingBox.position.z);
                }
            }
        }
    }
}
