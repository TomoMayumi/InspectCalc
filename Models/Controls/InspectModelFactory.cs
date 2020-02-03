using InspectCalc.Models.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OfficeOpenXml;

namespace InspectCalc.Models.Controls
{
    public class InspectModelFactory
    {
        static private string _configFilePath = "./InspectionList.xlsx";
        static public InspectModel[] getInstances()
        {
            return readConfig().Select(
                configObject =>
                new ConfiguredInspectModel(
                    configObject.Ranks,
                    configObject.Name,
                    configObject.TopSentence,
                    configObject.Choices,
                    configObject.Questions
                )
            ).ToArray();
        }
        public struct ConfigObject
        {
            public string Name;
            public string TopSentence;
            public Rank[] Ranks;
            public string[] Choices;
            public Question[] Questions;
        }
        static private ConfigObject[] readConfig()
        {
            if (!System.IO.File.Exists(_configFilePath))
            {
                return new ConfigObject[] { };
            }

            var fullPath = System.IO.Path.GetFullPath(_configFilePath);
            var fileInfo = new System.IO.FileInfo(fullPath);
            var excelPackage = new ExcelPackage(fileInfo);

            var workbook = excelPackage.Workbook;

            var rawConfigObject = workbook.Worksheets.Select(sheet =>
                    {
                        var cells = sheet.Cells;
                        var InspectName = cells[1, 2].GetValue<string>();
                        var TopSentence = cells[2, 2].GetValue<string>();
                        var RankCount = cells[5, 2].GetValue<int>();
                        var RankNames = GetValues<string>(cells, (5, 3), (5, 2 + RankCount)).First();//Enumerable.Range(3, 2 + RankCount).Select(j => $"{cells[5, j].Value}").ToArray();//  cells[5, 3, 5, 2 + RankCount].GetValue<string[]>();
                        var RankMaxVals = GetValues<int>(cells, (6, 3), (6, 2 + RankCount)).First();//Enumerable.Range(3, 2 + RankCount).Select(j => Convert.ToInt32(cells[6, j].Value)).ToArray();//(cells[6, 3, 6, 2 + RankCount].Value as Object[]).OfType<int>().ToArray();
                        var RankMinVal = cells[7, 2].GetValue<int>();
                        var ChoiceCount = cells[10, 2].GetValue<int>();
                        var ChoiceStrings = GetValues<string>(cells, (10, 3), (10, 2 + ChoiceCount)).First(); //(cells[10, 3, 10, 2 + ChoiceCount].Value as Object[]).OfType<string>().ToArray();
                        var QuestionCount = cells[12, 2].GetValue<int>(); //Convert.ToInt32(cells[12, 2].Value);
                        var QuestionStrings = GetValues<string>(cells, (14, 2), (13 + QuestionCount, 2)).Select(x => x.First()).ToArray();//(cells[14, 2, 13 + QuestionCount, 2].Value as Object[]).OfType<string>().ToArray();
                        var AnswerEvals = GetValues<int>(cells, (14, 3), (13 + QuestionCount, 2 + ChoiceCount));//Enumerable.Range(0, AnswerEvalsTmp.GetLength(0)).
                        return new
                        {
                            InspectName,
                            TopSentence,
                            RankCount,
                            RankNames,
                            RankMaxVals,
                            RankMinVal,
                            ChoiceCount,
                            ChoiceStrings,
                            QuestionCount,
                            QuestionStrings,
                            AnswerEvals
                        };
                    }
                ).ToArray();
            
            return rawConfigObject.Select(
                rawConfig =>
                    new ConfigObject()
                    {
                        Name = rawConfig.InspectName,
                        TopSentence = rawConfig.TopSentence,
                        Ranks = rawConfig.RankMaxVals.
                            Prepend(rawConfig.RankMinVal).
                            Zip(rawConfig.RankMaxVals, (min, max) => (min, max)).
                            Zip(
                                rawConfig.RankNames,
                                (minmax, name) =>
                                    new Rank() { RankName = name, Lower = minmax.min, Upper = minmax.max }
                            ).ToArray(),
                        Choices = rawConfig.ChoiceStrings,
                        Questions = rawConfig.QuestionStrings.
                            Select((str, index) => (str, index)).
                            Zip(
                                rawConfig.AnswerEvals,
                                (strAndIndex, evals) =>
                                    new Question(strAndIndex.index+1, strAndIndex.str, evals)
                            ).ToArray()
                    }
            ).ToArray();
        }
        static private T[][] GetValues<T>(ExcelRange range, (int row, int col) start, (int row, int col) end)
        {
            return Enumerable.Range(start.row, end.row - start.row + 1).
                Select(row => 
                    Enumerable.Range(start.col, end.col - start.col + 1).
                    Select(col => range[row, col].GetValue<T>()).
                    ToArray()
                ).ToArray();
        }
    }
}
