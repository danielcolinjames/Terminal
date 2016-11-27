using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Global : MonoBehaviour {

    public static int currentPuzzle = 1;
    public static GamePadState state;
    public static GamePadState prevState;

    public static bool playerIndexSet = false;
    public static PlayerIndex playerIndex;

    // global objects
    public static Transform player;
    public static Transform monitor;
    public static Transform monitorCamera;

    // Use this for initialization
    void Start () {
        // global objects
        player = GameObject.FindGameObjectWithTag("Player").transform;
        monitor = GameObject.FindGameObjectWithTag("Monitor").transform;
        monitorCamera = GameObject.FindGameObjectWithTag("MonitorCamera").transform;
    }
	
	// Update is called once per frame
	void Update () {
        prevState = state;
        state = GamePad.GetState(playerIndex);
    }
}
