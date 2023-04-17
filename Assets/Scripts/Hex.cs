using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex
{
    private Vector2Int indexCoordinates;

    private List<Hex> neighbours;

    private GameObject displayHex;

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

    public Hex(Vector2Int iC, GameObject dH){
        indexCoordinates = iC;
        displayHex = dH;
    }
}
