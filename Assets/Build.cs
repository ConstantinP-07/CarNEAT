using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Copyright 2020 7-Script
public class Build : MonoBehaviour
{

    private GameObject selectedObject;

    public void BuildTrack(GameObject partToBuild)
    {
        selectedObject = partToBuild;
    }
}
