using UnityEngine;
using System.Collections;

public class Enemy_SetToHealth : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (transform.parent.GetComponent<Enemy>().Health > 60){
			GetComponent<ParticleSystem>().emissionRate = 0;
		}

		if (transform.parent.GetComponent<Enemy>().Health > 25 && transform.parent.GetComponent<Enemy>().Health <= 60) {
			GetComponent<ParticleSystem>().emissionRate = 7;
		}
		if (transform.parent.GetComponent<Enemy>().Health <= 25){
			GetComponent<ParticleSystem>().emissionRate = 25;
		}
	}
}
