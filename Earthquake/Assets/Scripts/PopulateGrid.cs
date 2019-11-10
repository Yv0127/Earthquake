using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PopulateGrid : MonoBehaviour
{
    public GameObject m_txtPlayerAndScorePrefab; // This is our prefab object that will be exposed in the inspector

    void Start()
    {
        Populate();
    }

    void Update()
    {

    }

    void Populate()
    {
        string path = Path.m_savePath;

        if (File.Exists(path))
        {
            using (StreamReader r = new StreamReader(path))
            {
                while (!r.EndOfStream)
                {
                    string line = r.ReadLine();

                    string[] decoded = line.Split('|');

                    GameObject newObj; // Create GameObject instance

                    // Create new instances of our prefab until we've created as many as we specified
                    newObj = (GameObject)Instantiate(m_txtPlayerAndScorePrefab, transform);

                    newObj.transform.SetParent(this.transform, false);

                    newObj.GetComponent<Text>().text = string.Format("{0} {1}", decoded[0], decoded[1]);
                };
            };
        }

    }
}