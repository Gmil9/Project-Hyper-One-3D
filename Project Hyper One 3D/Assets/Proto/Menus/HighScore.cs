using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {

    Text highscoretext;

    private void Start()
    {
        highscoretext = GetComponent<Text>();
        highscoretext.text = "High Score: " + PlayerPrefs.GetInt("bestScore", 0).ToString();
    }
}
