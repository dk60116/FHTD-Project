﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AmazingAssets.AdvancedDissolve.ExampleScripts
{
    [ExecuteInEditMode]
    public class LookAt : MonoBehaviour
    {
        public Transform target;
        
        // Update is called once per frame
        void Update()
        {
            if(target != null)
                transform.LookAt(target);
        }
    }
}