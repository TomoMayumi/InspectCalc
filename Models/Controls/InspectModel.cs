using InspectCalc.Models.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectCalc.Models.Controls
{
    abstract public class InspectModel : ModelBase
    {
        abstract protected Rank[] Ranks { get; }
        abstract public string InspectName { get; }
        abstract public ObservableCollection<Question> Questions { get; }
        abstract public string TopSentence { get; }
        abstract public string[] Choices { get; }
        public int[] RankWidthsInt
        {
            // 各RankのLower～Upperをつなげると連続した値域となる前提のロジック
            get
            {
                return new int[] { Ranks[0].Upper - Ranks[0].Lower }.Concat(
                      Enumerable.Range(1, Ranks.Length - 1)
                                .Select(i => (Ranks[i].Upper - Ranks[i - 1].Upper))
                  ).ToArray();
            }
        }
        public string[] RankStrings
        {
            get
            {
                return Ranks.Select(x => x.RankName).ToArray();
            }
        }
        public int RankRangeLower
        {
            get { return Ranks[0].Lower; }
        }
        public int RankRangeUpper
        {
            get { return Ranks.Last().Upper; }
        }
        public string Rank
        {
            get { return Ranks.First(r => r.Upper >= Score).RankName; }
        }
        public int Score
        {
            get { return Scores.Sum(); }
        }
        public int[] Scores
        {
            get { return Questions.Select(x => x.Score).ToArray(); }
        }
        public AnswerValType[] Answers
        {
            get { return Questions.Select(x => x.Answer).ToArray(); }
        }

        public InspectModel()
        {
        }
    }
}
