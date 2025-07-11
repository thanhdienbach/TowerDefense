using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefense.Towers;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class StructureBuilder : MonoBehaviour
{

    public GameObject buildingPrefabs; // Structer will build
    public LayerMask buildLayer; // Layer can build
    public LayerMask obsticalLayer; // Layer can not build
    public float maxPlacementDistance = 100; // Max distan from main camere to buildLayer can raycash 
    public GameObject previewObject; // Object preview before build
    public float hightOffset; // Offset for position of structer when build

    public Touch touch;
    public bool canPlace;
    public Vector3 hitPosition;

    public List<tower> towers;

    public PlayerInputForCamera playerInputForCamera;

    #region
    public static StructureBuilder instance;
    private void OnEnable()
    {
        instance = this;
    }
    private void OnDisable()
    {
        instance = null;
    }
    #endregion

    public void Init()
    {

    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (EnoughEnergy())
            {
                Builder();
            }
            else
            {
                playerInputForCamera.isFreeZone = true;
            }
        }
    }
    bool EnoughEnergy()
    {
        return buildingPrefabs.GetComponent<tower>().towerConfig.cost <= MainHall.instance.energy;
    }
    void Builder()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxPlacementDistance, buildLayer))
        {
            playerInputForCamera.isFreeZone = false;
            if (previewObject == null && MainHall.instance.energy >= buildingPrefabs.GetComponent<tower>().towerConfig.cost)
            {
                previewObject = Instantiate(buildingPrefabs);
                previewObject.GetComponent<Attack>().enabled = false;
            }
            else if (MainHall.instance.energy < buildingPrefabs.GetComponent<tower>().towerConfig.cost)
            {
                return;
            }
            hitPosition = hit.point;
            hitPosition = SnapToGrid(hitPosition);
            previewObject.transform.position = hitPosition;

            canPlace = !Physics.CheckBox(hitPosition, previewObject.GetComponent<Collider>().bounds.extents, previewObject.transform.rotation, obsticalLayer);

            SetColor(previewObject, canPlace ? Color.green : Color.red);
            SetMaterialTransparent(previewObject);
            if (touch.phase == TouchPhase.Ended && canPlace)
            {
                BuildTower();
                UpdateEnergy();
                playerInputForCamera.isFreeZone = true;
                GameObject.Destroy(previewObject);
            }
            else if (touch.phase == TouchPhase.Ended && !canPlace)
            {
                GameObject.Destroy(previewObject);
            }
        }
        else
        {
            GameObject.Destroy(previewObject);
        }
    }
    Vector3 SnapToGrid(Vector3 position)
    {
        Collider colider = previewObject.GetComponent<Collider>();
        if (colider != null)
        {
            
            if (Math.Abs(colider.bounds.min.y - previewObject.transform.position.y) < 0.1)
            {
                hightOffset = 0f;
            }
            else
            {
                hightOffset = colider.bounds.extents.y;
            }
            
        }
        return new Vector3(Mathf.Round(position.x), position.y + hightOffset, Mathf.Round(position.z));
    }
    void SetMaterialTransparent(GameObject gameObject)
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        Material newCloneMaterial = renderer.material;
        Color newcolor = newCloneMaterial.color;
        newcolor.a = 0.25f;
        if (renderer != null)
        {
            newCloneMaterial.color = newcolor;
        }
    }
    void SetColor(GameObject gameObject, Color color)
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        Material newCloneMaterial = renderer.material;
        newCloneMaterial.color = color;
    }
    void BuildTower()
    {
        buildingPrefabs.layer = 10;
        Renderer renderer = buildingPrefabs.GetComponent<Renderer>();
        renderer.material = renderer.sharedMaterial;
        Instantiate(buildingPrefabs, hitPosition, Quaternion.identity);
        buildingPrefabs.layer = 0;
    }
    void UpdateEnergy()
    {
        MainHall.instance.SetEnergy(buildingPrefabs.GetComponent<tower>().towerConfig.cost);
        PlayingPanle.instance.CheckCostOfTowerAndShowToUI();
    }

    public void SetBuilingPrefabsToBuild(int index)
    {
        buildingPrefabs = towers[index].GameObject();
    }
}
