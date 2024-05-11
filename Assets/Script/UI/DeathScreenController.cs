using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenController : MonoBehaviour
{
    
    public GameObject deathScreenUI;

    //--------------------------------------------------------------------------------------------------------------------------------------------

    void OnEnable()
    {
        //PlayerController.OnPlayerDeath += ShowDeathScreen; 
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    void OnDisable()
    {
       // PlayerController.OnPlayerDeath -= ShowDeathScreen;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public void ShowDeathScreen()
    {
        deathScreenUI.SetActive(true);
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public void RestartGame()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene("Game");
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game");
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

}
