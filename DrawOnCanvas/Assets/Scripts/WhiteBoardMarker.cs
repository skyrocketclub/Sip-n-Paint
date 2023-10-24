using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WhiteBoardMarker : MonoBehaviour
{
    [SerializeField] private Transform _tip;
    [SerializeField] private int _penSize = 10;
    [SerializeField] private float _tipHeightModifier = 0.9f;

    private Renderer _renderer;
    private Color[] _colors;
    private float _tipHeight;

    private RaycastHit _touch;
    private WhiteBoard _whiteboard;
    private Vector2 _touchPos;
    private Vector2 _lastTouchPos;
    private bool _touchedLastFrame;
    private Quaternion _lastTouchRot;


    // Start is called before the first frame update
    void Start()
    {
        _renderer = _tip.GetComponent<Renderer>(); //We get access to the color here...
        //Using the geometry of length times width to get the pixels that do the actual painting
        _colors = Enumerable.Repeat(_renderer.material.color, _penSize * _penSize).ToArray();
        //to help check if the tip is touching the whiteboard
        _tipHeight = _tip.localScale.y - _tipHeightModifier ;
    }

    // Update is called once per frame
    void Update()
    {
        Draw(); //called every frame
    }

    private void Draw()
    {
        //check if we are touching anything
        if(Physics.Raycast(_tip.position, transform.up, out _touch, _tipHeight))
        {
            if (_touch.transform.CompareTag("Whiteboard"))
            {
                if(_whiteboard == null)
                {
                    _whiteboard = _touch.transform.GetComponent<WhiteBoard>();
                }

                //Getting the touch position where we touched the whiteboard
                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                //detecting the position of contact
                var x = (int)(_touchPos.x * _whiteboard.textureSize.x - (_penSize/2));
                var y = (int)(_touchPos.y * _whiteboard.textureSize.y - (_penSize / 2));
                //we are out of bounds so we exit the loop
                if(y<0 || y > _whiteboard.textureSize.y || x < 0 || x > _whiteboard.textureSize.x)
                {
                    return;
                }

                if (_touchedLastFrame)
                {
                    //Actual drawing
                    _whiteboard.texture.SetPixels(x, y, _penSize, _penSize, _colors);
                    
                    //Interpolation incase we move our hands very fast...
                    for(float f = 0.01f; f <1.00f; f+= 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
                        _whiteboard.texture.SetPixels(lerpX, lerpY, _penSize, _penSize, _colors);
                    }

                    //preventing rotation of the whiteboard marker

                    transform.rotation = _lastTouchRot;
                    _whiteboard.texture.Apply(); //updates the texture
                }
                _lastTouchPos = new Vector2(x, y); ;
                _lastTouchRot = transform.rotation;
                _touchedLastFrame = true;
                return;
            }
        }
        //Our pen is not touching anything...
        _whiteboard = null;
        _touchedLastFrame = false;
    }
}
