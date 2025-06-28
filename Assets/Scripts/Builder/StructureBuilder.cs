using System.Collections;
using System.Collections.Generic;
using TowerDefense.Towers;
using Unity.VisualScripting;
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

    public bool isBuildingState;
    public bool canPlace;
    public Vector3 hitPosition;

    public List<tower> towers;

    private void Start()
    {
        isBuildingState = true;
    }

    void Update()
    {
        if (isBuildingState && Input.touchCount > 0)
        {
            Builder(); 
        }
    }

    void Builder()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxPlacementDistance, buildLayer))
        {
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

            if (Input.GetMouseButtonUp(0) && canPlace)
            {
                BuildTower();
                UpdateEnergy();
                GameObject.Destroy(previewObject);
            }
            else if (Input.GetMouseButtonUp(0) && !canPlace)
            {
                GameObject.Destroy(previewObject);
            }
        }
    }
    Vector3 SnapToGrid(Vector3 position)
    {
        Collider colider = previewObject.GetComponent<Collider>();
        if (colider != null)
        {
            hightOffset = colider.bounds.extents.y;
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
        PlayingPanle.instance.ShowInfoToUI(PlayingPanle.instance.mainHallEnergy_Text , MainHall.instance.energy.ToString());
    }
}
