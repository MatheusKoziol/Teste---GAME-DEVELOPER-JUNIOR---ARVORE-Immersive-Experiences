using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{

    Vector2 _direction = Vector2.right;

    List<Transform> _segments;

    public Transform segmentPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _segments = new List<Transform>();
        _segments.Add(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) // Turning Left
        {
            TurnLeft();
        }

        if (Input.GetKeyDown(KeyCode.D)) // Turning Right
        {
            TurnRight();
        }

    }

    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + _direction.x, Mathf.Round(this.transform.position.y) + _direction.y, 0.0f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Food")
        {
            Grow();
        }

        if(collision.tag == "Enemy")
        {
            // game over
        }
    }


    void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }

    void TurnRight()
    {
        if (_direction == Vector2.right)
        {
            _direction = Vector2.down;
        }
        else if (_direction == Vector2.down)
        {
            _direction = Vector2.left;
        }
        else if (_direction == Vector2.left)
        {
            _direction = Vector2.up;
        }
        else if (_direction == Vector2.up)
        {
            _direction = Vector2.right;
        }
    }

    void TurnLeft()
    {
        if (_direction == Vector2.right)
        {
            _direction = Vector2.up;
        }
        else if (_direction == Vector2.up)
        {
            _direction = Vector2.left;
        }
        else if (_direction == Vector2.left)
        {
            _direction = Vector2.down;
        }
        else if (_direction == Vector2.down)
        {
            _direction = Vector2.right;
        }
    }

}
