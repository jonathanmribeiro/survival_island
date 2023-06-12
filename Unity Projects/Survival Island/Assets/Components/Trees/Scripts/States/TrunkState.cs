using SurvivalIsland.Common.Bases;
using SurvivalIsland.Common.Enums;
using SurvivalIsland.Common.Extensions;
using SurvivalIsland.Common.Interfaces;
using SurvivalIsland.Common.Models;
using System;
using UnityEngine;

namespace SurvivalIsland.Components.Trees
{
    public class TrunkState : PlayerDetectionBase, IPlayerActionState
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

        public void EnterState()
        {
            _canopy.SetActive(false);
            _trunk.SetActive(true);
            _sapling.SetActive(false);

            _manager.AddMultiple(InventoryItemType.Wood, _treeProps.MaxWoodAmount / 3);
        }

        public void UpdateState() {  /*Left empty on purpose*/ }

        public void ExitState() {  /*Left empty on purpose*/ }

        public PlayerActionTypes GetAction() => PlayerActionTypes.Chopping;

        public void ExecuteAction(Func<PlayerActionTypes, object, bool> playerActionCallback)
        {
            if (!_playerInRange)
                return;

            var randomItem = _manager.ObtainRandom(InventoryItemType.Wood);
            
            if (!_woodParticleSystem.isPlaying)
                _woodParticleSystem.Play();

            if (randomItem != null)
            {
                var actionExecutedSuccessfully = playerActionCallback.Invoke(PlayerActionTypes.Collecting, randomItem);

                if (actionExecutedSuccessfully)
                {
                    _manager.Remove(randomItem);
                    _treeProps.ReduceCurrentAmount(InventoryItemType.Wood);
                }
            }

            randomItem = _manager.ObtainRandom(InventoryItemType.Wood);

            if (randomItem == null)
                _manager.EnterGoneState();
        }
    }
}