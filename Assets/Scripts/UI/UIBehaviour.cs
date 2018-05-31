using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using UnityEngine.SceneManagement;

public class UIBehaviour : MonoBehaviour, IPlayerDataChangeHandler
{
    #region Variables

    public PlayerData PlayerData;

    [Header("Panels")]
    public GameObject MenuPanelObject;
    public GameObject PausePanelObject;
    public GameObject OptionsPanelObject;
    public GameObject HudPanelObject;
    public GameObject StartUpPanelObject;
    public GameObject[] HealthBar;

    [Header("Sliders")]
    public Slider MusicVolumeSlider;
    public Slider SfxVolumeSlider;

    [Header("Text")]
    public Text SfxValue;
    public Text MusicValue;
    public Text GemFragments;
    public Text LifeGems;
    public Sprite HealthPieceFull;
    public Sprite HealthPieceEmpty;

    public FloatVariable SavedMusicVolume;
    private GameObject _prevPanelObject;
    private GameObject _activePanelObject;

    private List<GameObject> _panels;

    #endregion
    void Awake()
    {
        MusicVolumeSlider.value = SavedMusicVolume.Value * 100;
        MusicValue.text = MusicVolumeSlider.value.ToString();
    }

    void Start()
    {
        _panels = new List<GameObject> { HudPanelObject, MenuPanelObject, OptionsPanelObject, PausePanelObject };
        _panels.ForEach(x => x.SetActive(false));
        StartUpPanelObject.SetActive(true);
        _activePanelObject = MenuPanelObject;
        GemFragments.text = PlayerData.GemFragments.ToString();
        LifeGems.text = PlayerData.LifeGems.ToString();
    }
    void Update()
    {
        CheckForActivePanel();


        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            if (OptionsPanelObject.activeSelf)
            {
                OptionsPanelObject.SetActive(false);
                HudPanelObject.SetActive(true);
            }

            else if (HudPanelObject.activeSelf)
            {
                HudPanelObject.SetActive(false);
                PausePanelObject.SetActive(true);
                Time.timeScale = 0;
            }
            else if (PausePanelObject.activeSelf)
            {
                PausePanelObject.SetActive(false);
                HudPanelObject.SetActive(true);
                Time.timeScale = 1;
            }
        }
    }

    #region SettingUpdates
    public void MusicVolumeUpdate()
    {
        MusicValue.text = MusicVolumeSlider.value.ToString();
        SavedMusicVolume.Value = MusicVolumeSlider.value * 1 / 100;
    }
    public void SfxVolumeUpdate()
    {
        SfxValue.text = SfxVolumeSlider.value.ToString();
    }
    #endregion
    #region MenuNavigation
    public void PlayButtonTrigger()
    {
        CheckForActivePanel();
        Time.timeScale = 1;
        _activePanelObject.SetActive(false);
        _prevPanelObject = _activePanelObject;
        HudPanelObject.SetActive(true);
    }

    public void OptionButtonTrigger()
    {
        CheckForActivePanel();
        _activePanelObject.SetActive(false);
        _prevPanelObject = _activePanelObject;
        OptionsPanelObject.SetActive(true);
    }

    public void BackButtonTrigger()
    {
        CheckForActivePanel();
        _activePanelObject.SetActive(false);
        _prevPanelObject.SetActive(true);
        _prevPanelObject = _activePanelObject;
    }

    public void ResumeButtonTrigger()
    {
        CheckForActivePanel();
        Time.timeScale = 1;
        _activePanelObject.SetActive(false);
        _prevPanelObject = _activePanelObject;
        HudPanelObject.SetActive(true);
    }

    public void QuitButtonTrigger()
    {
        CheckForActivePanel();
        Time.timeScale = 1;
        if (_activePanelObject == PausePanelObject)
        {
            _activePanelObject.SetActive(false);
            _prevPanelObject = _activePanelObject;
            MenuPanelObject.SetActive(true);
        }
        else if (_activePanelObject == MenuPanelObject)
        {
            Application.Quit();
        }
    }

    void CheckForActivePanel()
    {
        foreach (var panel in _panels)
        {
            if (panel.activeSelf)
                _activePanelObject = panel;
        }
    }
    #endregion


    public void OnPlayerDataChanged(Object[] args)
    {
        var sender = args[0] as PlayerData;
        if (sender == null)
            return;

        GemFragments.text = PlayerData.GemFragments.ToString();
        LifeGems.text = PlayerData.LifeGems.ToString();
        foreach (var healthSegment in HealthBar)
            healthSegment.GetComponent<Image>().sprite = HealthPieceEmpty;

        for (var i = 0; i < sender.Health; i++)
        {
            HealthBar[i].GetComponent<Image>().sprite = HealthPieceFull;
        }

    }
}
