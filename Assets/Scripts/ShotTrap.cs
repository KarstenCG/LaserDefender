﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotTrap : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collider)
    {
        Object.Destroy(collider.gameObject);
    }
}
