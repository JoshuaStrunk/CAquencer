using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class savingTextScript : MonoBehaviour {

	public ArtistStatment artistStatement;

	public void saveTheFuckingText(){
		artistStatement.text = GetComponent<InputField>().value;
	}
}
