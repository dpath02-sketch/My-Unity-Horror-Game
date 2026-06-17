using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviourScript : MonoBehaviour
{
    public Image Vignette;
    // Start is called before the first frame update
    void Start()
    {
        Vignette.color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Death()
    {
        Vignette.color = new Color(0, 0, 0, 255);
    }
}
