using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class City : MonoBehaviour
{
    public int money;
    public int day;
    public int curPopulation;
    public int curJobs;
    public int curFood;
    public int maxPopulation;
    public int maxJobs;
    public int incomePerJob;

    public TextMeshProUGUI[] statsText;

    public List<Building> buildings = new List<Building>();

    public static City instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateStatText();
    }

    //called when we place down a building
    public void OnPlaceBuilding (Building building)
    {
        buildings.Add(building);

        money -= building.preset.cost;

        maxPopulation += building.preset.population;
        maxJobs += building.preset.jobs;

        UpdateStatText();
    }

    //called when we bulldoze a building
    public void OnRemoveBuilding(Building building)
    {
        buildings.Remove(building);

        maxPopulation -= building.preset.population;
        maxJobs -= building.preset.jobs;
        Destroy(building.gameObject);

        UpdateStatText();
    }

    public void EndTurn()
    {
        day++;

        CalculateMoney();
        CalculatePopulation();
        CalculateJobs();
        CalculateFood();

        UpdateStatText();
    }

    private void UpdateStatText()
    {
        statsText[0].text = String.Format("Money:", money);
        statsText[1].text = String.Format("Pop:{0}/{1}", new object[2] { curPopulation, maxPopulation });
        statsText[2].text = String.Format("Jobs:{0}/{1}", new object[2] { curJobs, maxJobs});
        statsText[3].text = String.Format("Food:", curFood);
        statsText[4].text = String.Format ("Day: ", DayCycle.instance.days);
        statsText[5].text = String.Format("M: ", DayCycle.instance.hours);
        statsText[6].text = String.Format("S: ", DayCycle.instance.minutes);
        //statsText.text = String.Format("Day:{0} Money:{1} Pop:{2}/{3} Jobs:{4}/{5} Food:{6}", new object[7] { day, money, curPopulation, maxPopulation, curJobs, maxJobs, curFood });
    }

    private void CalculateFood()
    {
        curFood = 0;

        foreach (Building building in buildings)
            curFood += building.preset.food;
    }

    private void CalculateJobs()
    {
        curJobs = Mathf.Min(curPopulation, maxJobs);
    }

    private void CalculatePopulation()
    {
        if (curFood >= curPopulation && curPopulation < maxPopulation)
        {
            curFood -= curPopulation / 4;
            curPopulation = Mathf.Min(curPopulation + (curFood / 4 ) , maxPopulation);
        }
        else if (curFood < curPopulation)
        {
            curPopulation = curFood;
        }
    }

    private void CalculateMoney()
    {
        money += curJobs * incomePerJob;

        foreach(Building building in buildings) 
            money -= building.preset.costPerTurn;
    }
}
