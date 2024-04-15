using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager I;
    public GameObject Square;
    public GameObject endPanel;
    public Text timeTxt;
    public Text thisScoreTxt;
    public Text bestScoreTxt;
    public Animator anim;

    bool isRunnung = true;

    float alive = 0f;

    void Awake()
    {
        I = this;
    }

    void Start()
    {
        Time.timeScale = 1f;
        InvokeRepeating("MakeSquare", 0.0f, 0.5f);
    }

    void Update()
    {
        if (isRunnung)
        {
            alive += Time.deltaTime;
            timeTxt.text = alive.ToString("N2");
        }
    }

    void MakeSquare()
    {
        Instantiate(Square);
    }

    public void GameOver()
    {
        isRunnung = false;
        anim.SetBool("isDie", true);

        Invoke(nameof(TimeStop), 0.5f);

        thisScoreTxt.text = alive.ToString("N2");
        endPanel.SetActive(true);

        if (PlayerPrefs.HasKey("bestScore") == false)
        {
            PlayerPrefs.SetFloat("bestScore", alive);
        }
        else
        {
            if (PlayerPrefs.GetFloat("bestScore") < alive)
            {
                PlayerPrefs.SetFloat("bestScore", alive);
            }
        }
        bestScoreTxt.text = PlayerPrefs.GetFloat("bestScore").ToString("N2");
    }

    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }

    void TimeStop()
    {
        Time.timeScale = 0.0f;
    }
}
