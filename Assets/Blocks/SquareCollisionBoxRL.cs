using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SquareCollisionBoxRL : MonoBehaviour
{

    private BlockController _block;


    // Start is called before the first frame update
    void Start()
    {
        _block = transform.parent.gameObject.GetComponent<BlockController>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_block == null)
        {
            _block = transform.parent.gameObject.GetComponent<BlockController>();
        }
       

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.ToString().Split(" ")[0] == "Block" && _block != null && collision.transform.parent != _block.transform.parent)
        {
            //Debug.Log("Collision detected");
            //Debug.Log("--------------");
            //Debug.Log(_block.name);
            //Debug.Log(collision);
            //Debug.Log(_block);
            //Debug.Log(collision.transform.parent);
            //Debug.Log(_block.transform.parent);
            //Debug.Log(_block.transform.parent == collision.transform.parent);
            //Debug.Log("--------------");
            if (transform.position.x - _block.transform.position.x < -0.1)
            {
                // Collision Left triggered
                //Debug.Log("Left touching");
                _block.SetCanMoveLeft(false);

            }
            else if (transform.position.x - _block.transform.position.x > 0.1)
            {
                // Collision Right triggered
                //Debug.Log("Right touching");
                _block.SetCanMoveRight(false);
            }
            else
            {
                //Debug.Log("Error collision");
            }
        }

    }

}
