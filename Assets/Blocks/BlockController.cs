using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockController : MonoBehaviour
{

    private PieceController _piece;

    // Start is called before the first frame update
    void Start()
    {
        _piece = GetComponentInParent<PieceController>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.attachedRigidbody.ToString().Split(" ")[0] != "Block")
            _piece.CollisionDetected();
    }

    public void CreateNextCollision()
    {
        Debug.Log("Create next collision");
    }
}
