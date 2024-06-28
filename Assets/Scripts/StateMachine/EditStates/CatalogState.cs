using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatalogState : EditBaseState
{
    public CatalogState() {
        stateManager.catalogPanel.SetActive(false);
    }

    public override void stateEnter()
    {
        base.stateEnter();

        stateManager.switchHighlighter(false);
        stateManager.catalogPanel.SetActive(true);
        fakeObjectController.instance.setMove(false);
    }

    public override void stateExit()
    {
        base.stateExit();

        stateManager.switchHighlighter(true);
        stateManager.catalogPanel.SetActive(false);
        fakeObjectController.instance.setMove(true);
    }

    public override void stateUpdate()
    {
        base.stateUpdate();
    }
}
