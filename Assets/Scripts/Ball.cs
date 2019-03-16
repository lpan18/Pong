using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public float speed = 30;
    private Rigidbody2D rigidBody;
    private AudioSource audioSource;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = Vector2.right * speed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.name == "LeftPaddle") || (col.gameObject.name == "RightPaddle"))
        {
            handlePaddleHit(col);
        }

        if ((col.gameObject.name == "WallBottom") || (col.gameObject.name == "WallTop"))
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.wallBloop);
        }

        if ((col.gameObject.name == "LeftGoal") || (col.gameObject.name == "RightGoal"))
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.goalBloop);

            if (col.gameObject.name == "LeftGoal")
            {
                increaseTextUIScore("RightScoreUI");
            }
            else if (col.gameObject.name == "RightGoal")
            {
                increaseTextUIScore("LeftScoreUI");
            }

            transform.position = new Vector2(0, 0); 
            rigidBody.velocity = new Vector2(-1, 0) * speed;
        }
    }

    float ballHitPaddleWhere(Vector2 ball, Vector2 paddle,
        float paddleHeight)
    {
        return (ball.y - paddle.y) / paddleHeight;
    }

    void handlePaddleHit(Collision2D col)
    {
        float y = ballHitPaddleWhere(transform.position,
            col.transform.position,
            col.collider.bounds.size.y);

        Vector2 dir = new Vector2();

        if (col.gameObject.name == "LeftPaddle")
        {
            dir = new Vector2(1, y).normalized;
            Vector2 dir2 = dir = new Vector2(1, y);
            Debug.Log("Dir : " + dir + "Dir2 : " + dir2);
        }

        if (col.gameObject.name == "RightPaddle")
        {
            dir = new Vector2(-1, y).normalized;
        }

        rigidBody.velocity = dir * speed;

        SoundManager.Instance.PlayOneShot(SoundManager.Instance.hitPaddleBloop);
    }

    void increaseTextUIScore(string textUIName)
    {
        var textUIComp = GameObject.Find(textUIName)
            .GetComponent<Text>();
        int score = int.Parse(textUIComp.text);
        score++;
        textUIComp.text = score.ToString();
    }


}