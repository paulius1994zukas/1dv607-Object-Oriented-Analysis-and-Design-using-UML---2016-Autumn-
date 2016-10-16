#Sequence diagram:
The diagram notation looks good and the notation perfectly clear. But for future diagrams some points could be considered. 
* Sequence numbering of messages is redundant because the y axis shows order/time.
* Create message line usually go directly to the lifeline box. [1, Chapter 15 Section 4, "Creation of Instances"]
* Notation for return messages could be simplified. [1, Chapter 15, Figure 15.8]
* The program lifeline could probably be removed in both both diagrams and the diagram could start somewhere after 5: ShowMenu instead. As it is now both diagrams show a sequence plus parts of the startup of the program.
* It is unclear what the purpose of message "8: Handle AddNewMember" is in List Members diagram.

#Class diagram:
Auto-generated in Visual Studio so not much to comment on. Only perhaps that for a newcomer to the project. The full class diagram is quite heavy and doesnt communicate which parts are important and which are less so. 
It's missing dependencies so it will be harder for a new coder to understand which parts could be affected when making a change. [1, Chapter 16, Section 11]

#Code:
* There is quite a few Properties in a few classes with "System.NotImplementedException" which to me looks like dead code.
* More than one class declared in same file in some files. f.ex. "Menu.cs". Made it more confusing for me as I'm used to "one class, one file".
* Good error handling and code looks robust in that regard.
* Good naming of methods and classes.

#Design/Architechture:
* There is good Model View separation and it looks like it would be fairly simple and straightforward to change the ConsoleView to some other UI.
* Classes have high cohesion. Perhaps the controller is a bit large but could be because of the dead code.
* Low representational gap between domain model and design which is good. However Member inheriting from Boat and MemberRegistry inheriting from Member seems unnecessary. Could not find out why this was done.
* Consider using memberRegistry as the creator of new members instead of the controller, both alternatives could be in line with GRASP pattern "controller" but as Larman states in the book "If more than one option applies, usually prefer a class B which aggreagates or contains class A". [1, Chapter 17, Section 10]

#Runnable version:
Could not run application. 
When running YachtClub.application it says the application is missing required files.
When trying to compile I get error:
https://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k(MSBuild.SignFile.SignToolError);k(TargetFrameworkMoniker-.NETFramework,Version%3Dv4.5.2)&rd=true

#Conclusion:
Very good implementation and design.
To pass grade 2: The unnecessary inheritance and dead code needs to be cleaned up, dependencies should be added to the class diagram and a functioning release should be provided.

#References:
1. Larman C., Applying UML and Patterns 3rd Ed, 2005, ISBN: 0131489062
