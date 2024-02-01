using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaloonDart
{

    public class Level : MonoBehaviour
    {
        public bool showAdAfterThisLevel = false;
        public LevelType levelType;
        public int maxNumberOfBalloonsInThisLevel = 9;
        public int currentBaloonPopped = 0;
        public bool isLevelEnded = false;
        public static Level Instance;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                return;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    [Serializable]
    public class LevelType
    {
        public bool isBombLevel;
        public bool isNumberOfMovesLevel;
    }
}