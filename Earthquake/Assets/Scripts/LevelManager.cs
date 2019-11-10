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
        SceneManager.LoadScene(m_levelNumber + 1);
        LevelCounter.m_levelCounter++;
    }
    public void LoadScene(int pLevel)
    {
        SceneManager.LoadScene(pLevel);
        LevelCounter.m_levelCounter = pLevel;
    }
}
