using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditBaseState : IState
{
    protected EditStateManager stateManager;

    public EditBaseState() {
        stateManager = EditStateManager.instance;
    }

    public virtual void stateEnter()
    {
        // Debug.Log("State Entered!" + this);
    }

    public virtual void stateExit()
    {
        // throw new System.NotImplementedException();
    }

    public virtual void stateUpdate()
    {
        // throw new System.NotImplementedException();
    }
}
