using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TestButton : MonoBehaviour
{
    [SerializeField] private HexGridLayout grid;
    [SerializeField] private HexRenderer testHex;

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            grid.LayoutGrid();

            // This hex can be used to test and create prefabs, (ground, water, etc.)
            testHex.DrawMesh();
        });
    }
}
