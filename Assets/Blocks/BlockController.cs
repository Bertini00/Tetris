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
            //Debug.Log("Generator not null");
            _generator.MoveDownBlocks += MoveDownBlock;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_piece == null)
        {
            _piece = GetComponentInParent<PieceController>();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {



        if (collision.GetComponent<SquareCollisionBox>()?.GetType().ToString() == "SquareCollisionBox" && _piece != null)
        {
            //Debug.Log("Collision detected " + collision.GetComponent<SquareCollisionBox>().GetType());
            //_piece.CollisionDetected();
            _collided = true;
            _piece.SetBlockCollided(true);
        }
         
            
    }


    public void CreateNextCollision()
    {
        //Debug.Log("Create next collision");
        SquareCollisionBox box = Instantiate(_CollisionBox, this.transform);
        box.transform.Translate(0, 1, 0, Space.World);
        
    }

    public void SetCanMoveLeft(bool canMoveLeft)
    {
        _piece?.SetCanMoveLeft(canMoveLeft);
    }

    public void SetCanMoveRight(bool canMoveRight)
    {
        _piece?.SetCanMoveRight(canMoveRight);
    }

    
    private void MoveDownBlock()
    {
        int rowDeleted = _generator.RowDeleted;

        if ((int)System.Math.Round(transform.position.y + 4.5) > rowDeleted)
        {
            transform.Translate(0, -1, 0, Space.World);
            
        }
        
    }

    private void OnDestroy()
    {
        if (_generator != null)
            _generator.MoveDownBlocks -= MoveDownBlock;
    }

}
