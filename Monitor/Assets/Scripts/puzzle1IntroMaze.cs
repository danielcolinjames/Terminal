using UnityEngine;
using System.Collections;

public class puzzle1IntroMaze : MonoBehaviour {

    // puzzle one objects
    public Transform puzzleOneBox;
    public Transform puzzleOneComplete;
    float puzzleOneDistanceToCompletion;

    public Transform fallingBox;
    public Transform key;

    public Transform movingBox;

    // audio stuff
    public AudioClip lightsOff;

    public static Light screenLight;

    public static GameObject[] mainLights;
    public static GameObject[] backupLights;

    public Material cameraFeedTwo;

    // Use this for initialization
    void Start () {
        mainLights = GameObject.FindGameObjectsWithTag("MainLight");
        backupLights = GameObject.FindGameObjectsWithTag("BackupLight");

        // puzzle one objects
        puzzleOneBox.position = new Vector3(puzzleOneBox.position.x, puzzleOneBox.position.y, puzzleOneBox.position.z);

        puzzleOneDistanceToCompletion = 0;

        screenLight = GameObject.FindGameObjectWithTag("ScreenLight").GetComponent<Light>();
    }

    // Update is called once per frame
    void Update () {
        puzzleOneDistanceToCompletion = Vector3.Distance(puzzleOneBox.position, puzzleOneComplete.position);

        //print("p1bp: " + puzzleOneBox.position + ", p1cp: " + puzzleOneComplete.position + ", distance: " + puzzleOneDistanceToCompletion);


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
                float movementSpeed = 0.005f;

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

                movingBox.position = new Vector3(puzzleOneBox.position.x * 10 - 218.5f,
                    puzzleOneBox.position.y + 4.7f,
                    puzzleOneBox.position.z * 10 - 289f);

                // puzzleOneDistanceToCompletion = Vector3.Distance(player.position, monitor.position);

                //print(puzzleOneDistanceToCompletion);

                //print("BOX: " + puzzleOneBox.position);
                //print("COMPLETE: " + puzzleOneComplete.position);

                // test for completion

                if (puzzleOneDistanceToCompletion < 0.05) {

                    Global.securityCameraPlane.GetComponent<Renderer>().material = cameraFeedTwo;

                    movingBox.GetComponent<Rigidbody>().useGravity = true;

                    Global.source.PlayOneShot(lightsOff, Global.volumeMed);

                    foreach (GameObject mainLight in mainLights) {
                        mainLight.GetComponent<Light>().enabled = false;
                    }

                    foreach (GameObject backupLight in backupLights) {
                        backupLight.GetComponent<Light>().enabled = true;
                    }

                    Global.currentPuzzle = 2;

                    //MonitorMode.monitorMode = false;

                    Global.monitorCamera.position = new Vector3(puzzleOneBox.position.x - 2.0f, puzzleOneBox.position.y + 10f, puzzleOneBox.position.z);

                    // swap lightmaps here

                    /*
                    PuzzleThree.redPlane.enabled = false;
                    PuzzleThree.bluePlane.enabled = false;
                    PuzzleThree.greenPlane.enabled = false;
                    PuzzleThree.yellowPlane.enabled = false;
                    */

                    screenLight.enabled = false;

                    fallingBox.position = new Vector3(fallingBox.position.x, fallingBox.position.y - 2f, fallingBox.position.z);
                    key.position = new Vector3(key.position.x, key.position.y -2f, key.position.z);
                }
            }
        }
    }
}
