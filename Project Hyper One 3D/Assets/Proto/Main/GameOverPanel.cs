using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour {

    public void restart(){
        SceneManager.LoadScene(2);
    }

    public void toMenuScreen(){
        SceneManager.LoadScene(0);
    }

}
