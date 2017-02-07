using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Global : MonoBehaviour {
    public static float lockPos = 0;

    public static int currentPuzzle = 1;
    public static GamePadState state;
    public static GamePadState prevState;

    public static bool playerIndexSet = false;
    public static PlayerIndex playerIndex;

    // global objects
    public static Transform player;
    public static Transform monitor;
    public static Transform monitorCamera;

    // audio stuff
    public static float volumeLow = 0.3f;
    public static float volumeMed = 0.5f;
    
    public static AudioSource source;

    public AudioClip startSound;

    public Material cameraFeedOne;

    public static GameObject securityCameraPlane;

    void Awake() {
        source = GetComponent<AudioSource>();
        securityCameraPlane = GameObject.FindGameObjectWithTag("SecurityCameraPlane");
    }

    // Use this for initialization
    void Start () {
        // global objects
        player = GameObject.FindGameObjectWithTag("Player").transform;
        monitor = GameObject.FindGameObjectWithTag("Monitor").transform;

        securityCameraPlane.GetComponent<Renderer>().material = cameraFeedOne;

        monitorCamera = GameObject.FindGameObjectWithTag("MonitorCamera").transform;
        monitorCamera.position = new Vector3(monitorCamera.position.x, monitorCamera.position.y, monitorCamera.position.z);

        // audio
        source.PlayDelayed(44100);
        source.PlayOneShot(startSound, volumeLow);
    }
	
	// Update is called once per frame
	void Update () {
        prevState = state;
        state = GamePad.GetState(playerIndex);
    }
}
