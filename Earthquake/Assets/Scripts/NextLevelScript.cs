using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI

public class NextLevelScript : MonoBehaviour
{
    public Text scoretext = null
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name +1);
    }

    void quit()
    {
        Application.Quit();
    }

    void Start()
    {
        scoretext.text = Int.ToString(Scorescript.GetScore());
    }
}
