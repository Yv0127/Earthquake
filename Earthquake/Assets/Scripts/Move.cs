using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float m_speed = 0.25f;
    public bool m_moving = true;
    public Direction m_currentDirection = Direction.Right;

    public enum Direction { Left, Up, Right, Down };


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_moving)
        {
            switch (m_currentDirection)
            {
                case Direction.Right:
                    transform.position = new Vector3(transform.position.x + m_speed, transform.position.y);
                    break;

                case Direction.Left:
                    transform.position = new Vector3(transform.position.x - m_speed, transform.position.y);
                    break;

                case Direction.Up:
                    transform.position = new Vector3(transform.position.x, transform.position.y + m_speed);
                    break;

                case Direction.Down:
                    transform.position = new Vector3(transform.position.x, transform.position.y - m_speed);
                    break; ;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "DestructionBarrier")
        {
            Destroy(this.gameObject, 0.3f);
        }
        if(collider.tag == "TurnRight")
        {
            switch (m_currentDirection)
            {
                case Direction.Right:
                    this.transform.Rotate(Vector3.forward, -90);
                    m_currentDirection = Direction.Down;
                    break;

                case Direction.Left:
                    this.transform.Rotate(Vector3.forward, -90);
                    m_currentDirection = Direction.Up;
                    break;

                case Direction.Up:
                    this.transform.Rotate(Vector3.forward, -90);
                    m_currentDirection = Direction.Right;
                    break;

                case Direction.Down:
                    this.transform.Rotate(Vector3.forward, -90);
                    m_currentDirection = Direction.Left;
                    break; ;
            }
        }

        if (collider.tag == "TurnLeft")
        {
            switch (m_currentDirection)
            {
                case Direction.Right:
                    this.transform.Rotate(Vector3.forward, 90);
                    m_currentDirection = Direction.Up;
                    break;

                case Direction.Left:
                    this.transform.Rotate(Vector3.forward, 90);
                    m_currentDirection = Direction.Down;
                    break;

                case Direction.Up:
                    this.transform.Rotate(Vector3.forward, 90);
                    m_currentDirection = Direction.Left;
                    break;

                case Direction.Down:
                    this.transform.Rotate(Vector3.forward, 90);
                    m_currentDirection = Direction.Right;
                    break; ;
            }
        }
    }
}
