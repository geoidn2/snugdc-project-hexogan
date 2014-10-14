﻿using UnityEngine;
using System.Collections;

public class CellGrid : MonoBehaviour
{
	private readonly HexGrid<Cell> m_Cells = new HexGrid<Cell>();

	public bool TryGet(HexCoor _coor, out HexCell<Cell> _output)
	{
		return m_Cells.TryGet(_coor, out _output);
	}

	public static void Locate(Transform _transform, HexCoor _coor)
	{
		var _pos = (Vector3) (Vector2) _coor;
		var _posOld = _transform.localPosition;
		_pos.z = _posOld.z;
		_transform.localPosition = _pos;
	}

	public HexCell<Cell> Add(Cell _cell, HexCoor _coor)
	{
		var _hexCell = new HexCell<Cell>(_cell);
		if (!m_Cells.TryAdd(_coor, _hexCell))
			return null;
		
		_cell.SetGrid(this);
		_cell.transform.parent = transform;
		Locate(_cell.transform, _coor);

		var _side = -1;
		foreach (var _neighbor in _hexCell.GetNeighbors())
		{
			++_side;
			if (_neighbor == null) continue;
			_cell.SetNeighbor(_neighbor.data, _side);
		}
		
		return _hexCell;
	}

	public bool IsRemovable(Cell _cell)
	{
		return _cell.grid == this;
	}

	public void Remove(Cell _cell)
	{
		if (! IsRemovable(_cell)) return;
		_cell.DetachGrid();
	}
}