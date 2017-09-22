using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
        iTween.MoveBy(gameObject, iTween.Hash("z", 2, "easeType", "easeInOutQuad", "delay", 1, "time", 5));
    }
}
