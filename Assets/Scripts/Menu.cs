using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject deathScreen;
    public GameObject cursor;
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
    if (Input.GetKeyDown(pause) && !deathScreen.activeSelf)
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
    public void restart()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        time = true;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        deathScreen.SetActive(false);
    }
    public void death()
    {
        time = false;
        Time.timeScale = 0;
        deathScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
