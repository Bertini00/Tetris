using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{

    [SerializeField]
    private GameObject _SpawnLocation;

    [SerializeField]
    private GameObject _Line;



    private BlocksEnum _block = BlocksEnum.LINE;


    // Start is called before the first frame update
    void Start()
    {
        GenerateBlock();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Instantiate the next block
    public void GenerateBlock()
    {

        Debug.Log("Generate block");

        switch (_block)
        {
            case (BlocksEnum.LINE):
                Instantiate(_Line, _SpawnLocation.transform);
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
    }
}
