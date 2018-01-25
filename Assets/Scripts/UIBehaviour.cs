using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    public GameObject[] PanelList;
    private GameObject activePanel;
    private GameObject prevPanel;
    public Slider MusicVolumeSlider;
    public Text musicValue;

    void Start()
    {
        PanelList = GameObject.FindGameObjectsWithTag("Panel");
        foreach (var o in PanelList)
        {
            o.SetActive(false);
            if (o.name == "MenuPanel")
            {
                activePanel = o;
                activePanel.SetActive(true);
            }
        }
    }

    void Update()
    {
        CheckForActivePanel();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (activePanel !=null && activePanel.name == "PausePanel" && activePanel.activeInHierarchy)
            {
                SwapActivePanel("");
            }
            else if(activePanel == null)
            {
                foreach (var o in PanelList)
                {
                    if (o.name == "PausePanel")
                    {
                        activePanel = o;
                        activePanel.SetActive(true);
                    }
                }
            }
        }
    }

    public void MusicVolumeUpdate()
    {
        musicValue.text = MusicVolumeSlider.value.ToString();
    }
    
    public void PlayButtonTrigger()
    {
        SwapActivePanel("");
    }

    public void OptionButtonTrigger()
    {
        SwapActivePanel("OptionsPanel");
    }

    public void BackButtonTrigger()
    {
        CheckForActivePanel();
        activePanel.SetActive(false);
        activePanel = prevPanel;
        activePanel.SetActive(true);
        prevPanel = activePanel;
    }

    public void ResumeButtonTrigger()
    {
        SwapActivePanel("");
    }

    public void QuitButtonTrigger()
    {
        SwapActivePanel("MenuPanel");
    }
    
    void SwapActivePanel(string panelName)
    {
        CheckForActivePanel();
        if (panelName == "")
        {
            prevPanel = activePanel;
            activePanel.SetActive(false);
            activePanel = null;
        }
        else
        {
            foreach (var o in PanelList)
            {
                if (o.name == panelName)
                {
                    activePanel.SetActive(false);
                    prevPanel = activePanel;
                    activePanel = o;
                    activePanel.SetActive(true);
                }
            }
        }
    }

    void CheckForActivePanel()
    {
        if (PanelList == null)
            Debug.LogError("No Panels Found");
        foreach (var o in PanelList)
        {
            if (activePanel != o && o.activeInHierarchy)
                activePanel = o;
        }
    }
}
