//This script is used in most scene displays for mapping variable buttons to variable scenes to load.
//This script is attached to a "manager" (empty) game object which is then attached to a button.
using UnityEngine;
using System.Collections;

public class changeScene : MonoBehaviour {

	// Use this for initialization
	public void ChangeToScene(string sceneToChangeTo){
		Application.LoadLevel (sceneToChangeTo);
	}
}
