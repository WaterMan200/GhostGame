using UnityEngine;

public class gizmosStuff : MonoBehaviour
{
    
   void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(10,0,10));;
    }
}