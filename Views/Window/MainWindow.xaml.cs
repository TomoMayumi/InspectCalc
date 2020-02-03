using InspectCalc.Models.Window;
using InspectCalc.ViewModels.Window;
using InspectCalc.Views.Controls;
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

namespace InspectCalc
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var model = new MainWindowModel();
            var vm = new MainWindowViewModel(model);
            this.DataContext = vm;
        }
        private void Tab_Loaded(object sender, RoutedEventArgs e)
        {
            // 動的生成したタブに対し初期値を設定
            // 初期表示をタブにしたいのと、起動時からフォーカスを当てたいので指定する
            Tab.SelectedIndex = 0;
            Tab.Focus();
        }
    }
}
