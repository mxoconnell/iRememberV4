using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class generateRandomMemory : MonoBehaviour {
	public GameObject nameDisplay;
	public UnityEngine.UI.Image imageDisplay;
	public Sprite o;
	public SpriteRenderer x;
	public Sprite t;
	void Start () {
		Memory displayedMemory = GameControl.control.CreateMemory();
		nameDisplay.GetComponent<Text>().text = displayedMemory.getDescription();
		//imageDisplay = displayedMemory.getDisplayImage();
		//imageDisplay = j;//(UnityEngine.UI.Image)Resources.Load("ssets/People Pictures/Female Pictures/093.jpg");
		//imageDisplay = (Sprite)Resources.Load("ssets/People Pictures/Female Pictures/093.jpg");
		//imageDisplay.//imageDisplay.renderer.material.mainTexture = Resources.Load("_textures/T01", Texture2D);// = (Texture2D)Resources.Load("Assets/People Pictures/Female Pictures/093.jpg");
		///WWW www = new WWW("Assets/People Pictures/Female Pictures/093.jpg");
		//yield return www; 
		Sprite newSprite =  Resources.Load <Sprite>("pic.jpg");
		t = newSprite;//Sprite.Create((Texture2D)myTextures[0], new Rect(0, 0, 170, 170),new Vector2(0, 0),100.0f);
		//SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
		//Sprite sprite = new Sprite();
		x.sprite = t;//Sprite.Create(www.texture, new Rect(0, 0, 170, 170),new Vector2(0, 0),100.0f);
		imageDisplay.sprite = t;
		//x.sprite = Sprite.Create(Resources.Load("Assets/People Pictures/Female Pictures/093.jpg"), Rect(0f, 0f, 48f, 48f), new Vector2(0f, 0f), 128f); //new Sprite("Assets/People Pictures/Female Pictures/093.jpg");//(Sprite)Resources.Load("ssets/People Pictures/Female Pictures/093.jpg");
		//imageDisplay = (UnityEngine.UI.Image)Resources.Load ("Assets/People Pictures/Female Pictures/093.jpg");
	}
	
	// Update is called once per frame
	void Update () {

	}
	// save the memory being displayed and bring user to the main menu
	public void SaveMemory(){
		//SAVE MEMORY HERE TODO
		Application.LoadLevel ("mainMenu");
		
	}
}
