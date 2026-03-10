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
    private Vision playerVision;
    private List<Transform> Targets = new List<Transform>();
    private bool following;
    private bool searching;
    private bool stalking;
    private bool frozen;
    private Vector3 playerLastSeen;
    private float searchTime;
    private float speed;
    private float freezeTime;


  void Start ()
{
      agent = GetComponent<NavMeshAgent>();
      roamTime = 0f;
      playerPos = player.transform.position;
      enemyVision = enemy.GetComponent<Vision>();
      playerVision = player.GetComponent<Vision>();
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
          following = true;
          playerLastSeen = player.transform.position;
      }
      else
      {
        if(searchTime == 5f && agent.remainingDistance <= 1f)
        {
        
          searchTime -= Time.deltaTime;
          following = false;
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
    if(moveState == MoveState.Stalk) ChaseMove(stalkTarget.transform.position);

    roamTime -= Time.deltaTime;

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