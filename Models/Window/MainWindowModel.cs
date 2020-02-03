using InspectCalc.Models.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectCalc.Models.Window
{
    class MainWindowModel : ModelBase
    {
        //コンストラクタ
        public MainWindowModel()
        {
        }
        // プロパティ
        public InspectModel[] InspectModels { get; private set; } = InspectModelFactory.getInstances();
    }
}
