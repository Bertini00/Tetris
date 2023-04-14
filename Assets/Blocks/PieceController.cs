using System;
using System.Collections;
using System.Collections.Generic;
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
    [Tooltip("If the block has arrived at the bottom")]
    private bool _bottom = false;
    [Tooltip("If the block has reached the bottom and can't move down")]
    private bool _blocked = false;

    private bool _canMoveHorizzontally = true;

    private bool _canMoveRight = true;
    private bool _canMoveLeft = true;

    private bool _blockCollided = false;

    private int _timesRotated = 0;



    // Start is called before the first frame update
    void Start()
    {




        //SetGenerator(GetComponent<GeneratorController>());

        _timeToMove = _MoveDelay;

        _timeToGo = _GoDelay;

        _timeToRotate = _RotateDelay;


    }

    // Update is called once per frame
    void Update()
    {
        if (!IsActive) return;

        if (_timeToGo <= 0 && !_blocked)
        {
            MoveDown();
            _timeToGo = _GoDelay;
        }

        if (_timeToMove <= 0 && !_blocked)
        {
            InputMoveController();
        }

        if (_timeToRotate <= 0 && !_blocked)
        {
            InputRotateController();
        }

        _timeToGo -= Time.deltaTime;
        _timeToMove -= Time.deltaTime;
        _timeToRotate -= Time.deltaTime;
    }

    private void MoveDown()
    {
        _canMoveHorizzontally = false;
        //foreach (BlockController block in this.GetComponentsInChildren<BlockController>())
        //{
        //    if (block._collided)
        //    {
        //        _bottom = true;
        //        Debug.Log("Trovato bottom");
        //        break;
        //    }
        //}

        if (_blockCollided)
        {
            _bottom = true;
        }

        if (!_bottom)
        {

            ResetMovementController();
            transform.position = Vector3.down * 1 + transform.position;
            _timeToMove = _MoveDelay;

        }

        else
        {
            SetBlocked(true);

            GetGenerator().BlockStopped();
        }
        _canMoveHorizzontally = true;

        //_blocked = true;

    }

    private void Rotate()
    {
        transform.Rotate(0, 0, 90);
    }

    private void MoveHorizontally(DirectionEnum direction)
    {
        if (_canMoveHorizzontally)
            ResetMovementController();
        switch (direction)
        {
            case DirectionEnum.LEFT:
                //Debug.Log("Move Left");
                transform.localPosition = transform.localPosition + Vector3.left * 1;
                break;
            case DirectionEnum.RIGHT:
                //Debug.Log("Move Right");
                transform.localPosition = transform.localPosition + Vector3.right * 1;
                break;
        }
    }

    private void InputMoveController()
    {
        if (!_blocked)
        {
            if (Input.GetKey(KeyCode.RightArrow) && _canMoveRight)
            {
                ResetBlockCollided();
                MoveHorizontally(DirectionEnum.RIGHT);
                _timeToMove = _MoveDelay;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                MoveDown();
                _timeToMove = _MoveDelay;
                _timeToGo = _GoDelay;
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && _canMoveLeft)
            {
                ResetBlockCollided();
                MoveHorizontally(DirectionEnum.LEFT);
                _timeToMove = _MoveDelay;
            }
        }
    }

    private void InputRotateController()
    {

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Rotate();
            _timeToRotate = _RotateDelay;
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
    }

    public void SetBlockCollided(bool collided)
    {
        _blockCollided = collided;
    }

    private void ResetBlockCollided()
    {
        _blockCollided = false;
    }


}
