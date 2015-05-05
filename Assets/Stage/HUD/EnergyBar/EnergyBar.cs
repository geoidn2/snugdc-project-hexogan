﻿using JetBrains.Annotations;
using UnityEngine;

namespace HX.Stage
{
	public class EnergyBar : MonoBehaviour
	{
		private const float THRESHHOLD = 0.1f;
		private const float LENGTH_MAX = 800;
		private const float VALUE_TO_LEN = 1;

		private float mValue;
		public float value
		{
			get { return mValue; }
			set
			{
				if (Mathf.Abs(this.value - value) < THRESHHOLD)
					return;

				mValue = value;
				Refresh();
			}
		}

		private int mMax;
		public int max
		{
			get { return mMax; }
			set
			{
				mMax = value;
				var _width = (int)Mathf.Min(value * VALUE_TO_LEN, LENGTH_MAX);
				mBar.width = _width;
				mBack.width = _width;
				Refresh();
			}
		}

		[SerializeField, UsedImplicitly] 
		private UISprite mBar;

		[SerializeField, UsedImplicitly]
		private UISprite mBack;

		[SerializeField, UsedImplicitly]
		private UIProgressBar mProgress;

		void Refresh()
		{
			if (max != 0)
				mProgress.value = value / max;
		}
	}
}