using UnityEngine;

public class bodyScript : MonoBehaviour
{
    public GameObject winObj;
    winscript ws;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ws = winObj.GetComponent<winscript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Collider>().gameObject.tag == "Player")ws.setDay();
    }
}
