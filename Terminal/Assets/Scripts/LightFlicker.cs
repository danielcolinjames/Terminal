using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {

    float timer = 0f;
    float interval = 1f;

    bool off = false;

    public Light flickerLight;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (timer > interval && Global.currentPuzzle != 2) {
            if (!off) {
                flickerLight.enabled = false;
                off = true;
            } else {
                flickerLight.enabled = true;
                off = false;
            }

            timer = 0f;
            interval = Random.Range(0.05f, 0.3f);
        }
	}
}
