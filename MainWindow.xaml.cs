using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Coding4Fun.Kinect.WinForm;

using Microsoft.Kinect;

namespace depthdata3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)        //whenever the sensor is changed, the event fires
        {
            kinectSensorChooser1.KinectSensorChanged += new DependencyPropertyChangedEventHandler(kinectSensorChooser1_KinectSensorChanged);
        }

        void kinectSensorChooser1_KinectSensorChanged(object sender, DependencyPropertyChangedEventArgs e)  //if plugging a new sensor, and seeing if its null
        {
            KinectSensor oldsensor = (KinectSensor)e.OldValue;
            StopKinect(oldsensor);

            KinectSensor newsensor = (KinectSensor)e.NewValue;
            newsensor.DepthStream.Enable();

            try
            {
                newsensor.Start();
            }
            catch (System.IO.IOException)    //if we hit this exception when we start, tells kinectsensorchooser1
                                             //we have an application conflict

            {
                kinectSensorChooser1.AppConflictOccurred();
            }
        }



        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            StopKinect(kinectSensorChooser1.Kinect);   //references the current kinect

        }

        void StopKinect(KinectSensor sensor)
        {
            if (sensor != null)      //to see if sensor is running, used to stop kinect
            {
                sensor.Stop();
                sensor.AudioSource.Stop();
            }
        }
    }
}


