using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager instance {get; private set;}

    public Vector3 mousePosOnPlane {get; private set;}
    public Vector3Int mousePosOnPlaneGrid {get; private set;}
    public Vector3 mousePos;

    [SerializeField]private Camera sceneCamera;
    [SerializeField]private LayerMask planeLayer;
    [SerializeField]private GameObject highlighter;
    [SerializeField]public Grid gridObject;

    void Awake() {
        if(instance == null) {
            instance = this;
        } else if(instance != this) {
            Destroy(this);
        }
    }

    void Update() {
        getMousePosOnPlane();

        // Convert grid pos to world pos for highlighter
        mousePosOnPlaneGrid = gridObject.WorldToCell(mousePosOnPlane);
        highlighter.transform.position = gridObject.CellToWorld(mousePosOnPlaneGrid);
    }

    void getMousePosOnPlane() {
        mousePos = Input.mousePosition;
        mousePos.z = sceneCamera.nearClipPlane;

        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 500, planeLayer)) {
            mousePosOnPlane = hit.point;
        }
    }
}
