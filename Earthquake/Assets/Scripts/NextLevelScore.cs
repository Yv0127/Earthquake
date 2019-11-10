using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelScore : MonoBehaviour
{
    public Text scoretext = null;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Nextlevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name + 1);
    }

    public void quit()
    {
        Application.Quit();
    }
    void Start()
    {
        scoretext.text = Scorescript.Instance.GetScore().ToString();
    }


}
