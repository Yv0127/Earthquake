using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Scorescript : MonoBehaviour
{
    static private Scorescript InstanceRef;

    static public Scorescript Instance
    {
        get
        {
            if(InstanceRef == null)
            {
                InstanceRef = FindObjectOfType<Scorescript>();
            }
            return InstanceRef;
        }
    }

    private int scorevalue = 0;
    public Text score;
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
     
        score.text = scorevalue.ToString();
        
    }

    public void AddScore(int scoreToAdd)
    {
        scorevalue += scoreToAdd;
    }

    public int GetScore()
    {
        return scorevalue;
    }
}
