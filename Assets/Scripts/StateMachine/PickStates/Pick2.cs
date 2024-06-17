using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pick2 : PickBaseState
{
    int pickForm;

    public override void switchForm(InputAction.CallbackContext ctx) {
        pickForm++;
        if(pickForm == 4) pickForm = 0;
        
        pickPf = stateManager.pick2pf[pickForm];

        destroyPf();
        initiatePf();
    }

    public override void stateEnter()
    {
        base.stateEnter();

        pickForm = 0;
        pickPf = stateManager.pick2pf[pickForm];

        initiatePf();
        pickText = "Wall";
        Debug.Log(pickObject);
    }
}
