using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex
{
    private Vector2Int indexCoordinates;

    private List<Hex> neighbours;

    private GameObject displayHex;

    private Material basic;
    private Material selected;
    private Material neighbour;

    public bool showNeighbours;

    public Vector2Int IndexCoordinates{
        set => indexCoordinates = value;
        get => indexCoordinates;
    }

    public List<Hex> Neighbours{
        set => neighbours = value;
        get => neighbours;
    }

    public GameObject DisplayHex{
        set => displayHex = value;
        get => displayHex;
    }

    public Hex(Vector2Int iC, GameObject dH, Material b, Material s, Material n){
        indexCoordinates = iC;
        displayHex = dH;
        neighbours = new List<Hex>();
        showNeighbours = false;
        basic = b;
        selected = s;
        neighbour = n;
    }
    
    /*
    void Update(){
        if(showNeighbours){
            this.setMaterial(selected);
            foreach(Hex n in neighbours){
                if (n != null){
                    n.setMaterial(neighbour);
                }
            }
        } else {
            this.setMaterial(basic);
            foreach(Hex n in neighbours){
                if (n != null){
                    n.setMaterial(basic);
                }
            }
        }
    }*/

    public void setMaterial(Material mat){
        displayHex.GetComponent<MeshRenderer>().material = mat;
    }

    public void AddNeighbour(Hex nb=null){
        neighbours.Add(nb);
    }
}
