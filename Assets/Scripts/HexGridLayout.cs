using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridLayout : MonoBehaviour
{
    [Header("Grid Settings")]
    public Vector2Int gridSize;

    [Header("Tile Settings")]
    public float size = 1f;

    public Material ground;
    public Material water;
    public Material backGround;
    public Material selected;
    public Material neighbour;

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
                Hex tile = Instantiate(hex, GetPositionForHexFromCoordinate(new Vector2Int(x, y)), transform.rotation).GetComponent<Hex>();
                tile.transform.SetParent(gameObject.transform);
                tile.transform.localScale = new Vector3(size*0.2f, size*0.2f, size*0.2f);
                tile.transform.localRotation *= Quaternion.Euler(-90f, 0f, 0f);
                tile.GetComponent<MeshRenderer>().material = ground;
                tile.initiateHex(new Vector2Int(x,y), ground, selected, neighbour);
                // Current hex will go in the current = y * gridSize.x + x; slot in hexes
                int current = y * gridSize.x + x;
                Debug.Log($"Current place in list: {current}");
                // Need to add the already existing hexes, but need to check if they exist
                // (In the case of hex 0,0 there will be no other existing hexes)
                // In case of a non-existent hex, set the value to null
                // Already existing hexes are: Left, Top-left, Top-right, since
                // the grid is built from the top-left to the right then down.
                // the left is easy allways current-1, except when x == 0, then it's null
                
                if ( x != 0 ){
                    for(int i = 0; i < hexes.Count; i++){
                        if(hexes[i].IndexCoordinates.x == (x - 1) && hexes[i].IndexCoordinates.y == y){
                            Debug.Log($"{hexes[i].IndexCoordinates}, {tile.IndexCoordinates}");
                            tile.AddNeighbour(hexes[i]);
                            hexes[i].AddNeighbour(tile);
                        }
                    }
                    /*
                    foreach(Hex h in hexes){
                        if(h.IndexCoordinates.x == x - 1 && h.IndexCoordinates.y == y){
                            tile.AddNeighbour(h);
                            h.AddNeighbour(tile);
                        }
                    }
                    */
                }
                if (!(y == 0 || (x == 0 && y % 2 != 0))){
                    foreach(Hex h in hexes){
                        if(y % 2 == 0){
                            if(h.IndexCoordinates.x == x && h.IndexCoordinates.y == y - 1){
                                tile.AddNeighbour(h);
                                h.AddNeighbour(tile);
                            }
                        }
                        if(y % 2 != 0){
                            if(h.IndexCoordinates.x == x - 1 && h.IndexCoordinates.y == y - 1){
                                tile.AddNeighbour(h);
                                h.AddNeighbour(tile);
                            }
                        }
                    }
                }
                if (!(y == 0 || (x == ((y + 1) * gridSize.x) - 1 && y % 2 == 0))){
                    foreach(Hex h in hexes){
                        if(y % 2 == 0){
                            if(h.IndexCoordinates.x == x + 1 && h.IndexCoordinates.y == y - 1){
                                tile.AddNeighbour(h);
                                h.AddNeighbour(tile);
                            }
                        }
                        if(y % 2 != 0){
                            if(h.IndexCoordinates.x == x && h.IndexCoordinates.y == y - 1){
                                tile.AddNeighbour(h);
                                h.AddNeighbour(tile);
                            }
                        }
                    }
                }
                
                // The other neighbours will be added as we build the grid.

                hexes.Add(tile);
            }
        }
    }

    public void DestroyGrid()
    {
        Debug.Log("Destroying grid...");

        foreach (Hex child in hexes)
        {
            Destroy(child.gameObject);
        }
        hexes.Clear();
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
