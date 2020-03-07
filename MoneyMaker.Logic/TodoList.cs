using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MoneyMeter.Logic
{
    public class TodoList : Module
    {
        public List<string> ThingsToDo { get; set; }
        public TodoList(string filePath) : base(filePath)
        {
        }
        public override void OverrideFileValues()
        {
            _fileFactory.OverrideFileWithValues(ThingsToDo);
        }

        protected override void BuildListFromFile()
        {
            ThingsToDo = new List<string>();
            var fileContent = _fileFactory.ReadNormalizedData().Split(';');
            foreach(var content in fileContent)
            {
                if (!string.IsNullOrEmpty(content))
                    ThingsToDo.Add(content);
            }
        }
        public void RemoveFromToDoList(string name)
        {
            if (ThingsToDo.Contains(name))
                ThingsToDo.Remove(name);
        }
        public void AddToToDoList(string name)
        {
            ThingsToDo.Add(name);
        }
    }
}
