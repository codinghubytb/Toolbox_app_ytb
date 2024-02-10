using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsApp.ViewModels
{

    public class CompassViewModel : BaseViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the rotation angle of the compass needle.
        /// </summary>
        private double _needleRotation;

        /// <summary>
        /// Gets or sets the compass direction (Nord, Sud, Est, Ouest).
        /// </summary>
        private string _compassDirection;

        public double NeedleRotation
        {
            get => _needleRotation;
            set => SetProperty(ref _needleRotation, value);
        }

        public string CompassDirection
        {
            get => _compassDirection;
            set => SetProperty(ref _compassDirection, value);
        }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CompassViewModel"/> class.
        /// </summary>
        public CompassViewModel()
        {
            // Stop monitoring compass if already active
            if (Compass.IsMonitoring)
            {
                Compass.Stop();
            }

            // Subscribe to the ReadingChanged event of the compass
            Compass.ReadingChanged += Compass_ReadingChanged;

            // Start compass updates
            StartCompassUpdates();
        }

        #endregion Constructor

        #region Compass Updates

        /// <summary>
        /// Starts receiving compass updates.
        /// </summary>
        void StartCompassUpdates()
        {
            try
            {
                // Start monitoring compass at UI speed
                if (!Compass.IsMonitoring)
                {
                    Compass.Start(SensorSpeed.UI);
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Compass not supported on this device
            }
            catch (Exception ex)
            {
                // Handle other exceptions
            }
        }

        /// <summary>
        /// Handles the ReadingChanged event of the compass.
        /// </summary>
        void Compass_ReadingChanged(object sender, CompassChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                // Update needle angle based on compass reading
                UpdateNeedleRotation(e.Reading.HeadingMagneticNorth);
            });
        }

        /// <summary>
        /// Updates the rotation angle of the compass needle based on the compass reading.
        /// </summary>
        void UpdateNeedleRotation(double heading)
        {
            // Update needle rotation angle based on compass reading
            NeedleRotation = Math.Round(heading, 1);

            if(NeedleRotation == 0.0)
            {
                Vibration.Vibrate();
            }

            // Update compass direction based on the angle
            CompassDirection = GetCompassDirection(heading);
        }

        /// <summary>
        /// Determines the compass direction (Nord, Sud, Est, Ouest) based on the angle.
        /// </summary>
        string GetCompassDirection(double heading)
        {
            // Logic to determine direction (Nord, Sud, Est, Ouest) based on angle
            // Add your own logic here based on your requirements
            // This example is simplified and may require adjustments
            if (heading >= 337.5 || heading < 22.5)
                return "Nord";
            else if (heading >= 22.5 && heading < 67.5)
                return "Nord-Est";
            else if (heading >= 67.5 && heading < 112.5)
                return "Est";
            else if (heading >= 112.5 && heading < 157.5)
                return "Sud-Est";
            else if (heading >= 157.5 && heading < 202.5)
                return "Sud";
            else if (heading >= 202.5 && heading < 247.5)
                return "Sud-Ouest";
            else if (heading >= 247.5 && heading < 292.5)
                return "Ouest";
            else
                return "Nord-Ouest";
        }

        #endregion Compass Updates

        #region Compass Control

        /// <summary>
        /// Stops receiving compass updates.
        /// </summary>
        public void StopCompassUpdates()
        {
            try
            {
                // Stop monitoring compass if active
                if (Compass.IsMonitoring)
                {
                    Compass.Stop();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
        }

        #endregion Compass Control
    }

}
