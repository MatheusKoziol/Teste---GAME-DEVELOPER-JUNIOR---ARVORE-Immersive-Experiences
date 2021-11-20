using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    public BoxCollider2D SpawnArea;

    public bool enginePowerBlock;
    public bool batteringRamBlock;
    public SpriteRenderer _sprite;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomSpawn());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StartCoroutine(RandomSpawn());
        }

        if(collision.tag == "Enemy")
        {
            StartCoroutine(RandomSpawn());
        }
    }


    IEnumerator RandomSpawn()
    {
        

        Bounds bounds = SpawnArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);


        yield return new WaitForSeconds(0.1f);
        ChangeType();
    }

    void ChangeType()
    {
        int typeOfBlock = (int)Random.Range(0, 100);

        if (typeOfBlock >= 0 && typeOfBlock <= 70) // Regular Block
        {
            _sprite.color = new Color(255, 0, 0);
            enginePowerBlock = false;
            batteringRamBlock = false;

        }
        else if (typeOfBlock > 70 && typeOfBlock <= 85) // Battering Ram Block
        {
            _sprite.color = new Color(164, 164, 164);
            enginePowerBlock = false;
            batteringRamBlock = true;
        }
        else if (typeOfBlock > 85 && typeOfBlock <= 100) //Engine Block
        {
            _sprite.color = new Color(255, 239, 0);
            enginePowerBlock = true;
            batteringRamBlock = false;
        }
    }

}
