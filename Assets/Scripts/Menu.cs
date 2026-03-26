using UnityEngine;

public class Menu : MonoBehaviour
{

    [HideInInspector] public bool time;
    public KeyCode pause = KeyCode.Escape;
    void Start()
    {
        time = true;
    }
    void Update()
    {
    if (Input.GetKeyDown(pause))
        {
            if (time)
            {
                time = false;
                Time.timeScale = 0;
            }
            
            else if (!time)
            {
                time = true;
                Time.timeScale = 1;
            } 
        } 
    }

}
