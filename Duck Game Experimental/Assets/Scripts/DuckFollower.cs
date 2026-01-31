using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class DuckFollower : MonoBehaviour
{
    [Header("Target")]
    public Transform player;
    public MovementController playerMovement; // assign for "follow behind" behavior

    [Header("Follow Tuning")]
    [Tooltip("How far behind the player the duck tries to stay.")]
    public float followBehindDistance = 0.75f;

    [Tooltip("Extra offset applied after behind-distance (useful for side positioning).")]
    public Vector2 extraOffset = new Vector2(-0.2f, 0f);

    [Tooltip("Max follow speed of the duck.")]
    public float maxSpeed = 4.0f;

    [Tooltip("How quickly the duck accelerates toward desired speed.")]
    public float acceleration = 25.0f;

    [Tooltip("If within this radius of desired spot, the duck stops.")]
    public float stopRadius = 0.15f;

    [Tooltip("If the duck gets too far, snap it back to the desired position.")]
    public float teleportIfFartherThan = 8.0f;

    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 currentVelocity;

    private const string AnimParam = "AnimationState";

    private enum DuckStates
    {
        walkEast = 1,
        walkSouth = 2,
        walkWest = 3,
        walkNorth = 4,
        idleSouth = 5
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    private void FixedUpdate()
    {
        if (!player) return;

        // Determine the "behind" direction.
        Vector2 behindDir = Vector2.down;
        if (playerMovement != null)
        {
            behindDir = playerMovement.LastMoveDir;
            if (behindDir.sqrMagnitude < 0.0001f)
                behindDir = Vector2.down;
        }

        Vector2 desiredPos = (Vector2)player.position - behindDir * followBehindDistance + extraOffset;

        Vector2 pos = rb.position;
        Vector2 toTarget = desiredPos - pos;
        float dist = toTarget.magnitude;

        if (dist > teleportIfFartherThan)
        {
            rb.position = desiredPos;
            currentVelocity = Vector2.zero;
            UpdateAnim(Vector2.zero);
            return;
        }

        // Stop when close enough
        Vector2 desiredVel = Vector2.zero;

        if (dist > stopRadius)
        {
            // Scale speed by distance so it eases in when approaching.
            float speedScale = Mathf.Clamp01(dist);
            desiredVel = toTarget.normalized * (maxSpeed * speedScale);
        }

        // Accelerate smoothly toward desired velocity
        currentVelocity = Vector2.MoveTowards(
            currentVelocity,
            desiredVel,
            acceleration * Time.fixedDeltaTime
        );

        rb.MovePosition(pos + currentVelocity * Time.fixedDeltaTime);

        UpdateAnim(currentVelocity);
    }

    private void UpdateAnim(Vector2 vel)
    {
        if (vel.x > 0.01f) animator.SetInteger(AnimParam, (int)DuckStates.walkEast);
        else if (vel.x < -0.01f) animator.SetInteger(AnimParam, (int)DuckStates.walkWest);
        else if (vel.y > 0.01f) animator.SetInteger(AnimParam, (int)DuckStates.walkNorth);
        else if (vel.y < -0.01f) animator.SetInteger(AnimParam, (int)DuckStates.walkSouth);
        else animator.SetInteger(AnimParam, (int)DuckStates.idleSouth);
    }
}
