using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    public Text scoreText;
    public Text livesText;

    private Rigidbody rb;

    private int count;
    private int countScore;
    private int countLives;

    Vector3 defaultPosition = new Vector3(0, 3, 0);

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        countScore = 0;
        countLives = 3;
        SetCountText();
        SetScoreText();
        SetLivesText();
        winText.text = "";
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (countLives != 0)
        {
            if (other.gameObject.CompareTag("Pick Up"))
            {
                other.gameObject.SetActive(false);
                count = count + 1;
                countScore = countScore + 1;
                SetCountText();
                SetScoreText();
            }
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.SetActive(false);
                // removed to allow for lives
                // countScore = countScore - 1; 
                countLives = countLives - 1;
                SetLivesText();
                rb.velocity = Vector3.zero;
                ResetPlayerPosition();
            }
        }


    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count == 12)
        {
            defaultPosition = new Vector3(45, 3, 0);
            countScore = 0;
            SetScoreText();
            ResetPlayerPosition();
        }
        if (count >= 20)
        {
            winText.text = "You Win!";
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + countScore.ToString();
    }

    void SetLivesText()
    {
            livesText.text = "Lives: " + countLives.ToString();
        if (countLives == 0)
        {
            this.gameObject.SetActive(false);
            winText.text = "You Lose.";
        }
    }

    private void ResetPlayerPosition()
    {
        transform.position = defaultPosition;
    }
}
