using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class savingTextScript : MonoBehaviour {

	public ArtistStatment artistStatement;

	public void saveText(){
		artistStatement.text = GetComponent<InputField>().value;
	}
}
