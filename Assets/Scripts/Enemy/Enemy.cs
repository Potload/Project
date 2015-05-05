using UnityEngine;
using System.Collections;

public class Enemy : Humanoid {

	public float HealthDecrease1,HealthDecrease2,HealthDecrease3;
	// Use this for initialization
	void Start () {
		Speed = 1;
	}
	
	// Update is called once per frame
	void Update () {
	if (Health <= 0){
			Destroy(this.gameObject);
		}
	}
	public void LaserNoCombo()
	{
		Health -= HealthDecrease1;
	}
	public void LaserCombined()
	{
		Health -= HealthDecrease2;
	}
	public void LaserFinal()
	{
		Health -= HealthDecrease3;
	}
}
