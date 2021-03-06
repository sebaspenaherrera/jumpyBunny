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
        CreateInitialBlocks();
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
    public void AddNewBlock(bool initialBlock = false) {

        //If first block is needed, then select block0, else randomize level
        int randNumber = initialBlock? 0:Random.Range(0, legoBlocks.Count);
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

    public void RemoveOldBlock() {
        var oldBlock = currentBlocks[0];
        currentBlocks.Remove(oldBlock);
        Destroy(oldBlock.gameObject);
    }

    public void RemoveAllBlocks() {
        while (currentBlocks.Count > 0) {
            RemoveOldBlock();
        }
    }

    public void CreateInitialBlocks() {
        if (currentBlocks.Count > 0) {
            return;
        }
        for (byte i = 0; i < initialBlockNumber; i++)
        {
            AddNewBlock(true);
        }
    }

}
