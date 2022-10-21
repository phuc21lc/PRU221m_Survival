using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScrolling : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    Vector2Int currentTilePosition = new Vector2Int(0, 0);
    [SerializeField] Vector2Int playerTilePosition;

    Vector2Int onTileGridPlayerPos;

    [SerializeField] float tileSize = 20f;
    GameObject[,] terrainTiles;

    [SerializeField] int terrainTileHorizontalCount;
    [SerializeField] int terrainTileVerticalCount;

    [SerializeField] int fieldOfVisionHeight = 3;
    [SerializeField] int fieldOfVisionWidth = 3;

    private void Awake()
    {
        terrainTiles = new GameObject[terrainTileHorizontalCount, terrainTileVerticalCount];
    }
    public void Add(GameObject tileGameObject, Vector2Int tilePosition)
    {
        terrainTiles[tilePosition.x, tilePosition.y] = tileGameObject;
    }

    private void Update()
    {
        playerTilePosition.x = (int)(playerTransform.position.x / tileSize);
        playerTilePosition.y = (int)(playerTransform.position.y / tileSize);

        playerTilePosition.x -= playerTransform.position.x < 0 ? 1 : 0;
        playerTilePosition.y -= playerTransform.position.y < 0 ? 1 : 0;


        if (currentTilePosition != playerTilePosition)
        {
            currentTilePosition = playerTilePosition;

            onTileGridPlayerPos.x = CalculatePositionOnAxis(onTileGridPlayerPos.x, true);
            onTileGridPlayerPos.y = CalculatePositionOnAxis(onTileGridPlayerPos.y, false);
            UpdateTileOnSCreen();
        }
    }

    private void UpdateTileOnSCreen()
    {
        for (int posX = -(fieldOfVisionWidth / 2); posX <= (fieldOfVisionWidth / 2); posX++)
        {
            for (int posY = -(fieldOfVisionHeight / 2); posY <= (fieldOfVisionHeight / 2); posY++)
            {
                int tileToUpdate_X = CalculatePositionOnAxis(playerTilePosition.x + posX, true);
                int tileToUpdate_y = CalculatePositionOnAxis(playerTilePosition.y + posY, false);

                GameObject tile = terrainTiles[tileToUpdate_X, tileToUpdate_y];
                tile.transform.position = CalculateTilePosition(
                    playerTilePosition.x + posX,
                    playerTilePosition.y + posY
                    );
            }
        }
    }

    private Vector3 CalculateTilePosition(int x, int y)
    {
        return new Vector3(x * tileSize, y * tileSize, 0f);
    }

    private int CalculatePositionOnAxis(float currentValue, bool horizontal)
    {
        if (horizontal)
        {
            if (currentValue >= 0)
            {
                currentValue = currentValue % terrainTileHorizontalCount;
            }
            else
            {
                currentValue += 1;
                currentValue = terrainTileHorizontalCount - 1 + currentValue % terrainTileHorizontalCount;
            }
        }
        else
        {

            if (currentValue >= 0)
            {
                currentValue = currentValue % terrainTileVerticalCount;
            }
            else
            {
                currentValue += 1;
                currentValue = terrainTileVerticalCount - 1 + currentValue % terrainTileVerticalCount;
            }
        }

        return (int)currentValue;
    }
}
