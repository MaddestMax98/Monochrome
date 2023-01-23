using PlayerCharacter;
using System.Collections.Generic;
using UnityEngine;

namespace AnomalySystem
{
    public class AnomalyHandler : MonoBehaviour
    {
        //TODO - Make a referee to the current day
        public int day = 1;

        [SerializeField]
        private Player _player;

        public List<Anomaly> _anomalies;
        public List<Anomaly> _selectedAnomalies;

        [SerializeField]
        [Tooltip("Sets inital time which anomalies can be triggered")]
        private int triggerTime;
        [SerializeField]
        [Tooltip("Sets time in between triggers")]
        private int delayTime;

        private BoxCollider _roomTrigger;
        private float _currentProbability = 1f;
        private bool _isPlayerInRoom;

        private int _currentAnomalies = 0;

        private void Awake()
        {
            _roomTrigger = gameObject.GetComponent<BoxCollider>();
            _currentAnomalies = 0;
        }
        private void Start() 
        { 
            AdjustToDay();
            InvokeRepeating("TriggerRandomAnomaly", triggerTime, delayTime);
        }
        private void AdjustToDay() 
        {
            if (day == 1)
            {
                _currentProbability = 0.25f;

                for (int i = 0; i < _anomalies.Count; i++)
                {
                    if (_anomalies[i].GetData().aType != AnomalyType.NIGHTMARISH)
                    {
                        _selectedAnomalies.Add(_anomalies[i]);
                        i++;
                    }
                    else _anomalies[i].Disable();
                }
            }
        }

        private void TriggerRandomAnomaly()
        {
            if (Random.value > 1 - _currentProbability)
            {
                int rand = Random.Range(0, _selectedAnomalies.Count);
                TriggerAnomaly(rand);
            }
        }

        private void TriggerAnomaly(int i) 
        {
            if (_selectedAnomalies.Count > 0 && _currentAnomalies < 3 && _currentAnomalies != _selectedAnomalies.Count)
            {
                if (i > _selectedAnomalies.Count)
                    i = 0;

                if (_selectedAnomalies[i].isActive())
                {
                    _selectedAnomalies[i].Manifest(_player);
                    _currentAnomalies++;
                    return;
                }
                //else TriggerAnomaly(++i);
            }
           
        }

    }
}

