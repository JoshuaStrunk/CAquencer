using UnityEngine;
using System.Collections;


public class CAController : MonoBehaviour {

	public CellList cellList;
	public int[] startingSet;
	public AudioClip sourceTrack;
	public AudioSource[] tracks;

	int[] currentSet;
	int[] oldSet;

	AudioSource audio;

	// Use this for initialization
	void Start () {
		currentSet = new int[startingSet.Length];
		oldSet = new int[startingSet.Length];
		startingSet.CopyTo(currentSet, 0);
		tracks = new AudioSource[currentSet.Length];
		for(int i=0; i<currentSet.Length; i++){
			tracks[i] = gameObject.AddComponent<AudioSource>() as AudioSource;
			tracks[i].clip = sourceTrack;
			tracks[i].pitch = i *.5f + .5f;
			tracks[i].volume = .5f;
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
		for(int i=0; i < currentSet.Length; i++) {
			oldSet[i] = currentSet[i];
		}
		int l = currentSet.Length;
		for(int i=0; i < l; i++) {
			currentSet[i] = patternMatch(i);
			if(currentSet[i] != 0) {
				tracks[i].Play();
			}
		}
	}

	void OnGUI() {
		for(int i=0; i<currentSet.Length; i++) {
			GUIStyle temp = new GUIStyle();
			Texture2D quickTexture = new Texture2D(1,1);
			quickTexture.SetPixel(1,1,cellList.cellList[currentSet[i]].color);
			quickTexture.Apply();
			temp.normal.background = quickTexture;
			GUI.Box(new Rect(Screen.width/2, i*50+100, 50, 50), "", temp);
		}
	}

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
	
}
