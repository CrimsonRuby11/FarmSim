using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New DeleteTileObject", menuName = "Delete")]
public class DeleteTile : TileObject 
{
    private Material deleteMaterial;

    public override void stateEnter()
    {
        gridManager = GridManager.instance;
        gameManager = GameManager.instance;
        controller = PicksController.instance;
        inputManager = controller.inputManager;
        
        gridObject = gridManager.gridObject;

        inputManager.Keyboard.SwitchPickForm.Enable();
        inputManager.Keyboard.PlacePick.Enable();
        inputManager.Keyboard.SwitchPickForm.performed += switchForm;
        inputManager.Keyboard.PlacePick.performed += placePick;

        deleteMaterial = new Material(controller.transparentMaterial)
        {
            color = new Color(255, 1, 1, 100)
        };

        initiatePf();

        pickObject.GetComponentInChildren<Renderer>().SetMaterials(new List<Material>() {
            deleteMaterial,
        });

        Debug.Log(pickObject);
    }

    public override void placePick(InputAction.CallbackContext ctx)
    {
        Vector3Int gridPos = gridManager.mousePosOnPlaneGrid;

        gameManager.resetGrid(gridPos.x, gridPos.z);
    }
}