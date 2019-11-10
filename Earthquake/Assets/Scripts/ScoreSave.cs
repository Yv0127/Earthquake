using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoreSave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

        using (StreamWriter writer = new StreamWriter(path) )
        {
            writer.WriteLine("JAJAJA|500");
        }
    }
}
