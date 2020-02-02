using UnityEngine;

namespace RulesEngine.Components
{
    public enum OscillationDirection
    {
        Horizontal,
        Vertical
    }
    
    public class PlatformMovementComponent : MonoBehaviour
    {
        public Vector3 initialPosition;
        public float oscillationSpeed = 1f;
        public float oscillationAmplitude = 3f;
        public OscillationDirection oscillationDirection = OscillationDirection.Vertical;
    }
}