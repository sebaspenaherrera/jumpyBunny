using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    // ATTRIBUTES
    // Sample blocks from where to create new blocks
    public List<LevelBlock> legoBlocks = new List<LevelBlock>();
    // Blocks added to the game
    List<LevelBlock> currentBlocks = new List<LevelBlock>();

    public Transform initialPoint;
    private static LevelGenerator _sharedInstance;
    public static LevelGenerator sharedInstance {
        get {
            return _sharedInstance;
        }
    }

    public byte initialBlockNumber = 2;

    //Initialize before Start
    private void Awake()
    {
        _sharedInstance = this;
        for (byte i = 0; i < initialBlockNumber; i++) {
            addNewBlock();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // METHODS
    // Add new bloc
    public void addNewBlock() {
        int randNumber = Random.Range(0, legoBlocks.Count);
        //Same than create an object in C# but for Unity is used "Instantiate" to create unity block that is not just code
        //Similar to var block = new LevelBlock();
        var block = Instantiate(legoBlocks[randNumber]);
        block.transform.SetParent(transform);

        Vector3 blockPosition = Vector3.zero;
        if (currentBlocks.Count == 0)
        {
            blockPosition = initialPoint.position;
        }
        else {
            int lastBlockPos = currentBlocks.Count - 1;
            blockPosition = currentBlocks[lastBlockPos].exitPoint.position;
        }

        block.transform.position = blockPosition;
        currentBlocks.Add(block);
    }

}