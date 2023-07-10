using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace SurvivalIsland.Common.Utils
{
    public class DayNightCycle : MonoBehaviour
    {
        const float SECONDS_IN_A_DAY = 86400f;

        [Header("Time")]
        [Tooltip("Day length in minutes")]
        [SerializeField]
        private float _targetDayLength = 0.5f;
        public float TargetDayLength => _targetDayLength;

        [SerializeField]
        [Range(0f, 1f)]
        private float _timeOfDay;
        public float TimeOfDay => _timeOfDay;

        [SerializeField]
        private int _dayNumber = 1;
        public int DayNumber => _dayNumber;

        [SerializeField]
        private int _monthNumber = 1;
        public int MonthNumber => _monthNumber;

        [SerializeField]
        private int _yearNumber = 1;
        public int YearNumber => _yearNumber;

        private float _timeScale = 100f;

        [SerializeField]
        private int _yearLength = 100;
        public float YearLength => _yearLength;

        [SerializeField]
        private Gradient _lightColor;
        public Gradient LightColor => _lightColor;

        private List<Action> _minuteByMinuteSubscribers;
        public List<Action> MinuteByMinuteSubscribers => _minuteByMinuteSubscribers;
        
        private int _lastMinuteCalculated;

        private void Awake()
        {
            _minuteByMinuteSubscribers = new();
        }

        public DateTime CurrentDateTime
            => GetCurrentTime();

        public void UpdateDayNightCycle()
        {
            UpdateTimeScale();
            UpdateTime();
            UpdateNewMinute();
            AdjustSunColor();
        }

        private void UpdateTimeScale()
        {
            _timeScale = 24 / (_targetDayLength / 60);
        }

        private void UpdateTime()
        {
            _timeOfDay += Time.fixedDeltaTime * _timeScale / SECONDS_IN_A_DAY;

            if (_timeOfDay > 1) //new day
            {
                _dayNumber++;
                _timeOfDay -= 1;

                if (_dayNumber > GetMonthLength())
                {
                    _dayNumber = 1;

                    if (_monthNumber < 12)
                    {
                        _monthNumber++;
                    }
                    else
                    {
                        _monthNumber = 1;
                        _yearNumber++;
                    }
                }
            }
        }

        private void AdjustSunColor()
        {
            GetComponent<Light2D>().color = _lightColor.Evaluate(_timeOfDay);
        }

        private int GetMonthLength()
            => _monthNumber switch
            {
                1 => 31,
                2 => 28,
                3 => 31,
                4 => 30,
                5 => 31,
                6 => 30,
                7 => 31,
                8 => 31,
                9 => 30,
                10 => 31,
                11 => 30,
                12 => 31,
                _ => 1
            };

        private DateTime GetCurrentTime()
        {
            int seconds = Mathf.FloorToInt(_timeOfDay * SECONDS_IN_A_DAY);

            if (seconds < 0)
                return default;

            var timeSpan = TimeSpan.FromSeconds(seconds);

            return new DateTime(
                _yearNumber,
                _monthNumber,
                _dayNumber,
                timeSpan.Hours,
                timeSpan.Minutes,
                timeSpan.Seconds);
        }

        private void UpdateNewMinute()
        {
            int seconds = Mathf.FloorToInt(_timeOfDay * SECONDS_IN_A_DAY);

            if (_lastMinuteCalculated != TimeSpan.FromSeconds(seconds).Minutes)
            {
                _lastMinuteCalculated = TimeSpan.FromSeconds(seconds).Minutes;
                foreach (var subscriber in _minuteByMinuteSubscribers)
                {
                    subscriber.Invoke();
                }
            }
        }

        public void SetCurrentTime(DateTime currentTime)
        {
            _yearNumber = currentTime.Year;
            _monthNumber = currentTime.Month;
            _dayNumber = currentTime.Day;

            var secondsFromHour = currentTime.Hour * 60 * 60;
            var secondsFromMinutes = currentTime.Minute * 60;

            _timeOfDay = (secondsFromHour + secondsFromMinutes + currentTime.Second) / SECONDS_IN_A_DAY;
        }
    }
}