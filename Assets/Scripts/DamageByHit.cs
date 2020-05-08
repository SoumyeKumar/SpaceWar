using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageByHit : MonoBehaviour
{

	public int health = 1;
	public GameManager gameManager;
	public int score = 0;
	public Text scoreText, highscoreText;
	public float invulnPeriod = 0;
	float invulnTimer = 0;
	int correctLayer;
	public HealthBar healthbar;

	SpriteRenderer spriteRend;

	void Start()
	{
		correctLayer = gameObject.layer;
		healthbar.SetMaxHealth(health);
		highscoreText.text = "HIGHSCORE:" + PlayerPrefs.GetInt("HIGHSCORE").ToString();


		spriteRend = GetComponent<SpriteRenderer>();

		if (spriteRend == null)
		{
			spriteRend = transform.GetComponentInChildren<SpriteRenderer>();

			if (spriteRend == null)
			{
				Debug.LogError("Object '" + gameObject.name + "' has no sprite renderer.");
			}
		}
	}

	void OnTriggerEnter2D()
	{
		health--;
		healthbar.SetHealth(health);
		if (invulnPeriod > 0)
		{
			invulnTimer = invulnPeriod;
			gameObject.layer = 10;
		}
	}

	void Update()
	{
		score++;
		scoreText.text = "Score:" + score.ToString();
		if (invulnTimer > 0)
		{
			invulnTimer -= Time.deltaTime;

			if (invulnTimer <= 0)
			{
				gameObject.layer = correctLayer;
				if (spriteRend != null)
				{
					spriteRend.enabled = true;
				}
			}
			else
			{
				if (spriteRend != null)
				{
					spriteRend.enabled = !spriteRend.enabled;
				}
			}
		}

		if (health <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		Destroy(gameObject);
		gameManager.GameOver();
		OnGameOver();
	}
	private void OnGameOver()
	{
		var highScore = PlayerPrefs.GetInt("HIGHSCORE");
		highScore = score > highScore ? score : highScore;
		PlayerPrefs.SetInt("HIGHSCORE", highScore);
	}

}
