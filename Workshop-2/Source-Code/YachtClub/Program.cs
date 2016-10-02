using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YachtClub
{
    class Program
    {
        internal Controller.ControllerClass ControllerClass
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public View.Menu Menu
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        static void Main(string[] args)
        {
            View.ConsoleView v = new View.ConsoleView();
            Model.MemberRegistry registry = new Model.MemberRegistry();
            Controller.ControllerClass controller = new Controller.ControllerClass();
            controller.Start(v, registry);
        }
    }
}