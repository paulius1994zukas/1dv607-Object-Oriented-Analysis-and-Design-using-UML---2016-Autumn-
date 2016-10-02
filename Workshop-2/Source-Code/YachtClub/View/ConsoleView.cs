using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace YachtClub.View
{
    class ConsoleView
    {
        private Menu _mainMenu;
        private Menu _currentMenu;

        public ConsoleView()
        {
            _mainMenu = new Menu("Yacht Club System", (int)MenuEvent.MainMenu, null);
            _mainMenu.Additem((int)MenuEvent.AddNewMember, "Add Member");
            Menu memberListMenu = new Menu("Members List", (int)MenuEvent.MemberListMenu, "View Members List");
            memberListMenu.Additem((int)MenuEvent.MemberListSimple, "Compact List");
            memberListMenu.Additem((int)MenuEvent.MemberListComplete, "Verbose List");
            memberListMenu.Additem((int)MenuEvent.Back, "Go Back");
            _mainMenu.AddSubMenu(memberListMenu);
            Menu memberInfoMenu = new Menu("Member Information", (int)MenuEvent.MemberInfoMenu, "View Member Info");
            memberInfoMenu.Additem((int)MenuEvent.EditMemberName, "Edit Name");
            memberInfoMenu.Additem((int)MenuEvent.EditMemberPersonalNumber, "Edit Personal Number");
            Menu manageBoatsMenu = new Menu("Boat Management", (int)MenuEvent.ManageBoatsMenu, "Manage Boats");
            manageBoatsMenu.Additem((int)MenuEvent.AddBoat, "Add a Boat");
            Menu editBoatMenu = new Menu("Edit Boat Information", (int)MenuEvent.EditBoatMenu, "Edit Boat");
            editBoatMenu.Additem((int)MenuEvent.EditBoatModel, "Edit Model");
            editBoatMenu.Additem((int)MenuEvent.EditBoatLength, "Edit Length");
            editBoatMenu.Additem((int)MenuEvent.Back, "Go Back");
            manageBoatsMenu.AddSubMenu(editBoatMenu);
            manageBoatsMenu.Additem((int)MenuEvent.DeleteBoat, "Delete Boat");
            manageBoatsMenu.Additem((int)MenuEvent.Back, "Go Back");
            memberInfoMenu.AddSubMenu(manageBoatsMenu);
            memberInfoMenu.Additem((int)MenuEvent.Back, "Go Back");
            _mainMenu.AddSubMenu(memberInfoMenu);
            _mainMenu.Additem((int)MenuEvent.DeleteMember, "Delete Member");
            _mainMenu.Additem((int)MenuEvent.Exit, "Close window");
        }

        public Menu Menu
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public Model.Member Member
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        internal Model.MemberRegistry MemberRegistry
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public MenuEvent MenuEvent
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public Error Error
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public void ShowMenu(MenuEvent menuId, Model.Member member)
        {
            Menu menu = _mainMenu.GetSubMenu((int)menuId);

            if (menu != null)
            {
                _currentMenu = menu;
                System.Console.Clear();
                if (member != null)
                {
                    PrintHeader("Member Information");
                    PrintMemberInfo(member);
                    System.Console.WriteLine();
                }

                PrintHeader(menu.Header);
                foreach (MenuItem item in menu.GetItems())
                {
                    System.Console.WriteLine("{0}. {1}", menu.GetListIndex(item), item.Title);
                }
            }
        }

        public MenuEvent GetMenuSelection()
        {
            ConsoleKeyInfo key = System.Console.ReadKey(true);

            int selection = (int)MenuEvent.Invalid;
            int.TryParse(key.KeyChar.ToString(), out selection);

            if (selection != (int)MenuEvent.Invalid)
            {
                if (selection >= _currentMenu.GetListIndex(_currentMenu.GetItems().First())
                    && selection <= _currentMenu.GetListIndex(_currentMenu.GetItems().Last()))
                {
                    return (MenuEvent)_currentMenu.GetItemId(selection);
                }
            }
            return MenuEvent.Invalid;
        }

        public void ShowInputInfo(MenuEvent action, Model.Member member)
        {
            if (member != null)
            {
                System.Console.Clear();
                PrintHeader("Member Information");
                PrintMemberInfo(member);
            }
            System.Console.WriteLine();

            switch (action)
            {
                case MenuEvent.AddNewMember:
                    PrintHeader("Add new Member");
                    break;
                case MenuEvent.MemberInfoMenu:
                    PrintHeader("View Member Info");
                    break;
                case MenuEvent.EditMemberName:
                    PrintHeader("Edit Member");
                    break;
                case MenuEvent.AddBoat:
                    PrintHeader("Add new Boat");
                    break;
                case MenuEvent.EditBoatMenu:
                case MenuEvent.EditBoatLength:
                case MenuEvent.EditBoatModel:
                    PrintHeader("Edit Boat");
                    break;
                case MenuEvent.DeleteBoat:
                    PrintHeader("Delete Boat");
                    break;
                case MenuEvent.DeleteMember:
                    PrintHeader("Delete Member");
                    break;
            }
        }

        public string InputMemberName()
        {
            System.Console.Write("Name: ");
            return System.Console.ReadLine();
        }

        public string InputMemberPersonalNumber()
        {
            System.Console.Write("Personal Number: ");
            return System.Console.ReadLine();
        }

        public int InputBoatModel()
        {
            int model = -1;
            System.Console.WriteLine("{0}. Sailboat", (int)Model.BoatModel.Sailboat);
            System.Console.WriteLine("{0}. Motorsailer", (int)Model.BoatModel.Motorsailer);
            System.Console.WriteLine("{0}. Kayak/Canoe", (int)Model.BoatModel.KayakCanoe);
            System.Console.WriteLine($"{{0}}. Other{Environment.NewLine}", (int)Model.BoatModel.Other);
            System.Console.Write("Model: ");
            string input = System.Console.ReadLine();
            int.TryParse(input, out model);
            return model;
        }

        public double InputBoatLenght()
        {
            System.Console.Write("Length: ");
            try
            {
                return Convert.ToDouble(System.Console.ReadLine().Replace(',', '.'), CultureInfo.InvariantCulture);
            }
            catch (FormatException) { }
            return 0;
        }

        public int InputMemberID()
        {
            int id;
            System.Console.Write("Member ID: ");
            if (int.TryParse(System.Console.ReadLine(), out id))
                return id;
            return -1;
        }

        public int InputBoatID()
        {
            int id;
            System.Console.Write("Boat ID: ");
            if (int.TryParse(System.Console.ReadLine(), out id))
                return id;
            return -1;
        }

        public void ShowMemberList(IEnumerable<Model.Member> list, bool compact)
        {
            System.Console.Clear();
            if (compact)
            {
                PrintHeader("Compact Member List");
                System.Console.WriteLine();
                System.Console.WriteLine();
                System.Console.WriteLine("{0,-5} {1,-26}  {3}", "ID", "Name", "Personal Number", "Boats");
                System.Console.WriteLine($"_____________________________________________________________{Environment.NewLine}");

                foreach (Model.Member m in list)
                    System.Console.WriteLine("{0,-5} {1,-26} {3}", m.ID, m.Name, m.PersonalNumber, m.GetBoatCount());
            }
            else
            {
                PrintHeader("Verbose Member List");
                foreach (Model.Member memb in list)
                {
                    System.Console.WriteLine();
                    PrintMemberInfo(memb);
                    System.Console.WriteLine($"_____________________________________________________________");
                }
            }
        }

        public void ShowErrorMessage(Error err, String arg)
        {
            System.Console.Clear();
            PrintHeader("!!! Error !!!");
            switch (err)
            {
                case Error.NoMemberWithId:
                    System.Console.WriteLine("There exists no member with id = {0}!", arg);
                    break;
                case Error.NoBoatWithId:
                    System.Console.WriteLine("This member does not own boat with id = {0}!", arg);
                    break;
                case Error.InvalidBoatLenght:
                    System.Console.WriteLine("Invalid input! Length must always be greater than 0!");
                    break;
                case Error.InvalidMemberName:
                    System.Console.WriteLine("Invalid input! Name cannot be empty!");
                    break;
                case Error.InvalidPersonalNumber:
                    System.Console.WriteLine("Invalid format of personal number entered! Personal number must follow (YYMMDD-XXXX) format!");
                    break;
                case Error.InvalidBoatModel:
                    System.Console.WriteLine("Selected boat model is invalid!");
                    break;
            }
        }

        private void PrintHeader(String title)
        {
            System.Console.WriteLine($"_____________________________________________________________{Environment.NewLine}");
            System.Console.WriteLine($"                      {title}");
            System.Console.WriteLine($"_____________________________________________________________{Environment.NewLine}");
        }

        private void PrintMemberInfo(Model.Member member)
        {
            System.Console.WriteLine("{0,-16}{1}", "ID:", member.ID);
            System.Console.WriteLine("{0,-16}{1}", "Name:", member.Name);
            System.Console.WriteLine("{0,-16}{1}", "Personal Nr:", member.PersonalNumber);
            System.Console.WriteLine("{0,-16}{1}", "Boats:", member.GetBoatCount() == 0 ? "None" : "");

            if (member.GetBoatCount() > 0)
            {
                System.Console.WriteLine();
                System.Console.WriteLine("{0,4}{1,-5} {2,-12} {3,-6}", "", "ID", "Model", "Length");
                System.Console.WriteLine("    ----- ------------ ------");

                foreach (Model.Boat boat in member.GetBoats())
                    System.Console.WriteLine("{0,4}{1,-5} {2,-12} {3}m", "", boat.ID, GetNameForBoatModel(boat.Model), boat.Length.ToString("0.0"));
            }
        }

        public void Wait()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("To continue press any key...");
            System.Console.ReadKey();
        }

        private string GetNameForBoatModel(Model.BoatModel model)
        {
            switch (model)
            {
                case Model.BoatModel.Sailboat:
                    return "Sailboat";
                case Model.BoatModel.Motorsailer:
                    return "Motorsailor";
                case Model.BoatModel.KayakCanoe:
                    return "Kayak/Canoe";
                case Model.BoatModel.Other:
                    return "Other";
            }
            return null;
        }
    }

    public enum MenuEvent
    {
        Invalid,
        Back,
        MainMenu,
        AddNewMember,
        MemberListMenu,
        MemberListSimple,
        MemberListComplete,
        MemberInfoMenu,
        EditMemberName,
        EditMemberPersonalNumber,
        ManageBoatsMenu,
        AddBoat,
        EditBoatMenu,
        EditBoatModel,
        EditBoatLength,
        DeleteBoat,
        DeleteMember,
        Exit
    }

    public enum Error
    {
        NoMemberWithId,
        NoBoatWithId,
        InvalidBoatLenght,
        InvalidPersonalNumber,
        InvalidMemberName,
        InvalidBoatModel
    }
}