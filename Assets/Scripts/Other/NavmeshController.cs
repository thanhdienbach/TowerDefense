using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshController : MonoBehaviour
{
    public NavMeshSurface groundNavmesh;
    public NavMeshSurface airNavmesh;

    public NavMeshDataInstance groundInstance;
    public NavMeshDataInstance airInstance;

    public void Init()
    {
        groundInstance = NavMesh.AddNavMeshData(groundNavmesh.navMeshData);
        airInstance = NavMesh.AddNavMeshData(airNavmesh.navMeshData);
    }
}
