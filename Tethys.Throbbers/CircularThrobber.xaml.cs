#region Header
// --------------------------------------------------------------------------
// Tethys.Throbbers
// ==========================================================================
//
// A custom control library for WPF applications.
//
// ==========================================================================
// <copyright file="CircularThrobber.xaml.cs" company="Tethys">
// Copyright  2017 by T. Graf
//            All rights reserved.
//            Licensed under the Apache License, Version 2.0.
//            Unless required by applicable law or agreed to in writing, 
//            software distributed under the License is distributed on an
//            "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
//            either express or implied. 
// </copyright>
// 
// System ... Microsoft .Net Framework 4.5. 
// Tools .... Microsoft Visual Studio 2017
//
// ---------------------------------------------------------------------------
#endregion

namespace Tethys.Throbbers
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for CircularThrobber.
    /// </summary>
    public partial class CircularThrobber
    {
        #region PRIVATE PROPERTIES        
        /// <summary>
        /// The step size for turning angle.
        /// </summary>
        private const int StepSize = 9;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region DEPENDENCY PROPERTIES
        #region DP - OuterCircleBrush
        /// <summary>
        /// Dependency property for the outer circle brush.
        /// </summary>
        public static readonly DependencyProperty OuterCircleBrushProperty =
          DependencyProperty.Register("OuterCircleBrush", typeof(Brush),
          typeof(CircularThrobber), new PropertyMetadata(Brushes.Gray,
              (o, args) =>
                  {
                      var obj = o as CircularThrobber;
                      if (obj == null)
                      {
                          return;
                      } // if

                      obj.OuterCircleLeft.Stroke = (Brush)args.NewValue;
                      obj.OuterCircleRight.Stroke = (Brush)args.NewValue;
                  }));

        /// <summary>
        /// Gets or sets the outer circle brush.
        /// </summary>
        public Brush OuterCircleBrush
        {
            get { return (Brush)this.GetValue(OuterCircleBrushProperty); }
            set { this.SetValue(OuterCircleBrushProperty, value); }
        }
        #endregion

        #region DP - InnerCircleBrush
        /// <summary>
        /// Dependency property for the inner circle brush.
        /// </summary>
        public static readonly DependencyProperty InnerCircleBrushProperty =
          DependencyProperty.Register("InnerCircleBrush", typeof(Brush),
          typeof(CircularThrobber), new PropertyMetadata(Brushes.DarkGray,
              (o, args) =>
              {
                  var obj = o as CircularThrobber;
                  if (obj == null)
                  {
                      return;
                  } // if

                  obj.InnerCircleLeft.Stroke = (Brush)args.NewValue;
                  obj.InnerCircleRight.Stroke = (Brush)args.NewValue;
              }));

        /// <summary>
        /// Gets or sets the inner circle brush.
        /// </summary>
        public Brush InnerCircleBrush
        {
            get { return (Brush)this.GetValue(InnerCircleBrushProperty); }
            set { this.SetValue(InnerCircleBrushProperty, value); }
        }
        #endregion

        #region DP - Speed
        /// <summary>
        /// Dependency property for the update/spin speed.
        /// </summary>
        public static readonly DependencyProperty SpeedProperty =
          DependencyProperty.Register("Speed", typeof(int),
          typeof(CircularThrobber), new PropertyMetadata(60,
              (o, args) =>
              {
                  var obj = o as CircularThrobber;
                  if (obj == null)
                  {
                      return;
                  } // if

                  obj.AnimationTimer.Interval = new TimeSpan(0, 0, 0, 0, (int)args.NewValue);
              }));

        /// <summary>
        /// Gets or sets the update/spin speed.
        /// </summary>
        public int Speed
        {
            get { return (int)this.GetValue(SpeedProperty); }
            set { this.SetValue(SpeedProperty, value); }
        }
        #endregion
        #endregion // DEPENDENCY PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION            
        /// <summary>
        /// Initializes a new instance of the <see cref="CircularThrobber"/> class.
        /// </summary>
        public CircularThrobber()
        {
#if ONLY_FOR_DEVELOPMENT
            Calculate();
#endif // ONLY_FOR_DEVELOPMENT

            this.InitializeComponent();
            this.InitializeTimer(60);
        } // CircularThrobber()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        #endregion // PUBLIC METHODS

        //// ---------------------------------------------------------------------

        #region PRIVATE METHODS            
        /// <summary>
        /// Handles the animation tick.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void HandleAnimationTick(object sender, EventArgs e)
        {
            this.SpinnerRotateOuter.Angle = (this.SpinnerRotateOuter.Angle + StepSize) % 360;
            this.SpinnerRotateInner.Angle = (this.SpinnerRotateInner.Angle - StepSize) % 360;
        } // HandleAnimationTick()

#if ONLY_FOR_DEVELOPMENT
        private static void Calculate()
        {
            var r = 60;

            var x = CalcPathCircleCoord(r, 0);
            x = CalcPathCircleCoord(r, 90);
            x = CalcPathCircleCoord(r, 180);
            x = CalcPathCircleCoord(r, 270);

            // outer
            x = CalcPathCircleCoord(r, 60);
            x = CalcPathCircleCoord(r, 300);

            x = CalcPathCircleCoord(r, 120);
            x = CalcPathCircleCoord(r, 240);

            // inner
            r = 40;
            x = CalcPathCircleCoord(r, 30);
            x = CalcPathCircleCoord(r, 150);

            x = CalcPathCircleCoord(r, 210);
            x = CalcPathCircleCoord(r, 330);
        }
#endif // ONLY_FOR_DEVELOPMENT
        #endregion // PRIVATE METHOD
    } // CircularThrobber()
}
