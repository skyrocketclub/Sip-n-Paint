using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBoard : MonoBehaviour
{
    //Getinng the resolution
    public Texture2D texture; //Texture of the WHiteboard
    public Vector2 textureSize; //Resolution of the whiteboard, and this depends on the whiteboard size
   
    void Start()
    {
        ////Getting the texture of the white board so that it can be manipulated
        var r = GetComponent<Renderer>();
        texture = r.material.mainTexture as Texture2D;
        textureSize = new Vector2(texture.width, texture.height);

        // Check if the existing texture is not a Texture2D (null or some other type)
        if (texture == null)
        {
            Debug.Log("NULLL");
            // If the material's main texture is not a Texture2D, create a new one
            texture = new Texture2D(2048, 2048); // Create a new 2048x2048 texture
            r.material.mainTexture = texture;
            textureSize = new Vector2(texture.width, texture.height);
        }
    }


}
