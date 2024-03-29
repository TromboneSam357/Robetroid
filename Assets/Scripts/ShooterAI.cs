﻿using System.Collections;
using UnityEngine;

public class ShooterAI : MonoBehaviour
{
    public GameObject plazmaBullet;
    public Transform firePoint;
    public int bulletsShotPerRound;
    public int shootWaitTime;

    private bool canShoot;
    private float timeSinceLastShot = 0f;

    private void Update()
    {
        if (canShoot && timeSinceLastShot >= shootWaitTime)
        {
            StartCoroutine(Shoot());
            timeSinceLastShot = 0f;
        }

        timeSinceLastShot += Time.deltaTime;
    }

    private void OnBecameVisible()
    {
        if (Camera.current.CompareTag("MainCamera"))
            canShoot = true;
    }

    private void OnBecameInvisible()
    {
        if (Camera.current.CompareTag("MainCamera"))
            canShoot = false;
    }

    private IEnumerator Shoot()
    {
        for (int i = 0; i < bulletsShotPerRound; i++)
        {
            FindObjectOfType<AudioManager>().Play("Shoot");
            Instantiate(plazmaBullet, firePoint.position, firePoint.rotation);
            yield return new WaitForSeconds(.25f);
        }
    }
}