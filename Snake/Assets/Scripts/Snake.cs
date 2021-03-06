using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{

    Vector2 _direction = Vector2.right;

    List<Transform> _segments;

    public Transform segmentPrefab;

    [Tooltip("The bigger the value, the slower the snake moves")]
    public float TimeBetweenMoves;

    public SpriteRenderer _sprite;

    public bool invulnerable;

    public GameObject GameOverUI;
    // Start is called before the first frame update
    void Awake()
    {
        _segments = new List<Transform>();
        _segments.Add(this.transform);
        StartCoroutine(UpdateSnake());
        invulnerable = false;
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

    public IEnumerator UpdateSnake()
    {
        for (int i = _segments.Count - 1; i > 0; i--) // Making the tail follow the head
        {
            _segments[i].position = _segments[i - 1].position;
        }

        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + _direction.x, Mathf.Round(this.transform.position.y) + _direction.y, 0.0f); //movement

        yield return new WaitForSeconds(TimeBetweenMoves);
        StartCoroutine(UpdateSnake());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Food")
        {
            Grow();

            if (collision.GetComponent<Food>().enginePowerBlock) //If the snake eats an Engine power block
            {
                TimeBetweenMoves -= 0.007f; // The Snake speeds up
            }

            if (collision.GetComponent<Food>().batteringRamBlock)//If the snake eats a Battering ram block
            {
                _sprite.color = Color.gray; //The color of the snake`s head changes to match the block it just ate
                invulnerable = true; //The snake become invulnerable for its next hit
            }
        }

        if(collision.tag == "Enemy")
        {
            if (invulnerable)
            {
                invulnerable = false;
                _sprite.color = new Color(0, 151, 255);
            }
            else
            {
                Core_Game_Manager.Instance.StopTime();
                GameOverUI.SetActive(true);
            }
        }

        if(collision.tag == "Wall")
        {
            invulnerable = false;
            Core_Game_Manager.Instance.StopTime();
            GameOverUI.SetActive(true);
        }
    }


    void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
        TimeBetweenMoves += 0.005f;
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
