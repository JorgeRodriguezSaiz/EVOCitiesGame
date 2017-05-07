using UnityEngine;

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Filename: Controller2D.cs                                                                                      //         
//                                                                                                                 //
// Authors:                                                                                                        //
// v1 - Sebastian Lague (Original 2D physics) https://github.com/SebLague/2DPlatformer-Tutorial                    //
// v2 - Marcos Staib (Double Jumping and minor issues fixing) http://corruptedsmilestudio.blogspot.com/            //
// Last Modified Date(v2): 09 Feb 2012                                                                             //
// v3 - Sergio Vozmediano Ávilas (Climbing and other features added, like hurt movement. Support for 3 colliders.  //
//                                Code optimization. Raycast lines change between red and yellow,                  //
//                                depends on wheter the player is colliding or not with any other objects).        //
// Last Modified Date(v3): 11 Oct 2016                                                                             //
//                                                                                                                 //
// Copyright (c) 2016 Sergio Vozmediano Ávilas                                                                     //
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

public class Controller2D : RaycastController
{
    public float fallingThroughPlatformResetTimer = 0.1f;
    private float maxClimbAngle = 80f;
    private float maxDescendAngle = 80f;

    public CollisionInfo collisions;
    [HideInInspector]
    public Vector2 playerInput;

    public override void Start()
    {
        base.Start();

        //Right Facing
        collisions.faceDir = 1;
    }

    public void Move(Vector2 moveAmount, bool standingOnPlatform = false)
    {
        Move(moveAmount, Vector2.zero, standingOnPlatform);
    }

    public void Move(Vector2 moveAmount, Vector2 input, bool standingOnPlatform = false)
    {
        UpdateRaycastOrigins();
        collisions.Reset();
        collisions.moveAmountOld = moveAmount;
        playerInput = input;

        // Horizontal movement
        if (moveAmount.x != 0)
        {
            collisions.faceDirOld = collisions.faceDir;
            collisions.faceDir = (int)Mathf.Sign(moveAmount.x);

            //If the faceDirOld and faceDir are not equal,
            //it means the player's changed the x axis direction
            //so the colliders have to turn with him 
            //if (collisions.faceDir != collisions.faceDirOld) {
            //    turnCollidersXdir();
            //}
        }

        if (moveAmount.y < 0)
        {
            DescendSlope(ref moveAmount);
        }

        HorizontalCollisions(ref moveAmount);

        if (moveAmount.y != 0)
        {
            VerticalCollisions(ref moveAmount);
        }

        transform.Translate(moveAmount, Space.World);

        if (standingOnPlatform)
        {
            collisions.below = true;
        }
    }

    private void HorizontalCollisions(ref Vector2 moveAmount)
    {
        float directionX = collisions.faceDir;
        float rayLength = Mathf.Abs(moveAmount.x) + skinWidth;

        if (Mathf.Abs(moveAmount.x) < skinWidth)
        {
            rayLength = 2 * skinWidth;
        }

        for (int i = 0; i < horizontalRayCount; i++)
        {
            //// Number of rayCast for each collider
            //int collSeparator = horizontalRayCount / collList.Length;
            //Vector2 rayOrigin = new Vector2(0, 0);
            //if (i >= 0 && i <= collSeparator) {
            //    rayOrigin = (directionX == -1) ? new Vector2(raycastOrigins.bottomLeft.x, raycastOrigins.bottomLeft.y) : new Vector2(raycastOrigins.bottomRight.x, raycastOrigins.bottomRight.y);
            //}
            //else if (i > collSeparator && i <= collSeparator * 2) {
            //    rayOrigin = (directionX == -1) ? new Vector2(raycastOrigins.midLeft.x, raycastOrigins.bottomLeft.y) : new Vector2(raycastOrigins.midRight.x, raycastOrigins.bottomRight.y);

            //}
            //else if (i > collSeparator * 2 && i < collSeparator * 3) {
            //    rayOrigin = (directionX == -1) ? new Vector2(raycastOrigins.topLeft.x, raycastOrigins.bottomLeft.y) : new Vector2(raycastOrigins.topRight.x, raycastOrigins.bottomRight.y);
            //}

            Vector2 rayOrigin = (directionX == -1) ? new Vector2(raycastOrigins.midLeft.x, raycastOrigins.bottomLeft.y) : new Vector2(raycastOrigins.midRight.x, raycastOrigins.bottomRight.y);
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
            Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);

            if (hit)
            {
                collisions.horizontalCollisionRayCount++;
                Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.yellow);

                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

                if (i == 0 && slopeAngle <= maxClimbAngle)
                {
                    if (collisions.descendingSlope)
                    {
                        collisions.descendingSlope = false;
                        moveAmount = collisions.moveAmountOld;
                    }
                    float distanceToSlopeStart = 0f;
                    if (slopeAngle != collisions.slopeAngleOld)
                    {
                        distanceToSlopeStart = hit.distance - skinWidth;
                        moveAmount.x -= distanceToSlopeStart * directionX;
                    }
                    ClimbSlope(ref moveAmount, slopeAngle);
                    moveAmount.x += distanceToSlopeStart * directionX;
                }

                if (!collisions.climbingSlope || slopeAngle > maxClimbAngle)
                {
                    moveAmount.x = (hit.distance - skinWidth) * directionX;
                    rayLength = hit.distance;

                    if (collisions.climbingSlope)
                    {
                        moveAmount.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(moveAmount.x);
                    }

                    collisions.left = directionX == -1;
                    collisions.right = directionX == 1;
                }
            }
        }
    }

    private void ClimbSlope(ref Vector2 moveAmount, float slopeAngle)
    {
        float moveDistance = Mathf.Abs(moveAmount.x);
        float climbmoveAmountY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;

        if (moveAmount.y <= climbmoveAmountY)
        {
            moveAmount.y = climbmoveAmountY;
            moveAmount.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(moveAmount.x);
            collisions.below = true;
            collisions.climbingSlope = true;
            collisions.slopeAngle = slopeAngle;
        }
    }

    private void DescendSlope(ref Vector2 moveAmount)
    {
        float directionX = Mathf.Sign(moveAmount.x);
        Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);

        if (hit)
        {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            if (slopeAngle != 0 && slopeAngle <= maxDescendAngle)
            {
                if (Mathf.Sign(hit.normal.x) == directionX)
                {
                    if (hit.distance - skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(moveAmount.x))
                    {
                        float moveDistance = Mathf.Abs(moveAmount.x);
                        float descendmoveAmountY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
                        moveAmount.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(moveAmount.x);
                        moveAmount.y -= descendmoveAmountY;

                        collisions.slopeAngle = slopeAngle;
                        collisions.descendingSlope = true;
                        collisions.below = true;
                    }
                }
            }
        }
    }

    private void VerticalCollisions(ref Vector2 moveAmount)
    {
        float directionY = Mathf.Sign(moveAmount.y);
        float rayLength = Mathf.Abs(moveAmount.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + moveAmount.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);

            if (hit)
            {
                collisions.verticalCollisionRayCount++;
                Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.yellow);
                if (hit.collider.tag == "Through")
                {
                    if (directionY == 1 || hit.distance == 0)
                    {
                        continue;
                    }
                    if (collisions.fallingThroughPlatform)
                    {
                        continue;
                    }
                    if (playerInput.y == -1)
                    {
                        collisions.fallingThroughPlatform = true;
                        Invoke("ResetFallingThroughPlatform", fallingThroughPlatformResetTimer);
                        continue;
                    }
                }

                moveAmount.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                if (collisions.climbingSlope)
                {
                    moveAmount.x = moveAmount.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(moveAmount.x);
                }

                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
            }
        }

        if (collisions.climbingSlope)
        {
            float directionX = Mathf.Sign(moveAmount.x);
            rayLength = Mathf.Abs(moveAmount.x) + skinWidth;
            Vector2 rayOrigin = ((directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight) + Vector2.up * moveAmount.y;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            if (hit)
            {
                Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.yellow);
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                if (slopeAngle != collisions.slopeAngle)
                {
                    moveAmount.x = (hit.distance * skinWidth) * directionX;
                    collisions.slopeAngle = slopeAngle;
                }
            }
        }
    }

    private void ResetFallingThroughPlatform()
    {
        collisions.fallingThroughPlatform = false;
    }

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;
        public int horizontalCollisionRayCount, verticalCollisionRayCount;
        public bool climbingSlope;
        public bool descendingSlope;
        public float slopeAngle, slopeAngleOld;
        public Vector2 moveAmountOld;
        public int faceDirOld;
        public int faceDir;
        public bool fallingThroughPlatform;

        public void Reset()
        {
            above = below = false;
            left = right = false;
            climbingSlope = false;
            descendingSlope = false;

            slopeAngleOld = slopeAngle;
            slopeAngle = 0f;
            horizontalCollisionRayCount = verticalCollisionRayCount = 0;
        }
    }
}
