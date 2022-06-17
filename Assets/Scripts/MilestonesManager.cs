using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MilestonesManager : MonoBehaviour
{
    [SerializeField] private GameObject milestonePrefab;
    [SerializeField] private Transform milestoneParent;
    [SerializeField] private Transform startingPoint;
    private Queue<MilestoneController> milestoneControllers = new Queue<MilestoneController>();
    private int step = 10;
    private int targetDistance = 100;
    private int initialTargetDistance = 100;
    
    private void Awake()
    {
        BindEvents();
    }

    private void OnDestroy()
    {
        UnbindEvents();
    }

    private void BindEvents()
    {
        //EventManager.onMaxDistanceUpdated += OnMaxDistanceUpdated;
        EventManager.onMilestoneCollided += OnMilestoneCollided;

    }

    private void UnbindEvents()
    {
        EventManager.onMilestoneCollided -= OnMilestoneCollided;
        //EventManager.onMaxDistanceUpdated -= OnMaxDistanceUpdated;
    }

    private void Start()
    {
        AddMilestones();
    }

    private void AddMilestones()
    {
        int numberOfMilestones = initialTargetDistance / step;
        for (int i = 0; i < numberOfMilestones; i++)
        {
            GenerateMilestone();
        }
    }

    private void GenerateMilestone()
    {
        Vector3 newPosition = startingPoint.transform.position + step * Vector3.down;
        if (milestoneControllers.Count > 0)
        {
            newPosition = milestoneControllers.Last().transform.position + step * Vector3.down;
        }

        GameObject newMilestone = Instantiate(milestonePrefab, milestoneParent);
        newMilestone.transform.position = newPosition;
        MilestoneController newMilestoneController = newMilestone.GetComponent<MilestoneController>();
        newMilestoneController.Init(newPosition.y);
        milestoneControllers.Enqueue(newMilestoneController);
    }

    private void AddNewMilestones()
    {
        int numberOfMilestones = initialTargetDistance / step;
        for (int i = 0; i < numberOfMilestones/2 -1; i++)
        {
            GenerateNewMilestone();
        }
    }
    private void GenerateNewMilestone()
    {
        var newPosition = milestoneControllers.Last().transform.position + step * Vector3.down;
        MilestoneController newMilestoneController = milestoneControllers.Dequeue();
        newMilestoneController.transform.position = newPosition;
        newMilestoneController.Init(newPosition.y);
        milestoneControllers.Enqueue(newMilestoneController);
    }
    
    private void OnMaxDistanceUpdated(int maxDistance)
    {
        if (maxDistance > targetDistance / 2)
        {
            while (maxDistance > targetDistance / 2)
            {
                targetDistance *= 2;
            }

            AddNewMilestones();
        }

    }

    private void OnMilestoneCollided()
    {
        GenerateNewMilestone();
    }
    

    
}
