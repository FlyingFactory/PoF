using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnIndicatorText : MonoBehaviour {

    private UnityEngine.UI.Text txt;

	// Use this for initialization
	void Start () {
        txt = gameObject.GetComponent<UnityEngine.UI.Text>();
    }
	
	// Update is called once per frame
	void Update () {
        txt.text = "Current Turn: " + BattleManager.currentTurn;
	}
}
