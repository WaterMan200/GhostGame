    using UnityEngine;
    using UnityEngine.AI;

    public class MoveTo : MonoBehaviour {

       public Transform goal;

       void Update () {
          NavMeshAgent agent = GetComponent<NavMeshAgent>();
          agent.destination = goal.position;
       }

       void OnCollisionEnter(Collision other)
       {
         if (other.collider.gameObject.tag == "Player") Debug.Log("HAHAHAHAA");
       }
    }