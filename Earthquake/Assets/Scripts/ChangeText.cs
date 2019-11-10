using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
    public string m_text;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponentInChildren<Text>().text = m_text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeText(string pNewText)
    {
        this.m_text = pNewText;
    }
}
