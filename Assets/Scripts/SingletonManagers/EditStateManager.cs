using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EditStateManager : MonoBehaviour
{
    private static EditStateManager _instance;

    public static EditStateManager instance{get; private set;}

    public EditBaseState currentState {get; private set;}
    public EditBaseState lastState {get; private set;}

    public EditState editState {get; private set;}
    public ViewState viewState {get; private set;}
    public CatalogState catalogState;

    private PicksController picksController;

    // Input actions
    public InputManager inputManager;
    private InputAction switchEdit;
    private InputAction openCatalog;

    [SerializeField]
    private GameObject gridObject;
    [SerializeField]
    private GameObject highlighterObject;
    [SerializeField]
    private GameObject editModeUI;
    [SerializeField]
    private GameObject editModeUIPressed;
    [SerializeField]
    public GameObject catalogPanel;
    [SerializeField]
    protected CameraController cameraController;

    void Awake() {
        if(instance == null) {
            instance = this;
        } else if(instance != this) {
            Destroy(this);
        }

        editState = new EditState();
        viewState = new ViewState();
        catalogState = new CatalogState();

        currentState = viewState;

        inputManager = new InputManager();
    }

    void Start() {
        picksController = PicksController.instance;
        currentState.stateEnter();
    }

    void Update() {
        currentState.stateUpdate();
    }

    void OnEnable() {
        switchEdit = inputManager.Keyboard.SwitchEditMode;
        switchEdit.Enable();

        openCatalog = inputManager.Keyboard.OpenCatalog;
        openCatalog.Enable();

        switchEdit.performed += SwitchState;
        openCatalog.performed += OpenCatalog;
    }

    void OnDisable() {
        switchEdit.Disable();
    }

    void SwitchState(InputAction.CallbackContext ctx) {
        if(currentState == viewState) {
            transitionState(editState);
        } else {
            transitionState(viewState);
        }
    }

    void OpenCatalog(InputAction.CallbackContext ctx) {
        if(currentState != catalogState) {
            lastState = currentState;
            transitionState(catalogState);
        } else {
            transitionState(lastState);
        }
    }

    void transitionState(EditBaseState toState) {
        currentState.stateExit();
        currentState = toState;
        toState.stateEnter();

        Debug.Log("State changed! " + toState);
    }

    public void switchGrid(bool b) {
        gridObject.SetActive(b);
    }

    public void switchHighlighter(bool b) {
        highlighterObject.SetActive(b);
    }

    public void startUIAnimation() {
        StartCoroutine(uiAnimation());
    }

    IEnumerator uiAnimation() {
        editModeUI.SetActive(false);
        editModeUIPressed.SetActive(true);

        yield return new WaitForSeconds(.1f);

        editModeUI.SetActive(true);
        editModeUIPressed.SetActive(false);
    }

}
