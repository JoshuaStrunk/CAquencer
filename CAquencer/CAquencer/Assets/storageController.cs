using UnityEngine;
using System.Collections;

public class storageController : MonoBehaviour {
	public CellList cellList;

	void Start() {
		DontDestroyOnLoad(gameObject);
		Application.LoadLevel(1);
	}
}
