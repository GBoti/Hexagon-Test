using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    private Vector2Int indexCoordinates;

    private List<Hex> neighbours;

    private Material basic;
    private Material selected;
    private Material neighbour;
    private bool showNeighbours;
    private string terrain;

    public Vector2Int IndexCoordinates{
        set => indexCoordinates = value;
        get => indexCoordinates;
    }

    public string Terrain{
        set => terrain = value;
        get => terrain;
    }

    public List<Hex> Neighbours{
        set => neighbours = value;
        get => neighbours;
    }

    public void InitiateHex(Vector2Int iC, Material b, Material s, Material n){
        indexCoordinates = iC;
        neighbours = new List<Hex>();
        showNeighbours = true;
        basic = b;
        selected = s;
        neighbour = n;
    }

    public void ToggleHighlight(){
        if(showNeighbours){
            SetMaterial(selected);
            foreach(Hex n in neighbours){
                if (n != null){
                    n.SetMaterial(neighbour);
                }
            }
            showNeighbours = false;
        } else if (!showNeighbours){
            SetMaterial(basic);
            foreach(Hex n in neighbours){
                if (n != null){
                    n.SetMaterial(basic);
                }
            }
            showNeighbours = true;
        }
    }

    public void SetMaterial(Material mat){
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }

    public Material GetMaterial(){
        return gameObject.GetComponent<MeshRenderer>().material;
    }

    public void AddNeighbour(Hex nb){
        neighbours.Add(nb);
    }
}
