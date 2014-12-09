using UnityEngine;
using System.Collections;

public class SequencerController : MonoBehaviour {


	public int beatsPerMinute = 60;
	public float beat = 1f/4f;
	public float sequenceTo = 1f/8f;

	float timing;
	float timeBetweenBeats;

	AudioSource audio; 
	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
		timing = 0f;
		timeBetweenBeats = (((float)60) / ((float)beatsPerMinute)) / (beat / sequenceTo);

	}
	
	// Update is called once per frame
	void Update () {
		timing += Time.deltaTime;
		if(timing > timeBetweenBeats) {
			SendMessage("SequencedEvent");
			for(int i=0; i<transform.childCount; i++) 
				transform.GetChild(i).SendMessage("SequencedEvent");
			timing -= timeBetweenBeats;
		}
	}

	void SequencedEvent() {
		//audio.Play ();
	}
}
