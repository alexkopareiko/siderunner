using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Game
{
    public class RoadGenerator : MonoBehaviour
    {
        public PoolManager.PoolType blockPoolType; // Set the pool type for blocks in the Inspector
        public float blockWidth = 2.6f; // Width of the block
        public float blockHeight = 1.0f; // Height of the block
        public int initialBlocks = 10;  // Number of blocks to start with
        public float generationOffset = 10f; // Distance from camera to generate new blocks
        public Transform player; // Reference to the player character

        private List<GameObject> blocks = new List<GameObject>();
        [SerializeField] private float _xInitialOffset = -1f;
        [SerializeField] private float _yInitialOffset = -0.2f;

        private void Awake()
        {
            player = Player.Instance.transform;
        }

        void Start()
        {
            // Generate initial blocks
            for (int i = 0; i < initialBlocks; i++)
            {
                GenerateBlock();
            }
        }

        void Update()
        {
            // Generate new blocks as the player moves
            if (player.position.x + generationOffset > blocks[0].transform.position.x)
            {
                GenerateBlock();
                DeactivateBlock();
            }
        }

        void GenerateBlock()
        {
            Debug.Log(blocks.Count);
            Vector3 spawnPosition;

            if (blocks.Count == 0)
            {
                spawnPosition = new Vector3(0, -0.449999988f, 0);
            }
            else
            {
                Vector3 blockPrevPosition = blocks[blocks.Count - 1].transform.position;
                float y = Random.Range(-blockHeight * 5f, blockHeight / 2);
                Vector3 pos = new Vector3(blockPrevPosition.x + 1.25f, blockPrevPosition.y + y, 0);
                spawnPosition = pos;
            }

            Debug.Log(spawnPosition);

            GameObject newBlock = PoolManager.Instance.GetObjectFromPool(blockPoolType);
            newBlock.transform.position = spawnPosition;
            newBlock.SetActive(true);
            blocks.Add(newBlock);
            newBlock.name = "Block " + blocks.Count;
        }

        void DeactivateBlock()
        {
            GameObject oldBlock = blocks[0];
            oldBlock.SetActive(false);
            blocks.RemoveAt(0);
        }
    }

}
