using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int m_levelNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_levelNumber = LevelCounter.m_levelCounter;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextScene()
    {
        if(LevelCounter.m_isInScoreScreen)
        {
            SceneManager.LoadScene(m_levelNumber + 1);
            LevelCounter.m_levelCounter++;
            LevelCounter.m_isInScoreScreen = false;
        }
        else
        {
            SceneManager.LoadScene(1);
            LevelCounter.m_isInScoreScreen = true;
        }
    }
    public void LoadScene(int pLevel)
    {

        if (pLevel != 1)
            LevelCounter.m_isInScoreScreen = false;
        SceneManager.LoadScene(pLevel);
        LevelCounter.m_levelCounter = pLevel;
    }

    public void StartGame()
    {
        LevelCounter.m_isInScoreScreen = false;
        LevelCounter.m_levelCounter = 1;
        LoadNextScene();
    }
}
