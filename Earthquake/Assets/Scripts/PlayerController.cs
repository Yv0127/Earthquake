using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    // TODO (Leandro):
        // Implement score count
        // Implement maximum crack range

    // Configuration Parameters
    //[SerializeField]
    //private float _crackDistance = 10.0f; // Total distance covered by the crack

    // Reference Variables
    [SerializeField]
    private Tilemap[] _tilemaps = null;
    [SerializeField]
    private Tile[] _notDestroyed = null;
    [SerializeField]
    private Tile[] _destroyedTiles = null;
    private LineRenderer _line; // Stores reference to line renderer component

    public GameObject m_levelManager;

    public int m_counter = 0;
    public bool m_finished = false;

    // Local Variables
    private enum TileLayers
    {
        Ground = 0,
        Street = 1,
        Building = 2
    }
    private bool _didFirstClick = false; // To have control if the first click was done
    private bool _didSecondClick = false; // To have control if the second click was done
    private Vector3 _firstClickPosition = Vector3.zero; // The world position where the first click was done
    private Vector3 _secondClickPosition = Vector3.zero; // The world position of the second click

    // Hashset vector used for the tile fetching on the second mouse click (see CheckForClick method)
    private HashSet<Vector3Int>[] _hitTiles = new HashSet<Vector3Int>[3];

    // Start is called before the first frame update
    void Start()
    {
        // Gets reference to Line Renderer component and hiddes it from the screen for good measure
        _line = GetComponent<LineRenderer>();
        _line.enabled = false;

        // Initialization of hashes for the tile fetching on second mouse click
        _hitTiles[(int)TileLayers.Ground] = new HashSet<Vector3Int>();
        _hitTiles[(int)TileLayers.Street] = new HashSet<Vector3Int>();
        _hitTiles[(int)TileLayers.Building] = new HashSet<Vector3Int>();
    }

    // Update is called once per frame
    void Update()
    {
        // Draw line if there was a first click
        if(_didFirstClick && !_didSecondClick)
        {
            // Set end point of line to mouse position while second click isn't done
            Vector3 currentMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentMouse.z = 0;
            
            //Debug.Log("Drawing line to mouse position..." + _firstClickPosition + " / " + currentMouse);
            _line.SetPosition(1, currentMouse);
            
            // Enables line renderer
            if(!_line.enabled)
                _line.enabled = true;
        }

        // Check for left click
        CheckForClick();

        if(m_finished)
        {
            m_counter++;
            if(m_counter >= 100)
            {
                LevelCounter.m_score += Scorescript.Instance.GetScore();
                m_levelManager.GetComponent<LevelManager>().LoadNextScene();
            }
        }

    }

    // Method responsible for checking for left mouse button clicks
    private void CheckForClick()
    {
        // If left mouse button is pressed on this frame
        if(Input.GetMouseButtonDown(0))
        {
            // Checks if it is the first click
            if(!_didFirstClick)
            {
                /*
                    Store mouse position and sets first click as done
                    Get mouse position on screen space and convert to world position
                    Sets Z to 0 otherwise it will be -10 and won't show on Game view
                    Set it as the line origin (position 0) on the Line Renderer
                    Set that the first click is done
                */
                _firstClickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _firstClickPosition.z = 0;
                _line.SetPosition(0, _firstClickPosition);
                _didFirstClick = true;
            }
            else if(!_didSecondClick)
            {
                // Stores the second clicks's position
                _secondClickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _secondClickPosition.z = 0;
                _didSecondClick = true;
                
                // TODO(Leandro): Do crack and stuff

                /* This section is the manual collision with the tiles */
                // First we get the line that was formed with the mouse clicks, its magnitude and its direction
                Vector3 direction = (_secondClickPosition - _firstClickPosition);
                float magnitude = direction.magnitude;
                direction.Normalize();
                
                
                // After that it loops through the line from .25 units and populates the HashSets with the tiles
                for(float dist = 0; dist < magnitude; dist += .25f)
                {
                    CheckTileAndAdd(_tilemaps[(int)TileLayers.Ground].WorldToCell(_firstClickPosition + direction * dist), (int)TileLayers.Ground);
                    CheckTileAndAdd(_tilemaps[(int)TileLayers.Street].WorldToCell(_firstClickPosition + direction * dist), (int)TileLayers.Street);
                    CheckTileAndAdd(_tilemaps[(int)TileLayers.Building].WorldToCell(_firstClickPosition + direction * dist), (int)TileLayers.Building);
                }
                // Ending the loop, we do one last check with the last position to get a possible last tile
                CheckTileAndAdd(_tilemaps[(int)TileLayers.Ground].WorldToCell(_secondClickPosition), (int)TileLayers.Ground);
                CheckTileAndAdd(_tilemaps[(int)TileLayers.Street].WorldToCell(_secondClickPosition), (int)TileLayers.Street);
                CheckTileAndAdd(_tilemaps[(int)TileLayers.Building].WorldToCell(_secondClickPosition), (int)TileLayers.Building);

                Debug.Log("Ground: " + _hitTiles[(int)TileLayers.Ground].Count + "/ Street: " + _hitTiles[(int)TileLayers.Street].Count
                     + "/ Building: " + _hitTiles[(int)TileLayers.Building].Count);

                // Then there are loops through the hashsets, the tiles in it are swithed to their destroyed version
                if(_hitTiles[(int)TileLayers.Ground].Count > 0)
                {
                    foreach(Vector3Int tileCoord in _hitTiles[(int)TileLayers.Ground])
                    {
                        SwitchToDestroyed(tileCoord, (int)TileLayers.Ground);
                    }
                }

                if(_hitTiles[(int)TileLayers.Street].Count > 0)
                {
                    foreach(Vector3Int tileCoord in _hitTiles[(int)TileLayers.Street])
                    {
                        SwitchToDestroyed(tileCoord, (int)TileLayers.Street);
                    }
                }

                if(_hitTiles[(int)TileLayers.Building].Count > 0)
                {
                    foreach(Vector3Int tileCoord in _hitTiles[(int)TileLayers.Building])
                    {
                        SwitchToDestroyed(tileCoord, (int)TileLayers.Building);
                    }
                }

                // Disable line
                _line.enabled = false;

                // Play crack sound
                m_finished = true;

                // Stop all cars
                if (StopCars.Instance != null)
                    StopCars.Instance.Stop();
               

            }
        }
    }

    IEnumerator Wait()
    {
        //print(Time.time);
        yield return new WaitForSeconds(5);
        //print(Time.time);
    }

    private void CheckTileAndAdd(Vector3Int result, int tileLayer)
    {
        if(_tilemaps[tileLayer].GetTile(result))
        {
            //Debug.Log("Tile exists in " + index);
            _hitTiles[tileLayer].Add(result);
        }
    }

    private void SwitchToDestroyed(Vector3Int tileCoordinate, int tileLayer)
    {
        // Find tile in the array
        //Debug.Log("Hitting tile " + _tilemap.GetTile(tileCoord).name);
        Tile tile = _tilemaps[tileLayer].GetTile(tileCoordinate) as Tile;
        int arrayIndex = Array.IndexOf(_notDestroyed, tile);
        Tile destroyedTile = null;
        if(arrayIndex < _destroyedTiles.Length)
            destroyedTile = _destroyedTiles[arrayIndex];
        else
            Debug.Log("ERROR: NO DESTROYED TILE!");
                        
        // Change to destroyed tile
        _tilemaps[tileLayer].SetTile(tileCoordinate, destroyedTile);
        
        // Count points
        switch(tileLayer)
        {
            case 0:
                Scorescript.Instance.AddScore(2000);
                break;
            case 1:
                Scorescript.Instance.AddScore(10000);
                break;
            case 2:
                Scorescript.Instance.AddScore(5000000);
                break;
        }
    }
}
