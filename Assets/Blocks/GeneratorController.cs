using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{

    [SerializeField]
    private GameObject _SpawnLocation;

    [SerializeField]
    private Piece _Line;

    private Piece _activePiece;


    private BlocksEnum _block = BlocksEnum.LINE;


    // Start is called before the first frame update
    void Start()
    {
        GenerateBlock();
        Instantiate(_Line, _SpawnLocation.transform);
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
                //_activePiece = Instantiate(_Line, _SpawnLocation.transform);
                Debug.Log("Genero blocco");
                Instantiate(_Line, _SpawnLocation.transform);
                Debug.Log("Blocco generato");

                //  _activePiece.SetGenerator(this);
                break;
        }

        SelectBlock();

    }
    //Select randomly which block will be the next
    private void SelectBlock()
    {
        _block = BlocksEnum.LINE;
    }

    public void BlockStopped()
    {
        Debug.Log("Block stopped");
        GenerateBlock();
    }
}
