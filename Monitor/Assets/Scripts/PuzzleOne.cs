﻿using UnityEngine;
using System.Collections;

public class PuzzleOne : MonoBehaviour {

    // puzzle one objects
    public static Transform puzzleOneBox;
    public Transform puzzleOneComplete;
    float puzzleOneDistanceToCompletion;

    float lockPos = 0;

    // Use this for initialization
    void Start () {

        // puzzle one objects
        puzzleOneBox = GameObject.FindGameObjectWithTag("PuzzleOneCube").transform;
        puzzleOneComplete = GameObject.FindGameObjectWithTag("PuzzleOneComplete").transform;
        puzzleOneDistanceToCompletion = 0;
        Global.monitorCamera.position = new Vector3(puzzleOneBox.position.x, puzzleOneBox.position.y + 10f, puzzleOneBox.position.z);
    }
	
	// Update is called once per frame
	void Update () {
        puzzleOneDistanceToCompletion = Vector3.Distance(puzzleOneBox.position, puzzleOneComplete.position);

        if (Global.currentPuzzle == 1)
        {
            Global.monitorCamera.position = new Vector3(puzzleOneBox.position.x, puzzleOneBox.position.y + 10f, puzzleOneBox.position.z);
        }
    }

    void FixedUpdate()
    {
        if (MonitorMode.monitorMode == true)
        {

            if (Global.currentPuzzle == 1)
            {
                // have to do all the physics changes inside FixedUpdate or else the box jerks around

                // how quickly the box will translate
                float movementSpeed = 0.05f;

                // locks rotation of box
                puzzleOneBox.transform.rotation = Quaternion.Euler(puzzleOneBox.transform.rotation.eulerAngles.x, lockPos, lockPos);

                // up and down
                puzzleOneBox.Translate(Vector3.back * Global.state.ThumbSticks.Left.Y * movementSpeed);
                puzzleOneBox.Translate(Vector3.forward * -Global.state.ThumbSticks.Left.Y * movementSpeed);

                // left and right
                puzzleOneBox.Translate(Vector3.right * -Global.state.ThumbSticks.Left.X * movementSpeed);
                puzzleOneBox.Translate(Vector3.left * Global.state.ThumbSticks.Left.X * movementSpeed);

                // puzzleOneDistanceToCompletion = Vector3.Distance(player.position, monitor.position);

                //print(puzzleOneDistanceToCompletion);

                //print("BOX: " + puzzleOneBox.position);
                //print("COMPLETE: " + puzzleOneComplete.position);

                // test for completion
                if (puzzleOneDistanceToCompletion < 1)
                {
                    Global.currentPuzzle = 2;
                    PuzzleTwo.puzzleTwoFallingBox.position = new Vector3(PuzzleTwo.puzzleTwoFallingBox.position.x,
                        PuzzleTwo.puzzleTwoFallingBox.position.y - 1f, PuzzleTwo.puzzleTwoFallingBox.position.z);
                }
            }
        }
    }
}