using System;
using UnityEngine;

namespace Gallery.Content.Misc
{
    public class AutoMoveAndRotate : MonoBehaviour
    {
        public Vector3AndSpace MoveUnitsPerSecond;
        public Vector3AndSpace RotateDegreesPerSecond;
        public bool IgnoreTimescale;
        private float _m_lastRealTime;


        private void Start()
        {
            _m_lastRealTime = Time.realtimeSinceStartup;
        }


        private void Update()
        {
            float deltaTime = Time.deltaTime;
            if (IgnoreTimescale)
            {
                deltaTime = (Time.realtimeSinceStartup - _m_lastRealTime);
                _m_lastRealTime = Time.realtimeSinceStartup;
            }
            transform.Translate(MoveUnitsPerSecond.Value*deltaTime, MoveUnitsPerSecond.Space);
            transform.Rotate(RotateDegreesPerSecond.Value*deltaTime, MoveUnitsPerSecond.Space);
        }


        [Serializable]
        public class Vector3AndSpace
        {
            public Vector3 Value;
            public Space Space = Space.Self;
        }
    }
}
