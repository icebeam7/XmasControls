using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Shapes;

namespace XmasControls.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class XmasStarControl : ContentView
    {
        public static readonly BindableProperty StarStrokeProperty =
            BindableProperty.Create(nameof(StarStroke),
                typeof(Brush),
                typeof(XmasTreeControl),
                SolidColorBrush.Transparent,
                BindingMode.TwoWay,
                propertyChanged: OnStarStrokeChanged);

        public Brush StarStroke
        {
            get => (Brush)GetValue(StarStrokeProperty);
            set { SetValue(StarStrokeProperty, value); }
        }

        private static void OnStarStrokeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var xmasStarControl = bindable as XmasStarControl;
            xmasStarControl.FillStroke(xmasStarControl.Content as StackLayout, newValue as Brush);
        }

        void FillStroke(StackLayout layout, Brush brush)
        {
            foreach (var item in layout.Children)
            {
                var control = item as Path;

                if (control != null)
                {
                    control.Stroke = brush;
                }
            }
        }

        public XmasStarControl()
        {
            InitializeComponent();
        }

        private void OnDrag(object sender, DragStartingEventArgs e)
        {
            var stack = (sender as Element).Parent as StackLayout;
            var star = stack.Children[0] as Path;

            e.Data.Properties.Add("Star", new Path()
            {
                Data = star.Data,
                Stroke = star.Stroke
            });
        }
    }
}