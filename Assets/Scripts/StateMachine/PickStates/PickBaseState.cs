using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickBaseState : IState
{
    protected string pickText;

    protected GameObject pickPf;
    protected GameObject pickObject;
    protected PickStateManager stateManager;
    protected GridManager gridManager;

    private Grid gridObject;

    public virtual void switchForm(InputAction.CallbackContext ctx) {}

    public void PlacePick(InputAction.CallbackContext ctx) {
        GameObject placedPick = GameObject.Instantiate(pickPf, pickObject.transform.position, pickObject.transform.rotation, stateManager.runtimeParent.transform);
    }

    public virtual void stateEnter()
    {
        // Debug.Log("PickStateChanged!");
        stateManager = PickStateManager.instance;
        gridManager = GridManager.instance;
        
        gridObject = gridManager.gridObject;

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

        Debug.Log("Initiated from " + pickText);
    }

    protected void destroyPf() {
        GameObject.Destroy(pickObject);
    }

}
