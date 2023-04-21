using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{

    [SerializeField]
    private GameObject _SpawnLocation;
    [SerializeField]
    private GameObject _SpawnLocationNextPiece;

    [SerializeField]
    private Piece _Line;
    [SerializeField]
    private Piece _Square;
    [SerializeField]
    private Piece _L;
    [SerializeField]
    private Piece _LMirrored;
    [SerializeField]
    private Piece _T;
    [SerializeField]
    private Piece _TwoTwo;
    [SerializeField]
    private Piece _TwoTwoMirrored;

    //Current active piece
    private Piece _activePiece;

    //Next piece
    private Piece _nextPiece;

    private BlocksEnum[] _blocks = { BlocksEnum.LINE, BlocksEnum.SQUARE, BlocksEnum.L, BlocksEnum.LMIRRORED, BlocksEnum.T, BlocksEnum.TWOTWO, BlocksEnum.TWOTWOMIRRORED };
    //private BlocksEnum[] _blocks = { BlocksEnum.LINE };
    private BlocksEnum _block = BlocksEnum.TWOTWO;

    private List<List<BlockController>> _blockStopped = new List<List<BlockController>>();


    public event Action MoveDownBlocks;
    public int RowDeleted;

    public void NotifyBlocksDown(int row)
    {
        RowDeleted = row;
        MoveDownBlocks?.Invoke();
    }


    // Start is called before the first frame update
    void Start()
    {
        //Setup the list
        _blockStopped.Add(new List<BlockController> { });
        _blockStopped.Add(new List<BlockController> { });
        _blockStopped.Add(new List<BlockController> { });
        _blockStopped.Add(new List<BlockController> { });
        _blockStopped.Add(new List<BlockController> { });
        _blockStopped.Add(new List<BlockController> { });
        _blockStopped.Add(new List<BlockController> { });
        _blockStopped.Add(new List<BlockController> { });
        _blockStopped.Add(new List<BlockController> { });
        _blockStopped.Add(new List<BlockController> { });
        _blockStopped.Add(new List<BlockController> { });
        _blockStopped.Add(new List<BlockController> { });
        _blockStopped.Add(new List<BlockController> { });

        SelectBlock();

        GenerateBlock();
        //Instantiate(_Line, _SpawnLocation.transform);
    }

    //Instantiate the next block selected randomly
    private void GenerateBlock()
    {

        switch (_block)
        {
            case (BlocksEnum.LINE):
                _activePiece = Instantiate(_Line, _SpawnLocation.transform);
                break;

            case (BlocksEnum.SQUARE):
                _activePiece = Instantiate(_Square, _SpawnLocation.transform);
                break;

            case (BlocksEnum.L):
                _activePiece = Instantiate(_L, _SpawnLocation.transform);
                break;
            case (BlocksEnum.LMIRRORED):
                _activePiece = Instantiate(_LMirrored, _SpawnLocation.transform);
                break;
            case (BlocksEnum.T):
                _activePiece = Instantiate(_T, _SpawnLocation.transform);
                break;
            case (BlocksEnum.TWOTWO):
                _activePiece = Instantiate(_TwoTwo, _SpawnLocation.transform);
                break;
            case (BlocksEnum.TWOTWOMIRRORED):
                _activePiece = Instantiate(_TwoTwoMirrored, _SpawnLocation.transform);
                break;

        }

        _activePiece.SetGenerator(this);



        SelectBlock();

    }
    //Select randomly which piece will be the next and create 
    //the block on the right side of the screen to show the next piece
    private void SelectBlock()
    {

        int n = UnityEngine.Random.Range(0, _blocks.Count());

        _block = _blocks[n];

        //Destroy the last block if it's not the first time creating it
        if (_nextPiece != null)
        {
            Destroy(_nextPiece.gameObject);
        }

        switch (_block)
        {
            case (BlocksEnum.LINE):
                _nextPiece = Instantiate(_Line, _SpawnLocationNextPiece.transform);
                break;

            case (BlocksEnum.SQUARE):
                _nextPiece = Instantiate(_Square, _SpawnLocationNextPiece.transform);
                break;
            case (BlocksEnum.L):
                _nextPiece = Instantiate(_L, _SpawnLocationNextPiece.transform);
                break;
            case (BlocksEnum.LMIRRORED):
                _nextPiece = Instantiate(_LMirrored, _SpawnLocationNextPiece.transform);
                break;
            case (BlocksEnum.T):
                _nextPiece = Instantiate(_T, _SpawnLocationNextPiece.transform);
                break;
            case (BlocksEnum.TWOTWO):
                _nextPiece = Instantiate(_TwoTwo, _SpawnLocationNextPiece.transform);
                break;
            case (BlocksEnum.TWOTWOMIRRORED):
                _nextPiece = Instantiate(_TwoTwoMirrored, _SpawnLocationNextPiece.transform);
                break;

        }

        //Set the piece as inactive so it doesn't move
        _nextPiece.IsActive = false;

        //Debug.Log(_block.ToString());
    }

    //Function called when the current block stops. Generate the collision,
    //add the block to the array to check and generate another block
    public void BlockStopped(PieceController piece)
    {
        //Debug.Log("Block stopped");
        GenerateCollision();
        AddBlocksToArray(piece);

        GenerateBlock();
    }

    //Generate the collision on top for each block of the piece
    public void GenerateCollision()
    {
        foreach (BlockController block in _activePiece.GetComponentsInChildren<BlockController>())
        {
            //Debug.Log("Blocco trovato " + block);
            block.CreateNextCollision();
        }
    }

    //Add each block to the row and then check if the row has been completed
    //If the row is completed, delete the row
    private void AddBlocksToArray(PieceController piece)
    {
        //Debug.Log("Block stopped dimension " + _blockStopped.Count);
        List<int> rows = new List<int>();
        foreach (BlockController block in piece.GetComponentsInChildren<BlockController>())
        {
            //Debug.Log(block);
            //y+4.5 - Offset of the y axis
            int currY = (int)System.Math.Round(block.transform.position.y + 4.5);
            _blockStopped[currY].Add(block);
            if (_blockStopped[currY].Count == 10)
            {
                //Debug.Log("Row Completed");
                rows.Add(currY);
            }
        }

        if (rows.Count > 0)
        {
            DeleteRows(rows);
        }
    }

    //Deletes the row and notify each block in the rows on top of the row deleted
    //Then rearrenges the rows
    private void DeleteRows(List<int> rows)
    {
        int temp = 0;
        foreach (int row in rows)
        {
            foreach (BlockController block in _blockStopped[row])
            {
                Destroy(block.gameObject);
            }
            NotifyBlocksDown(row - temp);
            ++temp;
        }

        RearrengeRows(rows);
    }

    //Rearrenges the arrays of rows, removing the empty rows and moving each row on top of the rows
    //just deleted down by one
    private void RearrengeRows(List<int> rows)
    {
        List<List<BlockController>> temp = new();
        for (int i = 0; i < _blockStopped.Count; i++)
        {
            if (!rows.Contains(i))
            {
                temp.Add(_blockStopped[i]);
            }
        }

        for (int i = 0; i < rows.Count; i++)
        {
            temp.Add(new List<BlockController>());
        }

        _blockStopped = temp;
    }
}
