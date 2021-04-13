using ChartDirector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LotoApp
{
    public partial class Form2 : Form
    {

        //Name of demo module
        public string getName() { return "Cylinder Bar Shading"; }

        //Number of charts produced in this demo module
        public int getNoOfCharts() { return 1; }
        LotoUtils u = new LotoUtils();
        Dictionary<string, Tuple<int, DateTime>> lotoResults;
        public Form2()
        {
            InitializeComponent();             
            u.readFile("Lotto.csv", out int numOfRepeat);
            this.WindowState = FormWindowState.Maximized;
            lotoResults = u.GetLoto();
       
        }

        int m_diffToShow = 1000;

        //Main code for creating chart.
        //Note: the argument chartIndex is unused because this demo only has 1 chart.
        public void createChart(WinChartViewer viewer, int chartIndex)
        {
            // The data for the bar chart
            List<double> data = new List<double>();                

            // The labels for the bar chart
            List<string> labels = new List<string>();
            foreach (KeyValuePair<string, Tuple<int, DateTime>> d in lotoResults)
            {
                int[] nums = d.Key.Split(',').Select(int.Parse).ToArray();
                int diff = nums[5] - nums[0];
                if (diff <= m_diffToShow)
                {
                    labels.Add(d.Value.Item2.ToShortDateString());
                    data.Add(diff);
                }
            }
            int width = this.Width;
            int width1 = this.Width;

            if (data.Count > 880 && data.Count < 1000)
            {
                width = this.Width * 10 + 100;
                width1 = this.Width * 10;
            }
            else
            if (data.Count > 280 && data.Count < 1000)
            {
                width = this.Width * 5 + 100;
                width1 = this.Width * 5;
            }
            else
            if (data.Count > 1000)
            {
                width = this.Width * 20 + 100;
                width1 = this.Width * 20;
            } else
            if (data.Count < 200)
            {
                width = this.Width * 2 + 100;
                width1 = this.Width* 2;
            }

                // Create a XYChart object of size 600 x 380 pixels. Set background color to brushed
                // silver, with a 2 pixel 3D border. Use rounded corners of 20 pixels radius.
                XYChart c = new XYChart(width, this.Height, Chart.brushedSilverColor(), Chart.Transparent, 2);

            // Add a title to the chart using 18pt Times Bold Italic font. Set top/bottom margins to
            // 8 pixels.
            c.addTitle("Loto Max Min all over years", "Times New Roman Bold Italic", 18
                ).setMargin2(0, 0, 8, 8);

            // Set the plotarea at (70, 55) and of size 460 x 280 pixels. Use transparent border and
            // black grid lines. Use rounded frame with radius of 20 pixels.
            c.setPlotArea(70, 55, width1, 480, -1, -1, Chart.Transparent, 0x000000);
            c.setRoundedFrame(0xffffff, 20);

            // Add a multi-color bar chart layer using the supplied data. Set cylinder bar shape.
            c.addBarLayer3(data.ToArray()).setBarShape(Chart.CircleShape);

            // Set the labels on the x axis.
            c.xAxis().setLabels(labels.ToArray());

            // Show the same scale on the left and right y-axes
            c.syncYAxis();

            // Set the left y-axis and right y-axis title using 10pt Arial Bold font
            c.yAxis().setTitle("Max - Min", "Arial Bold", 10);
            c.yAxis2().setTitle("Max - Min", "Arial Bold", 10);

            // Set y-axes to transparent
            c.yAxis().setColors(Chart.Transparent);
            c.yAxis2().setColors(Chart.Transparent);

            // Disable ticks on the x-axis by setting the tick color to transparent
            c.xAxis().setTickColor(Chart.Transparent);

            // Set the label styles of all axes to 8pt Arial Bold font
            c.xAxis().setLabelStyle("Arial Bold", 8, 90,90);
            c.yAxis().setLabelStyle("Arial Bold", 8);
            c.yAxis2().setLabelStyle("Arial Bold", 8);

            // Output the chart
            viewer.Chart = c;

            //include tool tip for the chart
            viewer.ImageMap = c.getHTMLImageMap("clickable", "",
                "title='Year {xLabel}: US$ {value}M'");
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            createChart(chartViewer1, 0);
        }

        private void btnShowDiffOnly_Click(object sender, EventArgs e)
        {
            bool b = int.TryParse(txtDiffToShow.Text, out m_diffToShow);
            if (b == true)
                createChart(chartViewer1, 0);
        }
    }
}
