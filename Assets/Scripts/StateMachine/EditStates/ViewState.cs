using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewState : EditBaseState
{
    public override void stateEnter()
    {
        base.stateEnter();

        stateManager.switchHighlighter(true);
        stateManager.switchGrid(false);

        stateManager.startUIAnimation();
    }
}
