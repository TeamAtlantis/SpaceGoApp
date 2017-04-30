using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class Runtime
    {
        private bool isFloating;
        public bool IsFloating
        {
            get
            {
                return isFloating;
            }
            set
            {
                isFloating = value;
            }
        }

        private int currentSateliteIndex;
        public int CurrentSateliteIndex
        {
            get
            {
                return currentSateliteIndex;
            }
            set
            {
                currentSateliteIndex = value;
            }
        }

        private float currentSateliteAngle;
        public float CurrentSateliteAngle
        {
            get
            {
                return currentSateliteAngle;
            }
            set
            {
                currentSateliteAngle = value;
            }
        }

        private float countDownIndex;
        public float CountDownIndex
        {
            get
            {
                return countDownIndex;
            }
            set
            {
                countDownIndex = value;
            }
        }

        #region singleton
        private Runtime()
        {

        }

        private static Runtime instance;

        public static Runtime Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Runtime();
                }
                return instance;
            }

            set
            {
                instance = value;
            }
        }
        #endregion
    }
}
