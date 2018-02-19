using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTestingScript : MonoBehaviour {

    public Vector3 initialposition;
    public GameObject unit1;
    public GameObject unit2;

	// Use this for initialization
	void Start () {
        initialposition = GetComponent<Transform>().transform.position;
    }
	
	// Update is called once per frame
	void Update () {

    }

    // Functions to be called by buttons:

    public void Attack_1() {
        //unit1.GetComponent<BaseUnit>().DashTo(unit2);
        BattleManager.currentTarget = unit2;
        BattleManager.AttackButtonPressed();
    }

    public void Attack_2() {
        unit1.GetComponent<BaseUnit>().JumpTo(unit2);
    }
    
}
