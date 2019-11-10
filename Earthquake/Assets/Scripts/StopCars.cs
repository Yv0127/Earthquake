using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCars : MonoBehaviour
{
    public GameObject[] m_carSpawners;
    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        i++;
        if(i >= 60)
        {
            Stop();
        }
    }

    public void Stop()
    {
        foreach (GameObject spawner in m_carSpawners)
        {
            spawner.GetComponent<Spawner>().Stop();
        }
    }
}
