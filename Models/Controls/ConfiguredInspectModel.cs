using InspectCalc.Models.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectCalc.Models.Controls
{
    public class ConfiguredInspectModel : InspectModel
    {
        private Rank[] _ranks;
        private string _inspectName;
        private string _topSentence;
        private string[] _choices;
        private ObservableCollection<Question> _questions;
        override protected Rank[] Ranks { get { return _ranks; } }
        override public string InspectName { get { return _inspectName; } }
        override public string TopSentence { get { return _topSentence; } }
        override public string[] Choices { get { return _choices; } }
        override public ObservableCollection<Question> Questions { get { return _questions; } }
        public ConfiguredInspectModel(Rank[] ranks, string inspectName, string topSentence, string[] choices, Question[] questions)
        {
            _ranks = ranks;
            _inspectName = inspectName;
            _topSentence = topSentence;
            _choices = choices;
            _questions = new ObservableCollection<Question>(questions);
        }
    }
}
