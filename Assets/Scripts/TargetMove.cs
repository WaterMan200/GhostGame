using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetMove : MonoBehaviour 
{
    public GameObject player;
    public GameObject enemy;
    private float playerEnemyDist;

    void Start()
    {
        transform.position = player.transform.position;
    }
    void Update()
    {
        playerEnemyDist =  Mathf.Sqrt(Mathf.Pow(player.transform.position.x - enemy.transform.position.x, 2)  + Mathf.Pow(player.transform.position.z- enemy.transform.position.z, 2));
        transform.localPosition = new Vector3(0,0, -Mathf.Abs(playerEnemyDist)*2/3 +3);
    }
}
