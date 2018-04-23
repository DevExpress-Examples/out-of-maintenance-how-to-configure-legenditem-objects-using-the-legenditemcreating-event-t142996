using System;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraMap;

namespace LegendItemCreatingExample {
    public partial class Form1 : Form {
        const string filePath = @"../../Countries.shp";
        
        public Form1() {
            InitializeComponent();

            InitializeMap();
        }

        private void InitializeMap() {
            MapControl map = new MapControl() { Dock = DockStyle.Fill };
            this.Controls.Add(map);
            map.LegendItemCreating += map_LegendItemCreating;

            VectorItemsLayer fileLayer = new VectorItemsLayer() {
                Colorizer = CreateColorizer(),
                Data = CreateData(filePath),
            };
            map.Layers.Add(fileLayer);

            map.Legends.Add(new ColorScaleLegend() { Header = "GDP in Countries", Layer = fileLayer });
        }

        #region #LegendItemCreating
        void map_LegendItemCreating(object sender, LegendItemCreatingEventArgs e) {
            e.Item.Text = String.Format("{0}$", e.Item.Value);
        }
        #endregion #LegendItemCreating

        private IMapDataAdapter CreateData(string path) {
            Uri baseUri = new Uri(System.Reflection.Assembly.GetEntryAssembly().Location);
            return new ShapefileDataAdapter() { FileUri = new Uri(baseUri, path) };
        }

        private MapColorizer CreateColorizer() {
            
            ChoroplethColorizer colorizer = new ChoroplethColorizer() {
                ApproximateColors = true,
                PredefinedColorSchema = PredefinedColorSchema.Gradient,
                ValueProvider = new ShapeAttributeValueProvider() { AttributeName = "GDP_MD_EST" }
            };
            colorizer.RangeStops.Add(0);
            colorizer.RangeStops.Add(1000);
            colorizer.RangeStops.Add(10000);
            colorizer.RangeStops.Add(100000);
            colorizer.RangeStops.Add(1000000);
            colorizer.RangeStops.Add(10000000);
           
            return colorizer;
        }
    }
}
