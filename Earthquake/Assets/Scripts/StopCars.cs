using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCars : MonoBehaviour
{
    public GameObject[] m_carSpawners;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Stop()
    {
        foreach (GameObject spawner in m_carSpawners)
        {
            spawner.GetComponent<Spawner>().Stop();
        }
    }
}
