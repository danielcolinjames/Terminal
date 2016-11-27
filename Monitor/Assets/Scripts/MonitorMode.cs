using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class MonitorMode : MonoBehaviour {

    // global objects
    public Transform player;
    public Transform monitor;
    public Transform monitorCamera;

    // puzzle one objects
    public Transform puzzleOneBox;
    public Transform puzzleOneComplete;
    float puzzleOneDistanceToCompletion;

    // puzzle two objects
    public Transform puzzleTwoFallingBox;
    public Transform puzzleTwoRedBox;
    public Transform puzzleTwoBlueBox;
    public Transform puzzleTwoGreenBox;
    public Transform puzzleTwoYellowBox;

    public bool yellowFlag;
    public Transform yellowBoxOne;
    public Transform yellowBoxTwo;
    public Transform yellowBoxThree;

    public bool redFlag;
    public Transform redBoxOne;
    public Transform redBoxTwo;
    public Transform redBoxThree;

    public bool greenFlag;
    public Transform greenBoxOne;
    public Transform greenBoxTwo;
    public Transform greenBoxThree;

    public bool blueFlag;
    public Transform blueBoxOne;
    public Transform blueBoxTwo;
    public Transform blueBoxThree;


    // global variables
    public float distanceToMonitor;

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    float lockPos = 0;

    bool monitorMode = false;

    int currentPuzzle = 1;
    



    void Awake() {
        
    }
    
    void Start () {
        // global objects
        player = GameObject.FindGameObjectWithTag("Player").transform;
        monitor = GameObject.FindGameObjectWithTag("Monitor").transform;
        monitorCamera = GameObject.FindGameObjectWithTag("MonitorCamera").transform;

        // puzzle one objects
        puzzleOneBox = GameObject.FindGameObjectWithTag("PuzzleOneCube").transform;
        puzzleOneComplete = GameObject.FindGameObjectWithTag("PuzzleOneComplete").transform;
        puzzleOneDistanceToCompletion = 0;
        monitorCamera.position = new Vector3(puzzleOneBox.position.x, puzzleOneBox.position.y + 10f, puzzleOneBox.position.z);

        // puzzle two objects
        puzzleTwoFallingBox = GameObject.FindGameObjectWithTag("FallingBox").transform;
        puzzleTwoRedBox = GameObject.FindGameObjectWithTag("PuzzleTwoRedBox").transform;
        puzzleTwoBlueBox = GameObject.FindGameObjectWithTag("PuzzleTwoBlueBox").transform;
        puzzleTwoGreenBox = GameObject.FindGameObjectWithTag("PuzzleTwoGreenBox").transform;
        puzzleTwoYellowBox = GameObject.FindGameObjectWithTag("PuzzleTwoYellowBox").transform;
        
        // global variables
        distanceToMonitor = 0;
    }
	

	// Update is called once per frame
	void Update () {
        distanceToMonitor = Vector3.Distance(player.position, monitor.position);
        puzzleOneDistanceToCompletion = Vector3.Distance(puzzleOneBox.position, puzzleOneComplete.position);

        if (!playerIndexSet || !prevState.IsConnected) {
            for (int i = 0; i < 4; i++) {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected) {
                    //Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);
        
        // detect if A was pressed this frame
        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed) {
            // if a has been pressed, activate monitor mode
            if (distanceToMonitor < 1.5) {
                monitorMode = true;
            }
        }

        // detect if B was pressed this frame
        if (monitorMode == true && prevState.Buttons.B == ButtonState.Released && state.Buttons.B == ButtonState.Pressed) {
            // if B has been pressed, activate monitor mode
            monitorMode = false;
        }

        // detect if the A button was released this frame
        //if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released) {
        //    // print("a released");
        //}

        // Set vibration according to triggers
        //GamePad.SetVibration(playerIndex, state.Triggers.Left, state.Triggers.Right);

        // Make the current object turn
        //transform.localRotation *= Quaternion.Euler(0.0f, state.ThumbSticks.Left.X * 25.0f * Time.deltaTime, 0.0f);

        // box.Translate(Vector3.back * Time.deltaTime);

        if (monitorMode == true) {
            // if this is in FixedUpdate, the player can jerk around and it doesn't lock the position properly
            player.position = new Vector3(monitor.position.x, monitor.position.y - 0.5f, monitor.position.z + 1.5f);
        }

        if (currentPuzzle == 1) {
            monitorCamera.position = new Vector3(puzzleOneBox.position.x, puzzleOneBox.position.y + 10f, puzzleOneBox.position.z);
        }
        else if (currentPuzzle == 2) {
            monitorCamera.position = new Vector3(-29f, 10f, puzzleOneBox.position.z);
        }
    }

    void FixedUpdate() {
        if (monitorMode == true) {

            if (currentPuzzle == 1) {
                // have to do all the physics changes inside FixedUpdate or else the box jerks around

                // how quickly the box will translate
                float movementSpeed = 0.05f;

                // locks rotation of box
                puzzleOneBox.transform.rotation = Quaternion.Euler(puzzleOneBox.transform.rotation.eulerAngles.x, lockPos, lockPos);

                // up and down
                puzzleOneBox.Translate(Vector3.back * state.ThumbSticks.Left.Y * movementSpeed);
                puzzleOneBox.Translate(Vector3.forward * -state.ThumbSticks.Left.Y * movementSpeed);

                // left and right
                puzzleOneBox.Translate(Vector3.right * -state.ThumbSticks.Left.X * movementSpeed);
                puzzleOneBox.Translate(Vector3.left * state.ThumbSticks.Left.X * movementSpeed);

                // puzzleOneDistanceToCompletion = Vector3.Distance(player.position, monitor.position);
                
                print(puzzleOneDistanceToCompletion);

                //print("BOX: " + puzzleOneBox.position);
                //print("COMPLETE: " + puzzleOneComplete.position);

                // test for completion
                if (puzzleOneDistanceToCompletion < 1) {
                    currentPuzzle = 2;
                    puzzleTwoFallingBox.position = new Vector3(puzzleTwoFallingBox.position.x, 
                        puzzleTwoFallingBox.position.y - 1f, puzzleTwoFallingBox.position.z);
                }
            }
            else if (currentPuzzle == 2) {

                // 
                yellowFlag = false;




            }
            else if (currentPuzzle == 3) {

            }
        }
    }
}
