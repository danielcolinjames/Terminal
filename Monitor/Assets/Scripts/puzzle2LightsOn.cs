using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class puzzle2LightsOn: MonoBehaviour {

    // puzzle two objects
    public Transform breaker;
    public Transform player;

    float distanceToBreaker = 100;

    public static bool breakerFlipped = false;

    // audio stuff
    public AudioClip breakerFlippedAudio;

    // Use this for initialization
    void Start () {

        // puzzle two objects
    }
	
	// Update is called once per frame
	void Update () {
        //print(distanceToBreaker);
        distanceToBreaker = Vector3.Distance(player.position, breaker.position);
    }

    void FixedUpdate() {
        //print(distanceToBreaker);

        if (Global.currentPuzzle == 2) {

            if (distanceToBreaker < 3) {

                if ((Input.GetKeyDown(KeyCode.E) || Global.prevState.Buttons.A == ButtonState.Released && Global.state.Buttons.A == ButtonState.Pressed)) {
                    breakerFlipped = true;
                    //print("BREAKER FLIPPED");
                }
            }

            if (MonitorMode.monitorMode == true) {
                
            }

            if (breakerFlipped == true) {

                Global.source.PlayOneShot(breakerFlippedAudio, Global.volumeMed);

                foreach (GameObject mainLight in puzzle1IntroMaze.mainLights) {
                    mainLight.GetComponent<Light>().enabled = true;
                }

                foreach (GameObject backupLight in puzzle1IntroMaze.backupLights) {
                    backupLight.GetComponent<Light>().enabled = false;
                }

                puzzle1IntroMaze.screenLight.enabled = true;

                Global.currentPuzzle = 5;
            }
        }
    }
}
