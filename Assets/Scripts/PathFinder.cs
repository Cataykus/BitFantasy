using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    protected List<Tile> _poolToReset;
    protected List<Vector2> _movePointsQuery;


    protected void ResetTilesPool()
    {
        foreach (Tile tile in _poolToReset)
        {
            tile.Reset();
        }
    }

    protected void FindWay(float endPointX, float endPointY)
    {
        Vector2 startPoint = transform.position;
        Vector2 endPoint = new Vector2(endPointX, endPointY);

        int d = 0;

        List<Tile> neighbours = new List<Tile>();
        List<Tile> newNeighbours = new List<Tile>();


        Tile startTile = GetTile(startPoint.x, startPoint.y);
        Tile endTile = GetTile(endPointX, endPointY);

        startTile.MarkValue(0);
        endTile.MarkValue(int.MaxValue);

        _poolToReset.Add(startTile);
        _poolToReset.Add(endTile);

        neighbours.Add(startTile);

        while (d < 200)
        {
            foreach (Tile tile in neighbours)
            {
                float x = tile.transform.position.x;
                float y = tile.transform.position.y;

                if (x + 1 == endPointX && y == endPointY)
                {
                    BuildWay(endPoint);
                    return;
                }
                else
                {
                    Tile neighbour = GetTile(x + 1, y);

                    if (neighbour && neighbour.IsMarked == false && neighbour.TryGetComponent(out Ground ground))
                    {
                        neighbour.MarkValue(d + 1);
                        _poolToReset.Add(neighbour);
                        newNeighbours.Add(neighbour);
                    }

                }

                if (x - 1 == endPointX && y == endPointY)
                {
                    BuildWay(endPoint);
                    return;
                }
                else
                {
                    Tile neighbour = GetTile(x - 1, y);

                    if (neighbour && neighbour.IsMarked == false && neighbour.TryGetComponent(out Ground ground))
                    {
                        neighbour.MarkValue(d + 1);
                        _poolToReset.Add(neighbour);
                        newNeighbours.Add(neighbour);
                    }
                }

                if (x == endPointX && y + 1 == endPointY)
                {
                    BuildWay(endPoint);
                    return;
                }
                else
                {
                    Tile neighbour = GetTile(x, y + 1);

                    if (neighbour && neighbour.IsMarked == false && neighbour.TryGetComponent(out Ground ground))
                    {
                        neighbour.MarkValue(d + 1);
                        _poolToReset.Add(neighbour);
                        newNeighbours.Add(neighbour);
                    }
                }

                if (x == endPointX && y - 1 == endPointY)
                {
                    BuildWay(endPoint);
                    return;
                }
                else
                {
                    Tile neighbour = GetTile(x, y - 1);

                    if (neighbour && neighbour.IsMarked == false && neighbour.TryGetComponent(out Ground ground))
                    {
                        neighbour.MarkValue(d + 1);
                        _poolToReset.Add(neighbour);
                        newNeighbours.Add(neighbour);
                    }
                }

                if (x + 1 == endPointX && y + 1 == endPointY)
                {
                    BuildWay(endPoint);
                    return;
                }
                else
                {
                    Tile neighbour = GetTile(x + 1, y + 1);

                    if (neighbour && neighbour.IsMarked == false && neighbour.TryGetComponent(out Ground ground))
                    {
                        neighbour.MarkValue(d + 1);
                        _poolToReset.Add(neighbour);
                        newNeighbours.Add(neighbour);
                    }
                }

                if (x + 1 == endPointX && y - 1 == endPointY)
                {
                    BuildWay(endPoint);
                    return;
                }
                else
                {
                    Tile neighbour = GetTile(x + 1, y - 1);

                    if (neighbour && neighbour.IsMarked == false && neighbour.TryGetComponent(out Ground ground))
                    {
                        neighbour.MarkValue(d + 1);
                        _poolToReset.Add(neighbour);
                        newNeighbours.Add(neighbour);
                    }
                }

                if (x - 1 == endPointX && y + 1 == endPointY)
                {
                    BuildWay(endPoint);
                    return;
                }
                else
                {
                    Tile neighbour = GetTile(x - 1, y + 1);

                    if (neighbour && neighbour.IsMarked == false && neighbour.TryGetComponent(out Ground ground))
                    {
                        neighbour.MarkValue(d + 1);
                        _poolToReset.Add(neighbour);
                        newNeighbours.Add(neighbour);
                    }
                }

                if (x - 1 == endPointX && y - 1 == endPointY)
                {
                    BuildWay(endPoint);
                    return;
                }
                else
                {
                    Tile neighbour = GetTile(x - 1, y - 1);

                    if (neighbour && neighbour.IsMarked == false && neighbour.TryGetComponent(out Ground ground))
                    {
                        neighbour.MarkValue(d + 1);
                        _poolToReset.Add(neighbour);
                        newNeighbours.Add(neighbour);
                    }
                }
            }

            d++;

            neighbours = newNeighbours;

            newNeighbours = new List<Tile>();
        }

        ResetTilesPool();
    }

    protected void BuildWay(Vector2 endPoint)
    {
        int min = int.MaxValue;

        Vector2 nextPoint = endPoint;
        Vector2 newNextPoint = endPoint;

        _movePointsQuery.Add(endPoint);
        //int d = 0;
        while (true)
        {
            //d++;
            if (GetTile(nextPoint.x + 1, nextPoint.y) && min > GetTile(nextPoint.x + 1, nextPoint.y).MarkedValue && GetTile(nextPoint.x + 1, nextPoint.y).IsMarked)
            {
                Tile tile = GetTile(nextPoint.x + 1, nextPoint.y);
                min = tile.MarkedValue;
                newNextPoint = tile.transform.position;
            }
            if (GetTile(nextPoint.x - 1, nextPoint.y) && min > GetTile(nextPoint.x - 1, nextPoint.y).MarkedValue && GetTile(nextPoint.x - 1, nextPoint.y).IsMarked)
            {
                Tile tile = GetTile(nextPoint.x - 1, nextPoint.y);
                min = tile.MarkedValue;
                newNextPoint = tile.transform.position;
            }
            if (GetTile(nextPoint.x, nextPoint.y - 1) && min > GetTile(nextPoint.x, nextPoint.y - 1).MarkedValue && GetTile(nextPoint.x, nextPoint.y - 1).IsMarked)
            {
                Tile tile = GetTile(nextPoint.x, nextPoint.y - 1);
                min = tile.MarkedValue;
                newNextPoint = tile.transform.position;
            }
            if (GetTile(nextPoint.x, nextPoint.y + 1) && min > GetTile(nextPoint.x, nextPoint.y + 1).MarkedValue && GetTile(nextPoint.x, nextPoint.y + 1).IsMarked)
            {
                Tile tile = GetTile(nextPoint.x, nextPoint.y + 1);
                min = tile.MarkedValue;
                newNextPoint = tile.transform.position;
            }
            if (GetTile(nextPoint.x + 1, nextPoint.y + 1) && min > GetTile(nextPoint.x + 1, nextPoint.y + 1).MarkedValue && GetTile(nextPoint.x + 1, nextPoint.y + 1).IsMarked)
            {
                Tile tile = GetTile(nextPoint.x + 1, nextPoint.y + 1);
                min = tile.MarkedValue;
                newNextPoint = tile.transform.position;
            }
            if (GetTile(nextPoint.x + 1, nextPoint.y - 1) && min > GetTile(nextPoint.x + 1, nextPoint.y - 1).MarkedValue && GetTile(nextPoint.x + 1, nextPoint.y - 1).IsMarked)
            {
                Tile tile = GetTile(nextPoint.x + 1, nextPoint.y - 1);
                min = tile.MarkedValue;
                newNextPoint = tile.transform.position;
            }
            if (GetTile(nextPoint.x - 1, nextPoint.y + 1) && min > GetTile(nextPoint.x - 1, nextPoint.y + 1).MarkedValue && GetTile(nextPoint.x - 1, nextPoint.y + 1).IsMarked)
            {
                Tile tile = GetTile(nextPoint.x - 1, nextPoint.y + 1);
                min = tile.MarkedValue;
                newNextPoint = tile.transform.position;
            }
            if (GetTile(nextPoint.x - 1, nextPoint.y - 1) && min > GetTile(nextPoint.x - 1, nextPoint.y - 1).MarkedValue && GetTile(nextPoint.x - 1, nextPoint.y - 1).IsMarked)
            {
                Tile tile = GetTile(nextPoint.x - 1, nextPoint.y - 1);
                min = tile.MarkedValue;
                newNextPoint = tile.transform.position;
            }

            if (min == 0)
            {
                ResetTilesPool();
                _movePointsQuery.Reverse();
                return;
            }

            nextPoint = new Vector2(newNextPoint.x, newNextPoint.y);
            _movePointsQuery.Add(nextPoint);
        }
    }

    protected void ResetMovePointsQuery()
    {
        _movePointsQuery.Clear();
    }

    protected Tile GetTile(float x, float y)
    {
        try
        {
            Collider2D tile = Physics2D.OverlapBox(new Vector2(x, y), Vector2.zero, 0, 9);

            if (tile.TryGetComponent(out Ground ground))
                return ground;
            else
                return null;
        }
        catch (System.Exception)
        {
            return null;
        }
    }

    protected bool TryGetEnemy(float x, float y, out Enemy enemy)
    {
        try
        {
            Collider2D enemyCollider = Physics2D.OverlapBox(new Vector2(x, y), Vector2.zero, 0, 1 << 11);

            if (enemyCollider.TryGetComponent(out Enemy newEnemy))
            {
                enemy = newEnemy;
                return true;
            }
            else
            {
                enemy = null;
                return false;
            }
        }
        catch (System.Exception)
        {
            enemy = null;
            return false;
        }
    }
}
