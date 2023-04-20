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

    [Header("Neighbour test")]
    public bool showNeighbours;

    public Vector2Int IndexCoordinates{
        set => indexCoordinates = value;
        get => indexCoordinates;
    }

    public void initiateHex(Vector2Int iC, Material b, Material s, Material n){
        indexCoordinates = iC;
        neighbours = new List<Hex>();
        showNeighbours = false;
        basic = b;
        selected = s;
        neighbour = n;
    }

    void Update(){
        if(showNeighbours){
            setMaterial(selected);
            foreach(Hex n in neighbours){
                if (n != null){
                    n.setMaterial(neighbour);
                }
            }
        } else { // update sorrend hülyegyerek!!!!!!!!!!!!!!!!!!!!!!!!!!
            setMaterial(basic);
            foreach(Hex n in neighbours){
                if (n != null){
                    n.setMaterial(basic);
                }
            }
        }
    }

    public void setMaterial(Material mat){
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }

    public void AddNeighbour(Hex nb){
        neighbours.Add(nb);
    }
}
