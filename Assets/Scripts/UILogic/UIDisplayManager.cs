using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDisplayManager : MonoBehaviour
{
    public static UIDisplayManager Instance;

    [Header("Stats Panel for Resources")]
    public TextMeshProUGUI[] resourceTexts;
    public Image[] resourceBar;
    public Image[] resourceSprites;

    [Header("Tool panel for Buildings")]
    public BuildingPreset[] buildingPresets;
    public Transform buildingButtonsContent;
    public GameObject buildingButtonPrefab;

    [Header("Labels")]
    public GameObject labelPrefab;
    public RectTransform labelParent;
    public Vector3 labelPosition = new Vector3(0, 100, 0);
    private GameObject label;
    private TextMeshProUGUI labelText;

    [Header("Time UI")]
    public TextMeshProUGUI currentTimeText;
    public TextMeshProUGUI cycleSpeedText;


    private Button[] buildingButtons;
    private City city;
    private DayCycle dayCycle;

    private void Start()
    {
        Instance = this;
        city = City.instance;
        dayCycle = DayCycle.instance;
        UpdateDisplay();
        InstantiateButton(OnButtonClick);
        InstantiateLabel();
    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < resourceTexts.Length; i++)
        {
            resourceTexts[i].text = city.resourcesInventory[i].CurrentUnits.ToString() + " / " + city.resourcesInventory[i].MaxUnits.ToString();
            resourceBar[i].fillAmount = (float)city.resourcesInventory[i].CurrentUnits / (float)city.resourcesInventory[i].MaxUnits;
            resourceSprites[i].sprite = city.resourcesInventory[i].Sprite;
        }

        currentTimeText.text = "Day: " + dayCycle.days + " Hour: " + dayCycle.hours +" : "+ dayCycle.minutes;
        cycleSpeedText.text = dayCycle.cycleSpeed.ToString();
    }

    public void InstantiateButton(Action<BuildingPreset> onButtonClick)
    {
        // Instance resource buttons
        buildingButtons = new Button[buildingPresets.Length];
        int i = 0;
        foreach (BuildingPreset buildingPreset in buildingPresets)
        {
            GameObject btn = Instantiate(buildingButtonPrefab, buildingButtonsContent);
            btn.GetComponent<Image>().sprite = buildingPreset.Sprite;
            Button button = btn.GetComponent<Button>();
            button.onClick.AddListener(() => onButtonClick(buildingPreset));
            buildingButtons[i] = button;
            i++;
        }


    }

    void InstantiateLabel()
    {
        // Instance label
        label = Instantiate(labelPrefab, labelParent);
        labelText = label.GetComponentInChildren<TextMeshProUGUI>();
        label.SetActive(false);

        // Add event triggers for buttons
        foreach (Button btn in buildingButtons)
        {
            EventTrigger trigger = btn.gameObject.AddComponent<EventTrigger>();

            // Show label on hover
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((data) => ShowLabel(btn));
            trigger.triggers.Add(entry);

            // Hide label on exit
            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerExit;
            entry.callback.AddListener((data) => HideLabel());
            trigger.triggers.Add(entry);
        }
        /*for (int i = 0; i < buildingButtons.Length; i++)
        {
        }*/
    }
    private void ShowCost(int index)
    {
        if (index < 0)
        {
            labelText.text = "Destroy";
            return;
        }

        ResourceCost[] costs = buildingPresets[index].buildingCosts;

        string costString = buildingPresets[index].presetName + "\n\nCOST: \n";
        foreach (ResourceCost cost in costs)
            costString += cost.ResourceType.ToString() + ": " + cost.CostUnits + "\n";

        AddResource[] adds = buildingPresets[index].addResources;
        costString += "\nADD: \n";
        foreach (AddResource add in adds)
            costString += add.ResourceType.ToString() + ": " + add.Units + "\n";

        labelText.text = costString;
    }

    public void ShowLabel(Button button)
    {
        label.SetActive(true);
        ShowCost(buildingButtons.ToList<Button>().FindInstanceID(button));
        Vector3 targetPos = button.transform.localPosition + labelPosition;
        targetPos = labelParent.InverseTransformPoint(targetPos);
        StartCoroutine(MoveLabel(targetPos, 0.5f));
    }

    public void HideLabel()
    {

        /*Vector3 targetPos = labelParent.InverseTransformPoint(label.transform.position - new Vector3(-50, 0, 0));*/
        /*StartCoroutine(MoveLabel(targetPos, 0.5f));*/
        label.SetActive(false);
    }
    private IEnumerator MoveLabel(Vector3 targetPos, float time)
    {
        Vector3 startPos = label.GetComponent<RectTransform>().anchoredPosition;
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            label.transform.localPosition = Vector3.Lerp(startPos, targetPos, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        label.transform.localPosition = targetPos;
    }

    private void OnButtonClick(BuildingPreset buildingPreset)
    {
        GetComponent<BuildingPlacement>().BeginNewBuildingPlacement(buildingPreset);
    }
}
