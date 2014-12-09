using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateArtistStatement {

	[MenuItem("Assets/Create/ArtistStatement")]
	public static ArtistStatment Create() {
		ArtistStatment asset = ScriptableObject.CreateInstance<ArtistStatment>();
		asset.text = "";
		AssetDatabase.CreateAsset(asset, "Assets/ArtistStatement.asset");
		AssetDatabase.SaveAssets();
		return asset;
	}
}
