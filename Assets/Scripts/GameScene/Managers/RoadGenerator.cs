using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class RoadGenerator : MonoBehaviour
    {
        [SerializeField] private PoolManager.PoolType _blockPoolType; // Set the pool type for _blocks in the Inspector
        [SerializeField] private float _blockWidth = 2.6f; // Width of the block
        [SerializeField] private float _blockHeight = 1.0f; // Height of the block
        [SerializeField] private int _initialBlocks = 10;  // Number of _blocks to start with
        [SerializeField] private float _generationOffset = 10f; // Distance from camera to generate new _blocks
        [SerializeField] private float _xInitialOffset = -1f;
        [SerializeField] private float _yInitialOffset = -0.2f;

        private List<GameObject> _blocks = new List<GameObject>();
        private Transform _player; // Reference to the _player character

        private void Awake()
        {
            _player = Player.Instance.transform;
        }

        private void Start()
        {
            // Generate initial _blocks
            for (int i = 0; i < _initialBlocks; i++)
            {
                GenerateBlock();
            }
        }

        private void Update()
        {
            // Generate new _blocks as the _player moves
            if (_player.position.x + _generationOffset > _blocks[0].transform.position.x)
            {
                GenerateBlock();
                DeactivateBlock();
            }
        }

        private void GenerateBlock()
        {
            Vector3 spawnPosition;

            if (_blocks.Count == 0)
            {
                spawnPosition = new Vector3(0, -0.449999988f, 0);
            }
            else
            {
                Vector3 blockPrevPosition = _blocks[_blocks.Count - 1].transform.position;
                float y = Random.Range(-_blockHeight * 5f, _blockHeight / 2);
                Vector3 pos = new Vector3(blockPrevPosition.x + 1.25f, blockPrevPosition.y + y, 0);
                spawnPosition = pos;
            }

            GameObject newBlock = PoolManager.Instance.GetObjectFromPool(_blockPoolType);
            newBlock.transform.position = spawnPosition;
            newBlock.SetActive(true);
            _blocks.Add(newBlock);
            newBlock.name = "Block " + _blocks.Count;

            bool shouldSpawnEnemy = Random.Range(0, 100) < 40; // 20% chance to spawn an enemy
            if (_blocks.Count > 5 && shouldSpawnEnemy)
            {
                GameObject enemy = EnemyGenerator.Instance.SpawnEnemy();
                enemy.transform.position = new Vector3(spawnPosition.x + _blockWidth / 1.2f, spawnPosition.y + _blockHeight / 2f, 0);
                enemy.SetActive(true);
            }
        }

        private void DeactivateBlock()
        {
            GameObject oldBlock = _blocks[0];
            oldBlock.SetActive(false);
            _blocks.RemoveAt(0);
        }
    }

}
