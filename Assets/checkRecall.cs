//This script will prompt the user with a random picture saved in memory and evaluate if recall is correct
using UnityEngine;
using System.Collections;

public class checkRecall : MonoBehaviour {

    public SimpleSQL.SimpleSQLManager dbManager;// reference to our database manager object in the scene (to get prior memories and evaluate recall)
    public UnityEngine.UI.RawImage imgDisplay;

    //Buttons/Labels to deactivate if no memories are left
    public UnityEngine.UI.Button btnTestRecall;
    public UnityEngine.UI.Text txtDisplay;
    public UnityEngine.UI.Text txtNoMemoriesFound;
    public UnityEngine.UI.Text txtElapsedTime;
    public GameObject inputField;

    private  memoryMachine scriptToRecallMemories;
    private playerMemory memoryToRecall;  
    private int elapsedMinutes = 0; 

    // Use this for initialization
    void Start () {
        scriptToRecallMemories = new memoryMachine();
        memoryToRecall = scriptToRecallMemories.getSavedMemory(dbManager);
        
        if(memoryToRecall != null){ 
            //display picture for user to recall
            imgDisplay.texture = Resources.Load<Texture>(memoryToRecall.imageLocation);
            elapsedMinutes = memoryToRecall.minutesSinceLastRecall();
            txtElapsedTime.text = "Elapsed Time: " + elapsedMinutes + " minutes";
            txtNoMemoriesFound.enabled = false;//remove text that says "no memories found"
        }
        else{
            //No memories to recall
            btnTestRecall.interactable = false;
            txtDisplay.enabled = false;
            txtElapsedTime.enabled = false;
            inputField.SetActive(false);
        }
        
    }

	//evaluate user's guess of the memory's name
	public void evaluateRecall(UnityEngine.UI.Text textGuess){
        Debug.Log("Guess:" + textGuess.text);
        string userGuess = textGuess.text.ToUpper();
        bool wasUserCorrect = userGuess.Equals(memoryToRecall.displayText.ToUpper());

        //Save information of the memory to be displayed in post Recall
        PlayerPrefs.SetInt("wasUserCorrect", wasUserCorrect ? 1 : 0); //will save if user was correct into player prefs variable of the same name
        PlayerPrefs.SetString("memoryDisplayText", memoryToRecall.displayText);
        PlayerPrefs.SetString("memoryImageLocation", memoryToRecall.imageLocation);
        PlayerPrefs.SetInt("elapsedMinutes", elapsedMinutes);

        //Update memory's TOLRecall
        scriptToRecallMemories.updateMemory(dbManager, memoryToRecall);

        //Proceed to postRecall
        Application.LoadLevel("postRecall");
		
	}
} 
