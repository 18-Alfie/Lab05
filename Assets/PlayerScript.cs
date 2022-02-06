using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerScript : MonoBehaviour
{
    int score = 0;
    public float timer;

    public Text scoreText;
    public Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        timerText.text = "Timer: " + timer;

        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            timerText.text = "Timer: " + Mathf.RoundToInt(timer);
        }
        else if (timer <= 0)
        {
            SceneManager.LoadScene("GameLose");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        if (other.gameObject.CompareTag("Coin"))
        {
            score += 10;
            scoreText.text = "Score: " + score;
        }
    }
}
