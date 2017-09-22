using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class MonitorMode : MonoBehaviour {

    // global variables
    public static float distanceToMonitor;

    public static bool monitorMode = false;

    bool monitorEntered = false;

    void Awake() {
         
    }
    
    void Start () {

        // global variables
        distanceToMonitor = 0;
    }
	
	// Update is called once per frame
	void Update () {
        distanceToMonitor = Vector3.Distance(Global.player.position, Global.monitor.position);

        //print(distanceToMonitor);

        if (!Global.playerIndexSet || !Global.prevState.IsConnected) {
            for (int i = 0; i < 4; i++) {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected) {
                    //Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    Global.playerIndex = testPlayerIndex;
                    Global.playerIndexSet = true;
                }
            }
        }

        // detect if A was pressed this frame
		if (Input.GetKeyDown(KeyCode.E) || (Global.prevState.Buttons.A == ButtonState.Released && Global.state.Buttons.A == ButtonState.Pressed)) {
            // if a has been pressed, activate monitor mode
            if (distanceToMonitor < 1.5) {

                if (monitorEntered == false) {
                    Global.currentTimer = 1;
                    monitorEntered = true;
                }

                monitorMode = true;
            }
        }
        
        // detect if B was pressed this frame
        if (((monitorMode == true) && ((Input.GetKeyDown(KeyCode.Q)) || (Global.prevState.Buttons.B == ButtonState.Released && Global.state.Buttons.B == ButtonState.Pressed)))) {
            // if B wasn't pressed last frame, and IS pressed now, de-activate monitor mode
            monitorMode = false;
        }
        
        if (monitorMode == true) {
            // if this is in FixedUpdate, the player can jerk around and it doesn't lock the position properly
            Global.player.position = new Vector3(Global.monitor.position.x, Global.monitor.position.y - 0.5f, Global.monitor.position.z + 1.5f);
        }

    }

    void FixedUpdate() {
        
    }
}
