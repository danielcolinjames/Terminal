using UnityEngine;
using System.Collections;

public class MonitorMode : MonoBehaviour {

    public Transform player;
    public Transform monitor;
    public float distanceToMonitor;

    void Awake() {
        player = GameObject.Find("FPSController").transform;
        monitor = GameObject.Find("Monitor").transform;

        distanceToMonitor = 0;
    }
    

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        distanceToMonitor = Vector3.Distance(player.position, monitor.position);
        print(distanceToMonitor);
	}
}
