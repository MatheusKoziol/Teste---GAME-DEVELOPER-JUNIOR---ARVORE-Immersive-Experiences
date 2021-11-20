using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySnakeAI : MonoBehaviour
{
    Vector2 _direction = Vector2.left;

    List<Transform> _segments;

    public Transform segmentPrefab;

    [Tooltip("The bigger the value, the slower the snake moves")]
    public float TimeBetweenMoves;

    // Start is called before the first frame update
    void Awake()
    {
        _segments = new List<Transform>();
        _segments.Add(this.transform);
        StartCoroutine(UpdateSnake());
    }



    public IEnumerator UpdateSnake()
    {
        for (int i = _segments.Count - 1; i > 0; i--) // Making the tail follow the head
        {
            _segments[i].position = _segments[i - 1].position;
        }

        DetectFood();
        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + _direction.x, Mathf.Round(this.transform.position.y) + _direction.y, 0.0f); //movement

        yield return new WaitForSeconds(TimeBetweenMoves);
        StartCoroutine(UpdateSnake());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            Grow();
        }

        if (collision.tag == "Player" || collision.tag == "Wall" || collision.tag == "Enemy")
        {
            ResetSnake();
        }
    }

    void ResetSnake()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);

        this.transform.position = new Vector3(20, 10, 0);
    }

    void DetectFood()
    {
        Transform food = FindObjectOfType<Food>().transform;

        if(food.position.x < transform.position.x) //The food is to the left
        {
            _direction = Vector2.left;
        }
        else if(food.position.x > transform.position.x) //The food is to the right
        {
            _direction = Vector2.right;
        }


        if(food.position.y < transform.position.y) //The food is below
        {
            _direction = Vector2.down;
        }
        else if(food.position.y > transform.position.y) //The food is above
        {
            _direction = Vector2.up;
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
