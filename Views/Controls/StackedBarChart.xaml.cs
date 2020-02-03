using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InspectCalc.Views.Controls
{
    /// <summary>
    /// StackedBarChart.xaml の相互作用ロジック
    /// </summary>
    public partial class StackedBarChart : UserControl
    {
        /// <summary>
        /// 依存関係プロパティ
        /// 表示要素それぞれの幅（比率）
        /// </summary>
        public int[] ItemsWidth
        {
            get { return (int[])GetValue(ItemsWidthProperty); }
            set { SetValue(ItemsWidthProperty, value); }
        }
        public static readonly DependencyProperty ItemsWidthProperty =
            DependencyProperty.Register(
                nameof(ItemsWidth),       // プロパティ名
                typeof(int[]),            // プロパティの型
                typeof(StackedBarChart),  // プロパティを所有する型＝このクラスの名前
                new PropertyMetadata(new int[] { }, new PropertyChangedCallback(OnPropertyChanged))); // 初期値
        /// <summary>
        /// 依存関係プロパティ
        /// 表示要素それぞれのテキスト
        /// </summary>
        public string[] ItemsText
        {
            get { return (string[])GetValue(ItemsTextProperty); }
            set { SetValue(ItemsTextProperty, value); }
        }
        public static readonly DependencyProperty ItemsTextProperty =
            DependencyProperty.Register(
                nameof(ItemsText),      // プロパティ名
                typeof(string[]),         // プロパティの型
                typeof(StackedBarChart),  // プロパティを所有する型＝このクラスの名前
                new PropertyMetadata(new string[] { }, new PropertyChangedCallback(OnPropertyChanged))); // 初期値

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public StackedBarChart()
        {
            InitializeComponent();
        }
        private static void OnPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            // オブジェクトを取得して処理する
            StackedBarChart ctrl = obj as StackedBarChart;
            if (ctrl != null)
            {
                ctrl.MakeStackBarChart();
            }
        }
        /// <summary>
        /// 中身作成
        /// 中身は依存関係プロパティのバインドで指定されたものを使用するので、
        /// バインド値が入った後に処理する必要がある
        /// </summary>
        public void MakeStackBarChart()
        {
            var length = Math.Min(ItemsWidth.Length, ItemsText.Length);         // 使用するプロパティは同じ長さを期待するが、片方のみ指定されたときは短いほうに合わせる
            if (length < 1) return;

            var startColor = Color.FromRgb(0xBB, 0xDD, 0xFF);                   // 初期色
            var endColor = Color.FromRgb(0xFF, 0xDD, 0xBB);                     // 最終色
            var diffColor = (endColor - startColor) * ((float)1.0 / (length - 1));    // 色の差分幅

            grid.Children.Clear();
            grid.ColumnDefinitions.Clear();
            foreach (var ((width, text), index) in ItemsWidth.Zip(ItemsText, (w, t) => (w, t)).Select((v, i) => (v, i)))
            {
                var column = new ColumnDefinition();                    // 列の定義
                column.Width = new GridLength(width, GridUnitType.Star);  // 列幅の定義
                grid.ColumnDefinitions.Add(column);

                var rect = new Rectangle();                             // 背景色用四角形の定義
                rect.Stroke = Brushes.Black;                            // 枠線の色
                rect.Fill = new SolidColorBrush(startColor + diffColor * index);// 塗りつぶしの色
                Grid.SetColumn(rect, index);
                grid.Children.Add(rect);

                var viewbox = new Viewbox(); // テキストの定義
                var textbox = new TextBlock();
                textbox.Text = text;
                textbox.Margin = new Thickness(3);
                viewbox.MaxHeight = 20;
                viewbox.Child = textbox;
                Grid.SetColumn(viewbox, index);
                grid.Children.Add(viewbox);
            }
        }
    }
}
