using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System;
using System.IO;

public class Global : MonoBehaviour {
    public static float lockPos = 0;

    public static int currentPuzzle = 1;
    public static int currentTimer = 0;
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




    // timer stuff
    bool textFileWritten = false;

    bool fileExists = true;
    int increment = 0;

    string fileName = "PlayerTimesLog.txt";

    float timerValue = 0.0f;

    public static float timer0 = 0.0f;
    bool timer0set = false;
    public static float timer1 = 0.0f;
    bool timer1set = false;
    public static float timer2 = 0.0f;
    bool timer2set = false;
    public static float timer3 = 0.0f;
    bool timer3set = false;
    public static float timer4 = 0.0f;
    bool timer4set = false;
    public static float timer5 = 0.0f;
    bool timer5set = false;
    public static float timer6 = 0.0f;
    public static float timer7 = 0.0f;
    public static float timer8 = 0.0f;
    public static float timer10 = 0.0f;
    public static float timer11 = 0.0f;
    public static float timer12 = 0.0f;
    public static float timer13 = 0.0f;
    public static float timer14 = 0.0f;
    public static float timer15 = 0.0f;

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


        timerValue += Time.deltaTime;

        if (timer0set == false && currentTimer == 1) {
            // user starts game
            //        to
            // user interacts with monitor
            timer0 = timerValue;
            timer0set = true;
            timerValue = 0;
            print("T0: " + timer0);
        }

        else if (timer1set == false && currentTimer == 2) {
            // user interacts with monitor
            //        to
            // user flips breaker
            timer1 = timerValue;
            timer1set = true;
            timerValue = 0;
            print("T1: " + timer1);
        }
        
        else if (timer2set == false && currentTimer == 3) {
            // user flips breaker
            //        to
            // user places all boxes in consoles
            timer2 = timerValue;
            timer2set = true;
            timerValue = 0;
            print("T2: " + timer2);

            while(fileExists) {
                fileName = "PlayerTimesLog";
                fileName += increment;
                fileName += ".txt";
                
                if (File.Exists(fileName) == false) {
                    fileExists = false;
                }
                else {
                    increment++;
                }
            }

            if (textFileWritten == false) {
                StreamWriter sw = File.CreateText(fileName);
                sw.WriteLine("Time to interact with monitor: " + timer0);
                sw.WriteLine("Time to solve maze puzzle: " + timer1);
                sw.WriteLine("Time to turn on lights: " + timer2);
                sw.Close();
            }
        }
        
        else if (timer3set == false && currentTimer == 4) {
            // user places all boxes in consoles
            //        to
            // user solves conveyor belt stage 1
            timer3 = timerValue;
            timer3set = true;
            timerValue = 0;
            print("T3: " + timer3);
        }
        
        else if (timer4set == false && currentTimer == 5) {
            // user places all boxes in consoles
            //        to
            // user solves conveyor belt stage 1
            timer4 = timerValue;
            timer4set = true;
            timerValue = 0;
            print("T4: " + timer4);
        }        
    }
}
