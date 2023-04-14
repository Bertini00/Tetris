using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{


    [Tooltip("Generator of the level")]
    private GeneratorController _generator;

    private BlocksEnum _pieceType;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetGenerator(GeneratorController generator)
    {
        _generator = generator;
    }

    public GeneratorController GetGenerator() { return _generator; }

    public void DebugString(string s)
    {
        Debug.Log(s);
    }

    public void SetPieceType(BlocksEnum type)
    {
        _pieceType = type;
    }

    public BlocksEnum GetPieceType() { return _pieceType; }
}
