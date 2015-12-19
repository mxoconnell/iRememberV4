//This script is used in the Main Menu to randomly generate pictures and display them on the screen
//Credit: forum.unity3d.com/threads/resources-subfolder.36918
//Note: in all unity paths the backslash is represented by a forwardslash (clash must point northeast)

using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;//for directoryInfo & FileInfo
using System.Collections.Generic;//for List

public class memoryMachine { 
    //Here we determine if we want a male or female and then return a memory of that type
    //if nameDatabase is null, then a name is not created.
    public playerMemory getRandomGenderMemory(SimpleSQL.SimpleSQLManager nameDatabase)
    {
        bool isMale = true;
        //50% Chance memory is female
        if (UnityEngine.Random.value >= 0.5)
        {
            isMale = false;//switch gender to female
        }

        string displayTxt = "";
        //if this field was left null, then we client doesn't want a name
        if (nameDatabase != null){
            displayTxt = getDisplayText(isMale, nameDatabase);
        }
         
        string imgLocation = getDisplayImageName(isMale);
        
        playerMemory newMemory = new playerMemory { displayText = displayTxt, imageLocation = imgLocation };
        return newMemory;
    }

    //returns the text to display of a memory
    private string getDisplayText(bool isMale, SimpleSQL.SimpleSQLManager nameDatabase){
        // Compile a list of all names
        List<nameOptions> memoriesOnFile = nameDatabase.Query<nameOptions>(
                                                        "SELECT " +
                                                            "W.femaleName, " +
                                                            "W.maleName " +
                                                        "FROM " +
                                                            "names W "
                                                        );
        nameOptions nameOptionsToDisplay = memoriesOnFile[UnityEngine.Random.Range(0, memoriesOnFile.Count)];

        if (isMale)
            return nameOptionsToDisplay.maleName;
        else
            return nameOptionsToDisplay.femaleName;
    }

    //returns an image to display. The image is of a randomly chosen file from the male or femail pictures directory".
    private string getDisplayImageName(bool isMale){
        string startDelimiter = "";//this will tell me when to chop of the begining of the file path
        string randomImageName ="";
        DirectoryInfo dir;//determine which directory to use

        if (isMale)
        {
            dir = new DirectoryInfo("Assets/Resources/Male Pictures");
            startDelimiter = "Male Pictures";
        }
        else
        {
            dir = new DirectoryInfo("Assets/Resources/Female Pictures");
            startDelimiter = "Female Pictures";
        }

        FileInfo[] pictures = dir.GetFiles("*.*");//create a random index based on size of directory
        int randomIndex = UnityEngine.Random.Range(0, pictures.Length - 1);
        //Debug.Log("IMG Found:" + pictures[randomIndex]);
       

        randomImageName = pictures[randomIndex].ToString(); // this will look something like this: "Assets\Resources\Male Pictures\file8341308807137.jpg.meta"

        //ocassionally file will have a .meta on the end of their file name. So instead of chopping the last 4 characters off, we look for an end delimeter of either ".jpg" or ".JPG"
        string endDelimiter = "";
        if(randomImageName.IndexOf(".jpg") != -1)
        {
            endDelimiter = ".jpg";
        }
        else if (randomImageName.IndexOf(".JPG") != -1)
        {
            endDelimiter = ".JPG";
        }
        else
        {
            throw new System.ArgumentException("Invalid image file name: [" + randomImageName + "] Does not include .jpg or .JPG");
        }

        //Chop off begining of path: 
        //move the begining of the path from the file name so that we get "Male Pictures\file8341308807137.jpg.meta"
        randomImageName = randomImageName.Substring(randomImageName.IndexOf(startDelimiter));

        //Chop off the end of path:
        int nameLength = randomImageName.LastIndexOf(endDelimiter);

        randomImageName = randomImageName.Substring(0, nameLength);//we trim it to  "file8341308807137"

        randomImageName = randomImageName.Replace("\\","/");//replace all backslashes with forward slashes
        return randomImageName;
    }

    //returns a randmemory previously made by the user for ze to recall. Will return null if there are no memories.
    public playerMemory getSavedMemory(SimpleSQL.SimpleSQLManager savedMemories){
        // Compile a list of memories previously created by the user
        List<playerMemory> memoriesOnFile = savedMemories.Query<playerMemory>(
                                                        "SELECT " +
                                                            "W.displayText, " +
                                                            "W.imageLocation " +
                                                        "FROM " +
                                                            "playerMemory W "
                                                        );
        if(memoriesOnFile.Count <= 0){
            //no memories present
            return null;
        }

        playerMemory memoryToRemember = memoriesOnFile[UnityEngine.Random.Range(0, memoriesOnFile.Count)];
        return memoryToRemember;
    }
}
