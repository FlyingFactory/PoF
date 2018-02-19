using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitButtonScript : MonoBehaviour {

    public ParentUnitButtonScript parentscript;
    public int PlaceholderButtonFunction = 0;

	// Use this for initialization
	void Start () {
		parentscript = gameObject.transform.parent.GetComponent<ParentUnitButtonScript>();
        ParentUnitButtonScript.listOfButtons.Add(this);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseUpAsButton() {

        /*ButtonFunction holds the id of the action that the button is currently assigned to.
        This function will be changed as the active character and targeted character are varied.
        For now only case 1 works as a placeholder for 'attack'.
        Click validity is decided in this script. Functionality will be a call to BattleManager coroutines.
        */

        switch (PlaceholderButtonFunction) { 
            case 1:
                if (BattleManager.currentTurn != 0 && BattleManager.currentTurn != -1) {
                    if (BattleManager.currentTurn != ParentUnitButtonScript.attachedReticle.GetComponentInParent<BaseUnit>().turnID) {
                        ParentUnitButtonScript.attachedReticle.GetComponentInParent<BaseUnit>().DamageHealth(50);
                        BattleManager.activeunits[BattleManager.currentTurn].delay += 1;
                        BattleManager.FinishTurn();
                    }
                    else {
                        Debug.Log("Currently this unit's turn. Health: " + ParentUnitButtonScript.attachedReticle.GetComponentInParent<BaseUnit>().health);
                    }
                }
                break;
            case 2:

                break;
        }
        if (ParentUnitButtonScript.attachedReticle != null) ParentUnitButtonScript.attachedReticle.GetComponent<ReticleScript>().Reset();
        ParentUnitButtonScript.attachedReticle = null;
        BattleManager.buttonParent.GetComponent<ParentUnitButtonScript>().CancelButtons();
    }
}
