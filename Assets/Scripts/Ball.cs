using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public  float       Speed = 1f;
    private Rigidbody2D _rb2D;
    private Vector2     _direction;

    public void SetDirection(Vector2 dir)
    {
        _rb2D.velocity        = Vector2.zero;
        _rb2D.angularVelocity = 0f;
        _direction            = dir;
        _rb2D.AddForce(_direction * Speed);
    }

    public void Stop()
    {
        _rb2D.velocity        = Vector2.zero;
        _rb2D.angularVelocity = 0f;
    }

    private void OnEnable()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        _direction = Vector2.Reflect(_direction, col.contacts[0]
            .normal) * Random.Range(5f, 10f);
        _direction = _direction.normalized;
    }
}