using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class CAController : MonoBehaviour {

	public CellList cellList;

	public float cellHeigth = 50f;
	public float cellWidth = 50f;
	public int[] startingSet;
	public AudioClip sourceTrack;
	public AudioClip[] clips;
	private AudioSource[] tracks;
	public float pitchStep = .5f;
	public float startPitch = 1f;
	public float volume = .5f;
	

	private int selectedBrush = 0;
	private bool paused = true;
	private int setNum = -1;

	GUIStyle[] cellColors;
	GUIStyle[] cellBright;
	/*
	int[] currentSet;
	int[] oldSet;
	*/
	int[,] sets;


	List<int>[] currentSet;
	List<int>[] oldSet;
	 

	AudioSource audio;

	// Use this for initialization
	void Start () {
		/*
		currentSet = new int[startingSet.Length];
		oldSet = new int[startingSet.Length];
		*/


		currentSet = new List<int>[startingSet.Length];
		 oldSet = new List<int>[startingSet.Length];


		sets = new int[16, startingSet.Length];
		//startingSet.CopyTo(currentSet, 0);
		
		for(int i=0; i<currentSet.Length; i++) {
			currentSet[i] = new List<int>();
			currentSet[i].Add(0);
			oldSet[i] = currentSet[i];
		}


		tracks = new AudioSource[clips.Length];
		for(int i=0; i<clips.Length; i++){
			tracks[i] = gameObject.AddComponent<AudioSource>();
			tracks[i].clip = clips[i];
			tracks[i].volume = volume;
		}

		cellColors = new GUIStyle[cellList.cellList.Count];
		cellBright = new GUIStyle[cellList.cellList.Count];
		for(int i=0; i<cellList.cellList.Count; i++) {
			cellColors[i] = new GUIStyle();
			cellBright[i] = new GUIStyle();
			Texture2D quickTexture = new Texture2D(1,1);
			Texture2D brightTexture = new Texture2D(1,1);
			quickTexture.SetPixel(1,1,cellList.cellList[i].color);
			quickTexture.Apply();
			cellColors[i].normal.background = quickTexture;
			brightTexture.SetPixel(1,1,cellList.cellList[i].color + new Color(.25f,.25f,.25f));
			brightTexture.Apply();
			cellBright[i].normal.background = brightTexture;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	int modWraped(int x, int mod) {
		while(x < 0) {
			x = mod + x; 
		}
		return x % mod;
	}

	void SequencedEvent() {
		if(!paused) {
			if(++setNum >= 16) {
				setNum = 0;
				for(int i=0; i<16; i++) {
					for(int j=0; j<currentSet.Length; j++) {
						sets[i,j] =0;
					}
				}
			}

			for(int i=0; i < currentSet.Length; i++) {
				oldSet[i] = currentSet[i];
				//This will need to be changed
				sets[setNum, i] = currentSet[i].Last();
			}

			int l = currentSet.Length;
			for(int i=0; i < l; i++) {
				currentSet[i] = patternMatch(i);
				bool quickSwitch = false;
				foreach(int j in currentSet[i]) {
					if(cellList.cellList[j].playNote) {
						quickSwitch = true;
					}
				}
				if(quickSwitch) {
					tracks[i].PlayOneShot(clips[i], .125f);
				}
			}
		}
	}

	void OnGUI() {
		float offsetY = Mathf.Abs(transform.position.y);
		GUI.Box (new Rect(Screen.width/2 - (cellWidth/2)*cellList.cellList.Count - 75, 0 + offsetY, cellWidth, cellHeigth), "", cellColors[selectedBrush]);

		if(GUI.Button (new Rect(Screen.width/2 - (cellWidth/2)*cellList.cellList.Count -125, 0+ offsetY, cellWidth, cellHeigth), "pause", cellColors[0])) {
			paused = !paused;
		}

		for(int i=0; i<cellList.cellList.Count; i++) {
			if(GUI.Button(new Rect(Screen.width/2 - (cellWidth/2)*cellList.cellList.Count + i*cellWidth, 0+ offsetY, cellWidth, cellHeigth), "", cellColors[i])) {
				selectedBrush = i;
			}
		}
		for(int i=0; i<16; i++) {
		   for( int j=0; j<currentSet.Length; j++) {
				GUI.Box(new Rect(Screen.width/2 - (cellWidth/2)*16 +i*cellWidth, j*cellHeigth+100+ offsetY, cellWidth, cellHeigth), "", cellColors[sets[i,j]]);
			}
		}


		for(int i=0; i<currentSet.Length; i++) {
			if(GUI.Button(new Rect(Screen.width/2 - (cellWidth/2)*16 +setNum*cellWidth, i*cellHeigth+100+ offsetY, cellWidth, cellHeigth), "", cellColors[oldSet[i].Last()])){
			}
		}
		for(int i=0; i<currentSet.Length; i++) {
			if(GUI.Button(new Rect( Screen.width/2 - (cellWidth/2)*16 + ((setNum+1)%16)*cellWidth, i*cellHeigth+100+ offsetY, cellWidth, cellHeigth), "", cellBright[currentSet[i].Last ()])){
				List<int> temp = new List<int>();
				temp.Add (0);
				if(selectedBrush != 0)
					temp.Add (selectedBrush);
				currentSet[i] = temp;
			}
		}


	}
	/*
	int patternMatch(int cellIndex) {
		for(int i=0; i < cellList.cellList[oldSet[cellIndex]].patterns.Count; i++) {
			bool match = true;
			for(int j = 0; j<5; j++) {
				int quickA = oldSet[modWraped(cellIndex+j-2, oldSet.Length)];
				int quickB = cellList.cellList[oldSet[cellIndex]].patterns[i].pattern[j];
				if(quickB != -1 && quickA != quickB){
					match = false;
					break;
				}
			}
			if(match) {
				return cellList.cellList[oldSet[cellIndex]].patterns[i].id;
			}
		}
		return 0;
	}
	*/

	/*
	List<int> patternMatch(int cellIndex) {
		List<int> ret = new List<int>();
		// BigO() Scarrry
		foreach(int i in oldSet[cellIndex]) {
			bool match = true;
			for(int j=0; j<cellList.cellList[i].patterns.Count; j++) {
				Debug.Log ("j = " + j);
				for(int k = 0; k<5; k++) {
					bool innerMatch = false;
					foreach(int l in oldSet[modWraped(cellIndex+k-2, oldSet.Length)]) {
						int quickA = l;
						int quickB = cellList.cellList[i].patterns[j].pattern[k];
						if(quickB == -1 || quickA == quickB) {
							innerMatch = true;
							break;
						}

					}
					if(!innerMatch) {
						match = false;
					}
				}
				if(match) {
					ret.Add(cellList.cellList[i].patterns[j].id);
					break;
				}

			}
		}
		return ret;
	}
	*/
	List<int> patternMatch(int cellIndex) {

		List<int> ret = new List<int>();
		ret.Add(0);
		//Loop through all the states at cellIndex
		foreach(int i in oldSet[cellIndex]) {
			//Try to find a pattern to match with state i
			for(int pattern = 0; pattern < cellList.cellList[i].patterns.Count; pattern++) {
				bool match = true;
				//loop through neighboorhood of 5
				for(int j=0; j<5; j++){
					bool internalMatch = false;
					foreach(int cellState in oldSet[modWraped(cellIndex + j - 2,oldSet.Length)]) {
						int patternTarget = cellList.cellList[i].patterns[pattern].pattern[j];

						if(patternTarget == cellState || patternTarget == -1) {
							internalMatch = true;
							//only need one match
							break;
						}
					}
					if(!internalMatch) {
						match = false;
						// If one fails they all fail
						break;
					}

				}

				if(match) {
					//Add new state for matched pattern
					ret.Add(cellList.cellList[i].patterns[pattern].id);
					//Break cause only one new state per state
					//break;
				}
			}
		}
		return ret;
	}
	 
	
}
