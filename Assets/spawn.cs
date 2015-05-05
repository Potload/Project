using UnityEngine;
using System.Collections;

public class spawn : MonoBehaviour {
	public Transform spawnPos;
	public GameObject character;
	
	public int timer, timermax;
	// Use this for initialization
	
	
	
	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		timer++;
		if (timer >= timermax){
			GameObject characterInstance = (GameObject)Instantiate(character);
			characterInstance.transform.position = spawnPos.position;
			
			timer = 0;
		}
		
	}
	
	
}
