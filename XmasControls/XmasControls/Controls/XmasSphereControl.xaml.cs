using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Shapes;

namespace XmasControls.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class XmasSphereControl : ContentView
    {
        public static readonly BindableProperty SphereBrushProperty =
            BindableProperty.Create(nameof(SphereBrush),
                typeof(Brush),
                typeof(XmasTreeControl),
                SolidColorBrush.Transparent,
                BindingMode.TwoWay,
                propertyChanged: OnSphereBrushChanged);

        public Brush SphereBrush
        {
            get => (Brush)GetValue(SphereBrushProperty);
            set { SetValue(SphereBrushProperty, value); }
        }

        private static void OnSphereBrushChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var xmasSphereControl = bindable as XmasSphereControl;
            xmasSphereControl.FillBrush(xmasSphereControl.Content as StackLayout, newValue as Brush);
        }

        void FillBrush(StackLayout layout, Brush brush)
        {
            foreach (var item in layout.Children)
            {
                var control = item as Ellipse;

                if (control != null)
                {
                    control.Fill = brush;
                    control.Stroke = brush;
                }
            }
        }

        public XmasSphereControl()
        {
            InitializeComponent();
        }

        private void OnDrag(object sender, DragStartingEventArgs e)
        {
            var stack = (sender as Element).Parent as StackLayout;
            var sphere = stack.Children[1] as Ellipse;

            e.Data.Properties.Add("Sphere", new Ellipse()
            {
                WidthRequest = sphere.Width,
                HeightRequest = sphere.Height,
                Fill = sphere.Fill
            });
        }
    }
}