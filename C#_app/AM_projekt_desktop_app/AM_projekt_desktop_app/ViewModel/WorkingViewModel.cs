using System;
using System.Collections.Generic;
using System.Windows;
using AM_projekt_desktop_app.Commands;
using AM_projekt_desktop_app.Model;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace AM_projekt_desktop_app.ViewModel
{
    public class Measurements
    {
        public string type { get; set; }
        public string unit { get; set; }
        public double value { get; set; }
        //public double[][] pobrane = new double[2][];
    }

    class WorkingViewModel : BaseViewModel
    {
        public PlotModel Plot1 { get; set; }
        public PlotModel Plot2 { get; set; }
        public PlotModel Plot3 { get; set; }

        public string Temp { get; private set; }
        public string Pres { get; private set; }
        public string Light { get; private set; }

        public double Temp_d { get; private set; }
        public double Pres_d { get; private set; }
        public double Light_d { get; private set; }

        public ButtonCommand StartBtn { get; set; }
        public ButtonCommand StopBtn { get; set; }
        public ButtonCommand ClearBtn { get; set; }
        public ButtonCommand SendBtn { get; set; }

        private Action setSendHandler;

        public int[] tab_screen = new int[3];

        public double i = 0;

        System.Windows.Threading.DispatcherTimer disTim;
        System.Windows.Threading.DispatcherTimer listTim;


        public void update_list()
        {
            IoTserver update_list_server = new IoTserver(ipAddress);
            List<Measurements> measurements = update_list_server.Download();
            this.Temp = measurements[0].value.ToString();
            this.Pres = measurements[1].value.ToString();
            this.Light = measurements[2].value.ToString();
            OnPropertyChanged("Temp");
            OnPropertyChanged("Pres");
            OnPropertyChanged("Light");

        }

        public void listTim_tick(object sender, EventArgs e)
        {
            update_list();
        }

        public void StartListTimer()
        {
            if (listTim == null)
            {
                listTim = new System.Windows.Threading.DispatcherTimer();
                listTim.Tick += listTim_tick;
                double std = Convert.ToDouble(config.SampleTime);
                TimeSpan st_ms = TimeSpan.FromMilliseconds(std);
                listTim.Interval = st_ms;
                listTim.Start();
            }
        }

        public void StopListTimer()
        {
            listTim.Stop();
            listTim = null;
        }

        public WorkingViewModel()
        {
            ipAddress = config.IpAddress;
            sampleTime = config.SampleTime;

            StartBtn = new ButtonCommand(StartTimer);
            StopBtn = new ButtonCommand(StopTimer);
            ClearBtn = new ButtonCommand(ClearCharts);
            SendBtn = new ButtonCommand(SendScreen);

            StartListTimer();

            Plot1 = new PlotModel { Title = "Temperature" };
            Plot1.PlotType = PlotType.XY;

            Plot1.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = config.XAxisMax,
                Key = "Horizontal",
                Unit = "sec",
                Title = "Time"
            });
            Plot1.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = 20,
                Maximum = 40,
                MajorStep = 5,
                Key = "Vertical",
                Unit = "C",
                Title = "T"
            });
            Plot1.Series.Add(new LineSeries() { Title = "temperature", Color = OxyColor.Parse("#FFF18F01") });

            Plot2 = new PlotModel { Title = "Pressure" };
            Plot2.PlotType = PlotType.XY;

            Plot2.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = config.XAxisMax,
                Key = "Horizontal",
                Unit = "sec",
                Title = "Time"
            });
            Plot2.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = 975,
                Maximum = 1050,
                MajorStep = 10,
                Key = "Vertical",
                Unit = "hPa",
                Title = "P"
            });
            Plot2.Series.Add(new LineSeries() { Title = "pressure", Color = OxyColor.Parse("#FFDDD92A") });

            Plot3 = new PlotModel { Title = "Intensity" };
            Plot3.PlotType = PlotType.XY;

            Plot3.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = config.XAxisMax,
                Key = "Horizontal",
                Unit = "sec",
                Title = "Time"
            });
            Plot3.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = 0,
                Maximum = 1500,
                MajorStep = 300,
                Key = "Vertical",
                Unit = "lux",
                Title = "I"
            });
            Plot3.Series.Add(new LineSeries() { Title = "Intensity", Color = OxyColor.Parse("#FFE2C044") });
        }

        public void SetSLed(Action handler)
        {
            setSendHandler = handler;
        }

        private void SendScreen()
        {
            setSendHandler();
        }

        private void UpdatePlot(double t, double d, PlotModel Plot_n)
        {
            LineSeries lineSeries = Plot_n.Series[0] as LineSeries;

            lineSeries.Points.Add(new DataPoint(t, d));

            if (lineSeries.Points.Count > config.MaxSampleNumber)
                lineSeries.Points.RemoveAt(0);

            if (t >= config.XAxisMax)
            {
                Plot_n.Axes[0].Minimum = (t - config.XAxisMax);
                Plot_n.Axes[0].Maximum = t + config.SampleTime / 1000.0; ;
            }

            Plot_n.InvalidatePlot(true);
        }

        public void update_data()
        {
            IoTserver update_server = new IoTserver(ipAddress);
            List<Measurements> measurements = update_server.Download();
            Temp_d = measurements[0].value;
            Pres_d = measurements[1].value;
            Light_d = measurements[2].value;
            UpdatePlot(i, Temp_d, Plot1);
            UpdatePlot(i, Pres_d, Plot2);
            UpdatePlot(i, Light_d, Plot3);

            OnPropertyChanged("Temp_d");
            OnPropertyChanged("Pres_d");
            OnPropertyChanged("Light_d");
            i += config.SampleTime / 1000.0;
        }

        public void disTim_tick(object sender, EventArgs e)
        {
            update_data();
        }

        public void StartTimer()
        {
            if (disTim == null)
            {
                disTim = new System.Windows.Threading.DispatcherTimer();
                disTim.Tick += disTim_tick;
                double std = Convert.ToDouble(config.SampleTime);
                TimeSpan st_ms = TimeSpan.FromMilliseconds(std);
                disTim.Interval = st_ms;
                disTim.Start();
            }
        }

        public void StopTimer()
        {
            disTim.Stop();
            disTim = null;
        }


        public void ClearCharts()
        {
            LineSeries lineSeries1 = Plot1.Series[0] as LineSeries;
            lineSeries1.Points.Clear();
            LineSeries lineSeries2 = Plot2.Series[0] as LineSeries;
            lineSeries2.Points.Clear();
            LineSeries lineSeries3 = Plot3.Series[0] as LineSeries;
            lineSeries3.Points.Clear();

            i = 0;
            Plot1.Axes[0].Minimum = 0;
            Plot1.Axes[0].Maximum = config.XAxisMax;
            Plot2.Axes[0].Minimum = 0;
            Plot2.Axes[0].Maximum = config.XAxisMax;
            Plot3.Axes[0].Minimum = 0;
            Plot3.Axes[0].Maximum = config.XAxisMax;

            Plot1.InvalidatePlot(true);
            Plot2.InvalidatePlot(true);
            Plot3.InvalidatePlot(true);
        }

     

    }
}
