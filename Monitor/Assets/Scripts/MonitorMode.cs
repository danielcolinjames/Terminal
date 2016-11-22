using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class MonitorMode : MonoBehaviour {

    public Transform player;
    public Transform monitor;
    public float distanceToMonitor;
    
    public Transform box;

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    float lockPos = 0;

    bool monitorMode = false;
    
    void Awake() {
        player = GameObject.Find("FPSController").transform;
        monitor = GameObject.Find("Monitor").transform;

        box = GameObject.Find("PuzzleOneCube").transform;

        distanceToMonitor = 0;
    }
    
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        distanceToMonitor = Vector3.Distance(player.position, monitor.position);
        
        if (!playerIndexSet || !prevState.IsConnected) {
            for (int i = 0; i < 4; i++) {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected) {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
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
    }

    void FixedUpdate() {
        if (monitorMode == true) {
            player.position = new Vector3(monitor.position.x, monitor.position.y - 0.3f, monitor.position.z + 1f);

            // how quickly the box will translate
            float movementSpeed = 0.05f;

            // locks rotation of box
            box.transform.rotation = Quaternion.Euler(box.transform.rotation.eulerAngles.x, lockPos, lockPos);

            // up and down
            box.Translate(Vector3.back * state.ThumbSticks.Left.Y * movementSpeed);
            box.Translate(Vector3.forward * -state.ThumbSticks.Left.Y * movementSpeed);

            // left and right
            box.Translate(Vector3.right * state.ThumbSticks.Left.X * movementSpeed);
            box.Translate(Vector3.left * -state.ThumbSticks.Left.X * movementSpeed);

            
        }
    }
}
