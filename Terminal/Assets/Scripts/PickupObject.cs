using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PickupObject : MonoBehaviour {
    static GameObject mainCamera;
    public static bool carrying;
    public static GameObject carriedObject;
    public static Color ourColor;
    float pickupDistance = 2f; // how far away you can pick stuff up from
    float holdDistance = 1f;
    public float throwStrength = 6;

    public static float dropObjectStrength = 1;
    // Use this for initialization
    public static bool hasKey;
    public bool hitWall;

    public static Camera cam;

    // audio stuff
    public AudioClip pickupSound;

    void Start() {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update() {

        Debug.DrawRay(transform.position, transform.forward, Color.red, 1f);

        if (carrying) {
            carry(carriedObject);
            checkDrop();
            throwObject();
            changeThrowStrength();
        } else {
            pickup();
        }
    }

    void carry(GameObject o) {
        o.transform.position = mainCamera.transform.position + mainCamera.transform.forward * holdDistance;
    }

    void pickup() {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E) || (Global.prevState.Buttons.LeftShoulder == ButtonState.Released && Global.state.Buttons.LeftShoulder == ButtonState.Pressed)) {
            //int x = Screen.width / 2;
            //int y = Screen.height / 2;

            //Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y)); //FOR NON_VR GAME
			RaycastHit hit;
			Ray ray = new Ray (transform.position, transform.forward);
            if (Physics.Raycast(ray, out hit)) {
                Tagged4Pickup p = hit.collider.GetComponent<Tagged4Pickup>();
                if (p != null && (Physics.Raycast(ray, out hit, pickupDistance))) {
                    carrying = true;
                    carriedObject = p.gameObject;

                    //Select new shader for Transparency
                    Shader transparent;
                    transparent = Shader.Find("Transparent/Diffuse");
                    carriedObject.GetComponent<Renderer>().material.shader = transparent;

                    //To Control Object Transparency 
                    Color ourColor = carriedObject.GetComponent<Renderer>().material.color;
                    ourColor.a = 0.5f;
                    carriedObject.GetComponent<Renderer>().material.color = ourColor;

                    carriedObject.GetComponent<Rigidbody>().freezeRotation = true;


                    //Door Key stuff
                    if (carriedObject.name == "DoorKey" || carriedObject.tag == "isKey") {
                        hasKey = true;

                        Global.source.PlayOneShot(pickupSound, Global.volumeMed);

                        GameObject.FindGameObjectWithTag("isKey").SetActive(false);
                        dropObject();
                    } //else {
                      //	hasKey = false;
                      //}
                }
            }
        }
    }

    // Global.prevState.DPad.Up == ButtonState.Released && Global.state.DPad.Up == ButtonState.Pressed
    // state.DPad.Up
    void changeThrowStrength() {
        if (((Input.GetKeyDown(KeyCode.H))
            || (Global.prevState.DPad.Up == ButtonState.Released && Global.state.DPad.Up == ButtonState.Pressed))
            && throwStrength < 25) {

            throwStrength++;
            //Debug.Log ("STRENGTH IS " + throwStrength);
        }
        if (((Input.GetKeyDown(KeyCode.L)) || (Global.prevState.DPad.Down == ButtonState.Released && Global.state.DPad.Down == ButtonState.Pressed))
            && throwStrength > 0) {

            throwStrength = throwStrength - 1;
            //Debug.Log ("STRENGTH IS " + throwStrength);
        }
    }

    void checkDrop() {
        if ((Input.GetKeyDown(KeyCode.Q)) || (Global.prevState.Buttons.B == ButtonState.Released && Global.state.Buttons.B == ButtonState.Pressed)) {
            dropObject();
        }
    }

    public static void dropObject() {
        dropObjectNorm(carriedObject);
        carrying = false;

        Shader standard;
        standard = Shader.Find("Standard");
        carriedObject.GetComponent<Renderer>().material.shader = standard;
        ourColor = carriedObject.GetComponent<Renderer>().material.color;

        carriedObject.GetComponent<Rigidbody>().useGravity = true;
        carriedObject.GetComponent<Rigidbody>().isKinematic = false;
        carriedObject.GetComponent<Rigidbody>().freezeRotation = false;
        carriedObject = null;
    }

    void throwObject() {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.F) || (Global.prevState.Buttons.RightShoulder == ButtonState.Released && Global.state.Buttons.RightShoulder == ButtonState.Pressed)) {
            //carriedObject.transform.position = transform.position + Camera.main.transform.forward * 2;
            Rigidbody rb = carriedObject.GetComponent<Rigidbody>();
            rb.velocity = transform.forward * throwStrength;

            carrying = false;
            Shader standard;
            standard = Shader.Find("Standard");
            carriedObject.GetComponent<Renderer>().material.shader = standard;
            ourColor = carriedObject.GetComponent<Renderer>().material.color;
            
            carriedObject.GetComponent<Rigidbody>().useGravity = true;
            carriedObject.GetComponent<Rigidbody>().isKinematic = false;
            carriedObject.GetComponent<Rigidbody>().freezeRotation = false;
            carriedObject = null;
        }
    }

    public static void dropObjectNorm(GameObject ourObject) {
        ourObject.transform.position = mainCamera.transform.position + mainCamera.transform.forward * 1f;
        Rigidbody rb = carriedObject.GetComponent<Rigidbody>();
        rb.velocity = mainCamera.transform.forward * dropObjectStrength;
    }
}