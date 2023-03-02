using PlayerCharacter;
using System.Collections.Generic;
using AnomalySystem.ScriptableObjects;
using UnityEngine;
using UnityEditor;

namespace AnomalySystem
{
    public class AnomalyHandler : MonoBehaviour
    {
        //TODO - Make a reference to the current day
        public int day = 1;
        private Player _player;

        [SerializeField]
        private List<Anomaly> _anomalies;
        [SerializeField]
        private List<Anomaly> _selectedAnomalies;

        [Header("Timer and trigger settings")]
        [SerializeField]
        [Tooltip("Sets inital time which anomalies can be triggered")]
        private int triggerTime;
        [SerializeField]
        [Tooltip("Sets time in between triggers")]
        private int delayTime;

        [Header("Difficulty and balance settings")]
        [SerializeField]
        private float _currentProbability = 0.25f;
        [SerializeField]
        [Tooltip("Sets maximum amount of anomalies that can be triggered on this room.")]
        private int _maxAnomalies;

        private int _currentAnomalies = 0;

        [Header("Scene Persistance Settings")]
        [SerializeField]
        private AnomalyHandlerData _handlerData;
        private string _roomName;

        private void Awake()
        {
            _currentAnomalies = 0;
            _roomName = gameObject.scene.name + "_";
        }
        private void Start() 
        {
            _player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Player>();
            AdjustToDay();
            InvokeRepeating("TriggerRandomAnomaly", triggerTime, delayTime);
        }
        private void AdjustToDay() 
        {
            SetUpStoredData();

            if (day == 1)
            {
                _currentProbability = 0.25f;

                for (int i = 0; i < _anomalies.Count; i++)
                {
                    if (_anomalies[i].GetData().aType != AnomalyType.NIGHTMARISH)
                    {
                        _selectedAnomalies.Add(_anomalies[i]);
                    }
                    else _anomalies[i].Disable();
                }
            }

        }

        private void TriggerRandomAnomaly()
        {
            if (Random.value > 1 - _currentProbability)
            {
                int rand = Random.Range(0, _selectedAnomalies.Count + 1);
                TriggerAnomaly(rand);
            }
        }

        private void TriggerAnomaly(int i) 
        {

            if (_selectedAnomalies.Count > 0 && _currentAnomalies < _maxAnomalies && _currentAnomalies != _selectedAnomalies.Count)
            {
                if (i > _selectedAnomalies.Count - 1)
                    i = 0;

                if (_selectedAnomalies[i].isActive() == false)
                {
                    _selectedAnomalies[i].Manifest(_player);
                    _currentAnomalies++;
                    return;
                }
                else TriggerAnomaly(++i);
            }
           
        }

        private void SetUpStoredData()
        {
           
            if (_handlerData.currentAnomalies == 0)
            {
                foreach (var anomaly in _anomalies)
                {
                    anomaly.setOriginalPos(anomaly.gameObject.transform.position,
                                           anomaly.gameObject.transform.rotation,
                                           anomaly.gameObject.transform.localScale);
                }
            }
            else
            {
                int temp = 0;

                while (_currentAnomalies < _handlerData.currentAnomalies)
                {
                    //Get from anomaly array and set objects part


                    //Check from anomaly inventory and then setup


                    _anomalies[temp].transform.position = _handlerData.anomalies[temp].currentPos;
                    _anomalies[temp].transform.rotation = _handlerData.anomalies[temp].currentRot;
                    _anomalies[temp].transform.localScale = _handlerData.anomalies[temp].currentScale;

                    _anomalies[temp].setOriginalPos(_handlerData.anomalies[temp].originalPos,
                                                    _handlerData.anomalies[temp].originalRot,
                                                    _handlerData.anomalies[temp].originalScale);




                    if (_handlerData.anomalies[temp].isActive)
                    {
                        _anomalies[temp].Enable();
                        _currentAnomalies++;
                    }

                    temp++;
                }
            }
        }

        public void UpdateAnomalyHandler()
        {
            _handlerData.currentAnomalies = _currentAnomalies;

            if (_handlerData.anomalies.Count > 0)
            {
                for (int i = 0; i < _anomalies.Count; i++)
                {
                    _handlerData.anomalies[i].originalPos = _anomalies[i].GetOriginalPos().position;
                    _handlerData.anomalies[i].originalRot = _anomalies[i].GetOriginalPos().rotation;
                    _handlerData.anomalies[i].originalScale = _anomalies[i].GetOriginalPos().localScale;
                    _handlerData.anomalies[i].isActive = _anomalies[i].isActive();

                    _handlerData.anomalies[i].currentPos = _anomalies[i].gameObject.transform.position;
                    _handlerData.anomalies[i].currentRot = _anomalies[i].gameObject.transform.rotation;
                    _handlerData.anomalies[i].currentScale = _anomalies[i].gameObject.transform.localScale;
                }
            }
            else
            {
                for (int i = 0; i < _anomalies.Count; i++)
                {
                    //Saving in the anomaly scriptble object
                    var newData = ScriptableObject.CreateInstance<AnomalyStoringData>();

                    newData.originalPos = _anomalies[i].GetOriginalPos().position;
                    newData.currentPos = _anomalies[i].gameObject.transform.position;

                    newData.originalRot = _anomalies[i].GetOriginalPos().rotation;
                    newData.currentRot = _anomalies[i].gameObject.transform.rotation;

                    newData.originalScale = _anomalies[i].GetOriginalPos().localScale;
                    newData.currentScale = _anomalies[i].gameObject.transform.localScale;

                    newData.isActive = _anomalies[i].isActive();

                    _handlerData.anomalies.Add(newData);

#if UNITY_EDITOR
                    EditorUtility.SetDirty(newData);
                    AssetDatabase.SaveAssetIfDirty(newData);

                    AssetDatabase.CreateAsset(newData, "Assets/Scripts/Data/AnomalyStoragedData/AnomalyStoragedData" + _roomName + i + ".asset");
                    AssetDatabase.SaveAssets();
#endif
                }
            }
#if UNITY_EDITOR
            EditorUtility.SetDirty(_handlerData);
            AssetDatabase.SaveAssetIfDirty(_handlerData);
#endif
        }

    }
}

