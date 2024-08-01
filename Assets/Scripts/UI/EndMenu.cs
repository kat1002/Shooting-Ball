using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    [SerializeField] GameObject endMenuUI;
    [SerializeField] TextMeshProUGUI roundText;


    private void Start()
    {
    }

    private void Update()
    {
        roundText.text = GameManager.Instance.Round.ToString();
        if(GameManager.Instance.isGameEnd) EndGame();
    }

    public void EndGame() {
        endMenuUI.SetActive(true);
        roundText.text = GameManager.Instance.Round.ToString();
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(0);
    }
}
