using InspectCalc.Models;
using InspectCalc.Models.Controls;
using InspectCalc.Models.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InspectCalc.ViewModels.Controls
{
    public class InspectViewModel : ViewModelBase
    {
        private readonly int maxChoiceCount = 8; // コンフィグで指定可能な選択肢数の最大

        // フィールド
        private InspectModel inspectModel;
        private int evalVal = 0;
        private string evalRank = "";
        private bool hasAnyBlank = true;
        private int questionFocus = 0;

        //コマンド
        private ICommand executeClearCommand;
        private ICommand executeCalcCommand;
        private ICommand executeSetCommand;
        //プロパティ
        public InspectModel InspectModel
        {
            get { return inspectModel; }
            set { SetValue(ref inspectModel, value); }
        }
        public int EvalVal
        {
            get { return evalVal; }
            set { SetValue(ref evalVal, value); }
        }
        public string EvalRank
        {
            get { return evalRank; }
            set { SetValue(ref evalRank, value); }
        }
        public bool HasAnyBlank
        {
            get { return hasAnyBlank; }
            set { SetValue(ref hasAnyBlank, value); }
        }
        public ObservableCollection<Question> Questions
        {
            get { return InspectModel.Questions; }
        }
        public string[] Choices
        {
            get
            {
                return Enumerable.Range(0, maxChoiceCount)
                    .Select(i => inspectModel.Choices.ElementAtOrDefault(i))
                    .ToArray();
            }
        }

        public int[] ChoicesColWidths
        {
            get
            {
                int colSize = Questions[0].Scores.Length;
                return Enumerable.Range(0, maxChoiceCount).Select(i => i < colSize ? 70 : 0).ToArray();
            }
        }

        // コンストラクタ
        public InspectViewModel(ModelBase model) : base(model)
        {
            inspectModel = (InspectModel)model;
            ClearAnswers();
        }

        /*****************************************************************************/
        /// コマンド：回答のクリア
        /*****************************************************************************/
        public ICommand ExecuteClearCommand
        {
            get
            {
                executeClearCommand = new DelegateCommand(ExecuteClear, CanExecute);
                return executeClearCommand;
            }
        }

        /// アクション : 回答のクリア
        private void ExecuteClear(object obj)
        {
            ClearAnswers();
        }

        /*****************************************************************************/
        /// コマンド：結果計算
        /*****************************************************************************/
        public ICommand ExecuteCalcCommand
        {
            get
            {
                executeCalcCommand = new DelegateCommand(ExecuteCalc, CanExecute);
                return executeCalcCommand;
            }
        }

        /// アクション : 結果計算
        private void ExecuteCalc(object obj)
        {
            CalcAnswers();
        }

        /*****************************************************************************/
        /// コマンド：キーボードで回答を入力
        /*****************************************************************************/
        public ICommand ExecuteSetCommand
        {
            get
            {
                executeSetCommand = new DelegateCommand(ExecuteSet, CanExecute);
                return executeSetCommand;
            }
        }

        /// アクション : キーボードで回答を入力
        private void ExecuteSet(object obj)
        {
            if (obj != null)
            {
                var val = (AnswerValType)Enum.Parse(typeof(AnswerValType), (string)obj);
                Questions[questionFocus].Answer = val;
                if ((Questions[questionFocus].Answer == val) && (questionFocus < Questions.Count - 1))
                {
                    questionFocus++;
                }
                CalcAnswers();
            }
        }

        /*****************************************************************************/
        /// コマンド共通 : CanExecute
        /*****************************************************************************/
        private bool CanExecute(object arg)
        {
            return true;
        }

        /*****************************************************************************/
        /// 回答のクリア
        /*****************************************************************************/
        public void ClearAnswers()
        {
            EvalVal = 0;
            EvalRank = "";
            HasAnyBlank = true;
            foreach (Question question in Questions)
            {
                question.Answer = AnswerValType.None;
            }
            questionFocus = 0;
        }

        /*****************************************************************************/
        /// 結果計算
        /*****************************************************************************/
        public void CalcAnswers()
        {
            EvalVal = InspectModel.Score;
            EvalRank = InspectModel.Rank;
            HasAnyBlank = Questions.Any(x => x.Answer == AnswerValType.None);
        }
    }
}
