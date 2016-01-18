//This script is used to create a memory for adding new memories
//Credit: forum.unity3d.com/threads/resources-subfolder.36918
//Note: in all unity paths the backslash is represented by a forwardslash (clash must point northeast)

using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;//for directoryInfo & FileInfo


public class createMemory : MonoBehaviour {
     
public SimpleSQL.SimpleSQLManager dbManager;// reference to our database manager object in the scene (to get a random name and to save our new memory)
public UnityEngine.UI.RawImage imgDisplay;
public UnityEngine.UI.Text displayName;

private playerMemory newMemory;

    void Start () {
        //Setup a memory to display. If the user chooses to save a memory, this will be the one we save to file.
        memoryMachine scriptToGenerateMemories = new memoryMachine();
        newMemory = scriptToGenerateMemories.getRandomGenderMemory(dbManager);
        displayName.text = newMemory.displayText;
        imgDisplay.texture = Resources.Load<Texture>(newMemory.imageLocation);
    }
	
    //Save displayed memory to file
   public void saveMemory(){
        // Call our SQL statement using ? to bind our variables
        dbManager.Execute("INSERT INTO playerMemory (displayText, imageLocation, timeOfLastRecall) VALUES (?, ?, ?)", newMemory.displayText, newMemory.imageLocation, newMemory.timeOfLastRecall);
        Debug.Log("Memory Saved!");
        //Now return user to the main menu
        Application.LoadLevel("mainMenu");
    }
}
