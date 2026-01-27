using UnityEngine;

public class inventory : MonoBehaviour
{
    public KeyCode invKey = KeyCode.E;
    public KeyCode upKey = KeyCode.R;
    public KeyCode downKey = KeyCode.F;
    public GameObject heldItem;
    public ItemStats heldStats;
    int selectedSlot = 0;
    public GameObject[] invArr = new GameObject[4];

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
                        int slot = canAddItem();
                        if (slot != -1) // checks to see if slot is available
                            {
                                Debug.Log("Triggered by an object with tag: ");
                                GameObject item = interactRay.collider.gameObject;
                                item.transform.SetParent(transform, true);
                                item.transform.localEulerAngles = item.GetComponent<ItemStats>().holdAngle;
                                item.GetComponent<Rigidbody>().isKinematic = true;
                                item.GetComponent<MeshCollider>().enabled = false;
                                interactRay.collider.gameObject.transform.localPosition = itemPos;
                                invArr[slot] = item;
                                swapItem(slot);
                            }
                        else
                    {
                        
                    }
                        
                    }
                else
                    {
                        Debug.Log("AHAHA");
                    }
        }
        if (Input.GetKeyDown(upKey))
        {
            selectedSlot = (selectedSlot + 1)%4;
            swapItem(selectedSlot);
        }
        if (Input.GetKeyDown(downKey))
        {
            selectedSlot = (selectedSlot + 3)%4;
            swapItem(selectedSlot);
        }

        if (Input.GetMouseButtonDown(1) && heldItem != null)
        {
            OnClick();
            // Add your custom functionality here
        }
    }

    void swapItem(int slot)
    {
        for (int i = 0; i < 4; i++)
        {
            if (invArr[i] != null)
            {
                invArr[i].GetComponent<MeshRenderer>().enabled = false;
            }
        }
        
        heldItem = invArr[slot];
        heldStats = heldItem.GetComponent<ItemStats>();
        if (heldItem != null)
            heldItem.GetComponent<MeshRenderer>().enabled = true;
    }

    int canAddItem() // returns the index of where an object can be added to the inventory, returns -1 if no slots are available
    {
        for (int i = 0; i < 4; i++)
        {
            if (invArr[i] == null) return i;
        }
        return -1;
    }

    void OnClick()
    {
        string id = heldStats.id;

        if (id == "flashlight") // IF ITEM IS A FLASHLIGHT
        {
            heldStats.tiedStuff[0].SetActive(!heldStats.tiedStuff[0].activeSelf);
            
        }
    }
}
