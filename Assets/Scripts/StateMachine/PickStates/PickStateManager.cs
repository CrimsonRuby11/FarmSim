using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickStateManager : MonoBehaviour
{
    private static PickStateManager _instance;

    public static PickStateManager instance{get; private set;}

    public Pick1 pick1{get; private set;}
    Pick2 pick2;
    Pick3 pick3;
    Pick4 pick4;
    public NonEditPick nonEditPick {get; private set;}

    public PickBaseState currentState {get; private set;}

    [SerializeField]
    public GameObject[] pick1pf;
    [SerializeField]
    public GameObject[] pick2pf;
    [SerializeField]
    public GameObject pick3pf;
    [SerializeField]
    public GameObject pick4pf;

    [SerializeField]
    GameObject tileParent;
    [SerializeField]
    public Material transparentMaterial;
    [SerializeField]
    public GameObject pointerParent;
    [SerializeField]
    public GameObject runtimeParent;
    

    public InputManager inputManager;
    InputAction pickObjectInput;
    EditStateManager editStateManager;

    public GridManager gridManager {get; private set;}

    void Awake() {
        if(instance == null) {
            instance = this;
        } else if(instance != this) {
            Destroy(this);
        }

        pick1 = new Pick1();
        pick2 = new Pick2();
        pick3 = new Pick3();
        pick4 = new Pick4();
        nonEditPick = new NonEditPick();

        currentState = nonEditPick;

        inputManager = new InputManager();
    }

    void Start() {
        currentState.stateEnter();

        editStateManager = EditStateManager.instance;
        gridManager = GridManager.instance;
    }

    void OnEnable() {
        pickObjectInput = inputManager.Keyboard.PickObject;
        pickObjectInput.Enable();

        pickObjectInput.performed += ctx => pickInput(ctx);
    }

    void OnDisable() {
        pickObjectInput.Disable();
    }

    void Update() {
        currentState.stateUpdate();
    }

    public void transitionState(PickBaseState toState) {
        if(toState != currentState) {
            Debug.Log("Pick state changed! " + currentState.getPickText());
            currentState.stateExit();
            currentState = toState;
            currentState.stateEnter();
        }
    }

    void pickInput(InputAction.CallbackContext ctx) {
        // Debug.Log(EditStateManager.instance);
        if(editStateManager.currentState == editStateManager.editState) {
            transitionState(getState(pickObjectInput.ReadValue<float>()));
        }
    }

    PickBaseState getState(float n) {
        switch(n) {
            case 1f:
            return pick1;
            case 2f:
            return pick2;
            case 3f:
            return pick3;
            case 4f:
            return pick4;
            default:
            return nonEditPick;
        }
    }


}
