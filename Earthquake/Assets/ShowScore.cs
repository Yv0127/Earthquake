using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<TextMeshProUGUI>().text = "Your score: " +LevelCounter.m_score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
