﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuePlayingMusic : MonoBehaviour
{
    void Update()
    {
        DontDestroyOnLoad(gameObject);
    }
}
