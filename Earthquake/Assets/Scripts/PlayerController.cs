using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    // TODO (Leandro):
        // Find way to map normal tiles to destroyed tiles
        // Do cues for sound and effects
        // Count points
        // Leave gaps for the powerups

    // Configuration Parameters
    [SerializeField]
    private float _crackDistance = 10.0f; // Total distance covered by the crack

    // Reference Variables
    [SerializeField]
    private Tilemap[] _tilemaps;
    [SerializeField]
    private Tile[] _notDestroyed;
    [SerializeField]
    private Tile[] _destroyedTiles;

    private LineRenderer _line; // Stores reference to line renderer component
 
    // Local Variables
    private bool _didFirstClick = false; // To have control if the first click was done
    private bool _didSecondClick = false; // To have control if the second click was done
    private Vector3 _firstClickPosition = Vector3.zero; // The world position where the first click was done
    private Vector3 _secondClickPosition = Vector3.zero; // The world position of the second click

    // Start is called before the first frame update
    void Start()
    {
        // Gets reference to Line Renderer component and hiddes it from the screen for good measure
        _line = GetComponent<LineRenderer>();
        _line.enabled = false;
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
            else
            {
                // Stores the second clicks's position
                _secondClickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _secondClickPosition.z = 0;
                _didSecondClick = true;
                
                // TODO(Leandro): Do crack and stuff

                /* This section is the manual collision with the tiles */

                /*
                    First its declared a HashSet so its guaranteed that there won't have repeated tile entries
                    Then it gets the line that is drawn and gets its magnitude and direction
                */
                HashSet<Vector3Int> tiles = new HashSet<Vector3Int>();
                Vector3 direction = (_secondClickPosition - _firstClickPosition);
                float magnitude = direction.magnitude;
                direction.Normalize();
                
                
                // After that it loops through the line from .25 units and populates the HashSet with the tiles
                for(float dist = 0; dist < magnitude; dist += .25f)
                {
                    Vector3Int res = _tilemaps[0].WorldToCell(_firstClickPosition + direction * dist);
                    tiles.Add(res);
                }
                // Ending the loop, we do one last check with the last position to get a possible last tile
                Vector3Int finaltile = _tilemaps[0].WorldToCell(_secondClickPosition);
                tiles.Add(finaltile);

                //Debug.Log(tiles.Count);


                // Then there is a loop through the hash, the tiles in it are swithed to their destroyed version
                foreach(Vector3Int tileCoord in tiles)
                {
                    // Find tile in the array
                    //Debug.Log("Hitting tile " + _tilemap.GetTile(tileCoord).name);
                    Tile tile = _tilemaps[0].GetTile(tileCoord) as Tile;
                    int index = Array.IndexOf(_notDestroyed, tile);
                    Tile destroyedTile = _destroyedTiles[index];
                    
                    // Change to destroyed tile
                    _tilemaps[0].SetTile(tileCoord, destroyedTile);
                    // Count points ?
                }
            }
        }
    }
}
