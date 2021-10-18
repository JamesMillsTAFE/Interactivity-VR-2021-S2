using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRFramework.Pointers
{
    public class StraightPointerRenderer : PointerRenderer
    {
        private const float TracerWidth = 0.025f;

        [SerializeField] private float cursorScaleFactor = 0.1f;

        public override void Render(RaycastHit _hit, bool _didHit)
        {
            if(_didHit)
            {
                CalculateDirAndDst(transform.position, _hit.point, out Vector3 dir, out float dist);

                // Set the tracer position to the midpoint of the parent pos and the end point
                Vector3 midpoint = Vector3.Lerp(transform.position, _hit.point, 0.5f);
                tracer.transform.position = midpoint;

                // Scale the tracer to between the endpoint and this point
                tracer.transform.localScale = new Vector3(TracerWidth, TracerWidth, dist);

                // Set the cursor to the endpoint and scale it
                cursor.transform.position = _hit.point;
                cursor.transform.localScale = Vector3.one * cursorScaleFactor;
            }
            else
            {
                // Set the cursor and tracer position / scale values base on some distant endpoint
                CalculateDirAndDst(transform.position, transform.position + transform.forward * 100f, out Vector3 dir, out float dist);

                Vector3 midPoint = Vector3.Lerp(transform.position, transform.position + dir * dist, 0.5f);
                tracer.transform.position = midPoint;
                tracer.transform.localScale = new Vector3(TracerWidth, TracerWidth, dist);

                cursor.transform.position = transform.position + transform.forward * 100f;
                cursor.transform.localScale = Vector3.one * cursorScaleFactor;
            }
        }

        protected override void CreateCursor(GameObject _container)
        {
            cursor = customCursor == null ? GameObject.CreatePrimitive(PrimitiveType.Sphere) : Instantiate(customCursor);
            cursor.transform.parent = _container.transform;
        }

        protected override void CreateTracer(GameObject _container)
        {
            tracer = customTracer == null ? GameObject.CreatePrimitive(PrimitiveType.Cube) : Instantiate(customTracer);
            tracer.transform.parent = _container.transform;
        }
    }
}
