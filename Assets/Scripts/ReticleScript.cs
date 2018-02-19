using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleScript : MonoBehaviour {

    private bool mouseIsOver;
    private bool clickedAndHeld;
    private bool active;
    

	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        active = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseEnter() {
        mouseIsOver = true;
        if (active && !clickedAndHeld && ParentUnitButtonScript.attachedReticle != gameObject) {
            StopAllCoroutines();
            StartCoroutine(TransparencyFade(0.7f, 0.1f));
        }
    }

    private void OnMouseExit() {
        mouseIsOver = false;
        if (active && !clickedAndHeld && ParentUnitButtonScript.attachedReticle != gameObject) {
            StopAllCoroutines();
            StartCoroutine(TransparencyFade(0f, 0.4f));
        }
    }

    private void OnMouseDown() {
        if (active && BattleManager.ReticlesClickable) {
            StopAllCoroutines();
            StartCoroutine(TransparencyFade(1f, 0.1f));
            clickedAndHeld = true;
        }
    }

    private void OnMouseUp() {
        clickedAndHeld = false;
        //still needs to teleport buttons and reload button content
        if (active && mouseIsOver) {
            if (ParentUnitButtonScript.attachedReticle != gameObject) {
                if (ParentUnitButtonScript.attachedReticle != null) ParentUnitButtonScript.attachedReticle.GetComponent<ReticleScript>().Reset();
                ParentUnitButtonScript.attachedReticle = gameObject;
                BattleManager.buttonParent.transform.position = new Vector3(gameObject.transform.position.x,
                                                                            gameObject.transform.position.y,
                                                                            BattleManager.buttonParent.transform.position.z);
                BattleManager.buttonParent.GetComponent<ParentUnitButtonScript>().OpenButtons();
            }
            else {
                ParentUnitButtonScript.attachedReticle = null;
                BattleManager.buttonParent.GetComponent<ParentUnitButtonScript>().CancelButtons();
                StopAllCoroutines();
                StartCoroutine(TransparencyFade(0.4f, 0.1f));
            }
        }
        else if(active && ParentUnitButtonScript.attachedReticle != gameObject) {
            StopAllCoroutines();
            StartCoroutine(TransparencyFade(0f, 0.3f));
        }
    }

    public void Reset() {
        StopAllCoroutines();
        StartCoroutine(TransparencyFade(0f, 0.25f));
        mouseIsOver = false;
        clickedAndHeld = false;
        /*if (ParentUnitButtonScript.attachedReticle == gameObject) {
            ParentUnitButtonScript.attachedReticle = null;
        }*/
    }

    IEnumerator TransparencyFade(float target, float fadetime) {

        SpriteRenderer r = GetComponent<SpriteRenderer>();
        Vector4 oldcolor = r.color;
        Vector4 newcolor = new Color(1f, 1f, 1f, target);

        float rate = 1f / fadetime;
        float t = 0;
        t += Time.deltaTime * rate;
        do {
            r.color = Vector4.Lerp(oldcolor, newcolor, t); //Mathf.SmoothStep(0, 1, t)
            t += Time.deltaTime * rate;
            yield return null;
        } while (t < 1f);
        r.color = newcolor;
    }

    public void UnitRemoved() {
        StopAllCoroutines();
        active = false;
        StartCoroutine(FadeAndRemove());
        mouseIsOver = false;
        clickedAndHeld = false;
        /*if (ParentUnitButtonScript.attachedReticle == gameObject) {
            ParentUnitButtonScript.attachedReticle = null;
        }*/
    }

    IEnumerator FadeAndRemove() {

        SpriteRenderer r = GetComponent<SpriteRenderer>();
        Vector4 oldcolor = r.color;
        Vector4 newcolor = new Color(1f, 1f, 1f, 0f);

        float rate = 1f / 0.25f/*fade time*/ ;
        float t = 0;
        t += Time.deltaTime * rate;
        do {
            r.color = Vector4.Lerp(oldcolor, newcolor, t); //Mathf.SmoothStep(0, 1, t)
            t += Time.deltaTime * rate;
            yield return null;
        } while (t < 1f);
        r.color = newcolor;
        gameObject.SetActive(false);
    }
}
