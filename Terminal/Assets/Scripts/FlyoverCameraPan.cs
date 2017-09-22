using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyoverCameraPan : MonoBehaviour {

	// Use this for initialization
	void Start () {
            iTween.MoveBy(gameObject, iTween.Hash("y", 60, "easeType", "easeInOutQuad", "delay", 1, "time", 30));
    }
}
