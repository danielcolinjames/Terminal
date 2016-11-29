using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PuzzleTwo : MonoBehaviour {

    // puzzle two objects
    public Transform breaker;
    public Transform player;

    float distanceToBreaker = 100;


    public static bool breakerFlipped = false;

    // Use this for initialization
    void Start () {

        // puzzle two objects
        player = GameObject.FindGameObjectWithTag("Player").transform;
        breaker = GameObject.FindGameObjectWithTag("Breaker").transform;
    }
	
	// Update is called once per frame
	void Update () {
        //print(distanceToBreaker);
        distanceToBreaker = Vector3.Distance(player.position, breaker.position);
    }

    void FixedUpdate() {

        if (Global.currentPuzzle == 2) {

            if (distanceToBreaker < 2) {

                if ((Input.GetKeyDown(KeyCode.E) || Global.prevState.Buttons.A == ButtonState.Released && Global.state.Buttons.A == ButtonState.Pressed)) {
                    breakerFlipped = true;
                    //print("BREAKER FLIPPED");
                }
            }

            if (MonitorMode.monitorMode == true) {
                
            }

            if (breakerFlipped == true) {
                PuzzleOne.mainLightOne.enabled = true;
                PuzzleOne.mainLightTwo.enabled = true;
                PuzzleOne.mainLightThree.enabled = true;

                PuzzleOne.screenLight.enabled = true;

                PuzzleOne.backupLightOne.enabled = false;
                PuzzleOne.backupLightTwo.enabled = false;
                PuzzleOne.backupLightThree.enabled = false;

                Global.currentPuzzle = 3;
            }
        }
    }
}
