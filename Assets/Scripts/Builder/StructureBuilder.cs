using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


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
            if (previewObject == null)
            {
                previewObject = Instantiate(buildingPrefabs);
            }
            Vector3 position = hit.point;
            position = SnapToGrid(position);
            previewObject.transform.position = position;

            canPlace = !Physics.CheckBox(position, previewObject.GetComponent<Collider>().bounds.extents, previewObject.transform.rotation, obsticalLayer);

            SetColor(previewObject, canPlace ? Color.green : Color.red);
            SetMaterialTransparent(previewObject);

            if (Input.GetMouseButtonUp(0) && canPlace)
            {
                previewObject.transform.position = new Vector3 (0,-100, 0);
                buildingPrefabs.layer = 10;
                Renderer renderer = buildingPrefabs.GetComponent<Renderer>();
                renderer.material = renderer.sharedMaterial;
                Instantiate(buildingPrefabs, position, Quaternion.identity);
                buildingPrefabs.layer = 0;
            }
            else if (Input.GetMouseButtonUp(0) && !canPlace)
            {
                previewObject.transform.position = new Vector3(0, -100, 0);
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
}
