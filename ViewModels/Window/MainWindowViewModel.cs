using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using InspectCalc.Models;
using InspectCalc.ViewModels.Controls;
using InspectCalc.Models.Window;

namespace InspectCalc.ViewModels.Window
{
    public class MainWindowViewModel : ViewModelBase
    {
        //コンストラクタ
        public MainWindowViewModel(ModelBase model) :base(model)
        {
            var Model = (MainWindowModel)model;

            InspectViewModels = new ObservableCollection<InspectViewModel>(Model.InspectModels.Select(x => new InspectViewModel(x)));
        }
        public ObservableCollection<InspectViewModel> InspectViewModels { get; private set; }
    }
}