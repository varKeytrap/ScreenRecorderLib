﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using ScreenRecorderLib;

namespace TestApp.Sources
{
    public class MediaDeviceToDeviceIdConverter : DependencyObject, IValueConverter
    {


        public List<RecordableCamera> MediaDevices
        {
            get { return (List<RecordableCamera>)GetValue(MediaDevicesProperty); }
            set { SetValue(MediaDevicesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MediaDevices.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MediaDevicesProperty =
            DependencyProperty.Register("MediaDevices", typeof(List<RecordableCamera>), typeof(MediaDeviceToDeviceIdConverter), new PropertyMetadata(null));


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RecordableCamera device;
            if (value is null)
            {
                device = MediaDevices.FirstOrDefault(x => String.IsNullOrEmpty(x.DeviceName));
            }
            else
            {
                device = MediaDevices.FirstOrDefault(x => value.ToString().Equals(x.DeviceName));
            }
            return device;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as RecordableCamera)?.DeviceName;
        }
    }

    public class CheckableRecordableCamera : RecordableCamera, ICheckableRecordingSource
    {
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }

        private bool _isCheckable = true;
        public bool IsCheckable
        {
            get { return _isCheckable; }
            set
            {
                if (_isCheckable != value)
                {
                    _isCheckable = value;
                    OnPropertyChanged(nameof(IsCheckable));
                }
            }
        }

        private bool _isCustomPositionEnabled;
        public bool IsCustomPositionEnabled
        {
            get { return _isCustomPositionEnabled; }
            set
            {
                if (_isCustomPositionEnabled != value)
                {
                    _isCustomPositionEnabled = value;
                    OnPropertyChanged(nameof(IsCustomPositionEnabled));
                }
            }
        }

        private bool _isCustomOutputSizeEnabled;
        public bool IsCustomOutputSizeEnabled
        {
            get { return _isCustomOutputSizeEnabled; }
            set
            {
                if (_isCustomOutputSizeEnabled != value)
                {
                    _isCustomOutputSizeEnabled = value;
                    OnPropertyChanged(nameof(IsCustomOutputSizeEnabled));
                }
            }
        }

        public CheckableRecordableCamera() : base()
        {

        }
        public CheckableRecordableCamera(string friendlyName, string deviceName) : base(friendlyName, deviceName)
        {

        }
        public CheckableRecordableCamera(RecordableCamera cam) : base(cam.FriendlyName, cam.DeviceName)
        {

        }

        public override string ToString()
        {
            return $"{FriendlyName} ({DeviceName})";
        }

        public void UpdateScreenCoordinates(ScreenPoint position, ScreenSize size)
        {
            if (!IsCustomOutputSizeEnabled)
            {
                OutputSize = size;
            }
            if (!IsCustomPositionEnabled)
            {
                Position = position;
            }
        }
    }
}
