using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PieceController : Piece
{


    public bool IsActive = false;



    [SerializeField]
    [Tooltip("Time to wait before the player can move")]
    [Range(0.1f, 3f)]
    private double _MoveDelay = 0.5;

    [SerializeField]
    [Tooltip("Time to wait before the block go down by one block")]
    [Range(1f, 5f)]
    private double _GoDelay = 2;

    [SerializeField]
    [Tooltip("Time to wait before the block can rotate")]
    [Range(0.1f, 5f)]
    private double _RotateDelay = 0.3;

    [Tooltip("Can move right or left")]
    private double _timeToMove;
    [Tooltip("Can go down")]
    private double _timeToGo;
    [Tooltip("Can rotate")]
    private double _timeToRotate;
    [Tooltip("Input Delay")]
    private double _inputDelay;
    [Tooltip("If the block has arrived at the bottom")]
    private bool _bottom = false;
    [Tooltip("If the block has reached the bottom and can't move down")]
    private bool _blocked = false;

    private bool _canMoveHorizzontally = true;

    private bool _canMoveRight = true;
    private bool _canMoveLeft = true;
    private bool _canMoveDown = true;
    private bool _canRotate = true;

    private bool _blockCollided = false;




    
    // Start is called before the first frame update
    void Start()
    {

        if (!CheckSpawnPosition())
        {
            //Debug.Log("Cant spawn block, quitting");
            Application.Quit();
        }
        //SetGenerator(GetComponent<GeneratorController>());

        _timeToMove = _MoveDelay;

        _timeToGo = _GoDelay;

        _timeToRotate = _RotateDelay;

        _inputDelay = 0.05;

    }

    private void ResetInputDelay()
    {
        _inputDelay = 0.05;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsActive) return;

        if (_inputDelay <= 0)
        {
            if (_timeToGo <= 0 && !_blocked)
            {
                MoveDown();
                _timeToGo = _GoDelay;
                ResetInputDelay();
            }

            if (_timeToMove <= 0 && !_blocked)
            {
                InputMoveController();
            }

            if (_timeToRotate <= 0 && !_blocked)
            {
                InputRotateController();
            }
        }

        _timeToGo -= Time.deltaTime;
        _timeToMove -= Time.deltaTime;
        _timeToRotate -= Time.deltaTime;
        _inputDelay -= Time.deltaTime;
    }

    private void MoveDown()
    {
        _canMoveHorizzontally = false;
        _canRotate = false;
        
        if (_blockCollided)
        {
            _bottom = true;
        }

        if (!_bottom)
        {

            ResetMovementController();
            transform.position = Vector3.down * 1 + transform.position;
            _timeToMove = _MoveDelay;
            
            if (!CanStay())
            {
                
                FixPosition();    
            }

        }

        else
        {
            SetBlocked(true);

            GetGenerator().BlockStopped(this);
        }
        _canMoveHorizzontally = true;
        _canRotate = true;

        //_blocked = true;

    }

    private void Rotate()
    {
        _canMoveHorizzontally = false;
        _canMoveDown = false;
        transform.Rotate(0, 0, 90);

        bool lastMoveLeft = ResetMoveLeft();
        bool lastMoveRight = ResetMoveRight();
        bool lastBlockCollided = ResetBlockCollided();
        //transform.Rotate(0, 0, 360);
        if (!CanStay())
        {
            
            transform.Rotate(0, 0, -90);
            _canMoveLeft = lastMoveLeft;
            _canMoveRight = lastMoveRight;
            _blockCollided = lastBlockCollided;
            Debug.Log("Fixing position");
            if (!FixPosition()) 
            {
                //Debug.Log("Can move reset");
                Debug.Log("Position fixed");
                _canMoveLeft = lastMoveLeft;
                _canMoveRight = lastMoveRight;
                _blockCollided = lastBlockCollided;
            }
            
        }
        else
        {
            Debug.Log("Can rotate and rotated");
        }

        _canMoveHorizzontally = true;
        _canMoveDown = true;

    }

    private void MoveHorizontally(DirectionEnum direction)
    {
        if (_canMoveHorizzontally)
        {
            ResetMovementController();
            switch (direction)
            {
                case DirectionEnum.LEFT:
                    //Debug.Log("Move Left");
                    transform.position = transform.position + Vector3.left * 1;
                    break;
                case DirectionEnum.RIGHT:
                    //Debug.Log("Move Right");
                    transform.position = transform.position + Vector3.right * 1;
                    break;
            }
        }
    }

    private void InputMoveController()
    {
        if (!_blocked)
        {
            _canRotate = false;
            if (Input.GetKey(KeyCode.RightArrow) && _canMoveRight && _canMoveHorizzontally)
            {
                ResetBlockCollided();
                MoveHorizontally(DirectionEnum.RIGHT);
                _timeToMove = _MoveDelay;
                ResetInputDelay();
            }
            else if (Input.GetKey(KeyCode.DownArrow) && _canMoveDown)
            {
                MoveDown();
                _timeToMove = _MoveDelay;
                _timeToGo = _GoDelay;
                ResetInputDelay();
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && _canMoveLeft && _canMoveHorizzontally)
            {
                ResetBlockCollided();
                MoveHorizontally(DirectionEnum.LEFT);
                _timeToMove = _MoveDelay;
                ResetInputDelay();
            }
            _canRotate = true;
        }
    }

    private void InputRotateController()
    {

        if (Input.GetKey(KeyCode.UpArrow) && _canRotate)
        {
            _canMoveHorizzontally = false;
            _canMoveDown = false;

            Rotate();
            _timeToRotate = _RotateDelay;
            ResetInputDelay();

            _canMoveHorizzontally = true;
            _canMoveDown = true;
        }
    }



    private void SetBlocked(bool blocked)
    {
        _blocked = blocked;
    }

    private void SetBottom(bool bottom)
    {
        _bottom = bottom;
    }

    public void SetCanMoveRight(bool value)
    {
        _canMoveRight = value;
    }
    public void SetCanMoveLeft(bool value)
    {
        _canMoveLeft = value;
    }

    public bool GetCanMoveRight()
    {
        return _canMoveRight;
    }

    public bool GetCanMoveLeft()
    {
        return _canMoveLeft;
    }

    private void ResetMovementController()
    {
        _canMoveLeft = true;
        _canMoveRight = true;
        //Debug.Log("Reset can variable");
    }

    private bool ResetMoveRight()
    {
        bool temp = _canMoveRight;
        _canMoveRight = true;
        return temp;
    }
    private bool ResetMoveLeft()
    {
        bool temp = _canMoveLeft;
        _canMoveLeft = true;
        return temp;
    }

    public void SetBlockCollided(bool collided)
    {
        _blockCollided = collided;
    }

    private bool ResetBlockCollided()
    {
        bool temp = _blockCollided;
        _blockCollided = false;
        return temp;
    }


    //Check if the rotate piece can stay in that position without triggering any collision
    private bool CanStay()
    {
        foreach (BlockController block in this.GetComponentsInChildren<BlockController>())
        {
            Collider2D[] occupants = Physics2D.OverlapBoxAll(block.transform.position, Vector2.zero, 0f, LayerMask.GetMask("Block"));

            foreach (Collider2D occupant in occupants)
            {
                //Debug.Log("CanStay");
                //Debug.Log(occupant);
                if (occupant != null && occupant.transform.parent != transform)
                {
                    //Debug.Log("Occupant found, not myself");
                    //Debug.Log(occupant);
                    //Debug.Log(this);
                    return false;
                }
                
            }
        }
        return true;
    }

    //Try to adjust position up to 2 times in each direction
    private bool FixPosition()
    {
        
        //First checks up, then on the left and right
        int[][] array = new int[8][] {
            new int[3] { 0, 1, 0 },
            new int[3] { -1, 1, 0 },
            new int[3] { 1, 1, 0 },
            new int[3] { 0, 2, 0 },
            new int[3] { -1, 0, 0 },
            new int[3] { 1, 0, 0 },
            new int[3] { -2, 0, 0 },
            new int[3] { 2, 0, 0 },
        };

        for (int i = 0; i < array.Length; i++)
        {
            
            transform.Translate(array[i][0], array[i][1], 0, Space.World);

            if (!RotateAndCheck())
            {
                
                transform.Translate(-array[i][0], -array[i][1], 0, Space.World);    
            }
            else
            {
                //Debug.Log("Position found");
                
                return true;
            }
        }
        
        return false;

    }

    //Returns true if the piece can stay where it is
    private bool RotateAndCheck()
    {
        
        transform.Rotate(0, 0, 90);
        if (!CanStay())
        {
            
            transform.Rotate(0, 0, -90);
            return false;
        }
        return true;
    }

    private bool CheckSpawnPosition()
    {
        Collider2D[] occupants = Physics2D.OverlapBoxAll(transform.position, Vector2.zero, 0f, LayerMask.GetMask("Block"));

        foreach (Collider2D occupant in occupants)
        {
            if (occupant != null && occupant.transform.parent != transform)
            {
                Debug.Log("Occupant found, not myself");
                //Debug.Log(occupant.transform.parent);
                //Debug.Log(this);
                return false;
            }
            
        }
        return true;
    }
}
