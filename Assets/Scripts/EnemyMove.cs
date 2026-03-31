using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class EnemyMove : MonoBehaviour {

	public GameObject player;
	public GameObject enemy;
	public GameObject stalkTarget;
	private NavMeshAgent agent;
	public MoveState moveState;
	private float roamTime;
	private Vector3 playerPos;
	private Vision enemyVision;
	private Vision[] playerVision;
	private List<Transform> Targets = new List<Transform>();
	private bool following;
	private bool searching;
	private bool stalking;
	private bool frozen;
	private Vector3 playerLastSeen;
	private float searchTime;
	private float speed;
	private float freezeTime;
	private float playerEnemyDist;


	void Start ()
	{
		agent = GetComponent<NavMeshAgent>();
		roamTime = 0f;
		playerPos = player.transform.position;
		enemyVision = enemy.GetComponent<Vision>();
		playerVision = player.GetComponents<Vision>();
		following = false;
		searching = false;
		stalking = false;
		frozen = false;
		playerLastSeen = player.transform.position;
		searchTime = 5f;
		freezeTime = 0f;

	}
	void Update () 
	{
		if (moveState == MoveState.Default)
		{
			Targets = enemyVision.visibleTargets;
			if(Targets.Count > 0)
			{
				if(playerVision[1].visibleTargets.Count == 0 && following == false) stalking = true;
				following = true;
				playerLastSeen = player.transform.position;
			}
			else
			{
				if(searchTime == 5f && agent.remainingDistance <= 1f)
				{
					searchTime -= Time.deltaTime;
					following = false;
					stalking = false;
					searching = true;
				}
				if(searchTime < 5f)
				{
				//TURN RIGHT HERE
					searchTime -= Time.deltaTime;
				} 

				if(searchTime <= 2.5f)
				{
				//Turn Left HERE
				}
				if(searchTime <= 0f && searching)
				{
					searchTime = 5f;
					agent.isStopped = true;
					searching = false;                  
				}
			}
			if(!following && !searching)
			{
				if(roamTime <= 0f) 
				{
					WanderMove();
					roamTime = 5f;
				}
				if(agent.remainingDistance <= 1) agent.isStopped = true;
				else agent.isStopped = false;    
			}
			else if(following && stalking)
			{	
				playerEnemyDist =  Mathf.Sqrt(Mathf.Pow(player.transform.position.x - enemy.transform.position.x, 2)  + Mathf.Pow(player.transform.position.z- enemy.transform.position.z, 2));
				Debug.Log(playerEnemyDist);
				if(playerEnemyDist <= 10) stalking = false;
				else if(playerVision[1].visibleTargets.Count == 1 && playerVision[0].visibleTargets.Count == 0)
				{
					agent.isStopped = true;
					
				}
				else if(playerVision[1].visibleTargets.Count == 1 && playerVision[0].visibleTargets.Count == 1)
				{
					agent.isStopped = true;
					freezeTime += Time.deltaTime;

					if(freezeTime >= playerEnemyDist/5f){
						agent.isStopped = false;
						stalking = false;
						freezeTime = 0f;
					}
				}
				else if(playerVision[1].visibleTargets.Count == 0)
				{
					ChaseMove(stalkTarget.transform.position);
					agent.isStopped = false;
					if(freezeTime > 0f)freezeTime -= Time.deltaTime;
				}
			}
			else if(following)
			{
				ChaseMove(playerLastSeen);
				agent.isStopped = false; 
			}
		}
		if (moveState == MoveState.Wander)
		{
			if(roamTime <= 0f) 
			{
				WanderMove();
				roamTime = 5f;
			}
			if(agent.remainingDistance <= 1) agent.isStopped = true;
			else agent.isStopped = false;
		}
		if (moveState == MoveState.Chase)  ChaseMove(playerPos);
		if(moveState == MoveState.Stalk)
		{
			if(playerVision[1].visibleTargets.Count == 0)
			{
				ChaseMove(stalkTarget.transform.position);
				agent.isStopped = false;   				
			}
			else
			{
				agent.isStopped = true;
			}

		} 
		roamTime -= Time.deltaTime;
		if (agent.isStopped)
		{
			agent.velocity = Vector3.zero;
			agent.updateRotation = false;
			agent.updatePosition = false;

		}
		else
		{
			agent.updateRotation = true;
			agent.updatePosition = true;
		}
		
	}

	private void WanderMove()
	{
		Vector3 finalPosition =  new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
		while(finalPosition.x == Mathf.Infinity)
		{
			Vector3 randomDirection = Random.insideUnitSphere * 30;
			randomDirection += transform.position;
			NavMeshHit hit;
			NavMesh.SamplePosition(randomDirection, out hit, 10, 1);
			finalPosition = hit.position;
		}
		agent.destination = finalPosition;
	}
	private void ChaseMove(Vector3 pos)
	{
		agent.destination = pos;
	}
	void OnCollisionEnter(Collision other)
	{
		if (other.collider.gameObject.tag == "Player") Debug.Log("HAHAHAHAA");
	}

}
public enum MoveState
{
	Default,
	Wander,
	Stalk, 
	Chase,
	Idle
}