﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectHandler : MonoBehaviour
{
    public int maxLevel;
    private void OnDrawGizmos()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Transform childTransform = transform.GetChild(i);
            if (i > 0) GizmosArrow.Draw(transform.GetChild(i - 1).position, childTransform.position, Color.red, 30, 20);
        }
    }
}
