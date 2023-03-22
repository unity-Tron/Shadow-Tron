using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject[] panels;

    private void Start()
    {
        // Set all panels except the first one to inactive initially
        for (int i = 1; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
    }

    public void ActivatePanel(int panelIndex)
    {
        // Deactivate all panels
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }

        // Activate the requested panel
        panels[panelIndex].SetActive(true);
    }
}
