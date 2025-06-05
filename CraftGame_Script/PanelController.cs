using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject targetPanel;

    public void TogglePanel()
    {
        targetPanel.SetActive(!targetPanel.activeSelf);
    }

}
