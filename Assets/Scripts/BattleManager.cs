using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {

    //state trackers
    public static int currentTurn; //-1 is playing animation, 0 is time transition, each unit has a turnID starting from 1
    public static GameObject currentTarget; //target of the current selected skill
    public static bool UnitButtonsClickable = false;
    public static bool ReticlesClickable = false;
    public static GameObject clickedReticle = null;
    
    //dict of active units, turnID:GameObject
    public static Dictionary<int, BaseUnit> activeunits = new Dictionary<int, BaseUnit>();
    public static int highestTurnID;
    public static List<int> queuedturns = new List<int>();

    //for GUI
    public static Dictionary<string, GameObject> activebuttons = new Dictionary<string, GameObject>();

    //references to objects
    public static GameObject buttonParent;

	// Use this for initialization
	void Start () {
        buttonParent = GameObject.Find("UnitClickUIParent");
        buttonParent.SetActive(false);
        currentTurn = 0;
        LoadPlayerUnits();
        LoadBattle();
        //create background

        //temp
        ReticlesClickable = true;


        //randomise starting delays:
        foreach (KeyValuePair<int, BaseUnit> unit in activeunits) {
        }
        //apply start-of-battle effects
    }

    // Update is called once per frame
    void Update () {
		if(currentTurn == 0) {
            foreach (KeyValuePair<int, BaseUnit> unit in activeunits) {
                unit.Value.delay -= Time.deltaTime * Constants.DELAY_TICK_RATE;
                if (unit.Value.delay <= 0) {
                    queuedturns.Add(unit.Key);
                }
            }
            FinishTurn();
        }
    }

    public void LoadPlayerUnits() {
        //read player save files and implement
        //instantiate player units from save
        //fill activeunits dict
    }

    public void LoadBattle(/*file parameter*/) {
        //read each line and implement
        //instantiate enemy units from reference file
        //fill activeunits dict
        //create background
    }

    // These functions to be called by buttons
    public void UseSkill(GameObject user, GameObject target, Constants.MoveID move) {
        //play animation on user
        //check buffs on user
        //check for and trigger reaction skills
        //apply effects on target
    }

    // Helper functions
    public static void DistributePositions(Constants.RowID row) {
        //detects all units in a row and evens out the positions
    }

    public static void FinishTurn() {
        if (currentTurn == -1) { currentTurn = 0; }
        else if (queuedturns.Count > 0) {
            currentTurn = queuedturns[0];
            Debug.Log("Turn changed to: " + currentTurn);
            queuedturns.RemoveAt(0);
        }
        else { currentTurn = 0; }
    }

    /* NOT IN USE: Button responses:
     1. Add BattleManager to an object (main camera works)
     2. Add Onclick event to each button
     3. Add Main Camera to each 'object' field on each button's Onclick
     4. Select the function corresponding to the button
     5. Use wrappers for static functions
        */
    public static void AttackButtonPressed() {
        activeunits[currentTurn].GetComponent<BaseUnit>().DashTo(currentTarget);
    } public void AttackButtonPressedWrapper() { AttackButtonPressed(); }

    public static void SkillButtonPressed() {

    }
    public static void ItemButtonPressed() {

    }
    public static void MoveButtonPressed() {

    }
    public static void ChargeButtonPressed() {

    }
    public static void DelayButtonPressed() {

    }

    // NOT IN USE: GUI functions
    public static void PopulateBoxes() {
        // Clears and repopulates the skill and item boxes based on activeunits[currentturn]
    }
    public static void ShowCommonButtons() {
        // TBD: Show buttons (already on screen but hidden)
    }
    public static void ShowSkillBox() {
        // TBD
    }
    public static void ShowItemBox() {
        // TBD
    }
    public static void HideAllButtons() {
        // TBD
    }
    public static void HideSkillBox() {
        // TBD
    }
    public static void HideItemBox() {
        // TBD
    }
    public static void ShowTargetingReticles() {
        // This function is called after the user clicks a targeted skill button
        // Configures variables on the targetingreticle objects attached to each battlescene unit
        // So when each of them is moused over they exhibit correct lighting behaviour to indicate targeting
        // And also selectively shows them depending on valid skill target
    }

    //temp functions
    IEnumerator AdvanceTurn() {
        currentTurn += 1;
        while (!activeunits.ContainsKey(currentTurn)) {
            currentTurn += 1;
            if (currentTurn > highestTurnID) { currentTurn = 1; }
        }
        yield return null;
    }

    //Button scripts start here:

    public void BaseAttack(GameObject actorBaseUnit, GameObject targetBaseUnit) {
        StartCoroutine(BasicAttackCoroutine(actorBaseUnit, targetBaseUnit));
    }
    public IEnumerator BasicAttackCoroutine(GameObject actorBaseUnit, GameObject targetBaseUnit) {
        yield return null;
    }
}
