using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralManager : MonoBehaviour
{
    public GameObject LosePanel;

    private void Awake()
    {
        Time.timeScale = 1f;
    }


    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        if (PointOfInterestSystem.Instance.interest <= 0)
        {
            Debug.Log("Game Over");
           /* Time.timeScale = 0;
            LosePanel.SetActive(true);*/
        }
    }
}
