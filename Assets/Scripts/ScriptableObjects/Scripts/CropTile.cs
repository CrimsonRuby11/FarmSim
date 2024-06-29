using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New CropTile", menuName = "Crop")]
public class CropTile : TileObject
{
    // Crop variables
    float currentState;
    float timeToGrow;

    public override void placePick(InputAction.CallbackContext ctx)
    {
        Vector3Int gridPos = gridManager.mousePosOnPlaneGrid;

        if(!gameManager.getGrid(gridPos.x, gridPos.y).g) {
            Debug.Log("Tile empty");
        } else {
            if(gameManager.getGrid(gridPos.x, gridPos.y).t.canCrop) {
                // Place crop
            } else {
                Debug.Log("Cannot place crop in this tile.");
            }
        }
    }

    public override void switchForm(InputAction.CallbackContext ctx)
    {
        
    }

    public override void stateEnter()
    {
        
    }

    public override void stateUpdate()
    {
        
    }

    public override void stateExit()
    {
        
    }
}
