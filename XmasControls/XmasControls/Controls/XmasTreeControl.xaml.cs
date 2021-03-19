using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace XmasControls.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class XmasTreeControl : ContentView
    {
        public static readonly BindableProperty LeavesBrushProperty =
            BindableProperty.Create(nameof(LeavesBrush),
                typeof(Brush),
                typeof(XmasTreeControl),
                Brush.Transparent,
                BindingMode.TwoWay,
                propertyChanged: OnLeavesBrushChanged);

        public Brush LeavesBrush
        {
            get => (Brush)GetValue(LeavesBrushProperty);
            set { SetValue(LeavesBrushProperty, value); } 
        }

        public static readonly BindableProperty TrunkBrushProperty =
            BindableProperty.Create(nameof(TrunkBrush),
                typeof(Brush),
                typeof(XmasTreeControl),
                Brush.Transparent,
                BindingMode.TwoWay,
                propertyChanged: OnTrunkBrushChanged);

        public Brush TrunkBrush
        {
            get => (Brush)GetValue(TrunkBrushProperty);
            set { SetValue(TrunkBrushProperty, value); }
        }

        private static void OnLeavesBrushChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var xmasTreeControl = bindable as XmasTreeControl;
            xmasTreeControl.FillBrush(xmasTreeControl.Content as AbsoluteLayout, newValue as Brush, 3);
        }

        private static void OnTrunkBrushChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var xmasTreeControl = bindable as XmasTreeControl;
            xmasTreeControl.FillBrush(xmasTreeControl.Content as AbsoluteLayout, newValue as Brush, 4);
        }

        void FillBrush(AbsoluteLayout layout, Brush brush, int pointsCount)
        {
            foreach (var item in layout.Children)
            {
                var control = item as Polygon;

                if (control != null)
                {
                    if (control.Points.Count == pointsCount)
                    {
                        control.Fill = brush;
                        control.Stroke = brush;
                    }
                }
            }
        }

        public XmasTreeControl()
        {
            InitializeComponent();
        }

        List<Tuple<int, int, bool>> positions = new List<Tuple<int, int, bool>>()
        {
            new Tuple<int, int, bool>(0, 75, false),
            new Tuple<int, int, bool>(75, 25, false),
            new Tuple<int, int, bool>(100, 100, false),
            new Tuple<int, int, bool>(0, 175, false),
            new Tuple<int, int, bool>(150, 175, false),
        };

        private void OnDrop(object sender, DropEventArgs e)
        {
            var properties = e.Data.Properties;

            if (properties.ContainsKey("Sphere"))
            {
                var sphere = (Ellipse)properties["Sphere"];

                var xmasSphere = new XmasSphereControl()
                {
                    SphereBrush = sphere.Fill
                };

                var layout = this.Content as AbsoluteLayout;

                var position = positions.First(x => !x.Item3);
                layout.Children.Add(xmasSphere, new Point(position.Item1, position.Item2));
                positions[positions.IndexOf(position)] = new Tuple<int, int, bool>(position.Item1, position.Item2, true);
            }
            else if (properties.ContainsKey("Star"))
            {
                var star = (Path)properties["Star"];

                var xmasStar = new XmasStarControl()
                {
                    StarStroke = star.Stroke
                };

                var layout = this.Content as AbsoluteLayout;

                layout.Children.Add(xmasStar, new Point(90, -5));
            }
        }
    }
}