using UnityEngine;
using System.Collections;

public class RefillCooldownScript : MonoBehaviour {
    public int CombinedNr=0;
    int source=0;
    int partner=0;
	// Use this for initialization
	void OnEnable () {
        source = LaserCalculate.CombinedSource [CombinedNr];
        LaserCalculate.CoolDown [source] = 1;
        partner = LaserCalculate.Partner [LaserCalculate.CombinedSource [CombinedNr]];
        LaserCalculate.CoolDown [partner] = 1;
	}
	
    void OnDisable(){
        LaserCalculate.CoolDown [source] -= 0.25f;
        LaserCalculate.CoolDown [partner] -= 0.25f;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
