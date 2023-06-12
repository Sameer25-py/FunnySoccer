using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Bat : MonoBehaviour
    {
        private Camera _camera;

        [SerializeField] private Vector2 clampedXRange, clampedYRange;

        [SerializeField] private bool lockX, lockY;

        private bool        _enableMovement = false;
        private Rigidbody2D _rb2D;

        private void OnEnable()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Ray          ray   = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit2D = Physics2D.Raycast(ray.origin, ray.direction);
                if (hit2D.collider != null && hit2D.collider.gameObject == gameObject)
                {
                    _enableMovement = true;
                }
                else
                {
                    _enableMovement = false;
                }
            }
        }

        private void FixedUpdate()
        {
            if (_enableMovement)
            {
                Vector2 screenMousePos = Input.mousePosition;
                Vector2 worldMousePos  = _camera.ScreenToWorldPoint(screenMousePos);

                worldMousePos.x = Mathf.Clamp(worldMousePos.x, clampedXRange.x, clampedXRange.y);
                worldMousePos.y = Mathf.Clamp(worldMousePos.y, clampedYRange.x, clampedYRange.y);

                transform.position = new Vector2(
                    lockX ? transform.position.x : worldMousePos.x,
                    lockY ? transform.position.y : worldMousePos.y
                );
            }
        }
    }
}