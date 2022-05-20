using System;

namespace hanee.ThreeD
{
    public class CommandItem
    {
        public CommandItem(string command, string displayText, Action act)
        {
            this.command = command;
            this.displayText = displayText;
            this.act = act;
        }
        public string displayText { get; set; } = null;
        public string command { get; set; } = null;
        public Action act { get; set; }
        public override string ToString()
        {
            return this.command;
        }
    }
}
