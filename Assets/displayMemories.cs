using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// This script shows how to call a simple SQL query from a database using the class definition of the
/// database to format the results.
/// 
/// In this example we overwrite the working database since no data is being changed. This is set in the 
/// SimpleSQLManager gameobject in the scene.
/// </summary>
public class displayMemories : MonoBehaviour {

	// reference to our database manager object in the scene
	public SimpleSQL.SimpleSQLManager dbManager;
	
	// reference to the gui text object in our scene that will be used for output
	public GUIText outputText;

    void Start()
    {
        //add a new memory
        saveMemoryToFile("disp", "local");

        // Gather a list of memories and their info	
        List<playerMemory> memoriesOnFile = dbManager.Query<playerMemory>(
                                                        "SELECT " +
                                                            "W.displayText, " +
                                                            "W.imageLocation " +
                                                        "FROM " +
                                                            "playerMemory W "
                                                        );
      
        // output the list of memories 
        outputText.text = "Memories...\n\n";
        foreach (playerMemory currentMemory in memoriesOnFile) 
        {
            outputText.text += "Display text: " + currentMemory.displayText.ToString() +
                             "\nLocation:      " + currentMemory.imageLocation.ToString() +"\n\n";
        }
 
    }

    //We can add a class to the database if the name of the table is the same as the class name
     private void saveMemoryToFile(string display, string imgLocation)
     {
        // Initialize our playerMemory class
        playerMemory newMemory = new playerMemory { displayText = display, imageLocation = imgLocation } ;

         // Insert our playerMemory into the database into the table of the same name, "playerMemory"
         dbManager.Insert(newMemory);
     }
}
