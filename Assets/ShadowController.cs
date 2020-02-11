﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
    private Material myMaterial;
    private float myScale;
    private Color resetColor;
    private Color alphaColor;
    private Texture2D texture;
    // Start is called before the first frame update
    void Start()
    {
        myScale = transform.localScale.x;
        texture = new Texture2D(1024, 1024, TextureFormat.ARGB32, false);
        resetColor = new Color(0, 0, 0, 1f);
        alphaColor = new Color(0, 0, 0, 0f);
        Color[] resetColorArray = texture.GetPixels();

        for (int i = 0; i < resetColorArray.Length; i++)
        {
            resetColorArray[i] = resetColor;
        }

        texture.SetPixels(resetColorArray);
        texture.Apply();

        myMaterial = GetComponent<MeshRenderer>().material;
        myMaterial.mainTexture = texture;
    }

    public void SetUnit(Vector2 textureCoord, float rangeLight) {
        //Color[] resetColorArray = texture.GetPixels();
        /*for (int i = 0; i < resetColorArray.Length; i++)
        {
            resetColorArray[i] = resetColor;
        }*/
        Vector2 tP = new Vector2(textureCoord.x*1024,textureCoord.y*1024);
        int delta = 0;
        int r = Mathf.RoundToInt(17* rangeLight);
        float r2 = Mathf.Pow(r, 2);
        for (int j = 0; j <= r; j++)
        {
            int xL = Mathf.RoundToInt(Mathf.Sqrt(r2 - Mathf.Pow(j, 2)));
            for (int i = -xL; i <= xL; i++)
            {
                if (tP.x + i < texture.width && tP.y + j < texture.height && tP.x + i >= 0)
                {
                    texture.SetPixel((int)tP.x + i, (int)tP.y + j, alphaColor);
                }
                if (tP.x + i < texture.width && tP.y - j >= 0 && tP.x + i >= 0)
                {
                    texture.SetPixel((int)tP.x + i, (int)tP.y - j, alphaColor);
                }
            }
            delta++;
        }
        texture.Apply();
        //Debug.Log("Change textures"+ tP.ToString());
    }

    public void RemoveUnit()
    {

    }

}
