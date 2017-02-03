using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;

public class puzzle5ConveyorBelt : MonoBehaviour {

    public Transform goal1;
    public Transform goal2;
    public Transform goal3;
    public Transform goal4;

    // cubes
    public Transform s1c1;
    public Transform s1c2;
    public Transform s1c3;
    public Transform s1c4;

    public Transform s2c1;
    public Transform s2c2;
    public Transform s2c3;
    public Transform s2c4;

    public Transform s3c1;
    public Transform s3c2;
    public Transform s3c3;
    public Transform s3c4;

    public Transform s4c1;
    public Transform s4c2;
    public Transform s4c3;
    public Transform s4c4;

    public Transform s5c1;
    public Transform s5c2;
    public Transform s5c3;
    public Transform s5c4;
    // end cubes

    // stuff for this puzzle
    int currentStage = 1;

    public AudioClip successSound;
    public AudioClip failureSound;

    public Transform behindRedGoal;
    public Transform behindBlueGoal;
    public Transform behindGreenGoal;
    public Transform behindYellowGoal;

    float goalTolerance = 0.5f;

    public Transform player;
    public Transform conveyorBeltCheck;

    public Transform largeGoal;

    bool s1complete = false;
    bool s2complete = false;
    bool s3complete = false;
    bool s4complete = false;
    bool s5complete = false;

    int finishedCubeCount = 0;

    GameObject[] s1cubes;
    GameObject[] s2cubes;
    GameObject[] s3cubes;
    GameObject[] s4cubes;
    GameObject[] s5cubes;

    public Renderer whitePlane;
    public Renderer redPlane;
    public Renderer greenPlane;

    float speed;
    float step;

    // lights
    public Light berlin;
    public Light budapest;
    public Light cuba;
    public Light dubai;
    public Light egypt;
    public Light istanbul;
    public Light london;
    public Light losAngeles;
    public Light newYork;
    public Light reykjavik;
    public Light seoul;
    public Light shanghai;
    public Light sydney;
    public Light tokyo;
    public Light toronto;
    public Light vienna;

    // lights above console
    public Light light1;
    public Light light2;
    public Light light3;
    public Light light4;

    bool lightsOn = false;
    float lightsOnTime = 0.0f;

    public Light conveyorSpotlight;


    // stuff for the monitor camera
    public Canvas monitorCanvas;
    public Text monitorText;

    bool puzzle5Started = false;
    public Transform puzzle5Plane;
    Vector3 puzzle5CameraPosition;

    bool textWritingStarted = false;

    bool cubesReleased = false;


    void Start () {
        speed = 5f;
        step = speed * Time.deltaTime;

        puzzle5CameraPosition = new Vector3(puzzle5Plane.position.x, puzzle5Plane.position.y + 10f, puzzle5Plane.position.z);

        redPlane.enabled = false;
        greenPlane.enabled = false;

        s1cubes = GameObject.FindGameObjectsWithTag("s1cube");
        s2cubes = GameObject.FindGameObjectsWithTag("s2cube");
        s3cubes = GameObject.FindGameObjectsWithTag("s3cube");
        s4cubes = GameObject.FindGameObjectsWithTag("s4cube");
        s5cubes = GameObject.FindGameObjectsWithTag("s5cube");

    }

    // Update is called once per frame
    void Update () {

        // before the camera has gotten here

        if (Global.currentPuzzle == 5 && puzzle5Started == false) {

            float distance = Vector3.Distance(Global.monitorCamera.position, puzzle5CameraPosition);

            Global.monitorCamera.position = Vector3.MoveTowards(Global.monitorCamera.position, puzzle5CameraPosition, step);

            if (distance == 0) {
                // setting a flag when the camera has finished panning over
                // otherwise the player can move things around before the camera is finished panning
                puzzle5Started = true;
            }
        }



        if (Global.currentPuzzle == 5 && puzzle5Started)
        {
            // turn on the text on the screen
            monitorCanvas.enabled = true;

            // check light timing
            resetLights();

            // set up stage 1
            if (currentStage == 1)
            {

                if (textWritingStarted == false)
                {
                    textWritingStarted = true;
                    string message1 = "> Packages require delivery \n" +
                        "> hue sequence incoming... \n" +
                        "> \n" +
                        "> YELLOW\n" +
                        "> RED\n" +
                        "> BLUE\n" +
                        "> GREEN\n";

                    StartCoroutine(typeText(message1));

                }

                s1c1.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                s1c2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                s1c3.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                s1c4.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

                // if the cubes haven't been shot out of the tunnels yet
                if (cubesReleased == false)
                {
                    // shoot them out of the tunnels
                    s1c1.position = behindRedGoal.position;
                    s1c1.position = s1c1.transform.position + new Vector3(1f, 0f, 0);
                    s1c1.GetComponent<Rigidbody>().velocity = new Vector3(14f, 0.25f, 0);

                    s1c2.position = behindGreenGoal.position;
                    s1c2.position = s1c2.transform.position + new Vector3(1f, 0f, 0);
                    s1c2.GetComponent<Rigidbody>().velocity = new Vector3(14f, 0.25f, 0);

                    s1c3.position = behindBlueGoal.position;
                    s1c3.position = s1c3.transform.position + new Vector3(-1f, 0f, 0);
                    s1c3.GetComponent<Rigidbody>().velocity = new Vector3(-14f, 0.25f, 0);

                    s1c4.position = behindYellowGoal.position;
                    s1c4.position = s1c4.transform.position + new Vector3(-1f, 0f, 0);
                    s1c4.GetComponent<Rigidbody>().velocity = new Vector3(-14f, 0.25f, 0);

                    cubesReleased = true;
                }
                checkPlacementOfBoxes();

                if (s1complete == true)
                {
                    moveBoxesIntoWall();
                }

            }
            else if (currentStage == 2)
            {

                if (textWritingStarted == false)
                {
                    textWritingStarted = true;
                    string message2 = "> Packages require delivery \n" +
                        "> location sequence incoming... \n" +
                        "> \n" +
                        "> TORONTO\n" +
                        "> BERLIN\n" +
                        "> NEW_YORK\n" +
                        "> SYDNEY\n";

                    StartCoroutine(typeText(message2));
                    toronto.color = Color.red;
                    berlin.color = Color.blue;
                    newYork.color = Color.green;
                    sydney.color = Color.yellow;
                }

                s2c1.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                s2c2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                s2c3.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                s2c4.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;


                if (cubesReleased == false)
                {

                    s2c1.position = behindRedGoal.position;
                    s2c1.position = s2c1.transform.position + new Vector3(1f, 0f, 0);
                    s2c1.GetComponent<Rigidbody>().velocity = new Vector3(14f, 0.25f, 0);

                    s2c2.position = behindGreenGoal.position;
                    s2c2.position = s2c2.transform.position + new Vector3(1f, 0f, 0);
                    s2c2.GetComponent<Rigidbody>().velocity = new Vector3(14f, 0.25f, 0);

                    s2c3.position = behindBlueGoal.position;
                    s2c3.position = s2c3.transform.position + new Vector3(-1f, 0f, 0);
                    s2c3.GetComponent<Rigidbody>().velocity = new Vector3(-14f, 0.25f, 0);

                    s2c4.position = behindYellowGoal.position;
                    s2c4.position = s2c4.transform.position + new Vector3(-1f, 0f, 0);
                    s2c4.GetComponent<Rigidbody>().velocity = new Vector3(-14f, 0.25f, 0);

                    cubesReleased = true;
                }
                checkPlacementOfBoxes();

                if (s2complete == true)
                {
                    moveBoxesIntoWall();
                }
            }

            else if (currentStage == 3)
            {
                if (textWritingStarted == false)
                {
                    textWritingStarted = true;
                    string message3 = "> Packages require delivery \n" +
                        "> location sequence incoming... \n" +
                        "> \n" +
                        "> REYKJAVIK\n" +
                        "> SYDNEY\n" +
                        "> LONDON\n" +
                        "> ISTANBUL\n";

                    StartCoroutine(typeText(message3));
                    reykjavik.color = Color.blue;
                    sydney.color = Color.blue;
                    london.color = Color.green;
                    istanbul.color = Color.red;
                    light1.color = Color.blue;

                }
                s3c1.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                s3c2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                s3c3.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                s3c4.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

                if (cubesReleased == false)
                {

                    s3c1.position = behindRedGoal.position;
                    s3c1.position = s3c1.transform.position + new Vector3(1f, 0f, 0);
                    s3c1.GetComponent<Rigidbody>().velocity = new Vector3(14f, 0.25f, 0);

                    s3c2.position = behindGreenGoal.position;
                    s3c2.position = s3c2.transform.position + new Vector3(1f, 0f, 0);
                    s3c2.GetComponent<Rigidbody>().velocity = new Vector3(14f, 0.25f, 0);

                    s3c3.position = behindBlueGoal.position;
                    s3c3.position = s3c3.transform.position + new Vector3(-1f, 0f, 0);
                    s3c3.GetComponent<Rigidbody>().velocity = new Vector3(-14f, 0.25f, 0);

                    s3c4.position = behindYellowGoal.position;
                    s3c4.position = s3c4.transform.position + new Vector3(-1f, 0f, 0);
                    s3c4.GetComponent<Rigidbody>().velocity = new Vector3(-14f, 0.25f, 0);

                    cubesReleased = true;
                }
                checkPlacementOfBoxes();

                if (s3complete == true)
                {
                    moveBoxesIntoWall();
                }
            }
            else if (currentStage == 4)
            {
                if (textWritingStarted == false)
                {
                    textWritingStarted = true;
                    string message4 = "> Packages require delivery \n" +
                        "> location sequence incoming... \n" +
                        "> \n" +
                        "> BUDAPEST\n" +
                        "> VIENNA\n" +
                        "> SOEUL\n" +
                        "> LONDON\n";

                    StartCoroutine(typeText(message4));
                    budapest.color = Color.red;
                    vienna.color = Color.black;
                    seoul.color = Color.red;
                    london.color = Color.black;
                    light1.color = Color.red;
                    light2.color = Color.red;
                    light3.color = Color.white;
                    light4.color = Color.white;

                }
                s4c1.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                s4c2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                s4c3.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                s4c4.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

                if (cubesReleased == false)
                {

                    s4c1.position = behindRedGoal.position;
                    s4c1.position = s4c1.transform.position + new Vector3(1f, 0f, 0);
                    s4c1.GetComponent<Rigidbody>().velocity = new Vector3(14f, 0.25f, 0);

                    s4c2.position = behindGreenGoal.position;
                    s4c2.position = s4c2.transform.position + new Vector3(1f, 0f, 0);
                    s4c2.GetComponent<Rigidbody>().velocity = new Vector3(14f, 0.25f, 0);

                    s4c3.position = behindBlueGoal.position;
                    s4c3.position = s4c3.transform.position + new Vector3(-1f, 0f, 0);
                    s4c3.GetComponent<Rigidbody>().velocity = new Vector3(-14f, 0.25f, 0);

                    s4c4.position = behindYellowGoal.position;
                    s4c4.position = s4c4.transform.position + new Vector3(-1f, 0f, 0);
                    s4c4.GetComponent<Rigidbody>().velocity = new Vector3(-14f, 0.25f, 0);

                    cubesReleased = true;
                }
                checkPlacementOfBoxes();

                if (s4complete == true)
                {
                    moveBoxesIntoWall();
                }
            }
            else if (currentStage == 5)
            {
                if (textWritingStarted == false)
                {
                    textWritingStarted = true;
                    string message5 = "> Packages require delivery \n" +
                        "> location sequence incoming... \n" +
                        "> \n" +
                        "> CUBA\n" +
                        "> TOKYO\n" +
                        "> SOEUL\n" +
                        "> SHANGHAI\n";

                    StartCoroutine(typeText(message5));
                    cuba.color = Color.blue;
                    tokyo.color = Color.black;
                    seoul.color = Color.red;
                    shanghai.color = Color.black;
                    light1.color = Color.blue;
                    light2.color = Color.blue;
                    light3.color = Color.red;
                    light4.color = Color.red; ;

                }
                s5c1.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                s5c2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                s5c3.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                s5c4.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

                if (cubesReleased == false)
                {

                    s5c1.position = behindRedGoal.position;
                    s5c1.position = s5c1.transform.position + new Vector3(1f, 0f, 0);
                    s5c1.GetComponent<Rigidbody>().velocity = new Vector3(14f, 0.25f, 0);

                    s5c2.position = behindGreenGoal.position;
                    s5c2.position = s5c2.transform.position + new Vector3(1f, 0f, 0);
                    s5c2.GetComponent<Rigidbody>().velocity = new Vector3(14f, 0.25f, 0);

                    s5c3.position = behindBlueGoal.position;
                    s5c3.position = s5c3.transform.position + new Vector3(-1f, 0f, 0);
                    s5c3.GetComponent<Rigidbody>().velocity = new Vector3(-14f, 0.25f, 0);

                    s5c4.position = behindYellowGoal.position;
                    s5c4.position = s5c4.transform.position + new Vector3(-1f, 0f, 0);
                    s5c4.GetComponent<Rigidbody>().velocity = new Vector3(-14f, 0.25f, 0);

                    cubesReleased = true;
                }
                checkPlacementOfBoxes();

                if (s5complete == true)
                {
                    moveBoxesIntoWall();
                }
            }
            else if (currentStage == 6)
            {
                if (textWritingStarted == false)
                {
                    textWritingStarted = true;
                    string message6 = "> all packages delivered... \n" +
                        "> CONGRATULATIONS\n";

                    StartCoroutine(typeText(message6));
                    berlin.enabled = false;
                    budapest.enabled = false;
                    cuba.enabled = false;
                    dubai.enabled = false;
                    egypt.enabled = false;
                    istanbul.enabled = false;
                    london.enabled = false;
                    losAngeles.enabled = false;
                    newYork.enabled = false;
                    reykjavik.enabled = false;
                    seoul.enabled = false;
                    shanghai.enabled = false;
                    sydney.enabled = false;
                    tokyo.enabled = false;
                    toronto.enabled = false;
                    vienna.enabled = false;
                    light1.color = Color.black;
                    light2.color = Color.black;
                    light3.color = Color.black;
                    light4.color = Color.black; ;

                }
            }
        }
	}

    void checkPlacementOfBoxes() {
        float distanceFromConveyorBelt = Vector3.Distance(player.position, conveyorBeltCheck.position);

        float c1d = 100;
        float c2d = 100;
        float c3d = 100;
        float c4d = 100;

        if(currentStage == 1) {
            c1d = Vector3.Distance(s1c1.position, goal1.position);
            c2d = Vector3.Distance(s1c2.position, goal2.position);
            c3d = Vector3.Distance(s1c3.position, goal3.position);
            c4d = Vector3.Distance(s1c4.position, goal4.position);
        }

        else if (currentStage == 2) {
            c1d = Vector3.Distance(s2c1.position, goal1.position);
            c2d = Vector3.Distance(s2c2.position, goal2.position);
            c3d = Vector3.Distance(s2c3.position, goal3.position);
            c4d = Vector3.Distance(s2c4.position, goal4.position);
        }

        else if (currentStage == 3) {
            c1d = Vector3.Distance(s3c1.position, goal1.position);
            c2d = Vector3.Distance(s3c2.position, goal2.position);
            c3d = Vector3.Distance(s3c3.position, goal3.position);
            c4d = Vector3.Distance(s3c4.position, goal4.position);
        }
        else if (currentStage == 4)
        {
            c1d = Vector3.Distance(s4c1.position, goal1.position);
            c2d = Vector3.Distance(s4c2.position, goal2.position);
            c3d = Vector3.Distance(s4c3.position, goal3.position);
            c4d = Vector3.Distance(s4c4.position, goal4.position);
        }
        else if (currentStage == 5)
        {
            c1d = Vector3.Distance(s5c1.position, goal1.position);
            c2d = Vector3.Distance(s5c2.position, goal2.position);
            c3d = Vector3.Distance(s5c3.position, goal3.position);
            c4d = Vector3.Distance(s5c4.position, goal4.position);
        }



        if (distanceFromConveyorBelt < 1.2f) {
            if (Input.GetKeyDown(KeyCode.E) || (Global.prevState.Buttons.A == ButtonState.Released && Global.state.Buttons.A == ButtonState.Pressed)) {

                light1.enabled = true;
                light2.enabled = true;
                light3.enabled = true;
                light4.enabled = true;

                lightsOn = true;

                if (c1d < goalTolerance && c2d < goalTolerance && c3d < goalTolerance && c4d < goalTolerance) {
                    Global.source.PlayOneShot(successSound, Global.volumeMed);

                    // SUCCESS
                    whitePlane.enabled = false;
                    redPlane.enabled = false;
                    greenPlane.enabled = true;

                    conveyorSpotlight.color = Color.green;
                                      
                    if (currentStage == 1) {
                        s1complete = true;
                    }
                    else if (currentStage == 2) {
                        s2complete = true;
                    }
                    else if (currentStage == 3) {
                        s3complete = true;
                    }
                    else if (currentStage == 4) {
                        s4complete = true;
                    }
                    else if (currentStage == 5) {
                        s5complete = true;
                    }

                } else {
                    Global.source.PlayOneShot(failureSound, Global.volumeMed);

                    greenPlane.enabled = false;
                    redPlane.enabled = true;
                    whitePlane.enabled = false;

                    conveyorSpotlight.color = Color.red;
                }
            }
        }
    }

    void moveBoxesIntoWall() {
        float speed = 4f;
        float step = speed * Time.deltaTime;

        if (currentStage == 1) {
            float distanceFromLargeGoal;

            for (int i = 0; i < s1cubes.Length; i++) {
                GameObject cube = s1cubes[i];

                Transform cubeT = cube.transform;
                distanceFromLargeGoal = Vector3.Distance(cubeT.position, largeGoal.position);

                cubeT.position = Vector3.MoveTowards(cubeT.position, largeGoal.position, step);

                float cubeDistance = Vector3.Distance(cubeT.position, largeGoal.position);

                if (cubeDistance < 0.5 && cube.activeSelf == true) {
                    cube.SetActive(false);
                    finishedCubeCount++;
                }

                if (finishedCubeCount == 4) {
                    // clear everything ONCE, at the end of stage 1
                    textWritingStarted = false;
                    // so that it won't keep entering an if statement above
                    s1complete = false;
                    cubesReleased = false;
                    currentStage = 2;
                    finishedCubeCount = 0;

                    greenPlane.enabled = false;
                    redPlane.enabled = false;
                    whitePlane.enabled = true;
                }
            }
        }
        
        else if (currentStage == 2) {
            float distanceFromLargeGoal;

            for (int i = 0; i < s2cubes.Length; i++) {
                GameObject cube = s2cubes[i];

                Transform cubeT = cube.transform;
                distanceFromLargeGoal = Vector3.Distance(cubeT.position, largeGoal.position);

                cubeT.position = Vector3.MoveTowards(cubeT.position, largeGoal.position, step);

                float cubeDistance = Vector3.Distance(cubeT.position, largeGoal.position);

                if (cubeDistance < 0.5 && cube.activeSelf == true) {
                    cube.SetActive(false);
                    finishedCubeCount++;
                }

                if (finishedCubeCount == 4) {
                    textWritingStarted = false;
                    s2complete = false;
                    cubesReleased = false;
                    currentStage = 3;
                    finishedCubeCount = 0;

                    greenPlane.enabled = false;
                    redPlane.enabled = false;
                    whitePlane.enabled = true;

                    resetCityLights();
                   
                }
            }
        }

        else if (currentStage == 3)
        {
            float distanceFromLargeGoal;

            for (int i = 0; i < s3cubes.Length; i++)
            {
                GameObject cube = s3cubes[i];

                Transform cubeT = cube.transform;
                distanceFromLargeGoal = Vector3.Distance(cubeT.position, largeGoal.position);

                cubeT.position = Vector3.MoveTowards(cubeT.position, largeGoal.position, step);

                float cubeDistance = Vector3.Distance(cubeT.position, largeGoal.position);

                if (cubeDistance < 0.5 && cube.activeSelf == true)
                {
                    cube.SetActive(false);
                    finishedCubeCount++;
                }

                if (finishedCubeCount == 4)
                {
                    textWritingStarted = false;
                    s3complete = false;
                    cubesReleased = false;
                    currentStage = 4;
                    finishedCubeCount = 0;

                    greenPlane.enabled = false;
                    redPlane.enabled = false;
                    whitePlane.enabled = true;

                    resetCityLights();
                }
            }
        }
        else if (currentStage == 4)
        {
            float distanceFromLargeGoal;

            for (int i = 0; i < s4cubes.Length; i++)
            {
                GameObject cube = s4cubes[i];

                Transform cubeT = cube.transform;
                distanceFromLargeGoal = Vector3.Distance(cubeT.position, largeGoal.position);

                cubeT.position = Vector3.MoveTowards(cubeT.position, largeGoal.position, step);

                float cubeDistance = Vector3.Distance(cubeT.position, largeGoal.position);

                if (cubeDistance < 0.5 && cube.activeSelf == true)
                {
                    cube.SetActive(false);
                    finishedCubeCount++;
                }

                if (finishedCubeCount == 4)
                {
                    textWritingStarted = false;
                    s4complete = false;
                    cubesReleased = false;
                    currentStage = 5;
                    finishedCubeCount = 0;

                    greenPlane.enabled = false;
                    redPlane.enabled = false;
                    whitePlane.enabled = true;

                    resetCityLights();
                }

            }
        }
        else if (currentStage == 5)
        {
            float distanceFromLargeGoal;

            for (int i = 0; i < s5cubes.Length; i++)
            {
                GameObject cube = s5cubes[i];

                Transform cubeT = cube.transform;
                distanceFromLargeGoal = Vector3.Distance(cubeT.position, largeGoal.position);

                cubeT.position = Vector3.MoveTowards(cubeT.position, largeGoal.position, step);

                float cubeDistance = Vector3.Distance(cubeT.position, largeGoal.position);

                if (cubeDistance < 0.5 && cube.activeSelf == true)
                {
                    cube.SetActive(false);
                    finishedCubeCount++;
                }

                if (finishedCubeCount == 4)
                {
                    textWritingStarted = false;
                    s5complete = false;
                    cubesReleased = false;
                    currentStage = 6;
                    finishedCubeCount = 0;

                    greenPlane.enabled = false;
                    redPlane.enabled = false;
                    whitePlane.enabled = true;

                    resetCityLights();
                }
            }
        }
    }

    void resetLights() {
        // calculate how long the lights have been on (add to the variable)
        if (lightsOn == true) {
            lightsOnTime += Time.deltaTime;
        }

        // if it's been 2 seconds, reset the conveyor belt lights
        if (lightsOnTime >= 5.0f) {

            light1.enabled = false;
            light2.enabled = false;
            light3.enabled = false;
            light4.enabled = false;

            lightsOnTime = 0.0f;
            lightsOn = false;

            redPlane.enabled = false;
            greenPlane.enabled = false;
            whitePlane.enabled = true;

            conveyorSpotlight.color = Color.white;
        }
    }

    IEnumerator typeText(string outputText) {
        float letterPause = 0.05f;
        monitorText.text = "";
        foreach (char letter in outputText.ToCharArray()) {
            monitorText.text += letter;
            // play blip noise
            yield return new WaitForSeconds(letterPause);
        }
    } 
    void resetCityLights ()
    {
        berlin.color = Color.white;
        budapest.color = Color.white;
        cuba.color = Color.white;
        dubai.color = Color.white;
        egypt.color = Color.white;
        istanbul.color = Color.white;
        london.color = Color.white;
        losAngeles.color = Color.white;
        newYork.color = Color.white;
        reykjavik.color = Color.white;
        seoul.color = Color.white;
        shanghai.color = Color.white;
        sydney.color = Color.white;
        tokyo.color = Color.white;
        toronto.color = Color.white;
        vienna.color = Color.white;
}
}
