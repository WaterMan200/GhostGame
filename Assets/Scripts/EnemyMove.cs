    using UnityEngine;
    using UnityEngine.AI;

    public class EnemyMove : MonoBehaviour {

       public Transform player;
       public Transform enemy;
       private NavMeshAgent agent;
       public MoveState moveState;
       private float roamTime;

      void Start ()
    {
         agent = GetComponent<NavMeshAgent>();
         roamTime = 0f;

         
    }
      void Update () {
        
        if(roamTime <= 0f) {
          IdleMove();
          roamTime = 5f;
        }
      
          //ChaseMove();)
          roamTime -= Time.deltaTime;
      }
       
      private void IdleMove()
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
        agent.destination = player.position;
    }
       void OnCollisionEnter(Collision other)
       {
         if (other.collider.gameObject.tag == "Player") Debug.Log("HAHAHAHAA");
       }
    }
    public enum MoveState
{
    Idle,
    Stalk, 
    Chase
}