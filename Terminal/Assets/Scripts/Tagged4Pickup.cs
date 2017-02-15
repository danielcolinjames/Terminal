using UnityEngine;
using System.Collections;

public class Tagged4Pickup : MonoBehaviour {
    //for collision detection
    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Wall")
            if (PickupObject.carrying == true) {
                PickupObject.dropObject();
            }
    }
}