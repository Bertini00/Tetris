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
    private Piece _Line;
    [SerializeField]
    private Piece _Square;
    [SerializeField]
    private Piece _L;

    private Piece _activePiece;

    private BlocksEnum[] _blocks = { BlocksEnum.LINE, BlocksEnum.SQUARE, BlocksEnum.L };
    private BlocksEnum _block = BlocksEnum.LINE;


    // Start is called before the first frame update
    void Start()
    {
        GenerateBlock();
        //Instantiate(_Line, _SpawnLocation.transform);
    }

    // Update is called once per frame
    void Update()
    {

    }
    //Instantiate the next block
    private void GenerateBlock()
    {


        switch (_block)
        {
            case (BlocksEnum.LINE):
                _activePiece = Instantiate(_Line, _SpawnLocation.transform);
                //Debug.Log("Genero blocco");
                //Instantiate(_Line, _SpawnLocation.transform);
                //Debug.Log("Blocco generato");
                _activePiece.SetPieceType(BlocksEnum.LINE);
                break;

            case (BlocksEnum.SQUARE):
                _activePiece = Instantiate(_Square, _SpawnLocation.transform);
                //Debug.Log("Genero blocco");
                //Instantiate(_Line, _SpawnLocation.transform);
                //Debug.Log("Blocco generato");
                _activePiece.SetPieceType(BlocksEnum.SQUARE);
                break;
            case (BlocksEnum.L):
                _activePiece = Instantiate(_L, _SpawnLocation.transform);
                //Debug.Log("Genero blocco");
                //Instantiate(_Line, _SpawnLocation.transform);
                //Debug.Log("Blocco generato");
                _activePiece.SetPieceType(BlocksEnum.L);
                break;

        }

        _activePiece.SetGenerator(this);



        SelectBlock();

    }
    //Select randomly which block will be the next
    private void SelectBlock()
    {

        int n = Random.Range(0, _blocks.Count());

        _block = _blocks[n];
        Debug.Log(_block.ToString());
    }

    public void BlockStopped()
    {
        //Debug.Log("Block stopped");
        GenerateCollision();
        GenerateBlock();
    }

    public void GenerateCollision()
    {
        foreach (BlockController block in _activePiece.GetComponentsInChildren<BlockController>())
        {
            //Debug.Log("Blocco trovato " + block);
            block.CreateNextCollision();
        }
    }
}
