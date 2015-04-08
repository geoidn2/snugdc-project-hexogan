﻿using Gem;
using UnityEngine;

namespace HX.UI
{
	public class LobbyController : MonoBehaviour
	{
		[SerializeField] private AnatomyData mAnatomyData;
		[SerializeField] private AnatomyView mAnatomy;

		[SerializeField] private UIButton mGameStartButton;

		[SerializeField] private Neo mNeo;

		private WorldTransitionData mTransition;

		void Start()
		{
			if (!DisketManager.isLoaded)
				DisketManager.LoadOrDefault("test");

			mTransition.scene = "world";

			mAnatomy.Setup(mAnatomyData);
			mAnatomy.onSelectVertex += OnSelectAnatomyVertex;

			mGameStartButton.onClick.Add(new EventDelegate(OnClickStartButton));

			mNeo.mechanics.Build(DisketManager.saveData.neoStructure);
		}

		void OnClickStartButton()
		{
			if (string.IsNullOrEmpty(mTransition.tmxPath))
			{
				L.E("tmx is not specified.");
				return;
			}

			TransitionManager.StartWorld(mTransition);
		}

		void OnSelectAnatomyVertex(AnatomyVertexView v)
		{
			mTransition.tmxPath = mAnatomy.data.tmxDir + v.data.tmx;
		}
	}
}