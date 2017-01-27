using UnityEngine;
using System.Collections;

public class PlayTimer : MonoBehaviour {

    public int playTime = 0; // to manipulate time
    private int seconds = 0;
    private int minute = 0;
    private int hours = 0;
    private int days = 0;
    private int playtime;

    // Use this for initialization
    void Start () {

        StartCoroutine("Playtime");

	}

    private IEnumerator Playtime()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            playtime += 1;
            seconds = (playtime % 60);
            minute = (playtime / 60) % 60;
            hours = (playtime / 3600) % 24;
            days = (playtime / 86400) % 365;
        }
    }
    	
    void OnGUI()
    {
        GUI.Label(new Rect(50, 50, 400, 50), "Playtime = " + days.ToString() + " : " + hours.ToString() + " : " + minute.ToString() + " : " + seconds.ToString());
    }

}
