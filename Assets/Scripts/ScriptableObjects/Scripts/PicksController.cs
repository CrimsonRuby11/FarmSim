using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PicksController : MonoBehaviour
{
    public static PicksController instance;

    GameManager gameManager;

    public TileObject currentTile {get; private set;}

    public TileObject floor;
    public TileObject wall;
    public TileObject tree;
    public TileObject delete;
    public TileObject bush;
    public TileObject nullTile;

    public GameObject[] hotbarObjects;
    public InputManager inputManager;

    [SerializeField]
    public Material transparentMaterial;
    [SerializeField]
    public GameObject pointerParent;
    [SerializeField]
    public GameObject runtimeParent;

    void OnEnable() {
        inputManager.Keyboard.PickObject.performed += switchPick;
        inputManager.Keyboard.PickObject.Enable();
    }

    void OnDisable() {
        inputManager.Keyboard.PickObject.performed -= switchPick;
        inputManager.Keyboard.PickObject.Disable();
    }

    public void switchPick(InputAction.CallbackContext ctx) {
        if(EditStateManager.instance.currentState == EditStateManager.instance.editState) {
            Debug.Log(inputManager.Keyboard.PickObject.ReadValue<float>());
            transitionState(getTile(inputManager.Keyboard.PickObject.ReadValue<float>()));
        }
    }

    private TileObject getTile(float t) {
        return gameManager.tileHotbar[(int)t-1];
    }

    void Awake() {
        if(instance == null) {
            instance = this;
        } else if(instance != this) {
            Destroy(gameObject);
        }

        inputManager = new InputManager();
        currentTile = nullTile;
    }

    void Start() {
        gameManager = GameManager.instance;

        initializeHotbar();

        currentTile.stateEnter();        
    }

    void Update() {
        // Debug.Log(currentTile);
        currentTile.stateUpdate();
    }

    public void transitionState(TileObject toTile) {
        currentTile.stateExit();
        currentTile = toTile;
        toTile.stateEnter();

        Debug.Log("Switched tile");
    }

    void initializeHotbar() {
        gameManager.tileHotbar[0] = floor;
        gameManager.tileHotbar[1] = wall;
        gameManager.tileHotbar[2] = tree;
        gameManager.tileHotbar[3] = bush;

        initializeUI();
    }

    void initializeUI() {
        for(int i = 0; i < 4; i++) {
            setHotbarImage(i);
        }
    }

    public void setHotbarImage(int i) {
        hotbarObjects[i].GetComponent<HotbarElement>().setImage(i);
    }
}
