using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
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
            List<PlayerScore> scoreList = new List<PlayerScore>();
            using (StreamReader r = new StreamReader(path))
            {
                while (!r.EndOfStream)
                {
                    string line = r.ReadLine();

                    string[] decoded = line.Split('|');
                    string name = decoded[0];
                    int score = int.Parse(decoded[1]);
                    PlayerScore pscore = new PlayerScore();
                    pscore.Name = name;
                    pscore.Score = score;

                    scoreList.Add(pscore);
                };
            };


            scoreList.Sort(new PlayerScoreComparer());

            int count = 0;

            foreach (PlayerScore playerScore in scoreList)
            {
                GameObject newObj; // Create GameObject instance

                // Create new instances of our prefab until we've created as many as we specified
                newObj = (GameObject)Instantiate(m_txtPlayerAndScorePrefab, transform);

                newObj.transform.SetParent(this.transform, false);

                float yPos = count * newObj.GetComponent<RectTransform>().sizeDelta.y;

                newObj.transform.localPosition = new Vector3(newObj.transform.localPosition.x, yPos, 0);

                newObj.GetComponent<TextMeshProUGUI>().text = playerScore.Name;
                newObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = playerScore.Score.ToString();

                count++;
            }

            float size = 0f;

            foreach (Transform transform in this.transform)
            {
                size += transform.GetComponent<RectTransform>().sizeDelta.y;
            }

            Debug.Log(size);

            this.GetComponent<RectTransform>().sizeDelta = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x, size);

        }

    }
}