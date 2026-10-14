using UnityEngine;
using UnityEngine.Windows;

public class Player_GroundCheck : MonoBehaviour
{
    private Player_Motor MOTOR;

    public float playerHeight;

    public RaycastHit slopeHit;
    public float maxSlopeAngle;

    private void Awake()
    {
        MOTOR = GetComponent<Player_Motor>();
    }

    void FixedUpdate()
    {
        Vector3 spherePosition = new Vector3(
            transform.position.x,
            transform.position.y - MOTOR.GroundedOffset,
            transform.position.z
        );

        MOTOR.Grounded = Physics.CheckSphere(
            spherePosition,
            MOTOR.GroundedRadius,
            MOTOR.GroundLayers,
            QueryTriggerInteraction.Ignore
        );

        // small velocity check to avoid false positives while jumping up
        if (MOTOR.Grounded && MOTOR.rb.linearVelocity.y > 0.1f)
        {
            MOTOR.Grounded = false;
        }

        /*if (OnSlope())
        {
            MOTOR.Grounded = true;
        }*/
    }

    /*public bool OnSlope()
    {
        if (Physics.Raycast(MOTOR.transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(input.movement, slopeHit.normal).normalized;
    }*/
}
