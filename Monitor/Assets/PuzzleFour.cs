using UnityEngine;
using System.Collections;

public class PuzzleFour : MonoBehaviour {

    bool puzzleFourStarted = false;

    public Transform subPuzzleOneMoverSmall;
    public Transform subPuzzleOneMoverBig;


    void Awake() {
        subPuzzleOneMoverSmall = GameObject.FindGameObjectWithTag("SubPuzzleOneMoverSmall").transform;
        subPuzzleOneMoverBig = GameObject.FindGameObjectWithTag("SubPuzzleOneMoverBig").transform;
    }

    void Update() {
        if (Global.currentPuzzle == 4) {

            // move camera across from puzzle one
            float speed = 5f;
            float step = speed * Time.deltaTime;

            Vector3 subPuzzleOneCameraPosition = new Vector3(subPuzzleOneMoverSmall.position.x, subPuzzleOneMoverSmall.position.y + 25f, subPuzzleOneMoverSmall.position.z);

            float distance = Vector3.Distance(Global.monitorCamera.position, subPuzzleOneCameraPosition);

            // pan camera across
            Global.monitorCamera.position = Vector3.MoveTowards(Global.monitorCamera.position, subPuzzleOneCameraPosition, step);

            if (distance == 0) {
                // setting a flag when the camera has finished panning over
                // otherwise the player can move the puzzleTwoSelector around before puzzle one is finished
                puzzleFourStarted = true;
            }
        }
    }

    void FixedUpdate() {
        if (Global.currentPuzzle == 4 && puzzleFourStarted == true && MonitorMode.monitorMode == true) {

            // how quickly the box will translate
            float movementSpeedSmall = 0.005f;


            // SMALL

            // locks rotation of box
            subPuzzleOneMoverSmall.transform.rotation = Quaternion.Euler(subPuzzleOneMoverSmall.transform.rotation.eulerAngles.x, Global.lockPos, Global.lockPos);
                        

            // up and down
            subPuzzleOneMoverSmall.Translate(Vector3.back * Global.state.ThumbSticks.Left.Y * movementSpeedSmall);
            if (Input.GetKey(KeyCode.W)) subPuzzleOneMoverSmall.Translate(Vector3.back * 2 * movementSpeedSmall);

            subPuzzleOneMoverSmall.Translate(Vector3.forward * -Global.state.ThumbSticks.Left.Y * movementSpeedSmall);
            if (Input.GetKey(KeyCode.S)) subPuzzleOneMoverSmall.Translate(Vector3.forward * 2 * movementSpeedSmall);

            // left and right
            subPuzzleOneMoverSmall.Translate(Vector3.right * -Global.state.ThumbSticks.Left.X * movementSpeedSmall);
            if (Input.GetKey(KeyCode.D)) subPuzzleOneMoverSmall.Translate(Vector3.left * 2 * movementSpeedSmall);

            subPuzzleOneMoverSmall.Translate(Vector3.left * Global.state.ThumbSticks.Left.X * movementSpeedSmall);
            if (Input.GetKey(KeyCode.A)) subPuzzleOneMoverSmall.Translate(Vector3.right * 2 * movementSpeedSmall);


            float movementSpeedBig = 0.11f;


            // BIG

            // locks rotation of box
            subPuzzleOneMoverBig.transform.rotation = Quaternion.Euler(subPuzzleOneMoverBig.transform.rotation.eulerAngles.x, Global.lockPos, Global.lockPos);


            subPuzzleOneMoverBig.Translate(Vector3.back * Global.state.ThumbSticks.Left.Y * movementSpeedBig);
            if (Input.GetKey(KeyCode.W)) subPuzzleOneMoverBig.Translate(Vector3.back * 2 * movementSpeedBig);

            subPuzzleOneMoverBig.Translate(Vector3.forward * -Global.state.ThumbSticks.Left.Y * movementSpeedBig);
            if (Input.GetKey(KeyCode.S)) subPuzzleOneMoverBig.Translate(Vector3.forward * 2 * movementSpeedBig);

            // left and right
            subPuzzleOneMoverBig.Translate(Vector3.right * -Global.state.ThumbSticks.Left.X * movementSpeedBig);
            if (Input.GetKey(KeyCode.D)) subPuzzleOneMoverBig.Translate(Vector3.left * 2 * movementSpeedBig);

            subPuzzleOneMoverBig.Translate(Vector3.left * Global.state.ThumbSticks.Left.X * movementSpeedBig);
            if (Input.GetKey(KeyCode.A)) subPuzzleOneMoverBig.Translate(Vector3.right * 2 * movementSpeedBig);
        }
    }
}
