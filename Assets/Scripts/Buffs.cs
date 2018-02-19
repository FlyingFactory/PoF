using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuff {

    public readonly Constants.BuffID buffID;

    public float[] buffEffects = new float[Constants.TOTAL_BUFFEFFECTS];

    public delegate float[] conditioncheck();
    public Dictionary<string, conditioncheck> conditionslist = new Dictionary<string, conditioncheck>();

    public float[] CalculateBuffEffects() {
        float[] totalBuffEffects = buffEffects;
        foreach(conditioncheck condition in conditionslist.Values) {
            float[] newBuffEffects = condition();
            for (int i = 0; i < Constants.TOTAL_BUFFEFFECTS; i++) {
                totalBuffEffects[i] += newBuffEffects[i];
            }
        }
        return totalBuffEffects;
    }
	
}

public class ExampleBuff : BaseBuff {
    
    public ExampleBuff() {
        buffEffects[(int)Constants.BuffEffectID.attackbasebonus] = 1f;
        conditionslist.Add("highhealthbonus", MoreDamageAtHighHealth);
    }

    public float[] MoreDamageAtHighHealth() {
        float[] returnBuffEffects = new float[Constants.TOTAL_BUFFEFFECTS];
        if (BattleManager.activeunits[BattleManager.currentTurn].GetComponent<BaseUnit>().health > BattleManager.activeunits[BattleManager.currentTurn].GetComponent<BaseUnit>().maxhealth / 2) {
            returnBuffEffects[(int)Constants.BuffEffectID.attackfactorbonus] += 1;
        }
        return returnBuffEffects;
    }
}
