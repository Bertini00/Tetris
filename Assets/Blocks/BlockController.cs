using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BlockController : MonoBehaviour
{

    private PieceController _piece;

    [SerializeField]
    private SquareCollisionBox _CollisionBox;

    public bool _collided = false;

    

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



        if (collision.GetComponent<SquareCollisionBox>()?.GetType().ToString() == "SquareCollisionBox")
        {
            //Debug.Log("Collision detected " + collision.GetComponent<SquareCollisionBox>().GetType());
            //_piece.CollisionDetected();
            _collided = true;
        }
         
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {


        if (collision.GetComponent<SquareCollisionBox>()?.GetType().ToString() == "SquareCollisionBox")
        {
            //Debug.Log("Collision detected " + collision.GetComponent<SquareCollisionBox>().GetType());
            //_piece.CollisionUndetected();
            _collided = false;
        }


    }

    public void CreateNextCollision()
    {
        //Debug.Log("Create next collision");
        SquareCollisionBox box = Instantiate(_CollisionBox, this.transform);
        box.transform.Translate(0, 1, 0, Space.World);
        
    }
}
