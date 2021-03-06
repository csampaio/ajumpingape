﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour {

    public float jumpForce = 15f;
	public float startingForce = 15f;

	public int sceneToLoadOnDeath = 1;

	private bool firstJump = true;
	private Vector2 initPosition;

    public static event Action<ScoreManager.Items> ItemCollectedEvent;
	public static event Action<ScoreManager.Items> UpdateScoreEvent;
    public static event Action PlayerDiedEvent;

    void Start () {
        initPosition = transform.position;
		jumpForce = GameManager.Instance.JumpForce;
		startingForce = GameManager.Instance.StartJumpForce;
		ScoreManager sm = ScoreManager.Instance;
	}

    void Reset()
    {
        transform.position = initPosition;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("banana"))
        {
			Rigidbody2D rg = GetComponent<Rigidbody2D>();
			rg.velocity = Vector2.zero;

			if (firstJump) {
				firstJump = false;
				rg.AddForce(Vector2.up * startingForce,ForceMode2D.Impulse);
			}
			rg.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
            FireItemCollectedEvent(ScoreManager.Items.banana);
			FireUpdateScoreEvent(ScoreManager.Items.banana);

        } else if (other.CompareTag("brain"))
        {
            FireItemCollectedEvent(ScoreManager.Items.brain);
        }
        else if (other.CompareTag("DestroyerWall"))
        {
            Death();
        }
    }

    void FireItemCollectedEvent(ScoreManager.Items item)
    {
        if (ItemCollectedEvent != null)
        {
            ItemCollectedEvent(item);
        }
    }

	void FireUpdateScoreEvent(ScoreManager.Items item)
	{
		if (UpdateScoreEvent != null)
		{
			UpdateScoreEvent(item);
		}
	}

    private void Death()
    {
        Debug.Log("YOU LOSE");

		ScoreManager sm = ScoreManager.Instance;
		sm.updateScore ();

        if (PlayerDiedEvent != null)
        {
            PlayerDiedEvent();
        }
        /*Reset();        
        CameraFollow cam = FindObjectOfType<CameraFollow>();
        cam.Reset(); */

		SceneManager.LoadScene (sceneToLoadOnDeath);
    }
}
