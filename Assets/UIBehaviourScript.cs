using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIBehaviourScript : MonoBehaviour
{
    public Image Vignette;
    public Image Exit;
    public TextMeshProUGUI ExitText;
    public Button ExitButton;
    bool Escaped = false;
    // Start is called before the first frame update
    void Start()
    {
        Vignette.color = new Color(0, 0, 0, 0);
        Exit.color = new Color(0, 0, 0, 0);
        ExitText.color = new Color(0, 0, 0, 0);
        ExitButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.Input.GetKeyDown("escape"))
        {
            if (Escaped)
            {
                Escaped = false;
            }
            else
            {
                Escaped = true;
            }
            if (Escaped)
            {
                Exit.color = new Color(0.9F, 0.9F, 0.675F, 1);
                ExitText.color = new Color(0.9F, 0.9F, 0.675F, 1);
                ExitButton.interactable = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Exit.color = new Color(0, 0, 0, 0);
                ExitText.color = new Color(0, 0, 0, 0);
                ExitButton.interactable = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        
    }

    public void Death()
    {
        Vignette.color = new Color(1, 0.5F, 0.5F, 1);
    }
    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
