using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialTest : MonoBehaviour
{

    public MeshRenderer matarialColor;
    public Renderer mat;
    public float colorVolue1;
    public float colorVolue2;
    public float colorVolue3;
    public Color c;

    private void Start()
    {
        c = new Color();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            colorVolue2 -= 0.01f;
            colorVolue3 -= 0.01f;
            //matarialColor.material.color = c;
        }
        c = new Color(colorVolue1, colorVolue2, colorVolue3);
        mat.material.color = c;
    }


    
}
