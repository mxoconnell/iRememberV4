//This script is used to return to the main menu after recall
using UnityEngine;
using System.Collections;

public class changeScenePostRecall : MonoBehaviour {
    public SimpleSQL.SimpleSQLManager dbManager;// reference to our database manager object in the scene (if we need to delete it)
    public UnityEngine.UI.Toggle tglKeepMemory; 
    // Use this for initialization
    public void ChangeToScene(string sceneToChangeTo){
        //If tglKeepMemory is not toggled, then delete the memory from memory
        if(!tglKeepMemory.isOn){
            Debug.Log("Erasing Memory");
            dbManager.Execute("DELETE FROM playerMemory WHERE displayText = ?", PlayerPrefs.GetString("memoryDisplayText"));
        }

		Application.LoadLevel (sceneToChangeTo);
	}
}
