using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField]private List<GameObject> PauseMenuBottons;
    bool isPaused = false;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel")){
            PauseUnpause(ref isPaused);
        }
    }
    public void ResumeButton(){
        PauseUnpause(ref isPaused);
    }
    public void MenuButton(){
        SceneManager.LoadScene("Main Menu");
    }
    public void QuitButton(){
        Application.Quit();
    }
    private void PauseUnpause(ref bool pauseState){
        if (!pauseState){
            Time.timeScale = 0.0f;
            pauseState = true;
            Cursor.lockState = CursorLockMode.Locked;
            foreach  (GameObject button in PauseMenuBottons){
                button.SetActive(true);
            }
        }
        else {
            Time.timeScale = 0.1f;
            pauseState = true;
            Cursor.lockState = CursorLockMode.Locked;
            foreach  (GameObject button in PauseMenuBottons){
                button.SetActive(false);
            }
        }
    }
}
