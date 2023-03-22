using PlayerCharacter;
using System.Collections.Generic;
using AnomalySystem.ScriptableObjects;
using UnityEngine;
using UnityEditor;

namespace AnomalySystem
{
    public class AnomalyHandler : MonoBehaviour
    {
        public int day = 1;
        private Player _player;

        [SerializeField]
        private List<Anomaly> _anomalies;

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
        private int _maxAnomalies = 2;

        private int _currentAnomalies = 0;

        [Header("Scene Persistance Settings")]
        [SerializeField]
        private AnomalyHandlerData _handlerData;
        private string _roomName;

        private void Awake()
        {
            //This fixes the scene persistence
            List<AnomalyHandlerData> data = GameObject.Find("SceneManager").GetComponent<AnomalyInventory>().Inventory.data;
            if (_handlerData != null)
            {
                _handlerData = data.Find(anomalyHandlerData => anomalyHandlerData.name == _handlerData.name);
            }
            
            DaySystem Today = GameObject.Find("SceneManager").GetComponent<DaySystem>();
            if (Today != null)
            {
                day = Today.GetDay();
            }
 
            _currentAnomalies = 0;
            _roomName = gameObject.scene.name + "_";
        }
        private void Start() 
        {
            _player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Player>();
            if (_handlerData != null)
            {
                AdjustToDay();
            }

        }
        private void AdjustToDay() 
        {
            SetUpStoredData();

            if (day < 2)
            {
                _currentProbability = 0.25f;
            }
            else if (day > 2 && day < 5)
            {
                _currentProbability = 0.35f;
            }
            else if(day > 5)
            {
                _currentProbability = 0.45f;
            }

            InvokeRepeating("TriggerRandomAnomaly", triggerTime, delayTime);
        }

        private void TriggerRandomAnomaly()
        {
            if (Random.value > 1 - _currentProbability)
            {
                int rand = Random.Range(0, _anomalies.Count + 1);
                TriggerAnomaly(rand);
            }
        }

        private void TriggerAnomaly(int i) 
        {

            if (_anomalies.Count > 0 && _currentAnomalies < _maxAnomalies && _currentAnomalies != _anomalies.Count)
            { 

                if (i > _anomalies.Count - 1)
                    i = 0;

                if (_anomalies[i].isActive() == false)
                {
                    if (day < 4 && _anomalies[i].GetData().aType != AnomalyType.NIGHTMARISH)
                    {
                        _anomalies[i].Manifest(_player);
                        _currentAnomalies++;
                        return;
                    }
                    else if (day > 4)
                    {
                        _anomalies[i].Manifest(_player);
                        _currentAnomalies++;
                        return;
                    }
                    else TriggerAnomaly(++i); 
                }
                else TriggerAnomaly(++i);
            }
           
        }

        private void SetUpStoredData()
        {
            if (_handlerData.currentAnomalies != 0)
            {
                int temp = 0;

                while (_currentAnomalies < _handlerData.currentAnomalies && temp < _anomalies.Count)
                {
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
            if (_handlerData == null)
            {
                return;
            }

            _handlerData.currentAnomalies = 0;

            if (_handlerData.anomalies.Count > 0)
            {
                for (int i = 0; i < _anomalies.Count; i++)
                {
                    _handlerData.anomalies[i].originalPos = _anomalies[i].GetOriginalPos();
                    _handlerData.anomalies[i].originalRot = _anomalies[i].GetOriginalRotation();
                    _handlerData.anomalies[i].originalScale = _anomalies[i].GetOriginalScale();
                    _handlerData.anomalies[i].isActive = _anomalies[i].isActive();

                    if (_anomalies[i].isActive())
                        _handlerData.currentAnomalies++;

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

                    newData.originalPos = _anomalies[i].GetOriginalPos();
                    newData.currentPos = _anomalies[i].gameObject.transform.position;

                    newData.originalRot = _anomalies[i].GetOriginalRotation();
                    newData.currentRot = _anomalies[i].gameObject.transform.rotation;

                    newData.originalScale = _anomalies[i].GetOriginalScale();
                    newData.currentScale = _anomalies[i].gameObject.transform.localScale;

                    newData.isActive = _anomalies[i].isActive();

                    _handlerData.anomalies.Add(newData);

#if UNITY_EDITOR
                    EditorUtility.SetDirty(newData);
                    AssetDatabase.SaveAssetIfDirty(newData);

                    AssetDatabase.CreateAsset(newData, "Assets/ScriptableObjectsData/AnomalyStoragedData" + _roomName + i + ".asset");
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

