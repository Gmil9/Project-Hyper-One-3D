using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public void play()
    {
        SceneManager.LoadScene(2);
    }

    public void controls()
    {
        SceneManager.LoadScene(1);
    }

    public void back()
    {
        SceneManager.LoadScene(0);
    }

}
