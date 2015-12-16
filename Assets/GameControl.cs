using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;


//controls player data and data storage
public class GameControl : MonoBehaviour {

	public static GameControl control;

	public List<Memory> myMemories = new List<Memory>();

	void Awake (){
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		} 
		else if (control != this) {
			Destroy(gameObject);
		}
	}

	public Memory CreateMemory(){
		return (new Memory());
	}

	public void AddMemory(){
		myMemories.Add(new Memory ());
	}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
		
		PlayerData data = new PlayerData ();
		data.playerMemories = myMemories;
		
		bf.Serialize (file, data);
		file.Close ();
	}
	
	public void Load(){
		
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();
			
			myMemories = data.playerMemories;
		}
	}
}

[Serializable]
class PlayerData{
	
	public List<Memory> playerMemories;
}

//Designed to be a person to remember or any object
public class Memory{
	bool isMale;
	int indexOfImage; //when algebraically sorted (within the directory of the appropriate gender)
	string descriptionToRemember; 
	int numberOfHoursElapsed;
	
	public Memory()//default constructor
	{
		isMale = true;
		//50% Chance memory is female
		if(UnityEngine.Random.value >= 0.5){
			isMale = false;//switch gender to female
		}
		indexOfImage = generateRandomImageIndex(isMale);
		descriptionToRemember = generateRandomDescription(isMale);
		numberOfHoursElapsed = 0;
		
	}

	//constructor is data is specified
	public Memory(bool ifMale, int index, string desscription, int timeElapsed)
	{
		isMale = ifMale;
		indexOfImage = index;
		descriptionToRemember = desscription; 
		numberOfHoursElapsed = timeElapsed;
	}

	//create a random image index and ensure it is within the total number of pictures
	//available for a given gender.
	int generateRandomImageIndex(bool isMale){
		DirectoryInfo dir;//determine which directory to use
		if (isMale) {
			dir = new DirectoryInfo ("Assets/Resources/People Pictures/Male Pictures");
		}
		else {
			dir = new DirectoryInfo ("Assets/Resources/People Pictures/Female Pictures");
		}

		FileInfo[] pictures = dir.GetFiles("*.*");//create a random index based on size of directory
		int randomIndex = UnityEngine.Random.Range(0,pictures.Length-1);
		return randomIndex;
	}

	//creates a random name that corresponds with the correct gender
	string generateRandomDescription(bool isMale){
		string randomName = "";
		string[] names;

		if (isMale) {
			names = System.IO.File.ReadAllLines("Assets/Names/MaleNames.txt");
			randomName = names[UnityEngine.Random.Range(0,names.Length-1)];
		} 
		else {
			names = System.IO.File.ReadAllLines("Assets/Names/FemaleNames.txt");
			randomName = names[UnityEngine.Random.Range(0,names.Length-1)];
		}
		return randomName;
	}

	//returns an image to display of the memory
	public Texture2D getDisplayImage(){
		DirectoryInfo dir;//determine which directory to use
		if (isMale) {
			dir = new DirectoryInfo ("Assets/Resources/People Pictures/Male Pictures");
		}
		else {
			dir = new DirectoryInfo ("Assets/Resources/People Pictures/Female Pictures");
		}
		//var image = new Texture2D(2, 2);//2's will be replaced in next line
		//image.LoadImage(dir.GetFiles().GetValue(indexOfImage));

		//var images = dir.GetFiles();
		//image.LoadImage (files [2]);
//		var myTexture = Resources.Load(images[2]);
		var myTexture = (Texture2D)Resources.Load("Female Pictures/093.jpg");
		return myTexture;

	}

	public int getImageIndex(){//these may not need to be public
		return indexOfImage;
	}

	public bool getIsMale(){//these may not need to be public
		return isMale;
	}

	public string getDescription(){
		return descriptionToRemember;
	}

	public int getHoursElapsed(){
		return numberOfHoursElapsed;
	}
}














