using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerReference : MonoBehaviour
{
    public string m_spawnerName;

    public event EventHandler CarDestroyed;

    public void OnDestroy()
    {
        EventHandler handler = CarDestroyed;
        if(handler != null)
        {
            DestroyedCarEventArgs eArgs = new DestroyedCarEventArgs();
            eArgs.SpawnerName = m_spawnerName;
            handler(this, eArgs);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
