//This script is used in the Main Menu to randomly generate pictures and display them on the screen
//Credit: forum.unity3d.com/threads/resources-subfolder.36918
//Note: in all unity paths the backslash is represented by a forwardslash (clash must point northeast)

using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;//for directoryInfo & FileInfo


public class changePicture : MonoBehaviour {

     
public UnityEngine.UI.RawImage imgDisplay;
private float nextUsage, delay;
private string imgLocation;
private memoryMachine scriptToGenerateMemories;

    void Start () {
        delay = 0.5f;//half second delay
        scriptToGenerateMemories = new memoryMachine();

        //Start with random picture
        //get random picture name
        imgLocation = scriptToGenerateMemories.getRandomGenderMemory(null).imageLocation;
        //display the picture
        imgDisplay.texture = Resources.Load<Texture>(imgLocation);
    }
	
	// Update is called once per frame
	void Update () {

        if (Time.time > nextUsage)
        {
            nextUsage = Time.time + delay;
            //Replace display image
            //get random picture name
            imgLocation = scriptToGenerateMemories.getRandomGenderMemory(null).imageLocation;
            //display the picture
            imgDisplay.texture = Resources.Load<Texture>(imgLocation);
        }
    }

}
