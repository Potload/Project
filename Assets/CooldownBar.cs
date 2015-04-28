using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CooldownBar : MonoBehaviour {
    public int Player;
    RectTransform rTrans;
	// Use this for initialization
	void Start () {
        rTrans = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        rTrans.localScale = new Vector3(LaserCalculate.CoolDown[Player], 1, 1);
	}
}
