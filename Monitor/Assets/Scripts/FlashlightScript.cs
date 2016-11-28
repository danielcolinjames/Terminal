using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class FlashlightScript : MonoBehaviour {
    public Light flashlight;
    // Use this for initialization

    void Awake()
    {
        flashlight = GameObject.FindGameObjectWithTag("Flashlight").GetComponent<Light>();

    }

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetKeyDown(KeyCode.F)) || (Global.prevState.DPad.Left == ButtonState.Released && Global.state.DPad.Left == ButtonState.Pressed))
        {
            flashlight.enabled = !flashlight.enabled;
        }
    }
}
