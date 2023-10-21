using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBoard : MonoBehaviour
{
    //Getinng the resolution
    public Texture2D texture; //Texture of the WHiteboard
    public Vector2 textureSize = new Vector2(2048,2048); //Resolution of the whiteboard, and this depends on the whiteboard size
   
    void Start()
    {
        //Getting the texture of the white board so that it can be manipulated
        var r = GetComponent<Renderer>();
        texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
        r.material.mainTexture = texture;
    }


}
