using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EditState : EditBaseState
{
    public InputManager inputManager;
    PicksController picksController;
    
    InputAction pickObjectInput;

    public override void stateEnter()
    {
        base.stateEnter();

        picksController = PicksController.instance;

        stateManager.switchGrid(true);
        stateManager.switchHighlighter(false);

        stateManager.startUIAnimation();

        picksController.transitionState(GameManager.instance.tileHotbar[0]);
    }

    public override void stateUpdate()
    {
        base.stateUpdate();
    }

    public override void stateExit() {
        picksController.transitionState(picksController.nullTile);
    }
}
