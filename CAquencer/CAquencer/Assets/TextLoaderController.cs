using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextLoaderController : MonoBehaviour {

	public ArtistStatment artistStatement = null;

	public void saveText(string text) {
		artistStatement.text = text;
	}
	
	// Use this for initialization
	void Start () {
		GetComponent<Text>().text = artistStatement.text;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
