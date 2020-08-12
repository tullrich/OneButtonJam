using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
    public float speed;
    public bool _moveLeft;
    public float killX;

    void Start()
    {
        UpdateSpriteDir();
    }

    // Update is called once per frame
    void Update()
    {
        // Move in the proper direction
        Vector3 pos = transform.position;
        pos.x += Time.deltaTime * speed * (_moveLeft ? -1 : 1);
        transform.position = pos;

        // Destroy self when out of range 
        if ((_moveLeft && transform.position.x < killX)
            || (!_moveLeft && transform.position.x > killX))
        {
            Debug.Log("Kill");
            Destroy(gameObject);
        }
    }
    
    public bool MoveLeft
    {
        get { return _moveLeft; }
        set
        {
            _moveLeft = value;
            UpdateSpriteDir();
        }
    }

    void UpdateSpriteDir()
    {
        transform.localScale = new Vector3(_moveLeft ? -1 : 1, 1, 1);
    }
}
