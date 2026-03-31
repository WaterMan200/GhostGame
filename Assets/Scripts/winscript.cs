using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class winscript : MonoBehaviour
{
    public Material skyDay;
    public Material skyNight;
    public GameObject sun;
    public GameObject enemy;

    public GameObject body;

[Range(0,1000)]
    public int OuterSpawnRadius;
[Range(0,1000)]
    public int InnerSpawnRadius;

    private int layerMask;
    public int layerInclude;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RenderSettings.skybox = skyNight;
        sun.SetActive(false);
        enemy.SetActive(true);

        layerMask = 1 << layerInclude;

        Vector3 BodySpawnPos = new Vector3(0, 0, 0);

        int randNum = Random.Range(-OuterSpawnRadius/2, OuterSpawnRadius/2);
        while(Mathf.Abs(randNum) >= InnerSpawnRadius) randNum = Random.Range(-OuterSpawnRadius/2, OuterSpawnRadius/2);

        BodySpawnPos.x = randNum;
        randNum = Random.Range(-OuterSpawnRadius/2, OuterSpawnRadius/2);
        while(Mathf.Abs(randNum) >= InnerSpawnRadius) randNum = Random.Range(-OuterSpawnRadius/2, OuterSpawnRadius/2);
        BodySpawnPos.z = randNum;

        BodySpawnPos += transform.position;

        RaycastHit Hit;
        if (Physics.Raycast(BodySpawnPos, new Vector3(0,-1,0), out Hit, Mathf.Infinity, layerMask)) {
            BodySpawnPos = Hit.point;
        }

        body.transform.position = BodySpawnPos;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDay()
    {
        RenderSettings.skybox = skyDay;
        sun.SetActive(true);
        enemy.SetActive(false);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(OuterSpawnRadius,0,OuterSpawnRadius));;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(InnerSpawnRadius,0,InnerSpawnRadius));;
    }



}
