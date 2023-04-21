using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{


    [SerializeField]
    [Tooltip("Time to wait before the player can move")]
    [Range(0.1f, 3f)]
    public double _MoveDelay = 0.5;

    [SerializeField]
    [Tooltip("Time to wait before the block go down by one block")]
    [Range(1f, 5f)]
    public double _GoDelay = 2;

    [SerializeField]
    [Tooltip("Time to wait before the block can rotate")]
    [Range(0.1f, 5f)]
    public double _RotateDelay = 0.3;

    [Tooltip("Generator of the level")]
    private GeneratorController _generator;

    private BlocksEnum _pieceType;

    public bool IsActive = false;
    public void SetGenerator(GeneratorController generator)
    {
        _generator = generator;
    }

    public GeneratorController GetGenerator() { return _generator; }

    public void DebugString(string s)
    {
        Debug.Log(s);
    }

}
