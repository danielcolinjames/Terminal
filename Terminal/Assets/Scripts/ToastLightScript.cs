using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class ToastLightScript : MonoBehaviour {
    public Light flashlight;
    // Use this for initialization
    public KeyCode input = KeyCode.T;

    void Awake()
    {
        flashlight = GameObject.FindGameObjectWithTag("ToastLight").GetComponent<Light>();

    }

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetKeyDown(input)) || (Global.prevState.DPad.Right == ButtonState.Released && Global.state.DPad.Right == ButtonState.Pressed))
        {
            flashlight.enabled = !flashlight.enabled;
        }
    }
}
