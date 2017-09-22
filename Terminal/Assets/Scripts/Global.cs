using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour {
    int textAudio = 1;

    public Transform gameEndGoal;

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

    // cues
    public Transform voiceRecorder;
    float distanceFromRecorder = 100f;

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
    bool timer6set = false;
    public static float timer7 = 0.0f;
    bool timer7set = false;
    public static float timer8 = 0.0f;
    bool timer8set = false;
    public static float timer9 = 0.0f;
    bool timer9set = false;
    public static float timer10 = 0.0f;
    bool timer10set = false;
    public static float timer11 = 0.0f;
    bool timer11set = false;
    public static float timer12 = 0.0f;
    bool timer12set = false;
    public static float timer13 = 0.0f;
    bool timer13set = false;
    public static float timer14 = 0.0f;
    bool timer14set = false;
    public static float timer15 = 0.0f;
    bool timer15set = false;

    // audio cues
    public AudioClip cue1audio;
    public AudioClip cue2audio;
    public AudioClip cue3audio;
    public AudioClip cue4audio;
    public AudioClip cue5audio;
    public AudioClip cue6audio;
    public AudioClip cue7audio;
    public AudioClip cue8audio;
    public AudioClip cue9audio;

    
    // text cue stuff
    public static int currentCue = -1;
    float textCueTimer = 0;
    public Image textBG;
    public Image controllerImage;
    bool gamePaused = false;

    public Transform cueText;

    string cue0 = "Welcome to Terminal, a virtual reality game\n" +
                    "that focuses on two worlds: the computer\n" +
                    "monitor in front of you and the warehouse\n" +
                    "floor behind that.";
    float cue0timer = 6.25f;
    bool cue0played = false;
    bool cue0textStarted = false;


    string cue1 = "Events interacted with on the monitor will\n" +
                    "effect objects in the warehouse.\n\n" +
                    "Interacting with these objects can help you\n" +
                    "solve puzzles and progress to the end state of\n" +
                    "the game.\n\n" +
                    "Good luck and God speed!";
    float cue1timer = 8.75f;
    bool cue1played = false;
    bool cue1textStarted = false;


    string cue2 = "The emergency lights have been activated.\n\n" +
                    "You should probably reset the circuit breaker.";
    float cue2timer = 3.25f;
    bool cue2played = false;
    bool cue2textStarted = false;


    string cue3 = "Nice one, mate. But this door (like almost all\n" +
                    "doors imaginable) requires a key to open.";
    float cue3timer = 4.0f;
    bool cue3played = false;
    bool cue3textStarted = false;


    string cue4 = "Jesus Christ, that's bright!\n" +
                    "Well, now that power has been restored, the\n" +
                    "coloured boxes need to be delivered.\n\n" +
                    "Look for their corresponding terminals.";
    float cue4timer = 5.5f;
    bool cue4played = false;
    bool cue4textStarted = false;


    string cue5 = "Incredible!\n\n" +
                    "You’ve got a basic understanding of colour,\n" +
                    "and you are officially 6 years old.Now try\n" +
                    "finding out what to do next, on your own.\n\n" +
                    "Checking the monitor might be a good idea, mate.";
    float cue5timer = 8.75f;
    bool cue5played = false;
    bool cue5textStarted = false;


    string cue6 = "Well done!\n\n" +
                    "These new boxes now need to be delivered\n" +
                    "together on the conveyor belt, in a specific\n" +
                    "order.";
    float cue6timer = 5.5f;
    bool cue6played = false;
    bool cue6textStarted = false;


    string cue7 = "These boxes also have specific end\n" +
                    "destinations. It would be wise to reference a\n" +
                    "destination board or list located in the\n" +
                    "warehouse, in order to find the correct\n" +
                    "destination.";
    float cue7timer = 7.25f;
    bool cue7played = false;
    bool cue7textStarted = false;


    string cue8 = "Look, at, that! You’ve delivered all of the\n" +
                    "boxes to their correct destinations. Your\n" +
                    "ability to complete mundane point-to-point\n" +
                    "tasks is on par with an Amazon drone.";
    float cue8timer = 6.75f;
    bool cue8played = false;
    bool cue8textStarted = false;


    string cue9 = "That’s all she wrote. There’s really nothing left\n" +
                    "for you to do here. I mean, you can hang out\n" +
                    "for a bit. Maybe throw some things around.\n" +
                    "But the work is done.\n\n" +
                    "Maybe you need to go find a big door or something.\n\n" +
                    "That’s usually how games end, isn’t it?";
    float cue9timer = 12.5f;
    bool cue9played = false;
    bool cue9textStarted = false;

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

        controllerImage.enabled = false;

        // audio
        source.PlayDelayed(44100);
        source.PlayOneShot(startSound, volumeLow);
    }
	
	void Update () {
        updateController();
        checkStartButton();
        updateTimers();
        updateCues();
        checkCueReplay();

        if (currentPuzzle == 6) {
            float distanceFromEndGoal = Vector3.Distance(player.position, gameEndGoal.position);
            //print(distanceFromEndGoal);
            if (distanceFromEndGoal < 2.0f) {
                currentPuzzle = 1;
                currentCue = 0;
                currentTimer = 0;
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
        }
    }

    void updateController() {
        prevState = state;
        state = GamePad.GetState(playerIndex);
    }

    void checkStartButton() {
        if ((gamePaused == false) && (Input.GetKeyDown(KeyCode.P) || (prevState.Buttons.Start == ButtonState.Released && state.Buttons.Start == ButtonState.Pressed))) {
            gamePaused = true;
            controllerImage.enabled = true;
        }
        else if ((gamePaused == true) && (Input.GetKeyDown(KeyCode.P) || (prevState.Buttons.Start == ButtonState.Released && state.Buttons.Start == ButtonState.Pressed))) {
            gamePaused = false;
            controllerImage.enabled = false;
        }

        if (gamePaused == true) {
            if (Input.GetKeyDown(KeyCode.Q) || (prevState.Buttons.Back == ButtonState.Released && state.Buttons.Back == ButtonState.Pressed)) {
                print("QUIT");
                Application.Quit();
            }
        }
    }

    void updateTimers() {
        timerValue += Time.deltaTime;

        if (timer0set == false && currentTimer == 1) {
            // user starts game
            //        to
            // user interacts with monitor
            timer0 = timerValue;
            timer0set = true;
            timerValue = 0;
            print("T0: " + timer0);
        } else if (timer1set == false && currentTimer == 2) {
            // user interacts with monitor
            //        to
            // user flips breaker
            timer1 = timerValue;
            timer1set = true;
            timerValue = 0;
            print("T1: " + timer1);
        } else if (timer2set == false && currentTimer == 3) {
            // user flips breaker
            //        to
            // user places all boxes in consoles
            timer2 = timerValue;
            timer2set = true;
            timerValue = 0;
            print("T2: " + timer2);
        } else if (timer3set == false && currentTimer == 4) {
            // user places all boxes in consoles
            //        to
            // user solves conveyor belt stage 1
            timer3 = timerValue;
            timer3set = true;
            timerValue = 0;
            print("T3: " + timer3);
        } else if (timer4set == false && currentTimer == 5) {
            // user places all boxes in consoles
            //        to
            // user solves conveyor belt stage 1
            timer4 = timerValue;
            timer4set = true;
            timerValue = 0;
            print("T4: " + timer4);
        } else if (timer5set == false && currentTimer == 6) {
            // user places all boxes in consoles
            //        to
            // user solves conveyor belt stage 1
            timer5 = timerValue;
            timer5set = true;
            timerValue = 0;
            print("T5: " + timer5);
        } else if (timer6set == false && currentTimer == 7) {
            timer6 = timerValue;
            timer6set = true;
            timerValue = 0;
            print("T6: " + timer6);
        } else if (timer7set == false && currentTimer == 8) {
            timer7 = timerValue;
            timer7set = true;
            timerValue = 0;
            print("T7: " + timer7);
        } else if (timer8set == false && currentTimer == 9) {
            timer8 = timerValue;
            timer8set = true;
            timerValue = 0;
            print("T8: " + timer8);


            while (fileExists) {
                fileName = "PlayerTimesLog";
                fileName += increment;
                fileName += ".txt";

                if (File.Exists(fileName) == false) {
                    fileExists = false;
                } else {
                    increment++;
                }
            }

            if (textFileWritten == false) {
                StreamWriter sw = File.CreateText(fileName);
                sw.WriteLine("Time to interact with monitor: " + timer0);
                sw.WriteLine("Time to solve maze puzzle: " + timer1);
                sw.WriteLine("Time to turn on lights: " + timer2);
                sw.WriteLine("Time to solve boxes knocking down puzzle: " + timer3);
                sw.WriteLine("Time to solve conveyor belt stage 1: " + timer4);
                sw.WriteLine("Time to solve conveyor belt stage 2: " + timer5);
                sw.WriteLine("Time to solve conveyor belt stage 3: " + timer6);
                sw.WriteLine("Time to solve conveyor belt stage 4: " + timer7);
                sw.WriteLine("Time to solve conveyor belt stage 5: " + timer8);
                //sw.WriteLine("Time to solve boxes knocking down puzzle: " + timer10);
                sw.Close();
            }


        } else if (timer9set == false && currentTimer == 10) {
            timer9 = timerValue;
            timer9set = true;
            timerValue = 0;
            print("T9: " + timer9);
            

        } else if (timer10set == false && currentTimer == 11) {
            timer10 = timerValue;
            timer10set = true;
            timerValue = 0;
            print("T10: " + timer10);
        }

    }

    void updateCues() {
        
        // Add a 2 second delay before the first cue is triggered
        if (cue0played == false && currentCue == -1) {
            textBG.enabled = false;
            textCueTimer += Time.deltaTime;
            if (textCueTimer > 2.0f) {
                currentCue = 0;
            }
        }

        // Cue 0
        if (currentCue == 0 && cue0played == false) {
            // textAudio == 0 means this version is in text mode
            // textAudio == 1 means this version is in audio mode
            if (textAudio == 0) {
                textCueTimer += Time.deltaTime;

                cueText.GetComponent<TextMesh>().text = cue0;

                textBG.enabled = true;

                if (textCueTimer > cue0timer) {
                    textBG.enabled = false;
                    cue0played = true;
                    textCueTimer = 0;
                    cueText.GetComponent<TextMesh>().text = "";
                    currentCue = 1;
                }
            } else if (textAudio == 1) {
                source.PlayOneShot(cue1audio, volumeMed);
                cue0played = true;
            }
        }

        // Cue 1
        else if (currentCue == 1 && cue1played == false) {
            if (textAudio == 0) {
                textCueTimer += Time.deltaTime;

                cueText.GetComponent<TextMesh>().text = cue1;

                textBG.enabled = true;

                if (textCueTimer > cue1timer) {
                    textBG.enabled = false;
                    cue1played = true;
                    textCueTimer = 0;
                    cueText.GetComponent<TextMesh>().text = "";
                }
            } 
            // don't need to play audio because it already played in cue0
        }

        // Cue 2
        else if (currentCue == 2 && cue2played == false) {
            if (textAudio == 0) {
                textCueTimer += Time.deltaTime;

                cueText.GetComponent<TextMesh>().text = cue2;

                textBG.enabled = true;

                if (textCueTimer > cue2timer) {
                    textBG.enabled = false;
                    cue2played = true;
                    textCueTimer = 0;
                    cueText.GetComponent<TextMesh>().text = "";
                }
            } else if (textAudio == 1) {
                source.PlayOneShot(cue2audio, volumeMed);
                cue2played = true;
            }
        }

        // Cue 3
        else if (currentCue == 3 && cue3played == false) {
            if (textAudio == 0) {
                textCueTimer += Time.deltaTime;

                cueText.GetComponent<TextMesh>().text = cue3;

                textBG.enabled = true;

                if (textCueTimer > cue3timer) {
                    textBG.enabled = false;
                    cue3played = true;
                    textCueTimer = 0;
                    cueText.GetComponent<TextMesh>().text = "";
                }
            } else if (textAudio == 1) {
                source.PlayOneShot(cue3audio, volumeMed);
                cue3played = true;
            }
        }

        // Cue 4
        else if (currentCue == 4 && cue4played == false) {
            if (textAudio == 0) {
                textCueTimer += Time.deltaTime;

                cueText.GetComponent<TextMesh>().text = cue4;

                textBG.enabled = true;

                if (textCueTimer > cue4timer) {
                    textBG.enabled = false;
                    cue4played = true;
                    textCueTimer = 0;
                    cueText.GetComponent<TextMesh>().text = "";
                }
            } else if (textAudio == 1) {
                source.PlayOneShot(cue4audio, volumeMed);
                cue4played = true;
            }
        }

        // Cue 5
        else if (currentCue == 5 && cue5played == false) {
            if (textAudio == 0) {
                textCueTimer += Time.deltaTime;

                cueText.GetComponent<TextMesh>().text = cue5;

                textBG.enabled = true;

                if (textCueTimer > cue5timer) {
                    textBG.enabled = false;
                    cue5played = true;
                    textCueTimer = 0;
                    cueText.GetComponent<TextMesh>().text = "";
                }
            } else if (textAudio == 1) {
                source.PlayOneShot(cue5audio, volumeMed);
                cue5played = true;
            }
        }

        // Cue 6
        else if (currentCue == 6 && cue6played == false) {
            if (textAudio == 0) {
                textCueTimer += Time.deltaTime;

                cueText.GetComponent<TextMesh>().text = cue6;

                textBG.enabled = true;

                if (textCueTimer > cue6timer) {
                    textBG.enabled = false;
                    cue6played = true;
                    textCueTimer = 0;
                    cueText.GetComponent<TextMesh>().text = "";
                }
            } else if (textAudio == 1) {
                source.PlayOneShot(cue6audio, volumeMed);
                cue6played = true;
            }
        }

        // Cue 7
        else if (currentCue == 7 && cue7played == false) {
            if (textAudio == 0) {
                textCueTimer += Time.deltaTime;

                cueText.GetComponent<TextMesh>().text = cue7;

                textBG.enabled = true;

                if (textCueTimer > cue7timer) {
                    textBG.enabled = false;
                    cue7played = true;
                    textCueTimer = 0;
                    cueText.GetComponent<TextMesh>().text = "";
                }
            } else if (textAudio == 1) {
                source.PlayOneShot(cue7audio, volumeMed);
                cue7played = true;
            }
        }

        // Cue 8
        else if (currentCue == 8 && cue8played == false) {
            if (textAudio == 0) {
                textCueTimer += Time.deltaTime;

                cueText.GetComponent<TextMesh>().text = cue8;

                textBG.enabled = true;

                if (textCueTimer > cue8timer) {
                    textBG.enabled = false;
                    cue8played = true;
                    textCueTimer = 0;
                    cueText.GetComponent<TextMesh>().text = "";
                }
            } else if (textAudio == 1) {
                source.PlayOneShot(cue8audio, volumeMed);
                cue8played = true;
            }
        }

        // Cue 9
        else if (currentCue == 9 && cue9played == false) {
            if (textAudio == 0) {

                textCueTimer += Time.deltaTime;

                cueText.GetComponent<TextMesh>().text = cue9;

                textBG.enabled = true;

                if (textCueTimer > cue4timer) {
                    textBG.enabled = false;
                    cue9played = true;
                    textCueTimer = 0;
                    cueText.GetComponent<TextMesh>().text = "";
                }
            } else if (textAudio == 1) {
                source.PlayOneShot(cue9audio, volumeMed);
                cue9played = true;
            }
        }
    }

    void checkCueReplay() {
        distanceFromRecorder = Vector3.Distance(player.position, voiceRecorder.position);

        if (distanceFromRecorder < 1f) {
            if (Input.GetKeyDown(KeyCode.E) || (prevState.Buttons.X == ButtonState.Released && state.Buttons.X == ButtonState.Pressed)) {
                if (currentCue == 1) {
                    cue0played = false;
                } else if (currentCue == 2) {
                    cue2played = false;
                } else if (currentCue == 3) {
                    cue3played = false;
                } else if (currentCue == 4) {
                    cue4played = false;
                } else if (currentCue == 5) {
                    cue5played = false;
                } else if (currentCue == 6) {
                    cue6played = false;
                } else if (currentCue == 7) {
                    cue7played = false;
                } else if (currentCue == 8) {
                    cue8played = false;
                } else if (currentCue == 9) {
                    cue9played = false;
                }
            }
        }
    }
}
