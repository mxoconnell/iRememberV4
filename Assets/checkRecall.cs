//This script will prompt the user with a random picture saved in memory and evaluate if recall is correct
using UnityEngine;
using System.Collections;

public class checkRecall : MonoBehaviour {

    public SimpleSQL.SimpleSQLManager dbManager;// reference to our database manager object in the scene (to get prior memories and evaluate recall)
    public UnityEngine.UI.RawImage imgDisplay;
    public UnityEngine.UI.Button btnTestRecall;
    
    private  memoryMachine scriptToRecallMemories;
    private playerMemory memoryToRecall;   

    // Use this for initialization
    void Start () {
        scriptToRecallMemories = new memoryMachine();
        memoryToRecall = scriptToRecallMemories.getSavedMemory(dbManager);
        
        if(memoryToRecall != null){ 
            //display picture for user to recall
            imgDisplay.texture = Resources.Load<Texture>(memoryToRecall.imageLocation);
        }
        else{
            //No memories to recall
            btnTestRecall.interactable = false;
            //default display image of "no memories found" will be displayed
        }
        
    }

	//evaluate user's guess of the memory's name
	public void evaluateRecall(UnityEngine.UI.Text textGuess){
        Debug.Log("Guess:" + textGuess.text);
        string userGuess = textGuess.text.ToUpper();
        bool wasUserCorrect = userGuess.Equals(memoryToRecall.displayText.ToUpper());
        PlayerPrefs.SetInt("wasUserCorrect", wasUserCorrect ? 1 : 0); //will save if user was correct into player prefs variable of the same name
        PlayerPrefs.SetString("memoryDisplayText", memoryToRecall.displayText);
        PlayerPrefs.SetString("memoryImageLocation", memoryToRecall.imageLocation); 
        //TODO scoring
        Application.LoadLevel("postRecall");
		
	}
} 
