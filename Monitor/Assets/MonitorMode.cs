using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class MonitorMode : MonoBehaviour {

    public Transform player;
    public Transform monitor;
    public float distanceToMonitor;

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    bool monitorMode = false;
    bool aPressed = false;

    void Awake() {
        player = GameObject.Find("FPSController").transform;
        monitor = GameObject.Find("Monitor").transform;

        distanceToMonitor = 0;
    }
    

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        distanceToMonitor = Vector3.Distance(player.position, monitor.position);
       // print(distanceToMonitor);


        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }


        prevState = state;
        state = GamePad.GetState(playerIndex);
        

        // Detect if a button was pressed this frame
        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        {
            // print("a pressed");
            aPressed = true;
            print("YEAH");
        }

        // Detect if a button was released this frame
        if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released)
        {
            //   print("a released");

        }

        // if a has been pressed, activate monitor mode
        if (aPressed == true && distanceToMonitor < 1.5)
        {
            monitorMode = true;
        }

        if (monitorMode == true)
        {
            print("mm");
            player.position = new Vector3((float)-1.5,(float)1,(float)0);
        }

        // Set vibration according to triggers
        //GamePad.SetVibration(playerIndex, state.Triggers.Left, state.Triggers.Right);

        // Make the current object turn
        //transform.localRotation *= Quaternion.Euler(0.0f, state.ThumbSticks.Left.X * 25.0f * Time.deltaTime, 0.0f);

    }
}
