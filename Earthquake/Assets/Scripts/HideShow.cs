using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShow : MonoBehaviour
{
    private bool m_isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleShow()
    {
        m_isActive = !m_isActive;

        this.gameObject.SetActive(m_isActive);
    }
}
