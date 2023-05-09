using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace SurvivalIsland.Common.Utils
{
    internal class DayNightCycle : MonoBehaviour
    {
        [Header("Time")]
        [Tooltip("Day length in minutes")]
        [SerializeField]
        private float _targetDayLength = 0.5f;
        internal float TargetDayLength { get { return _targetDayLength; } }

        [SerializeField]
        [Range(0f, 1f)]
        private float _timeOfDay;
        internal float TimeOfDay { get { return _timeOfDay; } }

        [SerializeField]
        private int _dayNumber = 1;
        internal int DayNumber { get { return _dayNumber; } }

        [SerializeField]
        private int _monthNumber = 1;
        internal int MonthNumber { get { return _monthNumber; } }

        [SerializeField]
        private int _yearNumber = 1;
        internal int YearNumber { get { return _yearNumber; } }

        private float _timeScale = 100f;

        [SerializeField]
        private int _yearLength = 100;
        internal float YearLength { get { return _yearLength; } }

        [SerializeField]
        internal Gradient LightColor;

        public void UpdateDayNightCycle()
        {
            UpdateTimeScale();
            UpdateTime();
            AdjustSunColor();
        }

        private void UpdateTimeScale()
        {
            _timeScale = 24 / (_targetDayLength / 60);
        }

        private void UpdateTime()
        {
            _timeOfDay += Time.deltaTime * _timeScale / 86400; // seconds in a day

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
            GetComponent<Light2D>().color = LightColor.Evaluate(_timeOfDay);
        }

        private int GetMonthLength()
        {
            int monthLength = 1;

            switch (_monthNumber)
            {
                case 1: monthLength = 31; break;
                case 2: monthLength = 28; break;
                case 3: monthLength = 31; break;
                case 4: monthLength = 30; break;
                case 5: monthLength = 31; break;
                case 6: monthLength = 30; break;
                case 7: monthLength = 31; break;
                case 8: monthLength = 31; break;
                case 9: monthLength = 30; break;
                case 10: monthLength = 31; break;
                case 11: monthLength = 30; break;
                case 12: monthLength = 31; break;
            }

            return monthLength;
        }

        internal void SetCurrentTime(DateTime currentTime)
        {
            _yearNumber = currentTime.Year;
            _monthNumber = currentTime.Month;
            _dayNumber = currentTime.Day;

            var secondsFromHour = currentTime.Hour * 60 * 60;
            var secondsFromMinutes = currentTime.Minute * 60;

            _timeOfDay = (secondsFromHour + secondsFromMinutes + currentTime.Second) / 86400f; // seconds in a day
        }

        internal DateTime GetCurrentTime()
        {
            int seconds = Mathf.FloorToInt(_timeOfDay * 86400);

            if (seconds < 0)
                return default;

            var timeSpan = TimeSpan.FromSeconds(seconds);

            return new DateTime(
                _yearNumber,
                _monthNumber,
                _dayNumber,
                timeSpan.Hours,
                0,
                timeSpan.Seconds);
        }

        internal double GetTotalDays()
        {
            int seconds = Mathf.FloorToInt(_timeOfDay * 86400);
            var timeSpan = TimeSpan.FromSeconds(seconds);
            return timeSpan.TotalDays;
        }
    }

}