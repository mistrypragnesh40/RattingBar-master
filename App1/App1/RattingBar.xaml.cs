using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RattingBar : ContentView
    {
        public event EventHandler ItemTapped = delegate { };
        public string emptyStarImage = string.Empty;
        public string fillStarImage = string.Empty;
        public Image star1;
        public Image star2;
        public Image star3;
        public Image star4;
        public Image star5;
        public RattingBar()
        {
            InitializeComponent();
            star1 = new Image();
            star2 = new Image();
            star3 = new Image();
            star4 = new Image();
            star5 = new Image();

            #region adding Gesture Recognizer on Star(Image Control)
            star1.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = ItemTappedCommand,
                CommandParameter = 1
            });

            star2.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = ItemTappedCommand,
                CommandParameter = 2
            });

            star3.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = ItemTappedCommand,
                CommandParameter = 3
            });
            star4.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = ItemTappedCommand,
                CommandParameter = 4
            });
            star5.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = ItemTappedCommand,
                CommandParameter = 5
            });
            #endregion

            stkRattingbar.Children.Add(star1);
            stkRattingbar.Children.Add(star2);
            stkRattingbar.Children.Add(star3);
            stkRattingbar.Children.Add(star4);
            stkRattingbar.Children.Add(star5);

            // here i added pan gesture recognizer for selecting the star 
            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += (s, e) =>
            {
                // Handle the pan
                var obj = e;
                double width = star1.Width;
                if (obj.TotalX > 0)
                {
                    fillStar(1, this);
                    Command?.Execute(1);
                }
                if (obj.TotalX > width)
                {
                    fillStar(2, this);
                    Command?.Execute(2);
                }
                if (obj.TotalX > (width * 2))
                {
                    fillStar(3, this);
                    Command?.Execute(3);

                }
                if (obj.TotalX > (width * 3))
                {
                    fillStar(4, this);
                    Command?.Execute(4);

                }
                if (obj.TotalX > (width * 4))
                {
                    fillStar(5, this);
                    Command?.Execute(5);
                }
            };
            stkRattingbar.GestureRecognizers.Add(panGesture);
        }

        // this event will fire when you click on image(star)
        private ICommand ItemTappedCommand
        {
            get
            {
                return new Command((Object obj) =>
                {
                    CommandParameter = obj;
                    Command?.Execute(CommandParameter);
                });

            }
        }

        #region  Image Height Width Property
        public static readonly BindableProperty ImageHeightProperty = BindableProperty.Create(
        propertyName: "ImageHeight",
        returnType: typeof(double),
        declaringType: typeof(RattingBar),
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: ImageHeightPropertyChanged
         );

        public double ImageHeight
        {
            get { return (double)base.GetValue(ImageHeightProperty); }
            set { base.SetValue(ImageHeightProperty, value); }
        }

        private static void ImageHeightPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RattingBar)bindable;
            if (control != null)
            {
                // set all images height  equal
                control.star1.HeightRequest = (double)newValue;
                control.star2.HeightRequest = (double)newValue;
                control.star3.HeightRequest = (double)newValue;
                control.star4.HeightRequest = (double)newValue;
                control.star5.HeightRequest = (double)newValue;
            }

        }


        //image width
        public static readonly BindableProperty ImageWidthProperty = BindableProperty.Create(
        propertyName: "ImageWidth",
        returnType: typeof(double),
        declaringType: typeof(RattingBar),
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: ImageWidthPropertyChanged
         );

        public double ImageWidth
        {
            get { return (double)base.GetValue(ImageWidthProperty); }
            set { base.SetValue(ImageWidthProperty, value); }
        }

        private static void ImageWidthPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // set all images width  equal
            var control = (RattingBar)bindable;
            if (control != null)
            {
                control.star1.WidthRequest = (double)newValue;
                control.star2.WidthRequest = (double)newValue;
                control.star3.WidthRequest = (double)newValue;
                control.star4.WidthRequest = (double)newValue;
                control.star5.WidthRequest = (double)newValue;
            }
        }
        #endregion



        #region Horizontal Vertical Allignment 
        public static readonly BindableProperty HorizontalOptionsProperty = BindableProperty.Create(
        propertyName: "HorizontalOptions",
        returnType: typeof(LayoutOptions),
        declaringType: typeof(RattingBar),
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: HorizontalOptionsPropertyChanged
         );

        public LayoutOptions HorizontalOptions
        {
            get { return (LayoutOptions)base.GetValue(HorizontalOptionsProperty); }
            set { base.SetValue(HorizontalOptionsProperty, value); }
        }

        private static void HorizontalOptionsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RattingBar)bindable;
            control.stkRattingbar.HorizontalOptions = (LayoutOptions)newValue;
        }

        //VERTICLE option set

        public static readonly BindableProperty VerticalOptionsProperty = BindableProperty.Create(
        propertyName: "VerticalOptions",
        returnType: typeof(LayoutOptions),
        declaringType: typeof(RattingBar),
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: VerticalOptionsPropertyChanged
         );

        public LayoutOptions VerticalOptions
        {
            get { return (LayoutOptions)base.GetValue(VerticalOptionsProperty); }
            set { base.SetValue(VerticalOptionsProperty, value); }
        }

        private static void VerticalOptionsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RattingBar)bindable;
            control.stkRattingbar.VerticalOptions = (LayoutOptions)newValue;
        }
        #endregion

        #region Command binding property
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
         propertyName: "Command",
         returnType: typeof(ICommand),
         declaringType: typeof(RattingBar)
         );

        public ICommand Command
        {
            get { return (ICommand)base.GetValue(CommandProperty); }
            set { base.SetValue(CommandProperty, value); }
        }

        //  this property is private becuase i don't wanna access it globally
        private static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
         propertyName: "CommandParameter",
         returnType: typeof(object),
         declaringType: typeof(RattingBar),
         propertyChanged: CommandParameterPropertyChanged
         );
        private object CommandParameter
        {
            get { return base.GetValue(CommandParameterProperty); }
            set { base.SetValue(CommandParameterProperty, value); }
        }

        // on the change of command parameter replace empty star image with fillstar image
        private static void CommandParameterPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RattingBar)bindable;
            if (control != null)
            {
                var selectedValue = (int)newValue;
                fillStar(selectedValue, control);
            }

        }

        #endregion


        // this function will replace empty star with fill star
        private static void fillStar(int selectedValue, RattingBar obj)
        {
            obj.SelectedStarValue = selectedValue;
            switch (selectedValue)
            {
                case 1:
                    obj.star1.Source = obj.fillStarImage;
                    obj.star2.Source = obj.emptyStarImage;
                    obj.star3.Source = obj.emptyStarImage;
                    obj.star4.Source = obj.emptyStarImage;
                    obj.star5.Source = obj.emptyStarImage;
                    break;
                case 2:
                    obj.star1.Source = obj.fillStarImage;
                    obj.star2.Source = obj.fillStarImage;
                    obj.star3.Source = obj.emptyStarImage;
                    obj.star4.Source = obj.emptyStarImage;
                    obj.star5.Source = obj.emptyStarImage;
                    break;
                case 3:
                    obj.star1.Source = obj.fillStarImage;
                    obj.star2.Source = obj.fillStarImage;
                    obj.star3.Source = obj.fillStarImage;
                    obj.star4.Source = obj.emptyStarImage;
                    obj.star5.Source = obj.emptyStarImage;
                    break;
                case 4:
                    obj.star1.Source = obj.fillStarImage;
                    obj.star2.Source = obj.fillStarImage;
                    obj.star3.Source = obj.fillStarImage;
                    obj.star4.Source = obj.fillStarImage;
                    obj.star5.Source = obj.emptyStarImage;
                    break;
                case 5:
                    obj.star1.Source = obj.fillStarImage;
                    obj.star2.Source = obj.fillStarImage;
                    obj.star3.Source = obj.fillStarImage;
                    obj.star4.Source = obj.fillStarImage;
                    obj.star5.Source = obj.fillStarImage;
                    break;

            }
        }


        #region EmptyStar and fillstar property
        public static readonly BindableProperty EmptyStarImageProperty = BindableProperty.Create(
            propertyName: "EmptyStarImage",
            returnType: typeof(string),
            declaringType: typeof(RattingBar),
            defaultValue: "",
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: EmptyStarImagePropertyChanged
         );

        public string EmptyStarImage
        {
            get { return (string)base.GetValue(EmptyStarImageProperty); }
            set { base.SetValue(EmptyStarImageProperty, value); }
        }

        private static void EmptyStarImagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RattingBar)bindable;
            if (control != null)
            {
                control.emptyStarImage = (string)newValue;
                //default set to all images as emptyStar
                control.star1.Source = control.emptyStarImage;
                control.star2.Source = control.emptyStarImage;
                control.star3.Source = control.emptyStarImage;
                control.star4.Source = control.emptyStarImage;
                control.star5.Source = control.emptyStarImage;

                // when default SelectedStarValue is assign  first and fillstariamge or emptystart image assign later then on the Property Change of
                //SelectedStar fillStartImage and emptyStart Image show emty string so to handle this  here i write this logic
                // < customcontrol:RattingBar x:Name = "customRattingBar"  ImageWidth = "25" ImageHeight = "25" SelectedStarValue = "1"    FillStarImage = "fillstar" EmptyStarImage = "emptystar" />
                if (!string.IsNullOrEmpty(control.fillStarImage))
                {
                    fillStar(control.SelectedStarValue, control);
                }
            }

        }


        public static readonly BindableProperty FillStarImageProperty = BindableProperty.Create(
          propertyName: "FillStarImage",
          returnType: typeof(string),
          declaringType: typeof(RattingBar),
          defaultValue: "",
          defaultBindingMode: BindingMode.TwoWay,
          propertyChanged: FillStarImagePropertyChanged
       );

        public string FillStarImage
        {
            get { return (string)base.GetValue(FillStarImageProperty); }
            set { base.SetValue(FillStarImageProperty, value); }
        }

        private static void FillStarImagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RattingBar)bindable;
            if (control != null)
            {
                control.fillStarImage = (string)newValue;

                // when default SelectedStarValue is assign  first and fillstariamge or emptystart image assign later then on the Property Change of
                //SelectedStar fillStartImage and emptyStar Image show emty string so to handle this  here i write this logic
                // < customcontrol:RattingBar x:Name = "customRattingBar"  ImageWidth = "25" ImageHeight = "25" SelectedStarValue = "1"    FillStarImage = "fillstar" EmptyStarImage = "emptystar" />
                if (!string.IsNullOrEmpty(control.emptyStarImage))
                {
                    fillStar(control.SelectedStarValue, control);
                }
            }

        }
        #endregion


        #region this will return the selected star value and also you can set the default selected star
        public static readonly BindableProperty SelectedStarValueProperty = BindableProperty.Create(
          propertyName: "SelectedStarValue",
          returnType: typeof(int),
          declaringType: typeof(RattingBar),
          defaultValue: 0,
          defaultBindingMode: BindingMode.TwoWay,
          propertyChanged: SelectedStarValuePropertyChanged
       );

        private static void SelectedStarValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RattingBar)bindable;
            control.SelectedStarValue = (int)newValue;
            if (control != null)
            {
                // if the SelectedStarValue is assign first and later fillStarImage & EmptyStar assign   
                if (!string.IsNullOrEmpty(control.fillStarImage) && !string.IsNullOrEmpty(control.emptyStarImage))
                {
                    fillStar(control.SelectedStarValue, control);
                }
            }

        }
        public int SelectedStarValue
        {
            get { return (int)GetValue(SelectedStarValueProperty); }
            set { SetValue(SelectedStarValueProperty, value); }
        }
        #endregion

    }
}