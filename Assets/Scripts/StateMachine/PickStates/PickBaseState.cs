using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickBaseState : IState
{
    protected string pickText;

    protected GameObject pickPf;
    protected GameObject pickObject;
    protected Material transparentMaterial;
    protected PickStateManager stateManager;
    protected GridManager gridManager;
    protected GameManager gameManager;


    private Grid gridObject;

    public virtual void switchForm(InputAction.CallbackContext ctx) {}

    public virtual void PlacePick(InputAction.CallbackContext ctx) {
        Vector3Int gridPos = gridManager.mousePosOnPlaneGrid;
        // Debug.Log(gridPos);
        if(!gameManager.getGrid(gridPos.x, gridPos.z)) {
            GameObject placedPick = GameObject.Instantiate(pickPf, pickObject.transform.position, pickObject.transform.rotation, stateManager.runtimeParent.transform);
            gameManager.setGrid(gridPos.x, gridPos.z, placedPick);
        } else {
            Debug.LogError("Grid Occupied!");
        }
    }

    public virtual void stateEnter()
    {
        // Debug.Log("PickStateChanged!");
        stateManager = PickStateManager.instance;
        gridManager = GridManager.instance;
        gameManager = GameManager.instance;
        
        gridObject = gridManager.gridObject;
        transparentMaterial = stateManager.transparentMaterial;

        stateManager.inputManager.Keyboard.SwitchPickForm.Enable();
        stateManager.inputManager.Keyboard.PlacePick.Enable();
        stateManager.inputManager.Keyboard.SwitchPickForm.performed += switchForm;
        stateManager.inputManager.Keyboard.PlacePick.performed += PlacePick;

        Debug.Log(stateManager);
        Debug.Log(gridManager);
    }

    public virtual void stateUpdate()
    {
        if(EditStateManager.instance.currentState == EditStateManager.instance.editState) {
            pickObject.transform.position = gridObject.CellToWorld(gridObject.WorldToCell(gridManager.mousePosOnPlane));
        }
    }

    public virtual void stateExit()
    {
        destroyPf();

        stateManager.inputManager.Keyboard.PlacePick.performed -= PlacePick;
        stateManager.inputManager.Keyboard.SwitchPickForm.performed -= switchForm;

        stateManager.inputManager.Keyboard.SwitchPickForm.Disable();
        stateManager.inputManager.Keyboard.PlacePick.Disable();
    }

    public string getPickText() {
        return pickText;
    }

    protected void initiatePf() {
        pickObject = GameObject.Instantiate(pickPf, gridManager.mousePosOnPlane, Quaternion.identity, stateManager.pointerParent.transform);
        
        // Set transparent Material
        List<Material> l = new List<Material>
        {
            transparentMaterial
        };
        pickObject.GetComponentInChildren<Renderer>().SetMaterials(l);

        // Debug.Log("Initiated from " + pickText);
    }

    protected void destroyPf() {
        GameObject.Destroy(pickObject);
    }

}
