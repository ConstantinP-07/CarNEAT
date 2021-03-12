﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Copyright 2021 CB
public class Follow : MonoBehaviour
{

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float smoothSpeed = 2f;

    void FixedUpdate()
    {
        Vector3 smoothedMove = Vector3.Lerp(transform.position, target.position, smoothSpeed * Time.deltaTime);
        smoothedMove = new Vector3(smoothedMove.x, smoothedMove.y, -10);

        transform.position = smoothedMove;
    }
}
