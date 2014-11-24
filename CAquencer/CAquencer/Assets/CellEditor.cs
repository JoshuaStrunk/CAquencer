using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

public class CellEditor : EditorWindow {
	public CellList cellList;
	int cellIndex = 0;

	[MenuItem ("Window/Cell Editor")]
	static void Init() {
		EditorWindow.GetWindow(typeof(CellEditor));
	}

	void OnEnable() {
	}

	void OnGUI() {

		GUILayout.BeginHorizontal();
		GUILayout.Label("Cell List Editor", EditorStyles.boldLabel);
		if(GUILayout.Button("Open Cell List")) {
			OpenCellList();
		}
		GUILayout.EndHorizontal();

		if(cellList != null && cellList.cellList != null) {
			GUILayout.BeginHorizontal();
				if(GUILayout.Button ("New Cell")){
					CreateCell();
				}
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
			if(cellList.cellList.Count == 0){
				CreateCell();
			}
			GUILayout.BeginHorizontal();
			cellList.cellList[cellIndex].id = EditorGUILayout.IntField(cellList.cellList[cellIndex].id);
			cellList.cellList[cellIndex].color = EditorGUILayout.ColorField(cellList.cellList[cellIndex].color);
			GUILayout.EndHorizontal();

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
				EditorGUILayout.LabelField("Transform to");
				cellList.cellList[cellIndex].patterns[j].id = EditorGUILayout.IntField(cellList.cellList[cellIndex].patterns[j].id);
				GUILayout.EndHorizontal();
						for(int i=0; i<5; i++) {
							cellList.cellList[cellIndex].patterns[j].pattern[i] = EditorGUILayout.IntField(cellList.cellList[cellIndex].patterns[j].pattern[i]);
						}
				if(GUILayout.Button ("X")){
					cellList.cellList[cellIndex].patterns.RemoveAt(j);
				}
				GUILayout.EndVertical();
				GUILayout.BeginHorizontal();
				GUILayout.EndHorizontal();
			}
			GUILayout.EndHorizontal();
		}
		if(GUI.changed) {
			EditorUtility.SetDirty(cellList);
		}
	}

	void OpenCellList() {
		string absPath = EditorUtility.OpenFilePanel("Slect Cell List", "", "");
		if(absPath.StartsWith(Application.dataPath)) {
			string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
			cellList = AssetDatabase.LoadAssetAtPath(relPath, typeof(CellList)) as CellList;
			if(cellList) {
				EditorPrefs.SetString ("ObjectPath", relPath);
			}
		}
	}
	void CreateCell() {
		Cell temp = new Cell();
		cellList.cellList.Add(temp);
		cellIndex = cellList.cellList.Count-1;
		CreatePattern();
	}
	void CreatePattern() {
		cellList.cellList[cellIndex].addPattern(0, new int[5]{0,0,0,0,0});
	}

}
