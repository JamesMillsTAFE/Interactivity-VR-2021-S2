using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRFramework.Pointers
{
    public abstract class PointerRenderer : MonoBehaviour
    {
        [SerializeField] protected Color valid = Color.green;
        [SerializeField] protected Color invalid = Color.red;

        [Header("Custom Rendering")]
        [SerializeField] protected GameObject customCursor;
        [SerializeField] protected GameObject customTracer;

        protected Pointer pointer;

        // The parent object for the cursor and tracer
        protected GameObject pointerContainer;
        // Cursor is the endpoint of the pointer
        protected GameObject cursor;
        // Tracer is the line to the endpoint
        protected GameObject tracer;

        private Renderer cursorRenderer;
        private Renderer tracerRenderer;

        public void Setup(Pointer _pointer)
        {
            pointer = _pointer;
            CreatePointer();

            cursorRenderer = cursor.GetComponent<Renderer>();
            tracerRenderer = tracer.GetComponent<Renderer>();
            cursor.SetActive(false);
            tracer.SetActive(false);
        }

        public abstract void Render(RaycastHit _hit, bool _didHit);

        public void SetVisibility(bool _visible)
        {
            pointerContainer.SetActive(_visible);
        }

        public void SetValidState(bool _valid)
        {
            // Update the colors of the renderers to the corresponding state colors
            cursorRenderer.material.color = _valid ? valid : invalid;
            tracerRenderer.material.color = _valid ? valid : invalid;
        }

        protected void CalculateDirAndDst(Vector3 _start, Vector3 _end, out Vector3 _dir, out float _distance)
        {
            Vector3 heading = _end - _start;
            _distance = heading.magnitude;
            _dir = heading / _distance;
        }

        protected virtual void CreatePointer()
        {
            pointerContainer = new GameObject($"[Pointer {pointer.Controller.Source}]");
            pointerContainer.transform.SetParent(transform);

            CreateCursor(pointerContainer);
            CreateTracer(pointerContainer);
        }

        protected abstract void CreateCursor(GameObject _container);
        protected abstract void CreateTracer(GameObject _container);
    } 
}
