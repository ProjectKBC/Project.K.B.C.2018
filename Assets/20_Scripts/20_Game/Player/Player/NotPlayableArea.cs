﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotPlayableArea : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
    }

}