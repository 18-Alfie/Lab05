using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerScript : MonoBehaviour
{
    int score = 0;
    public float timer;
    int coinsRemaining;

    public Text scoreText;
    public Text timerText;

    public bool onGround = true;

    ParticleSystem coinParticle;

    AudioSource footsteps;
    AudioSource jump;
    AudioSource land;

    // Start is called before the first frame update
    void Start()
    {
        coinParticle = GetComponent<ParticleSystem>();

        timerText.text = "Timer: " + timer;

        scoreText.text = "Score: " + score;

        coinsRemaining = GameObject.FindGameObjectsWithTag("Coin").Length;

        AudioSource[] audios = GetComponents<AudioSource>();

        footsteps = audios[1];
        jump = audios[2];
        land = audios[3];
        
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

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            footsteps.Play();
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            footsteps.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump.Play();
            onGround = false;
        }

        if (transform.position.y <= 0.98)
        {
            onGround = true;
            if (onGround == true)
            {
                land.Play();
            }
            land.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        if (other.gameObject.CompareTag("Coin"))
        {
            score += 10;
            scoreText.text = "Score: " + score;

            coinsRemaining--;

            if (coinsRemaining == 0)
            {
                SceneManager.LoadScene("GameWin");
            }

            coinParticle.Play();

        }

        if (other.gameObject.CompareTag("Water"))
        {
            SceneManager.LoadScene("GameLose");
        }
    }
}
