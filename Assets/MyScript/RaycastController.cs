using UnityEngine;
    [RequireComponent(typeof(BoxCollider2D))]
    public class RaycastController : MonoBehaviour
    {
        public LayerMask collisionMask;                 //Unity collisionMask against we'll check the GameObjects collisions

        public float skinWidth = .155f;
        private const float dstBetweenRays = .2f;      //Distance between raycast rays. Represented by red/yellow gizmo lines

        [HideInInspector]
        public int horizontalRayCount;
        [HideInInspector]
        public int verticalRayCount;

        [HideInInspector]
        public float horizontalRaySpacing;
        [HideInInspector]
        public float verticalRaySpacing;

        [HideInInspector]
        public BoxCollider2D[] collList;

        [HideInInspector]
        public RaycastOrigins raycastOrigins;

        public virtual void Awake()
        {
            collList = GetComponents<BoxCollider2D>();
        }

        public virtual void Start()
        {
            CalculateRaySpacing();
        }

        public void UpdateRaycastOrigins()
        {

            //Adjusting the skinWidth of all colliders
            Bounds[] boundsList = new Bounds[collList.Length];
            for (int i = 0; i < collList.Length; i++)
            {
                boundsList[i] = collList[i].bounds;
                boundsList[i].Expand(skinWidth * -2);
            }

            //Top Origins
            raycastOrigins.topLeft = new Vector2(boundsList[0].min.x, boundsList[0].max.y);
            raycastOrigins.topRight = new Vector2(boundsList[0].max.x, boundsList[0].max.y);

            //Middle origins
            int boundIndex = raycastOrigins.maxWidthBoundIndex;
            raycastOrigins.midLeft = new Vector2(boundsList[boundIndex].min.x, boundsList[boundIndex].max.y);
            raycastOrigins.midRight = new Vector2(boundsList[boundIndex].max.x, boundsList[boundIndex].max.y);

            //Bottom origins
            raycastOrigins.bottomLeft = new Vector2(boundsList[collList.Length - 1].min.x, boundsList[collList.Length - 1].min.y);
            raycastOrigins.bottomRight = new Vector2(boundsList[collList.Length - 1].max.x, boundsList[collList.Length - 1].min.y);
        }

        public void CalculateRaySpacing()
        {
            //Adjusting the skinWidth of all colliders and getting the total sum of the collider's height
            raycastOrigins.maxHeightBound = 0f;
            Bounds[] boundsList = new Bounds[collList.Length];
            for (int i = 0; i < collList.Length; i++)
            {
                boundsList[i] = collList[i].bounds;
                boundsList[i].Expand(skinWidth * -2);
                raycastOrigins.maxHeightBound += collList[i].bounds.size.y;
            }

            // Looking for the widest bounds of all colliders
            raycastOrigins.maxWidthBound = 0f;
            raycastOrigins.maxWidthBoundIndex = 0;
            for (int i = 0; i < boundsList.Length; i++)
            {
                if (boundsList[i].size.x > raycastOrigins.maxWidthBound)
                {
                    raycastOrigins.maxWidthBound = boundsList[i].size.x;
                    raycastOrigins.maxWidthBoundIndex = i;
                }
            }

            // Calculate the total rays
            horizontalRayCount = Mathf.RoundToInt(raycastOrigins.maxHeightBound / dstBetweenRays);
            verticalRayCount = Mathf.RoundToInt(raycastOrigins.maxWidthBound / dstBetweenRays);

            //Calculating total space rays
            horizontalRaySpacing = raycastOrigins.maxHeightBound / (horizontalRayCount - 1);
            verticalRaySpacing = boundsList[boundsList.Length - 1].size.x / (verticalRayCount - 1);
        }

        //public void turnCollidersXdir() {
        //    for (int i = 0; i < collList.Length; i++) {
        //        collList[i].offset = new Vector2(collList[i].offset.x * -1, collList[i].offset.y);
        //    }
        //}

        public struct RaycastOrigins
        {
            public float maxWidthBound, maxHeightBound;
            public int maxWidthBoundIndex;
            public Vector2 topLeft, topRight;
            public Vector2 midLeft, midRight;
            public Vector2 bottomLeft, bottomRight;
        }
    }
