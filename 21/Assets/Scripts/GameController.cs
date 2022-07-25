using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject playScreen;

    public Enemy enemy;
    
    public void Exit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Win()
    {
        winScreen.SetActive(true);
        playScreen.SetActive(false);
    }

    public void Lose()
    {
        loseScreen.SetActive(true);
        playScreen.SetActive(false);
    }

    public void EnemyTurn()
    {
        enemy.StartTakeCards();
    }
}
