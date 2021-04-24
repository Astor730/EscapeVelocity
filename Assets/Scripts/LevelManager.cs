using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Text gravityText;
    public Text laserButtonText;
    public Text panelButtonText;
    public Text gameLostText;
    public Text gameWonText;
    public Text enemyDisableText;

    public static bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {
        gravityText.gameObject.SetActive(true);
        laserButtonText.gameObject.SetActive(false);
        panelButtonText.gameObject.SetActive(false);
        gameLostText.gameObject.SetActive(false);
        gameWonText.gameObject.SetActive(false);
        enemyDisableText.gameObject.SetActive(false);
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            gravityText.gameObject.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.B))
        {
            laserButtonText.gameObject.SetActive(false);
            panelButtonText.gameObject.SetActive(false);
        }
    }

    public void LevelLost()
    {
        HideAllText();
        isGameOver = true;
        gameLostText.gameObject.SetActive(true);
        Invoke("ReloadLevel", 2);
    }

    public void LevelBeat()
    {
        HideAllText();
        isGameOver = true;
        gameWonText.gameObject.SetActive(true);
        Invoke("LoadNextLevel", 2);

    }

    void ReloadLevel()
    {
        DisableEnemy.isQuitting = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void DisplayLaserButtonText()
    {
        laserButtonText.gameObject.SetActive(true);
    }

    public void HideLaserButtonText()
    {
        laserButtonText.gameObject.SetActive(false);
    }

    public void DisplayPanelButtonText()
    {
        panelButtonText.gameObject.SetActive(true);
    }

    public void HidePanelButtonText()
    {
        panelButtonText.gameObject.SetActive(false);
    }

    public void DisplayEnemyDisableText()
    {
        enemyDisableText.gameObject.SetActive(true);
    }
    
    public void HideEnemyDisableText()
    {
        enemyDisableText.gameObject.SetActive(false);
    }

    void HideAllText()
    {
        gravityText.gameObject.SetActive(false);
        laserButtonText.gameObject.SetActive(false);
        panelButtonText.gameObject.SetActive(false);
        gameLostText.gameObject.SetActive(false);
        gameWonText.gameObject.SetActive(false);
        enemyDisableText.gameObject.SetActive(false);
    }
}
