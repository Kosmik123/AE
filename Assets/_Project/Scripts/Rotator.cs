using UnityEngine;

public class Rotator : MonoBehaviour
{
    [field: SerializeField]
    public Vector3 RotationSpeed { get; set; }

    private void Update()
    {
        float dt = Time.deltaTime;
        transform.Rotate(dt * RotationSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        var previousMatrix = Gizmos.matrix;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = new Color(1, 0.75f, 0, 1);
        var rotationAxis = RotationSpeed.normalized;
        Gizmos.DrawLine(rotationAxis, -rotationAxis);
        Gizmos.matrix = previousMatrix;
    }
}
