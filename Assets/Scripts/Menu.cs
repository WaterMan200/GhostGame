using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject deathScreen;
    [HideInInspector] public bool time;
    public KeyCode pause = KeyCode.Escape;
    void Start()
    {
        time = true;
        pauseScreen.SetActive(false);
        deathScreen.SetActive(false);
    }
    void Update()
    {
    if (Input.GetKeyDown(pause))
        {
            if (time)
            {
                time = false;
                Time.timeScale = 0;
                pauseScreen.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            
            else if (!time)
            {
                time = true;
                Time.timeScale = 1;
                pauseScreen.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            } 
        } 
    }

    public void Unpause()
    {
        time = true;
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
