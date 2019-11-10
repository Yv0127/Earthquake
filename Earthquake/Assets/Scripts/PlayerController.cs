using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Configuration Parameters
    [SerializeField]
    private float _crackDistance = 10.0f; // Total distance covered by the crack

    // Reference Variables
    private LineRenderer _line; // Stores reference to line renderer component
 
    // Local Variables
    private bool _didFirstClick = false; // To have control if the first click was done
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
        if(_didFirstClick)
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
                // After second click, leave the line there for now
                // Handle second click
            }
        }
    }
}
