using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class CreateCellList {
	[MenuItem("Assets/Create/CellList")]
	public static CellList Create() {
		CellList asset = ScriptableObject.CreateInstance<CellList>();
		asset.cellList = new List<Cell>();
		AssetDatabase.CreateAsset(asset, "Assets/CellList.asset");
		AssetDatabase.SaveAssets();
		return asset;
	}

}
