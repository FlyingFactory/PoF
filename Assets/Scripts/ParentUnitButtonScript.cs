using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentUnitButtonScript : MonoBehaviour {

    public static List<MonoBehaviour> listOfButtons = new List<MonoBehaviour>();
    public static GameObject attachedReticle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void OpenButtons() {
        gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(Enable(0.5f));
    }

    public void CancelButtons() {
        StopAllCoroutines();
        StartCoroutine(FadeAndDisable(0.5f));
    }

    void OnMouseUpAsButton() {
        //buttons unclickable
        if (attachedReticle != null) attachedReticle.GetComponent<ReticleScript>().Reset();
        attachedReticle = null;
        CancelButtons();
    }

    IEnumerator Enable(float fadetime) {
        BattleManager.UnitButtonsClickable = true;
        SpriteRenderer[] r = GetComponentsInChildren<SpriteRenderer>();
        Vector4[] oldcolor = new Vector4[r.Length];
        Vector4[] newcolor = new Vector4[r.Length];

        for (int i = 0; i < r.Length; i++) {
            oldcolor[i] = r[i].color;
            oldcolor[i].w = 0f;
            newcolor[i] = new Vector4(oldcolor[i].x, oldcolor[i].y, oldcolor[i].z, 1f);
        }

        float rate = 1f / fadetime;
        float t = 0;
        t += Time.deltaTime * rate;
        do {
            for (int i = 0; i < r.Length; i++) {
                r[i].color = Vector4.Lerp(oldcolor[i], newcolor[i], t);
            }
            t += Time.deltaTime * rate;
            yield return null;
        } while (t < 1f);
        for (int i = 0; i < r.Length; i++) {
            r[i].color = newcolor[i];
        }
    }

    IEnumerator FadeAndDisable(float fadetime) {
        BattleManager.UnitButtonsClickable = false;
        SpriteRenderer[] r = GetComponentsInChildren<SpriteRenderer>();
        Vector4[] oldcolor = new Vector4[r.Length];
        Vector4[] newcolor = new Vector4[r.Length];

        for (int i = 0; i < r.Length; i++) {
            oldcolor[i] = r[i].color;
            newcolor[i] = new Vector4(oldcolor[i].x, oldcolor[i].y, oldcolor[i].z, 0f);
        }

        float rate = 1f / fadetime;
        float t = 0;
        t += Time.deltaTime * rate;
        do {
            for (int i = 0; i < r.Length; i++) {
                r[i].color = Vector4.Lerp(oldcolor[i], newcolor[i], t);
            }
            t += Time.deltaTime * rate;
            yield return null;
        } while (t < 1f);
        for (int i = 0; i < r.Length; i++) {
            r[i].color = newcolor[i];
        }
        gameObject.SetActive(false);
    }
}
