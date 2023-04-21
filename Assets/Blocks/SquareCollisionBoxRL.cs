using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SquareCollisionBoxRL : MonoBehaviour
{

    [Tooltip("The block parent of this collision box")]
    private BlockController _block;


    // Start is called before the first frame update
    void Start()
    {
        _block = transform.parent.gameObject.GetComponent<BlockController>();
    }


    //On trigger enter checks if the collision is a block and is not from the same piece
    //If the collision is verified, calculate the difference of the X position between block and collision box
    //to know the direction of the collision and block that movement direction
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.GetComponent<BlockController>());
        if (collision.GetComponent<BlockController>() != null && _block != null && collision.transform.parent != _block.transform.parent)
        {
            if (transform.position.x - _block.transform.position.x < -0.1)
            {
                // Collision Left triggered
                _block.SetCanMoveLeft(false);

            }
            else if (transform.position.x - _block.transform.position.x > 0.1)
            {
                // Collision Right triggered
                _block.SetCanMoveRight(false);
            }
        }

    }

}
