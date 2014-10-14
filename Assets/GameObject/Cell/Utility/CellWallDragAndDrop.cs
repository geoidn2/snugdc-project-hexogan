﻿using UnityEngine;
using System.Collections;

public class CellWallDragAndDrop : CellPartDragAndDrop
{
	protected override bool IsLocatable(HexCell<Cell> _cell, HexCoor _coor)
	{
		if (! base.IsLocatable(_cell, _coor))
			return false;

		return _cell == null || ! _cell.data.wall;
	}

	protected override bool Attach(HexCell<Cell> _cell, HexCoor _coor)
	{
		if (_cell != null && _cell.data.wall)
			return false;

		if (_cell == null)
		{
			var _cellGO = ComponentHelper.Instantiate(cellPrf);
			_cell = cellGrid.Add(_cellGO, _coor);
		}

		_cell.data.wall = GetComponent<CellWall>();

		return true;
	}
}