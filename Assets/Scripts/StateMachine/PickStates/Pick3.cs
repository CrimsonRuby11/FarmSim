using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick3 : PickBaseState
{
    public override void stateEnter()
    {
        base.stateEnter();

        pickText = "Tree";

        pickPf = stateManager.pick3pf;

        destroyPf();
        initiatePf();
    }
}
