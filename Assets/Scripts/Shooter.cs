using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
//using System.Diagnostics;
using UnityEngine;

public class Shooter : MonoBehaviour
{

	public Vector3 bulletOffset = new Vector3(0, 0.5f, 0);
	public Transform bulletSpawnPoint;
	public GameObject bulletPrefab;
	int bulletLayer;

	public float fireDelay = 0.25f;
	float cooldownTimer = 0;

	void Start()
	{
		bulletLayer = gameObject.layer;
	}

	// Update is called once per frame
	void Update()
	{
		cooldownTimer -= Time.deltaTime;

		if (Input.GetButton("Fire1") && cooldownTimer <= 0)
		{
			// SHOOT!
			cooldownTimer = fireDelay;

			Vector3 offset = transform.rotation * bulletOffset + transform.up * 2;
			Debug.Log(transform.forward);
			Debug.Log(offset);

			GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
			bulletGO.layer = bulletLayer;
		}
	}
}