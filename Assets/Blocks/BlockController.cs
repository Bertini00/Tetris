using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BlockController : MonoBehaviour
{

    private PieceController _piece;

    [SerializeField]
    private SquareCollisionBox _CollisionBox;

    public bool _collided = false;

    private GeneratorController _generator;

    [SerializeField]
    private bool IsWall;



    // Start is called before the first frame update
    void Start()
    {
        _piece = GetComponentInParent<PieceController>();

        _generator = FindFirstObjectByType<GeneratorController>();
        if (_generator != null && !IsWall)
        {
            //The block has to move down in case a row gets filled and deleted

            //Debug.Log("Generator not null");
            //Add the observer to this block
            _generator.MoveDownBlocks += MoveDownBlock;

        }
    }

    //On trigger enter checks if the collision is a collisionBox,
    //if it is, the next move down will block the piece and create another
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<SquareCollisionBox>() != null && _piece != null)
        {
            //_piece.CollisionDetected();
            _collided = true;
            _piece.SetBlockCollided(true);
        }


    }

    //Generate the collision of this block on top of it
    public void CreateNextCollision()
    {
        //Debug.Log("Create next collision");
        SquareCollisionBox box = Instantiate(_CollisionBox, this.transform);
        box.transform.Translate(0, 1, 0, Space.World);

    }

    //Set if the piece can move in the direction LEFT
    public void SetCanMoveLeft(bool canMoveLeft)
    {
        _piece?.SetCanMoveLeft(canMoveLeft);
    }

    //Set if the piece can move in the direction RIGHT
    public void SetCanMoveRight(bool canMoveRight)
    {
        _piece?.SetCanMoveRight(canMoveRight);
    }


    //Move down the block, called when a row gets completed and deleted
    private void MoveDownBlock()
    {
        int rowDeleted = _generator.RowDeleted;

        if ((int)System.Math.Round(transform.position.y + 4.5) > rowDeleted)
        {
            transform.Translate(0, -1, 0, Space.World);

        }

    }

    //On destroy remove the observer, otherwise it would give error
    private void OnDestroy()
    {
        if (_generator != null)
            _generator.MoveDownBlocks -= MoveDownBlock;
    }

}
