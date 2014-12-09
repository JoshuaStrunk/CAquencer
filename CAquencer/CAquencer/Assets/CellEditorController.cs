using UnityEngine;
using System.Collections;
using System;

public class CellEditorController : MonoBehaviour {

	public CellList cellList;
	int cellIndex = 0;
	int selectorIndex = 0;
	Texture2D[] cellTextures;
	// Use this for initialization
	void Start () {
		cellList = GameObject.Find ("Storage").GetComponent<storageController>().cellList;
		cellTextures = new Texture2D[cellList.cellList.Count];
		for(int i=0; i<cellList.cellList.Count; i++) {
			cellTextures[i] = new Texture2D(25,25);
			for(int j=1; j<=25; j++) {
				for(int k=1; k<=25;k++) {
					cellTextures[i].SetPixel(j,k, cellList.cellList[i].color);
				}
			}
			cellTextures[i].Apply();

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI() {
		if(cellList != null && cellList.cellList != null) {
			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical();
			GUILayout.BeginHorizontal();
			GUILayout.Box(cellTextures[cellList.cellList[cellIndex].id]);
			if(cellList.cellList[cellIndex].playNote) {
				if(GUILayout.Button ("plays note")) {
					cellList.cellList[cellIndex].playNote = false;
				}
			}
			else{
				if(GUILayout.Button ("doesn't play note")) {
					cellList.cellList[cellIndex].playNote = true;
				}
			}
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			if(cellIndex > 0) {
				if(GUILayout.Button ("Prev Cell")){
					cellIndex--;
				}
			}
			if(cellIndex < cellList.cellList.Count-1) {
				if(GUILayout.Button ("Next Cell")){
					cellIndex++;
				}
			}
			GUILayout.EndHorizontal();
			
			//Cell Editor - patterns
			
			// Pattern Editor
			GUILayout.BeginHorizontal();
			if(GUILayout.Button ("New Pattern")){
				CreatePattern();
			}
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal();
			for(int j=0; j<cellList.cellList[cellIndex].patterns.Count; j++) {
				GUILayout.BeginVertical();
				GUILayout.BeginHorizontal();
				GUILayout.Label("Transform to");
				if(GUILayout.Button(cellTextures[cellList.cellList[cellIndex].patterns[j].id])) {
					cellList.cellList[cellIndex].patterns[j].id = selectorIndex;
				}
				GUILayout.EndHorizontal();
				for(int i=0; i<5; i++) {
					if(GUILayout.Button (cellTextures[cellList.cellList[cellIndex].patterns[j].pattern[i]])) {
						cellList.cellList[cellIndex].patterns[j].pattern[i] = selectorIndex;
					}
				}
				if(GUILayout.Button ("X")){
					cellList.cellList[cellIndex].patterns.RemoveAt(j);
				}
				GUILayout.EndVertical();
				GUILayout.BeginHorizontal();
				GUILayout.EndHorizontal();
			}
			GUILayout.EndHorizontal();
			GUILayout.EndVertical();
			GUILayout.BeginVertical();
			for(int i=0; i<cellList.cellList.Count; i++) {
				if(GUILayout.Button (cellTextures[cellList.cellList[i].id])) {
					selectorIndex = i;
				}
			}
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		}
		if(GUI.changed) {
			GameObject.Find ("Storage").GetComponent<storageController>().cellList = cellList;
		}
	}
	
	void CreatePattern() {
		cellList.cellList[cellIndex].addPattern(0, new int[5]{0,0,0,0,0});
	}
}
