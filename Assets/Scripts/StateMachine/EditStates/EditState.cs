using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EditState : EditBaseState
{
    public InputManager inputManager;
    PickStateManager pickStateManager;
    
    InputAction pickObjectInput;

    public override void stateEnter()
    {
        base.stateEnter();

        pickStateManager = PickStateManager.instance;

        stateManager.switchGrid(true);
        stateManager.switchHighlighter(false);

        stateManager.startUIAnimation();

        pickStateManager.transitionState(pickStateManager.pick1);
    }

    public override void stateUpdate()
    {
        base.stateUpdate();
    }

    public override void stateExit() {
        pickStateManager.transitionState(pickStateManager.nonEditPick);
    }
}
