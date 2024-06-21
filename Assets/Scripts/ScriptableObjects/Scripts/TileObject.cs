using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New TileObject", menuName = "Tile")]
public class TileObject : ScriptableObject
{
    public string tileText;
    public GameObject[] tilePf;
    public Sprite UIImage;

    protected GridManager gridManager;
    protected GameManager gameManager;
    protected PicksController controller;
    public InputManager inputManager;

    protected Grid gridObject;
    protected GameObject pickObject;
    protected int pickIndex = 0;

    public void initiatePf() {
        pickObject = GameObject.Instantiate(tilePf[pickIndex], gridManager.mousePosOnPlane, Quaternion.identity, controller.pointerParent.transform);
        
        Debug.Log(pickObject);

        // Set transparent Material
        List<Material> l = new List<Material>
        {
            controller.transparentMaterial,
        };

        pickObject.GetComponentInChildren<Renderer>().SetMaterials(l);

        // Debug.Log("Initiated from " + pickText);
    }

    public void destroyPf() {
        GameObject.Destroy(pickObject);
    }

    public virtual void switchForm(InputAction.CallbackContext ctx) {
        pickIndex += 1;
        if(pickIndex >= tilePf.Length) {
            pickIndex = 0;
        }

        destroyPf();
        initiatePf();
    }

    public virtual void placePick(InputAction.CallbackContext ctx) {
        Vector3Int gridPos = gridManager.mousePosOnPlaneGrid;
        // Debug.Log(gridPos);
        if(!gameManager.getGrid(gridPos.x, gridPos.z)) {
            GameObject placedPick = GameObject.Instantiate(tilePf[pickIndex], pickObject.transform.position, pickObject.transform.rotation, controller.runtimeParent.transform);
            gameManager.setGrid(gridPos.x, gridPos.z, placedPick);
        } else {
            Debug.LogError("Grid Occupied!");
        }
    }

    public virtual void stateEnter() {
        gridManager = GridManager.instance;
        gameManager = GameManager.instance;
        controller = PicksController.instance;
        inputManager = controller.inputManager;
        
        gridObject = gridManager.gridObject;

        inputManager.Keyboard.SwitchPickForm.Enable();
        inputManager.Keyboard.PlacePick.Enable();
        inputManager.Keyboard.SwitchPickForm.performed += switchForm;
        inputManager.Keyboard.PlacePick.performed += placePick;

        initiatePf();
        Debug.Log(gridManager);
    }

    public virtual void stateUpdate() {
        if(EditStateManager.instance.currentState == EditStateManager.instance.editState) {
            pickObject.transform.position = gridObject.CellToWorld(gridObject.WorldToCell(gridManager.mousePosOnPlane));
        }
    }

    public virtual void stateExit() {
        destroyPf();

        inputManager.Keyboard.PlacePick.performed -= placePick;
        inputManager.Keyboard.SwitchPickForm.performed -= switchForm;

        inputManager.Keyboard.SwitchPickForm.Disable();
        inputManager.Keyboard.PlacePick.Disable();
    }
}
