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
    public Touch touch0;

    private void Start()
    {
        isBuildingState = true;
    }

    void Update()
    {
        if (isBuildingState)
        {
            Builder();
        }
    }

    void Builder()
    {
        if (Input.GetTouch(0).position == null) return;
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

            bool canPlace = !Physics.CheckBox(position, previewObject.GetComponent<Collider>().bounds.extents, previewObject.transform.rotation, obsticalLayer);

            SetColor(previewObject, canPlace ? Color.green : Color.red);
            SetMaterialTransparent(previewObject);

            if (Input.GetMouseButtonUp(0) && canPlace)
            {
                previewObject.transform.position = new Vector3 (0,-100, 0);
                buildingPrefabs.layer = 10;
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
        if (renderer != null)
        {
            Color newcolor = renderer.material.color;
            newcolor.a = 0.5f;
            renderer.material.color = newcolor;
        }
    }
    void SetColor(GameObject gameObject, Color color)
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        renderer.material.color = color;
    }
}
