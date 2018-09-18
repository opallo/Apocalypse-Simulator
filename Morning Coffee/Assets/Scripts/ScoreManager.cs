using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public int score;
    public int best;
	public Text scoreText;
	public Text bestText;

	static ScoreManager instance = null;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }

    }
    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void AddPoint()
    {
		score += 1;
        if (score > best)
        {
            best = score;
        }

		scoreText.text = score.ToString();
		bestText.text = "BEST: " + best.ToString();

    }


}
