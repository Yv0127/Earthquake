using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

            if (m_levelNumber + 1 == 12)
            {
                Save();
            }
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

        if(pLevel == 12)
        {
            Save();
        }

        SceneManager.LoadScene(pLevel);
        LevelCounter.m_levelCounter = pLevel;
    }

    public void StartGame()
    {
        LevelCounter.m_isInScoreScreen = false;
        LevelCounter.m_levelCounter = 1;
        LevelCounter.m_score = 0;
        LoadNextScene();
    }

    public void Save()
    {
        string path = Path.m_savePath;

        Debug.Log(path);

        FileInfo fi = new FileInfo(path);
        if (!fi.Directory.Exists)
        {
            System.IO.Directory.CreateDirectory(fi.DirectoryName);
        }

        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine("Player|" + LevelCounter.m_score.ToString());
        }
    }
}
