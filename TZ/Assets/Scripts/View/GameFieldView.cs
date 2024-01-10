using System;
using System.Collections.Generic;
using System.Linq;
using Core.MessengerStatic;
using DefaultNamespace;
using UnityEngine;

namespace View
{
    public class GameFieldView : MonoBehaviour
    {
        [SerializeField] private Transform _rootObject;
        [SerializeField] private WorldBoxView _boxViewPrefab;
        [SerializeField] private List<WorldBoxView> _worldBoxViews;

        private void Start()
        {
            //Messenger<WorldBox>.AddListener(Constnts.Events.Game.BoxClick, OnBoxClick);
            Messenger<List<WorldBox>>.AddListener(Constnts.Events.GameField.NewLevelCreated, CreateBoxes);
            Messenger<List<WorldBox>>.AddListener(Constnts.Events.GameField.UpdateBoxes, UpdateBoxes);
            Messenger.AddListener(Constnts.Events.GameField.ResetState, ResetState);
        }

        private void UpdateBoxes(List<WorldBox> obj)
        {
            foreach (var worldBox in obj)
            {
                var box = _worldBoxViews.FirstOrDefault(x => x.IsSameBox(worldBox.WorldPosition));
                if (box == null)
                {
                    continue;
                }

                box.SetState(worldBox.BoxState);
            }
        }

        private void ResetState()
        {
            foreach (var worldBoxView in _worldBoxViews)
            {
                worldBoxView.SetState(WorldBox.State.Empty);
            }
        }

        private void CreateBoxes(List<WorldBox> boxes)
        {
            ClearGameField();

            foreach (var worldBox in boxes)
            {
                var newBox = Instantiate(_boxViewPrefab, new Vector3(worldBox.WorldPosition.X * 2, 0, worldBox.WorldPosition.Y * 2), Quaternion.identity, _rootObject);
                newBox.Setup(worldBox);
                _worldBoxViews.Add(newBox);
            }

            foreach (var worldBoxView in _worldBoxViews)
            {
                worldBoxView.CreateProps();
            }
        }

        private void ClearGameField()
        {
            if (_worldBoxViews == null)
            {
                return;
            }

            for (int i = 0; i < _worldBoxViews.Count; i++)
            {
                Destroy(_worldBoxViews[i], 1);
            }

            _worldBoxViews.Clear();
        }
    }
}