using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonEditPick : PickBaseState
{
    public override void stateEnter()
    {
        stateManager = PickStateManager.instance;
        gridManager = GridManager.instance;
    }
}
