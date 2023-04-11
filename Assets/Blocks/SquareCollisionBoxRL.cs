using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareCollisionBoxRL : MonoBehaviour
{

    private PieceController _piece;

    private bool _entered = false;

    // Start is called before the first frame update
    void Start()
    {
        _piece = GetComponentInParent<PieceController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)

    {
        
        if (collision.attachedRigidbody.ToString().Split(" ")[0] == "Block")
        {
            //Debug.Log(collision);
            if (this.transform.position.x - _piece.transform.position.x < 0)
            {
                // Collision Left triggered
                Debug.Log("Left touching");
                _piece.SetCanMoveLeft(false);
                
            }
            else if (this.transform.position.x - _piece.transform.position.x > 0)
            {
                // Collision Right triggered
                Debug.Log("Right touching");
                _piece.SetCanMoveRight(false);
            }
            
        }
        _entered = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        

        if (collision.attachedRigidbody.ToString().Split(" ")[0] == "Block")
        {
            if (this.transform.position.x - _piece.transform.position.x < 0)
            {
                // Collision Left triggered (exit)
                Debug.Log("Left not touching anymore");
                _piece.SetCanMoveLeft(true);

            }
            else if (this.transform.position.x - _piece.transform.position.x > 0)
            {
                // Collision Right triggered (exit)
                Debug.Log("Right not touching anymore");
                _piece.SetCanMoveRight(true);
            }

            _entered = false;
        }

    }
}
