using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int m_maxCars = 5;
    public int m_curCars = 0;
    public GameObject m_car;
    public Move.Direction m_carDirection;

    [SerializeField]
    private int m_spawnRate = 5;
    private int m_spawnTick = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_spawnTick++;

        if(m_spawnTick >= m_spawnRate)
        {
            // Check if we already have the maximum amount of cars in the game
            if(m_curCars < m_maxCars)
            {
                // Spawn a car
                m_car.GetComponent<Move>().m_currentDirection = m_carDirection;
                GameObject newCar = Instantiate(m_car);
                

                var position = this.transform.position;
                newCar.transform.position = new Vector3(position.x, position.y);

                switch (m_carDirection)
                {
                    case Move.Direction.Left:
                        newCar.transform.Rotate(Vector3.forward, 180);
                        break;
                    case Move.Direction.Up:
                        newCar.transform.Rotate(Vector3.forward, 90);
                        break;
                    case Move.Direction.Right:
                        // Default spawn.
                        break;
                    case Move.Direction.Down:
                        newCar.transform.Rotate(Vector3.forward, -90);
                        break;
                    default:
                        break;
                }

                m_spawnTick = 0;
            }
        }
        else
        {

        }
    }
}
