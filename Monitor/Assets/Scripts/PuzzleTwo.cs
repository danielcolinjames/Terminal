using UnityEngine;
using System.Collections;

public class PuzzleTwo : MonoBehaviour
{

    // puzzle two objects
    public static Transform puzzleTwoFallingBox;
    public Transform puzzleTwoRedBox;
    public Transform puzzleTwoBlueBox;
    public Transform puzzleTwoGreenBox;
    public Transform puzzleTwoYellowBox;

    public bool yellowFlag;
    public Transform yellowBoxOne;
    public Transform yellowBoxTwo;
    public Transform yellowBoxThree;

    public bool redFlag;
    public Transform redBoxOne;
    public Transform redBoxTwo;
    public Transform redBoxThree;

    public bool greenFlag;
    public Transform greenBoxOne;
    public Transform greenBoxTwo;
    public Transform greenBoxThree;

    public bool blueFlag;
    public Transform blueBoxOne;
    public Transform blueBoxTwo;
    public Transform blueBoxThree;


    // Use this for initialization
    void Start()
    {

        // puzzle two objects
        puzzleTwoFallingBox = GameObject.FindGameObjectWithTag("FallingBox").transform;
        puzzleTwoRedBox = GameObject.FindGameObjectWithTag("PuzzleTwoRedBox").transform;
        puzzleTwoBlueBox = GameObject.FindGameObjectWithTag("PuzzleTwoBlueBox").transform;
        puzzleTwoGreenBox = GameObject.FindGameObjectWithTag("PuzzleTwoGreenBox").transform;
        puzzleTwoYellowBox = GameObject.FindGameObjectWithTag("PuzzleTwoYellowBox").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Global.currentPuzzle == 2)
        {
            Global.monitorCamera.position = new Vector3(-29f, 10f, PuzzleOne.puzzleOneBox.position.z);
        }
    }

    void FixedUpdate()
    {

        if (Global.currentPuzzle == 2)
        {

            // 
            yellowFlag = false;
            blueFlag = false;
            redFlag = false;
            greenFlag = false;

            // TODO box distance to goal
            //puzzleOneDistanceToCompletion = Vector3.Distance(puzzleOneBox.position, puzzleOneComplete.position);

            //if ()

            // TODO MonitorRedBox.scale *= 0.8 * numberOfRedBoxesInSpotlight; //or something like that

        }
    }
}
