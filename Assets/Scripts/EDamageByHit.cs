using UnityEngine;
using System.Collections;

public class EDamageByHit : MonoBehaviour
{

	public int health = 1;
	public float invulnPeriod = 0;
	float invulnTimer = 0;
	int correctLayer;

	SpriteRenderer spriteRend;

	void Start()
	{
		correctLayer = gameObject.layer;


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

		if (invulnPeriod > 0)
		{
			invulnTimer = invulnPeriod;
			gameObject.layer = 10;
		}
	}

	void Update()
	{

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

	public void Die()
	{

		Destroy(gameObject);

	}

}
