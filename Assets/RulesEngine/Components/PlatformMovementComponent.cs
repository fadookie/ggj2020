using UnityEngine;

namespace RulesEngine.Components
{
    public class PlatformMovementComponent : MonoBehaviour
    {
        public Vector3 initialPosition;
        public float oscillationSpeed = 1f;
        public float oscillationAmplitude = 3f;
    }
}