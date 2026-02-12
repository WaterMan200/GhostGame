using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class EnemyMove : MonoBehaviour {

    public GameObject player;
    public GameObject enemy;
    private NavMeshAgent agent;
    public MoveState moveState;
    private float roamTime;
    private Transform playerPos;
    private Vision enemyVision;
    private Vision playerVision;
    private List<Transform> Targets = new List<Transform>();

  void Start ()
{
      agent = GetComponent<NavMeshAgent>();
      roamTime = 0f;
      playerPos = player.transform;
      enemyVision = enemy.GetComponent<Vision>();
      playerVision = player.GetComponent<Vision>();
      //Targets = enemyVision.GetComponent<visibleTargets>();
      
}
  void Update () 
  {

    if (moveState == MoveState.Wander)
    {
          
      if(roamTime <= 0f) 
      {
        WanderMove();
        roamTime = 5f;
      }
    
        
        
    }
    if (moveState == MoveState.Chase)  ChaseMove();
    if(moveState == MoveState.Stalk) StalkMove();

    roamTime -= Time.deltaTime;

  }
    
  private void WanderMove()
{
  Vector3 randomDirection = Random.insideUnitSphere * 30;
  randomDirection += transform.position;
  NavMeshHit hit;
  NavMesh.SamplePosition(randomDirection, out hit, 10, 1);
  Vector3 finalPosition = hit.position;		
  agent.destination = finalPosition;
}
private void StalkMove()
{
    //idk how going to do this bro
}
private void ChaseMove()
{
  //Change to LAST SEEN POS after enemy FOV 
    agent.destination = playerPos.position;
}
void OnCollisionEnter(Collision other)
{
  if (other.collider.gameObject.tag == "Player") Debug.Log("HAHAHAHAA");
}

}
public enum MoveState
{
    Wander,
    Stalk, 
    Chase,
    Idle
}