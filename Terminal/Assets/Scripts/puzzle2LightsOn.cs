using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class puzzle2LightsOn: MonoBehaviour {

    public Renderer puzzleComplete1;
    public Renderer puzzleNotComplete1;

    // puzzle two objects
    public Transform breaker;
    public Transform player;

    float distanceToBreaker = 100;
    float distanceYToBreaker = 100;

    public static bool breakerFlipped = false;

    // audio stuff
    public AudioClip breakerFlippedAudio;

    // Use this for initialization
    void Start () {
        puzzleComplete1.enabled = false;
        // puzzle two objects
    }
	
	// Update is called once per frame
	void Update () {
        //print(distanceToBreaker);
        distanceToBreaker = Vector3.Distance(player.position, breaker.position);
        distanceYToBreaker = Mathf.Abs(player.position.y - breaker.position.y);
    }

    void FixedUpdate() {
        if (Global.currentPuzzle == 2 && distanceYToBreaker < 0.75) {

            if (distanceToBreaker < 3) {

                if ((Input.GetKeyDown(KeyCode.E) || Global.prevState.Buttons.A == ButtonState.Released && Global.state.Buttons.A == ButtonState.Pressed)) {
                    breakerFlipped = true;
                    //print("BREAKER FLIPPED");
                }
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

                puzzleComplete1.enabled = true;
                puzzleNotComplete1.enabled = false;

                Global.currentPuzzle = 3;
                Global.currentTimer = 3;
                Global.currentCue = 4;
            }
        }
    }
}