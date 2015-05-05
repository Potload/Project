
using UnityEngine;
using System.Collections;

public class Enemy1hologram : MonoBehaviour {
	public float scrollSpeed = 0.001F;
	public float scrollSpeedAttack = 0.001F;
	public Renderer attack;
	public Color HealthColor;
	float health;


	void Start() {
	
	}
	void Update() {
		health = transform.parent.gameObject.GetComponent<Enemy>().Health * 0.01f;
		
		HealthColor = new Color(1.0f, health, health, 0.8f); //decreasing green blue, to get more red while health drops
	
		float offset = Time.time * scrollSpeed;
		float offset2 = Time.time * scrollSpeedAttack;
		GetComponent<Renderer>().material.SetTextureOffset("_Alpha", new Vector2(0, offset));
		attack.material.SetTextureOffset("_MainTex", new Vector2(0, offset2));

		GetComponent<Renderer>().material.SetColor("_ImageColor", HealthColor);
	}
}