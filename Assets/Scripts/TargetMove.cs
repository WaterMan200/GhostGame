using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetMove : MonoBehaviour 
{
    public GameObject player;
    public GameObject enemy;
    private bool behind;
    private float playerEnemyDist;
    private float moveTime;
    private float resetTime;

    void Start()
    {
        behind = false;
        transform.position = player.transform.position;
        moveTime = 0.1f;
        resetTime = 0;
    }
    void Update()
    {
        playerEnemyDist =  Mathf.Sqrt(Mathf.Pow(player.transform.position.x - enemy.transform.position.x, 2)  + Mathf.Pow(player.transform.position.z- enemy.transform.position.z, 2));
        if (!behind)
        {
            if (resetTime <=0) transform.localPosition = new Vector3(0,0, -Mathf.Abs(playerEnemyDist)*2/3);
        }
        else
        {
            if(moveTime <= 0)
            {
                transform.localPosition = new Vector3(0,0, -Mathf.Abs(transform.localPosition.z) + 0.1f);
                resetTime = 3f;
                if (!behind)
                {
                    transform.localPosition = new Vector3(0,0, -Mathf.Abs(transform.localPosition.z) - 0.1f);
                }
                moveTime = 0.1f;
            }
            
            
        }
        moveTime -= Time.deltaTime;
        resetTime -= Time.deltaTime;

    }
    void OnCollisionEnter(Collision other)
    {
    if (other.collider.gameObject.tag == "Enemy") behind = true;
    }
    void OnCollisionExit(Collision other)
    {
    if (other.collider.gameObject.tag == "Enemy") behind = false;
    }
}
