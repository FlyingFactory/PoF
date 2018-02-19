using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour {
    
    public string displayname;
    public string internalname;
    public float health;
    public float maxhealth;
    public float mana;
    public float maxmana;
    public float[] stats;
    public Constants.StatusID status;
    public int[] buffs;
    public float[] buffDurations;
    public bool isimmortal;
    public float delay;
    public int turnID; //starts at 1, 0 is for transition state, assigned in order of adding to scene
    public Constants.RowID currentRow;
    public float positionInRow; //TBD, from 0 to 1, indicates unit order from top to bottom by y-position to scale
    public float size; //relates to model size for determining dash locations

    //reference variables
    //currently empty

    // Use this for initialization
    void Start() {
        BaseStart();
    }
    //This can be called in Start() of all derived classes.
    protected void BaseStart() {
        BattleManager.highestTurnID += 1;
        turnID = BattleManager.highestTurnID;
        BattleManager.activeunits.Add(BattleManager.highestTurnID, this);
        displayname = "Unit " + BattleManager.highestTurnID;
        internalname = "Unit " + BattleManager.highestTurnID;
        health = 100;
        maxhealth = 100;
        mana = 100;
        maxmana = 100;
        stats = new float[Constants.TOTAL_STATS];
        for (int i = 0; i < Constants.TOTAL_STATS; i++) { stats[i] = 0; }
        status = Constants.StatusID.normal;
        buffs = new int[Constants.TOTAL_BUFFS];
        for (int i = 0; i < Constants.TOTAL_BUFFS; i++) { buffs[i] = 0; }
        buffDurations = new float[Constants.TOTAL_BUFFS];
        for (int i = 0; i < Constants.TOTAL_BUFFS; i++) { buffDurations[i] = 0; }
        isimmortal = false;
        delay = Random.Range(Constants.MIN_RANDOM_DELAY, Constants.MAX_RANDOM_DELAY);
        Debug.Log(internalname + ": starting delay " + delay);
        //currentRow
        //positionInRow
        size = 1;
    }

    // Update is called once per frame
    void Update() {

    }

    // Helper functions
    public void LoadUnitData() {
        //TBD
    }

    // Extra getters and setters
    float SpendHealth(int raw) {
        //TBD
        if (health <= 0) { health = 1; }
        return health;
    }
    public float DamageHealth(int raw) {
        //TBD
        if(status == Constants.StatusID.dead) {
            Debug.Log(this + " is already dead");
            return 0;
        }
        health -= raw;
        Debug.Log("Dealt " + raw + " damage to " + gameObject + ".");
        if (health <= 0) {
            health = 0;
            if (isimmortal) {
                status = Constants.StatusID.immortality;
                Debug.Log(this + " has been downed");
                return 0;
            }
            else {
                status = Constants.StatusID.dead;
                Debug.Log(this + " has been slain");
                UnitDeath();
                return 0;
            }
        }
        else {
            Debug.Log("Remaining health: " + health);
        }
        return health;
    }
    float HealHealth(float raw) {
        //TBD
        if (health > maxhealth) { health = maxhealth; }
        return health;
    }
    float AddHealth(float raw) {
        //TBD
        if (health > maxhealth) { health = maxhealth; }
        return health;
    }
    float SpendMana(int raw) {
        //TBD
        if (mana <= 0) { mana = 0; }
        return mana;
    }
    float DamageMana(int raw) {
        //TBD
        if (mana <= 0) { mana = 0; }
        return mana;
    }
    float HealMana(int raw) {
        //TBD
        if (mana > maxmana) { mana = maxmana; }
        return mana;
    }
    float AddMana(int raw) {
        //TBD
        if (mana > maxmana) { mana = maxmana; }
        return mana;
    }

    // Unit outputs (to be expanded)
    float DoDamage(int raw) {
        float returnval = raw;
        //check for skill nultipliers
        return returnval;
    }
    float DoHealing(int raw) {
        float returnval = raw;
        //check for skill nultipliers
        return returnval;
    }

    public void UnitDeath() {
        BattleManager.activeunits.Remove(turnID);
        gameObject.GetComponentInChildren<ReticleScript>().UnitRemoved();
        //Placeholder image remove, eventually replace with death animation
        Vector4 spritecolor = transform.Find("PlaceholderSprite").GetComponent<SpriteRenderer>().color;
        transform.Find("PlaceholderSprite").GetComponent<SpriteRenderer>().color = new Color(spritecolor.x, spritecolor.y, spritecolor.z, 0f);
    }

    // Main animation functions
    // add as required
    public void DashAttack() {
        
    }
    public void JumpAttack() {

    }
    public void LeapAttack() {

    }
    public void ProjectileAttack() {

    }

    // Support animation functions (building blocks for main set)
    // remember to reference size on both objects
    IEnumerator LinearMovement(Vector3 displacement, float traveltime, float waittime) {
        yield return new WaitForSeconds(waittime);
        float rate = 1f / traveltime;
        float t = 0;
        Vector3 startpos = transform.position;
        t += Time.deltaTime * rate;
        do {
            transform.position = Vector3.Lerp(startpos, startpos + displacement, t);
            t += Time.deltaTime * rate;
            yield return null;
        } while (t < 1f);
        transform.position = startpos + displacement;
    }

    IEnumerator ParabolaMovement(Vector3 displacement, float traveltime, float waittime, float maxheight) {
        yield return new WaitForSeconds(waittime);
        float rate = 1f / traveltime;
        float t = 0;
        Vector3 startpos = transform.position;
        t += Time.deltaTime * rate;
        do {
            Vector3 vectorsum = Vector3.zero;
            vectorsum.y += SolveFalseHeight(maxheight, t);
            vectorsum += Vector3.Lerp(startpos, startpos + displacement, t);
            transform.position = vectorsum;
            t += Time.deltaTime * rate;
            yield return null;
        } while (t < 1f);
        transform.position = startpos + displacement;
    }

    public void DashTo(GameObject target) {
        Vector3 displacement;
        if (transform.position.x <= target.transform.position.x) { displacement = target.transform.position - new Vector3(size + target.GetComponent<BaseUnit>().size, 0, 0) - transform.position; }
        else { displacement = target.transform.position + new Vector3(size + target.GetComponent<BaseUnit>().size, 0, 0) - transform.position; }
        StartCoroutine(LinearMovement(displacement, 0.4f, 0.4f));
        StartCoroutine(LinearMovement(-displacement, 0.4f, 1.2f));
    }
    public void JumpTo(GameObject target) {
        Vector3 displacement;
        if (transform.position.x <= target.transform.position.x) { displacement = target.transform.position - new Vector3(size + target.GetComponent<BaseUnit>().size, 0, 0) - transform.position; }
        else { displacement = target.transform.position + new Vector3(size + target.GetComponent<BaseUnit>().size, 0, 0) - transform.position; }
        StartCoroutine(ParabolaMovement(displacement, 0.4f, 0.4f, 3f));
        StartCoroutine(ParabolaMovement(-displacement, 0.4f, 1.2f, 3f));

    }
    public void LeapTo() {

    }
    public void NothingPersonnelTM() {
         
    }
    public void ChangeRow(Constants.RowID newrow) {
        Constants.RowID previousrow = currentRow;
        currentRow = newrow;
        positionInRow += Random.Range(-0.01f, 0.01f);
        BattleManager.DistributePositions(newrow);
        BattleManager.DistributePositions(previousrow);
    }

    IEnumerator StopMovement(float delay) {
        yield return new WaitForSeconds(delay);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public float SolveFalseHeight(float maxheight, float progress/*alternate variables: delta_h, grav*/) {
        float falseprogress = 2 * Mathf.Abs(progress - 0.5f);
        float falsegrav = 2 * maxheight; // divide by falsetime^2 = 1
        float drop = 0.5f * falsegrav * Mathf.Pow(falseprogress, 2);
        return maxheight - drop;
    }
}
