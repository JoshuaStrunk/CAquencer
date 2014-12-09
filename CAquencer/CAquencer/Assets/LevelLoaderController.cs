using UnityEngine;
using System.Collections;

public class LevelLoaderController : MonoBehaviour {

	public void loadLevel(int i) {
		Application.LoadLevel(i);
	}

	public void exit() {
		Application.Quit();
	}
}
