using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {
	public GameObject[] playerPos;
	public Transform goal;
	public float[] distance;
	NavMeshAgent agent;
	public float distanceEnemy;
	public GameObject EnemyAim;

	void Awake () {
		playerPos[0] = GameObject.Find("Player 0");
		playerPos[1] = GameObject.Find("Player 1");
		playerPos[2] = GameObject.Find("Player 2");
		playerPos[3] = GameObject.Find("Player 3"); 
	}
	
	void Start () {
		agent = GetComponent<NavMeshAgent>();
	
	}

	void Update(){

		distance[0] = Vector3.Distance(this.transform.position, playerPos[0].transform.position);
		distance[1] = Vector3.Distance(this.transform.position, playerPos[1].transform.position);
		distance[2] = Vector3.Distance(this.transform.position, playerPos[2].transform.position);
		distance[3] = Vector3.Distance(this.transform.position, playerPos[3].transform.position);

		if ((distance[0] < distance[1]) && (distance[0] < distance[2]) && (distance[0] < distance[3]))
		{
			goal = playerPos[0].transform;
		}
		if ((distance[1] < distance[0]) && (distance[1] < distance[2]) && (distance[1] < distance[3]))
		{
			goal = playerPos[1].transform;
		}
		if ((distance[2] < distance[0]) && (distance[2] < distance[1]) && (distance[2] < distance[3]))
		{
			goal = playerPos[2].transform;
		}
		if ((distance[3] < distance[0]) && (distance[2] < distance[1]) && (distance[3] < distance[2]))
		{
			goal = playerPos[3].transform;
		}
		if (goal != null){
		distanceEnemy = Vector3.Distance(transform.position, goal.position);
		if (distanceEnemy  > 3.5f){
		agent.destination = goal.position; 
			EnemyAim.SetActive(false);
			agent.speed = 3.5f;
		}
		if (distanceEnemy  <= 3.5f){
			EnemyAim.SetActive(true);
			agent.speed = 0;
		}
		}
	}
}