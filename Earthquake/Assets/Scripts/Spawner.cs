using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Name of this spawner./\
    public string m_spawnerName;
    // The maximum number of cars allowed on the scene at the same time, from this spawner.
    public int m_maxCars = 5;
    // The current number of cars on the scene from this spawner
    public int m_curCars = 0;
    // Current car being spawned.
    public GameObject m_car;
    // The direction the car is spawned. Right means it is headed towards the right on the x axis.
    public Move.Direction m_carDirection;
    // Array of cars to choose from when spawning a car. If left empty, always the same car
    // will be spawned.
    public GameObject[] m_carArray;

    // How fast the cars are spawned. This is measured in frames.
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

        // Spawn a car only at a certain interval of time This time is in frames.
        if(m_spawnTick >= m_spawnRate)
        {
            // Check if we already have the maximum amount of cars in the game
            if(m_curCars < m_maxCars)
            {               
                GameObject newCar;

                if(m_carArray != null && m_carArray.Length > 0)
                {
                    // We have an array of cars, so we spawn one of these at random.
                    System.Random r = new System.Random();

                    int randomIndex = r.Next(0, m_carArray.Length);

                    m_car = m_carArray[randomIndex];
                }

                // Spawn a car

                m_car.GetComponent<Move>().m_currentDirection = m_carDirection;
                newCar = Instantiate(m_car);

                // We place the car at the center of the spawner.
                var position = this.transform.position;
                newCar.transform.position = new Vector3(position.x, position.y);

                // Depending on what direction we want the car to go, we rotate it.
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

                // Attaching the event handler so we know when the car was destroyed.
                newCar.GetComponent<SpawnerReference>().m_spawnerName = this.m_spawnerName;
                newCar.GetComponent<SpawnerReference>().CarDestroyed += this.OnCarDestroyed;

                // Let the iteration keep going.
                m_spawnTick = 0;
                m_curCars++;
            }
        }
        else
        {

        }
    }

    private void OnCarDestroyed(object sender, EventArgs e)
    {
        // When one of our spawner's cars is destroyed, we keep track of how many cars are left on
        // the scene that comes from our spawner.
        this.m_curCars--;
    }
}
