using Level3;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LinePathFind : MonoBehaviour
{
    public RectGrid rectGrid;

    [SerializeField]
    private Transform fromT = null;
    [SerializeField]
    private Transform toT = null;

    public Queue<Vector2Int> wayPoints = new Queue<Vector2Int>();

    private List<Vector2Int> zeros = new List<Vector2Int>() { Vector2Int.zero, Vector2Int.zero };

    public GameObject line;

    private Vector2 lineStartPoint;
    private Vector2 lineEndPoint;

    // Update is called once per frame
    void Update()
    {
#if UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
        {
            // Hit detection
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (!hit)
                return;
            if (!hit.transform.CompareTag("Component"))
            {
                Debug.Log("Please select a component");
                return;
            }
            if (!fromT && !toT)
            {
                fromT = hit.transform;
            }
            else if (fromT && !toT)
            {
                toT = hit.transform;
                if (toT == fromT)
                {
                    Debug.Log("Please select a different component");
                    toT = null;
                    return;
                }
                // When from and to are selected, find the nearest nodes of them
                List<Vector2Int> nearestNodes = GetNearestNode();
                if (nearestNodes[0] == zeros[0] || nearestNodes[1] == zeros[1])
                {
                    Debug.Log("No nodes found.");
                    fromT = null;
                    toT = null;
                    return;
                }
                else
                {
                    // Find path from from to to
                    SetDestination(nearestNodes[0], nearestNodes[1], rectGrid, rectGrid.pathFinder);

                    fromT = null;
                    toT = null;
                }
            }
        }
#endif
    }

    private List<Vector2Int> GetNearestNode()
    {
        // Get the two nearest connection points of from and to
        List<Transform> nearestTwoPoints = CalculateNearestConnectionPoints(fromT, toT);
        if (!nearestTwoPoints[0] || !nearestTwoPoints[1])
        {
            Debug.Log("No nearest points found.");
            return zeros;
        }
        // Vector2 fromPoint is the nearest connection point of from
        Vector2 fromPoint = (Vector2)nearestTwoPoints[0].position;
        // fromPointInt is the nearest connection point of from in Vector2Int
        Vector2Int fromPointInt = new Vector2Int(Mathf.RoundToInt(nearestTwoPoints[0].position.x), Mathf.RoundToInt(nearestTwoPoints[0].position.y));
        // Get the neighbours of fromPoint
        List<Vector2Int> fromPointNeighbours = rectGrid.GetNeighbours(fromPointInt, true);

        // Similar to from
        Vector2 toPoint = (Vector2)nearestTwoPoints[1].position;
        Vector2Int toPointInt = new Vector2Int(Mathf.RoundToInt(nearestTwoPoints[1].position.x), Mathf.RoundToInt(nearestTwoPoints[1].position.y));
        List<Vector2Int> toPointNeighbours = rectGrid.GetNeighbours(toPointInt, true);

        /// distanceFrom is the float distance between fromPoint and nearest neighbour Vector2Int of fromPoint,
        /// this is to make sure that the distance will be the smallest as
        /// distanceFromInt is the int distance between fromPoint and nearest neighbour Vector2Int of fromPoint,
        /// distanceFromInt may be the same for the four points around fromPoint,
        /// so we need to use distanceFrom to make sure we are choosing the smallest
        float distanceFrom = 10000f;
        float distanceFromInt = 10000f;
        Vector2Int nearestPointFrom = Vector2Int.zero;
        foreach (Vector2Int point in fromPointNeighbours)
        {
            // Check if the point cell is walkable
            if (!rectGrid.transform.Find("cell_" + point.x + "_" + point.y).GetComponent<RectGridCell>().isWalkable)
            {
                continue;
            }
            // Calculate the distance between points
            if (Vector2Int.Distance(fromPointInt, point) <= distanceFromInt)
            {
                if (Vector2.Distance(fromPoint, point) < distanceFrom)
                {
                    distanceFromInt = Vector2Int.Distance(fromPointInt, point);
                    distanceFrom = Vector2.Distance(fromPoint, point);
                    nearestPointFrom = point;
                }
            }
        }
        // When no nearest point found
        if (nearestPointFrom == Vector2Int.zero)
        {
            Debug.Log("Can't find a nearest point to start, try to get more neighbours");
            return new List<Vector2Int>() { Vector2Int.zero, Vector2Int.zero };
        }

        // Similar to from
        float distanceTo = 10000f;
        float distanceToInt = 10000f;
        Vector2Int nearestPointTo = Vector2Int.zero;
        foreach (Vector2Int point in toPointNeighbours)
        {
            if (!rectGrid.transform.Find("cell_" + point.x + "_" + point.y).GetComponent<RectGridCell>().isWalkable)
            {
                continue;
            }
            if (Vector2Int.Distance(toPointInt, point) <= distanceToInt)
            {
                if (Vector2.Distance(toPoint, point) < distanceTo)
                {
                    distanceToInt = Vector2Int.Distance(toPointInt, point);
                    distanceTo = Vector2.Distance(toPoint, point);
                    nearestPointTo = point;
                }
            }
        }
        if (nearestPointTo == Vector2Int.zero)
        {
            Debug.Log("Can't find a nearest point to start, try to get more neighbours");
            return new List<Vector2Int>() { Vector2Int.zero, Vector2Int.zero };
        }
        return new List<Vector2Int>() { nearestPointFrom, nearestPointTo };
    }

    private List<Transform> CalculateNearestConnectionPoints(Transform from, Transform to)
    {
        // Create two lists to store all the connection points of from and to
        List<Transform> fromConnectionPoints = new();
        List<Transform> toConnectionPoints = new();

        for (int i = 0; i < from.childCount; i++)
        {
            // add all the child connection points to the list.
            fromConnectionPoints.Add(from.GetChild(i));
        }

        for (int i = 0; i < to.childCount; i++)
        {
            toConnectionPoints.Add(to.GetChild(i));
        }
        // initial distance
        float distance = 10000f;
        Transform fromPoint = null;
        Transform toPoint = null;
        for (int i = 0; i < fromConnectionPoints.Count; i++)
        {
            for (int j = 0; j < toConnectionPoints.Count; j++)
            {
                // if the connection point is already with line drawn, skip it.
                if (!fromConnectionPoints[i].GetComponent<LineLimit>().AllowDrawLine ||
                    !toConnectionPoints[j].GetComponent<LineLimit>().AllowDrawLine)
                    continue;
                float newDist = Vector2.Distance(fromConnectionPoints[i].position, toConnectionPoints[j].position);
                if (newDist < distance)
                {
                    // if the new distance is smaller, update the distance and the points.
                    distance = newDist;
                    fromPoint = fromConnectionPoints[i];
                    toPoint = toConnectionPoints[j];
                }
            }
        }
        if (fromPoint && toPoint)
        {
            // set the two nearest connection points to be not allow draw line, then return the two points transform
            fromPoint.GetComponent<LineLimit>().AllowDrawLine = false;
            toPoint.GetComponent<LineLimit>().AllowDrawLine = false;
        }
        lineStartPoint = fromPoint.position;
        lineEndPoint = toPoint.position;
        return new List<Transform>() { fromPoint, toPoint };
    }

    public void SetDestination(Vector2Int origin, Vector2Int destination, RectGrid grid, PathFinder pathFinder = null)
    {
        if (pathFinder.status != PathFinderStatus.RUNNING)
        {
                // clear all the waypoints.
                wayPoints.Clear();

                pathFinder.Init(origin, destination);
                //grid.ResetCellColors();
                // Start a coroutine to do go to loop the pathfinding steps.
                StartCoroutine(Coroutine_PathFinding(pathFinder, grid));
        }
    }

    IEnumerator Coroutine_PathFinding(PathFinder pathFinder, RectGrid grid)
    {
        while (pathFinder.status == PathFinderStatus.RUNNING)
        {
            pathFinder.Step(false); ;
            yield return null;
        }
        // completed pathfinding.

        if (pathFinder.status == PathFinderStatus.FAILURE)
        {
            Debug.Log("Failed finding a path. No valid path exists");
        }
        if (pathFinder.status == PathFinderStatus.SUCCESS)
        {
            // found a valid path.
            // accumulate all the locations by traversing from goal to the start.
            List<Vector2Int> reversePathLocations = new List<Vector2Int>();
            PathFinderNode node = pathFinder.GetCurrentNode();
            while (node != null)
            {
                reversePathLocations.Add(node.location);
                node = node.parent;
            }
            GameObject line = Instantiate(this.line, transform);
            AddFromRotatePoint(line.GetComponent<DrawLine>(), reversePathLocations[reversePathLocations.Count - 1]);
            // add all these points to the waypoints.
            for (int i = reversePathLocations.Count - 1; i >= 0; i--)
            {
                AddWayPoint(reversePathLocations[i]);
                line.GetComponent<DrawLine>().points.Add(reversePathLocations[i]);
                rectGrid.transform.Find("cell_" + reversePathLocations[i].x + "_" + reversePathLocations[i].y).GetComponent<RectGridCell>().SetNonWalkable();
            }
            AddToRotatePoint(line.GetComponent<DrawLine>(), reversePathLocations[0]);
            line.GetComponent<DrawLine>().finishedAddingPoints = true;
        }
    }

    public void AddWayPoint(Vector2Int point)
    {
        wayPoints.Enqueue(point);
    }

    private void AddFromRotatePoint(DrawLine line, Vector2Int firstNodePos)
    {
        line.points.Add(lineStartPoint);
        line.points.Add(new Vector2(firstNodePos.x, lineStartPoint.y));
    }

    private void AddToRotatePoint(DrawLine line, Vector2Int lastNodePos)
    {
        line.points.Add(new Vector2(lastNodePos.x, lineEndPoint.y));
        line.points.Add(lineEndPoint);
    }
}
