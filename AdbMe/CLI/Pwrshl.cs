namespace AdbMe.CLI
{
    public class Pwrshl
    {
        private string BaseExecName { get; set; } = "Powershell.exe";
        private string UtilityName { get; set; }
        protected string ShortName { get; private set; }
        protected string ArgStart { get; private set; }
        protected string ArgStop { get; private set; }

        public Pwrshl(String psUtility) { 
            this.UtilityName = psUtility;
            this.ShortName = BaseExecName;
            this.ArgStart = "-Command \"& {" + psUtility;
            this.ArgStop = "}\"";
        }

        protected String[] GetArgsWithInject(String args) {
            var s = this.ArgStart + " " + args + this.ArgStop;
            return s.Split("\u0020");
        }
    }
}