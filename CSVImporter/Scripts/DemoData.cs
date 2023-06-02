using CSVImporter;
using System;
using UnityEngine;

namespace CSVImporter
{
    public class DemoData : BaseImportObject
    {
        new public string name;
        public int amount;
        public string text;
        public QuestType type;

        public override void SetupFromTokens(string[] tokens)
        {
            try
            {
                AssertRowLength(tokens.Length, 4);
                name = tokens[0];
                amount = int.Parse(tokens[1]);
                type = Enum.Parse<QuestType>(tokens[2]);
                text = tokens[3];
            }
            catch
            {

                throw new Exception("Cant Setup Data Because of to Much or Less Tokens");
            }
        }

    }


    public enum QuestType
    {
        NONE = 0,
        COLLECT = 1,
        KILL = 2,
        MEET = 3
    }
}