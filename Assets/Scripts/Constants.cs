using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Reminder section:
 * Implement talents and TalentID, talents being the "skills" in the skilltree
 * 
 * 
 */

public class Constants : MonoBehaviour {

    public const float MIN_RANDOM_DELAY = 1;
    public const float MAX_RANDOM_DELAY = 3;
    public const float DELAY_TICK_RATE = 0.5f;

    public enum StatusID {
        normal,
        dead,
        immortality,
        removed
    }

    public const int TOTAL_STATS = 5; //change as required
    public enum StatID {
        attack,
        magic,
        speed,
        //add as required
    }

    public const int TOTAL_BUFFS = 1; //change as required
    public enum BuffID {
        last,
        //one for each possible buff or debuff in the game
    }

    public const int TOTAL_BUFFEFFECTS = 14;
    public enum BuffEffectID {
        attackbasebonus,
        attackfactorbonus,
        attackmultibonus,
        attackflatbonus,
        attackflatpenalty,
        attackfactorpenalty,
        attackmultipenalty,
        defencebasebonus,
        defencefactorbonus,
        defencemultibonus,
        defenceflatbonus,
        defenceflatpenalty,
        defencefactorpenalty,
        defencemultipenalty,
    }

    public const int TOTAL_MOVES = 3;
    public enum MoveID {
        attack,
        strongattack,
        heal,
    }

    public const int TOTAL_ROWS = 6;
    public enum RowID {
        allyback,
        enemyforward,
        allyfront,
        enemyfront,
        allyforward,
        enemyback,
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
