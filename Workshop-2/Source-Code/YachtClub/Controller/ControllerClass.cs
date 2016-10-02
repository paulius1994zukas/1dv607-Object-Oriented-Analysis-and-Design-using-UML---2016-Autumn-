using System;
using System.Linq;

namespace YachtClub.Controller
{
    class ControllerClass
    {
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

        internal View.ConsoleView ConsoleView
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public void Start(View.ConsoleView view, Model.MemberRegistry registry) { ShowMainMenu(view, registry); }
        
        private void ShowMainMenu(View.ConsoleView view, Model.MemberRegistry registry)
        {
            while (true)
            {
                view.ShowMenu(View.MenuEvent.MainMenu, null);
                switch (view.GetMenuSelection())
                {
                    case View.MenuEvent.AddNewMember:
                        HandleEventNewMember(view, registry);
                        break;
                    case View.MenuEvent.MemberListMenu:
                        HandleEventShowMemberListMenu(view, registry);
                        break;
                    case View.MenuEvent.MemberInfoMenu:
                        view.ShowInputInfo(View.MenuEvent.MemberInfoMenu, null);
                        int id = view.InputMemberID();
                        if (id == 0)
                            break;
                        try
                        {
                            Model.Member m = registry.GetMember(id);
                            HandleEventShowMemberInfoMenu(m, view, registry);
                        }
                        catch (ArgumentException)
                        {
                            view.ShowErrorMessage(View.Error.NoMemberWithId, id.ToString());
                            view.Wait();
                        }
                        break;
                    case View.MenuEvent.DeleteMember:
                        view.ShowInputInfo(View.MenuEvent.DeleteMember, null);
                        id = view.InputMemberID();
                        if (id == 0)
                            break;
                        try
                        {
                            registry.DeleteMember(registry.GetMember(id));
                            registry.Save();
                        }
                        catch (ArgumentException)
                        {
                            view.ShowErrorMessage(View.Error.NoMemberWithId, id.ToString());
                            view.Wait();
                        }
                        break;
                    case View.MenuEvent.Exit:
                        return;
                }
            }
        }

        public void HandleEventNewMember(View.ConsoleView view, Model.MemberRegistry registry)
        {
            view.ShowInputInfo(View.MenuEvent.AddNewMember, null);
            Model.Member m = new Model.Member(registry.GetNextMemberId());
            try
            {
                m.Name = view.InputMemberName();
            }
            catch (ArgumentException)
            {
                view.ShowErrorMessage(View.Error.InvalidMemberName, null);
                view.Wait();
                return;
            }
            try { m.PersonalNumber = view.InputMemberPersonalNumber(); }
            catch (ArgumentException)
            {
                view.ShowErrorMessage(View.Error.InvalidPersonalNumber, null);
                view.Wait();
                return;
            }
            registry.AddMember(m);
            registry.Save();
        }

        public void HandleEventShowMemberListMenu(View.ConsoleView view, Model.MemberRegistry registry)
        {
            while (true)
            {
                view.ShowMenu(View.MenuEvent.MemberListMenu, null);
                switch (view.GetMenuSelection())
                {
                    case View.MenuEvent.MemberListSimple:
                        view.ShowMemberList(registry.GetMemberList(), true);
                        view.Wait();
                        break;
                    case View.MenuEvent.MemberListComplete:
                        view.ShowMemberList(registry.GetMemberList(), false);
                        view.Wait();
                        break;
                    case View.MenuEvent.Back:
                        return;
                }
            }
        }

        public void HandleEventShowMemberInfoMenu(Model.Member member, View.ConsoleView view, Model.MemberRegistry registry)
        {
            while (true)
            {
                view.ShowMenu(View.MenuEvent.MemberInfoMenu, member);
                switch (view.GetMenuSelection())
                {
                    case View.MenuEvent.EditMemberName:
                        view.ShowInputInfo(View.MenuEvent.EditMemberName, member);
                        try
                        {
                            member.Name = view.InputMemberName();
                            registry.Save();
                        }
                        catch (ArgumentException)
                        {
                            view.ShowErrorMessage(View.Error.InvalidMemberName, null);
                            view.Wait();
                        }
                        break;
                    case View.MenuEvent.EditMemberPersonalNumber:
                        view.ShowInputInfo(View.MenuEvent.EditMemberPersonalNumber, member);
                        try
                        {
                            member.PersonalNumber = view.InputMemberPersonalNumber();
                            registry.Save();
                        }
                        catch (ArgumentException)
                        {
                            view.ShowErrorMessage(View.Error.InvalidPersonalNumber, null);
                            view.Wait();
                        }
                        break;
                    case View.MenuEvent.ManageBoatsMenu:
                        HandleEventShowManageBoatsMenu(member, view, registry);
                        registry.Save();
                        break;
                    case View.MenuEvent.Back:
                        return;
                }
            }
        }
        
        public void HandleEventShowManageBoatsMenu(Model.Member member, View.ConsoleView view, Model.MemberRegistry registry)
        {
            while (true)
            {
                view.ShowMenu(View.MenuEvent.ManageBoatsMenu, member);
                int boatId;

                switch (view.GetMenuSelection())
                {
                    case View.MenuEvent.AddBoat:
                        HandleEventAddNewBoat(member, view, registry);
                        break;
                    case View.MenuEvent.EditBoatMenu:
                        if (member.GetBoatCount() == 1)
                        {
                            boatId = member.GetBoats().First().ID;
                        }
                        else
                        {
                            view.ShowInputInfo(View.MenuEvent.EditBoatMenu, member);
                            boatId = view.InputBoatID();
                        }
                        try
                        {
                            Model.Boat b = member.GetBoat(boatId);
                            HandleEventShowEditBoatMenu(member, b, view, registry);
                        }
                        catch (ArgumentException)
                        {
                            view.ShowErrorMessage(View.Error.NoBoatWithId, boatId.ToString());
                            view.Wait();
                        }
                        break;
                    case View.MenuEvent.DeleteBoat:
                        if (member.GetBoatCount() == 1)
                        {
                            boatId = member.GetBoats().First().ID;
                        }
                        else
                        {
                            view.ShowInputInfo(View.MenuEvent.DeleteBoat, member);
                            boatId = view.InputBoatID();
                        }
                        try
                        {
                            member.DeleteBoat(boatId);
                            break;
                        }
                        catch (ArgumentException)
                        {
                            view.ShowErrorMessage(View.Error.NoBoatWithId, boatId.ToString());
                            view.Wait();
                        }
                        break;
                    case View.MenuEvent.Back:
                        return;
                }
            }
        }

        private void HandleEventAddNewBoat(Model.Member member, View.ConsoleView view, Model.MemberRegistry registry)
        {
            view.ShowInputInfo(View.MenuEvent.AddBoat, member);
            Model.Boat boat = new Model.Boat();
            boat.ID = registry.GetNextBoatIdFor(member);
            try
            {
                boat.Model = (Model.BoatModel)view.InputBoatModel();
            }
            catch (ArgumentException)
            {
                view.ShowErrorMessage(View.Error.InvalidBoatModel, null);
                view.Wait();
                return;
            }
            try
            {
                boat.Length = view.InputBoatLenght();
            }
            catch (ArgumentException)
            {
                view.ShowErrorMessage(View.Error.InvalidBoatLenght, null);
                view.Wait();
                return;
            }
            member.AddBoat(boat);
            registry.Save();
        }

        private void HandleEventShowEditBoatMenu(Model.Member member, Model.Boat boat,
            View.ConsoleView view, Model.MemberRegistry registry)
        {
            while (true)
            {
                view.ShowMenu(View.MenuEvent.EditBoatMenu, member);
                switch (view.GetMenuSelection())
                {
                    case View.MenuEvent.EditBoatModel:
                        view.ShowInputInfo(View.MenuEvent.EditBoatModel, member);
                        try
                        {
                            boat.Model = (Model.BoatModel)view.InputBoatModel();
                            registry.Save();
                        }
                        catch (ArgumentException)
                        {
                            view.ShowErrorMessage(View.Error.InvalidBoatModel, null);
                            view.Wait();
                            return;
                        }
                        break;
                    case View.MenuEvent.EditBoatLength:
                        view.ShowInputInfo(View.MenuEvent.EditBoatLength, member);
                        try
                        {
                            boat.Length = view.InputBoatLenght();
                            registry.Save();
                            break;
                        }
                        catch (ArgumentException)
                        {
                            view.ShowErrorMessage(View.Error.InvalidBoatLenght, null);
                            view.Wait();
                            break;
                        }
                    case View.MenuEvent.Back:
                        return;
                }
            }
        }
    }
}