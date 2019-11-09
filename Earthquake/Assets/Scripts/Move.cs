using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float m_speed = 0.25f;
    public bool m_moving = true;
    public Sprite m_carSpriteYAxis;
    public Sprite m_carSpriteXAxis;
    public bool m_onXAxis = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (m_moving)
            transform.position = new Vector3(transform.position.x + m_speed, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "DestructionBarrier")
        {
            Destroy(this.gameObject, 0.3f);
        }
    }
}
