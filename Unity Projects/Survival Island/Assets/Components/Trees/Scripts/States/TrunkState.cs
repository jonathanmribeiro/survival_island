﻿using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Extensions;
using System;
using UnityEngine;

namespace SurvivalIsland.Components.Trees
{
    public class TrunkState : StateBase
    {
        private readonly GameObject _canopy;
        private readonly GameObject _trunk;
        private readonly GameObject _sapling;

        private readonly TreeManager _manager;
        private readonly TreeProps _treeProps;
        
        private readonly ParticleSystem _woodParticleSystem;

        public TrunkState(TreeManager manager, TreeProps treeProps)
        {
            _manager = manager;
            _treeProps = treeProps;

            _canopy = _manager.gameObject.FindChild("Canopy");
            _trunk = _manager.gameObject.FindChild("Trunk");
            _sapling = _manager.gameObject.FindChild("Sapling");

            _woodParticleSystem = _trunk.GetComponent<ParticleSystem>();
        }

        public override void EnterState()
        {
            _canopy.SetActive(false);
            _trunk.SetActive(true);
            _sapling.SetActive(false);

            _manager.TreeInventory.ForceAmount(InventoryItemType.Wood, _treeProps.MaxWoodAmount / 3);
        }

        public override PlayerActionTypes GetAction() 
            => PlayerActionTypes.Chopping;

        public override void ExecuteAction(Func<PlayerActionTypes, object, bool> playerActionCallback)
        {
            var randomItem = _manager.TreeInventory.ObtainRandom(InventoryItemType.Wood);
            
                _woodParticleSystem.TryPlay();

            if (randomItem != null)
            {
                var actionExecutedSuccessfully = playerActionCallback.Invoke(PlayerActionTypes.Collecting, randomItem);

                if (actionExecutedSuccessfully)
                {
                    _manager.TreeInventory.Remove(randomItem);
                }
            }

            randomItem = _manager.TreeInventory.ObtainRandom(InventoryItemType.Wood);

            if (randomItem == null)
                _manager.EnterGoneState();
        }
    }
}