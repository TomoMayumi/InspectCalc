using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectCalc.Models.Resources
{
    public enum AnswerValType
    {
        None = 0,
        Val1,
        Val2,
        Val3,
        Val4,
        Val5,
        Val6,
        Val7,
        Val8,
    };
    public struct Rank
    {
        public int Lower;
        public int Upper;
        public string RankName;
        public Rank(int lower, int upper, string rank)
        {
            Lower = lower;
            Upper = upper;
            RankName = rank;
        }
    }
    public class Question : ModelBase
    {
        private AnswerValType answer = AnswerValType.None;
        private int score = 0;

        public int Index { get; }
        public int[] Scores { get; }
        public string Sentence { get; }
        public AnswerValType Answer
        {
            get { return answer; }
            set
            {
                int? answerVal = GetScore(value);
                if (answerVal != null)
                {
                    SetValue(ref answer, value);
                    Score = (int)answerVal;
                }
            }
        }
        public Question(int index, string sentence, int[] scores)
        {
            Index = index;
            Sentence = sentence;
            Scores = scores;
            Answer = AnswerValType.None;
        }
        public int Score
        {
            get { return score; }
            private set { SetValue(ref score, value); }
        }
        public int? GetScore(AnswerValType answer)
        {
            int? newScore = null;
            switch (answer)
            {
                case AnswerValType.None:
                    newScore = 0;
                    break;
                case AnswerValType.Val1:
                    if (Scores.Length > 0) newScore = Scores[0];
                    break;
                case AnswerValType.Val2:
                    if (Scores.Length > 1) newScore = Scores[1];
                    break;
                case AnswerValType.Val3:
                    if (Scores.Length > 2) newScore = Scores[2];
                    break;
                case AnswerValType.Val4:
                    if (Scores.Length > 3) newScore = Scores[3];
                    break;
                case AnswerValType.Val5:
                    if (Scores.Length > 4) newScore = Scores[4];
                    break;
                case AnswerValType.Val6:
                    if (Scores.Length > 5) newScore = Scores[5];
                    break;
                case AnswerValType.Val7:
                    if (Scores.Length > 6) newScore = Scores[6];
                    break;
                case AnswerValType.Val8:
                    if (Scores.Length > 7) newScore = Scores[7];
                    break;
            }
            return newScore;
        }
    }
}
