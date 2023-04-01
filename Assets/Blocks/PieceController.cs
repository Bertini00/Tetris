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


    // Start is called before the first frame update
    void Start()
    {


        Debug.Log("Sono stato generato");
        //Non funziona, chidere al prof
        //_generator = GetComponent<GeneratorController>();

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

        if (!_bottom)
            transform.position = Vector3.down * 1 + transform.position;
        else
        {
            SetBlocked(true);

            GetGenerator().BlockStopped();
        }


        //_blocked = true;

    }

    private void Rotate()
    {
        // Debug.Log("Rotate");

        transform.Rotate(0, 0, 90);


    }

    private void MoveHorizontally(DirectionEnum direction)
    {
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


            if (Input.GetKey(KeyCode.RightArrow))
            {
                MoveHorizontally(DirectionEnum.RIGHT);
                _timeToMove = _MoveDelay;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                MoveDown();
                _timeToMove = _MoveDelay;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
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

    public void CollisionDetected()
    {
        //Debug.Log("Collision detected");
        SetBottom(true);
        //Debug.Log("Generator is: " + GetGenerator());

        //GetGenerator().GenerateBlock();
    }

    private void SetBlocked(bool blocked)
    {
        _blocked = blocked;
    }

    private void SetBottom(bool bottom)
    {
        _bottom = bottom;
    }
}
