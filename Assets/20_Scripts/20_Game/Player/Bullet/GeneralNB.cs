﻿using UnityEngine;

public class GeneralNB : MonoBehaviour
{
    [SerializeField]
    private float shotSpeed = 3.2f;
    private Vector3 shotStartPos;
    [SerializeField]
    private float shotRange = 70.0f;

    private void Start()
    {
        shotStartPos = this.transform.position;
    }

    private void Update()
    {
        this.transform.position += Vector3.up * shotSpeed * Time.deltaTime * 60;

        if (shotRange < Mathf.Abs(shotStartPos.y - this.transform.position.y))
        {
            Destroy(gameObject);
        }
    }
}
