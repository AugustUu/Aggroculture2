using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class TextColorScript : MonoBehaviour
{

    public TextMeshProUGUI text;
    // Start is called before the first frame update

    public VertexGradient colors;
    void Start()
    {
        colors = text.colorGradient;
    }

    // Update is called once per frame
    double last_swap = 0;
    void Update()
    {
        var new_colors = text.colorGradient;
        new_colors.topRight = Color.Lerp(new_colors.topRight, colors.topLeft, Time.deltaTime);
        new_colors.topLeft = Color.Lerp(new_colors.topLeft,  colors.bottomLeft, Time.deltaTime);
        new_colors.bottomLeft = Color.Lerp(new_colors.bottomLeft, colors.bottomRight, Time.deltaTime);
        new_colors.bottomRight = Color.Lerp(new_colors.bottomRight, colors.topRight, Time.deltaTime);
            
        
        if (Time.timeSinceLevelLoadAsDouble - last_swap > 1.0)
        {
            last_swap = Time.timeSinceLevelLoadAsDouble;
            var temp = colors.topRight;
            colors.topRight = colors.topLeft;
            colors.topLeft = colors.bottomLeft;
            colors.bottomLeft = colors.bottomRight;
            colors.bottomRight = temp;
            
            

        }
        
        text.colorGradient = new_colors;
    }
}
