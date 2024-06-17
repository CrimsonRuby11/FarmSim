using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pick1 : PickBaseState
{
    int pickForm;

    public override void switchForm(InputAction.CallbackContext ctx) {
        pickForm = pickForm == 0 ? 1 : 0;
        pickPf = stateManager.pick1pf[pickForm];

        destroyPf();
        initiatePf();
    }

    public override void stateEnter()
    {
        base.stateEnter();

        pickForm = 0;

        pickPf = stateManager.pick1pf[pickForm];
        pickText = "Floor";

        destroyPf();
        initiatePf();
    }
}
