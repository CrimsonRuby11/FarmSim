using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pick4 : PickBaseState
{
    private Material deleteMaterial;

    public override void stateEnter()
    {
        base.stateEnter();

        pickText = "Remove";

        pickPf = stateManager.pick4pf;

        destroyPf();
        initiatePf();

        deleteMaterial = new Material(stateManager.transparentMaterial);
        deleteMaterial.color = new Color(255, 0, 0, 140);

        pickObject.GetComponentInChildren<Renderer>().SetMaterials(
            new List<Material>() {
                deleteMaterial,
            }
        );
    }

    public override void PlacePick(InputAction.CallbackContext ctx)
    {
        // Remove whatever is at gridPos
        Vector3Int gridPos = gridManager.mousePosOnPlaneGrid;

        gameManager.resetGrid(gridPos.x, gridPos.z);
    }
}
