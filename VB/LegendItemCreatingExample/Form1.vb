Imports System
Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraMap

Namespace LegendItemCreatingExample
    Partial Public Class Form1
        Inherits Form

        Private Const filePath As String = "../../Countries.shp"

        Public Sub New()
            InitializeComponent()

            InitializeMap()
        End Sub

        Private Sub InitializeMap()
            Dim map As New MapControl() With {.Dock = DockStyle.Fill}
            Me.Controls.Add(map)
            AddHandler map.LegendItemCreating, AddressOf map_LegendItemCreating

            Dim fileLayer As New VectorItemsLayer() With {.Colorizer = CreateColorizer(), .Data = CreateData(filePath)}
            map.Layers.Add(fileLayer)

            map.Legends.Add(New ColorScaleLegend() With {.Header = "GDP in Countries", .Layer = fileLayer})
        End Sub

        #Region "#LegendItemCreating"
        Private Sub map_LegendItemCreating(ByVal sender As Object, ByVal e As LegendItemCreatingEventArgs)
            e.Item.Text = String.Format("{0}$", e.Item.Value)
        End Sub
        #End Region ' #LegendItemCreating

        Private Function CreateData(ByVal path As String) As IMapDataAdapter
            Dim baseUri As New Uri(System.Reflection.Assembly.GetEntryAssembly().Location)
            Return New ShapefileDataAdapter() With {.FileUri = New Uri(baseUri, path)}
        End Function

        Private Function CreateColorizer() As MapColorizer

            Dim colorizer As New ChoroplethColorizer() With { _
                .ApproximateColors = True, .PredefinedColorSchema = PredefinedColorSchema.Gradient, .ValueProvider = New ShapeAttributeValueProvider() With {.AttributeName = "GDP_MD_EST"} _
            }
            colorizer.RangeStops.Add(0)
            colorizer.RangeStops.Add(1000)
            colorizer.RangeStops.Add(10000)
            colorizer.RangeStops.Add(100000)
            colorizer.RangeStops.Add(1000000)
            colorizer.RangeStops.Add(10000000)

            Return colorizer
        End Function
    End Class
End Namespace
