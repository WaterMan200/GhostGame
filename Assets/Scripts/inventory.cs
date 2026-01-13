using UnityEngine;

public class inventory : MonoBehaviour
{
    public KeyCode invKey = KeyCode.E;
    public GameObject heldItem;

    public Vector3 itemPos = new Vector3(0.48300001f,0.423999995f,0.931999981f);

    public int interact_Distance;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * interact_Distance, Color.yellow);
        if (Input.GetKeyDown(invKey))
        {
            RaycastHit interactRay;
            Vector3 origin = transform.position;
            Vector3 direction = transform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(origin, direction, out interactRay, interact_Distance))
                if(interactRay.collider.gameObject.tag == "Item")
                    {
                        Debug.Log("Triggered by an object with tag: ");
                        GameObject item = interactRay.collider.gameObject;
                        item.transform.SetParent(transform, true);
                        item.GetComponent<Rigidbody>().isKinematic = true;
                        item.GetComponent<BoxCollider>().isTrigger = true;
                        interactRay.collider.gameObject.transform.localPosition = itemPos;
                    }
                else
                    {
                        Debug.Log("AHAHA");
                    }
        }
        
    }
}
