using System;
using DefaultNamespace;
using UnityEngine;

namespace View
{
    public class WorldBoxView : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _selectedObject;
        [SerializeField] private Material _selectedHeightLight;
        [SerializeField] private Material _enemyHeightLight;
        [SerializeField] private Material _moveHeightLight;
        [SerializeField] private Transform _root;
        [SerializeField] private WorldBox _worldBox;
        private GameObject _itemGameObject;

        public void Setup(WorldBox worldBox)
        {
            _worldBox = worldBox;
        }

        public WorldBox GetWorldBox()
        {
            return _worldBox;
        }

        public void SetState(WorldBox.State state)
        {
            if (HasProps())
            {
                state = WorldBox.State.Close;
            }
            
            switch (state)
            {
                case WorldBox.State.None:
                    _selectedObject.gameObject.SetActive(false);
                    break;
                case WorldBox.State.Empty:
                    _selectedObject.gameObject.SetActive(false);
                    break;
                case WorldBox.State.Interactive:
                    _selectedObject.gameObject.SetActive(true);

                    _selectedObject.material = _moveHeightLight;
                    break;
                
                case WorldBox.State.Selected:
                    _selectedObject.gameObject.SetActive(true);
                    _selectedObject.material = _selectedHeightLight;
                    break;
                case WorldBox.State.Enemy:
                    _selectedObject.gameObject.SetActive(true);
                    _selectedObject.material = _enemyHeightLight;
                    break;
            }

            _worldBox.BoxState = state;
        }

        public async void CreateProps()
        {
            if (_worldBox.ItemID == String.Empty)
            {
                return;
            }

            SetState(WorldBox.State.Empty);
            
            if (_worldBox.BoxState == WorldBox.State.Close)
            {
                var item = await AssetProvider.LoadAssetAsync<GameObject>(_worldBox.ItemID);
                _itemGameObject = Instantiate(item, _root);
            }
        }

        public bool IsSameBox(WorldPosition comparableBoxPosition)
        {
            return comparableBoxPosition == _worldBox.WorldPosition;
        }

        public bool HasProps()
        {
            return !string.IsNullOrEmpty(_worldBox.ItemID);
        }
    }
}