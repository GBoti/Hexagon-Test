using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridLayout : MonoBehaviour
{
    [Header("Grid Settings")]
    public Vector2Int gridSize;

    [Header("Tile Settings")]
    public float size = 1f;
    public bool isFlatTopped;

    public Material ground;
    public Material water;
    public Material backGround;

    public GameObject hex;

    private readonly float sqrt3 = Mathf.Sqrt(3);
    private List<Hex> hexes = new List<Hex>();
    private List<GameObject> backgroundHexes = new List<GameObject>();

    private void OnEnable()
    {
        LayoutGrid();
    }

    public void LayoutGrid()
    {
        DestroyGrid();
        Debug.Log($"Displaying grid {gridSize.x}, {gridSize.y}");

        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                GameObject tile = Instantiate(hex, GetPositionForHexFromCoordinate(new Vector2Int(x, y)), transform.rotation * Quaternion.Euler(-90f, 0f, 0f));
                tile.transform.localScale = new Vector3(size*0.2f, size*0.2f, size*0.2f);
                tile.GetComponent<MeshRenderer>().material = ground;
                Hex gridHex = new Hex(new Vector2Int(x, y), tile);
                // Current hex will go in the current = y * gridSize.x + x; slot in hexes
                Int current = y * gridSize.x + x;
                // Need to add the already existing hexes, but need to check if they exist
                // (In the case of hex 0,0 there will be no other existing hexes)
                // In case of a non-existent hex, set the value to null
                // Already existing hexes are: Left, Top-left, Top-right, since
                // the grid is built from the top-left to the right then down.
                // the left is easy allways current-1, except when x == 0, then it's null
                if(x == 0){
                    gridHex.AddNeighbour(null);
                } else {
                    gridHex.AddNeighbour(hexes(current - 1));
                    // Need to add them as neighbours to each other so both of them know of the connection
                    hexes(current - 1).AddNeighbour(gridHex);
                }
                // the top hexes are different for even and odd rows, if y == 0 then they are both null
                if(y == 0){
                    gridHex.AddNeighbour(null);
                    gridHex.AddNeighbour(null);
                } else if(y % 2){ //even
                    // in this case the needed hexes are: x-1,y-1 and x,y-1
                    // in relation to the current hex they are up a row so: -gridSize.x
                    // one of them is "directly" above the current, the other is to the left so: -0, -1
                    gridHex.AddNeighbour(hexes(current - gridSize.x));
                    hexes(current - gridSize.x).AddNeighbour(gridHex);

                    gridHex.AddNeighbour(hexes(current - gridSize.x - 1));
                    hexes(current - gridSize.x - 1).AddNeighbour(gridHex);
                } else { //odd
                    // in this case the needed hexes are: x,y-1 and x+1,y-1
                    // in relation to the current hex they are up a row so: -gridSize.x
                    // one of them is "directly" above the current, the other is to the right so: +0, +1
                    gridHex.AddNeighbour(hexes(current - gridSize.x));
                    hexes(current - gridSize.x).AddNeighbour(gridHex);

                    gridHex.AddNeighbour(hexes(current - gridSize.x + 1));
                    hexes(current - gridSize.x + 1).AddNeighbour(gridHex);
                }
                // The other neighbours will be added as we build the grid.

                hexes.Add(gridHex);

                tile = Instantiate(hex, GetPositionForHexFromCoordinate(new Vector2Int(x, y)), transform.rotation * Quaternion.Euler(-90f, 0f, 0f));
                tile.transform.localScale = new Vector3(size*0.21f, size*0.21f, size*0.21f);
                tile.transform.position =  GetPositionForHexFromCoordinate(new Vector2Int(x, y)) + new Vector3(0f, -0.1f, 0f); 
                tile.GetComponent<MeshRenderer>().material = backGround;
                backgroundHexes.Add(tile);
            }
        }
    }

    public void DestroyGrid()
    {
        Debug.Log("Destroying grid...");

        foreach (Hex child in hexes)
        {
            Destroy(child.DisplayHex);
        }

        foreach (GameObject child in backgroundHexes)
        {
            Destroy(child);
        }
    }

    private Vector3 GetPositionForHexFromCoordinate(Vector2Int coordinate)
    {
        int column = coordinate.x;
        int row = coordinate.y;

        float width;
        float height;
        float xPosition = 0;
        float yPosition = 0;
        bool shouldOffset;
        float horizontalDistance;
        float verticalDistance;
        float offset;
        float hexSize = size;

        shouldOffset = (row % 2) == 0;
        width = sqrt3 * hexSize;
        height = 2f * hexSize;

        horizontalDistance = width;
        verticalDistance = height * (3f / 4f);

        offset = shouldOffset ? width / 2 : 0;

        xPosition = column * horizontalDistance + offset;
        yPosition = row * verticalDistance;
        
        return new Vector3(xPosition, 0, -yPosition);
    }
}
