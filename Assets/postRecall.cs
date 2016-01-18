using UnityEngine;
using System.Collections;

public class postRecall : MonoBehaviour {
    public UnityEngine.UI.RawImage imgDisplay;//shows image of previously guessed memory
    public UnityEngine.UI.Text txtRecallEvaluation; //displays: if user recall was correct
    public UnityEngine.UI.Text txtMemoryDisplayText; //displays: "This memory was titled: " + memory.displayText

    // Use this for initialization
    void Start () {
        imgDisplay.texture = Resources.Load<Texture>(PlayerPrefs.GetString("memoryImageLocation"));
        txtMemoryDisplayText.text = "This memory was titled: " + PlayerPrefs.GetString("memoryDisplayText");

        //Determine if user recall was correct
        if (PlayerPrefs.GetInt("wasUserCorrect")==1){
            txtRecallEvaluation.text = "Correct!";
            txtRecallEvaluation.color = Color.green;
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + PlayerPrefs.GetInt("elapsedMinutes"));
            Debug.Log("Score is: " + PlayerPrefs.GetInt("Score"));
        }
        else{
            txtRecallEvaluation.text = "False!";
            txtRecallEvaluation.color = Color.red;
        }


    }
}
