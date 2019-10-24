import java.io.IOException;
import java.util.Scanner;
//does not load userlist or tasklist, but saves it?
/*
*One of my final projects in my COSC211 Programming Java Structures class from Winter 2018.
*
*A user interface that allows user to login, logout, check their tasks, add to their tasks and 
*check details about their tasks. 
*
*
*
*
*/
public class Main {
	public static void main(String [] args)
	{
		boolean in = false;
		String login = "";
		
		System.out.println("Meag's Todo List Program: ");
		System.out.println("---enter help for menu---");
		Scanner keyboard = new Scanner(System.in);
		int i = 0;
		while(i == 0) {
			
		String input = keyboard.nextLine();
		
		if(input.equals("exit"))
		{	
			if(in == true)
		{
			System.out.println("What would you like to save your user list as?");
			String savedlist = keyboard.next();
			if(savedlist.equals("exit"))
			{
				System.out.println("Thanks for using Meag's Todo List Program :)");
				System.exit(0);
			}
			try {
				Users.savelist(savedlist);
			} catch (IOException e) {
				e.printStackTrace();
			}
			System.out.println("What would you like to save your task list as? ");
			String savedtask = keyboard.nextLine();
			if(savedtask.equals("exit"))
			{
				System.out.println("Thanks for using Meag's Todo List Program :)");
				System.exit(0);
			}
			try {
				Tasks.savetask(savedtask);
			} catch (IOException e) {
				e.printStackTrace();
			}
			}
			System.out.println("Thanks for using Meag's Todo List Program :)");
			System.exit(0);
		
			
		}
		
		if(input.equals("help"))
		{
			System.out.println("--here are the commands--");
			System.out.println("adduser: add user to program");
			System.out.println("help: brings up list of commands");
			System.out.println("users: pulls up list of all users");
			System.out.println("userinfo: pulls up all info on user");
			System.out.println("login: logs in saved account");
			System.out.println("logout: logs out of saved acount");
			System.out.println("addtask: add task for user");
			System.out.println("tasks: see what tasks a user has");
			System.out.println("taskinfo: check info on specific task");
			System.out.println("exit: exits the program");
			System.out.println("------------------------");
			
		}
		if(!(input.equals("help")) && !(input.equals("exit")) && !(input.equals("adduser")) && !(input.equals("users")) && !(input.equals("userinfo")) && !(input.equals("login")) && !(input.equals("logout")) && !(input.equals("addtask")) && !(input.equals("tasks")) && !(input.equals("taskinfo")))
				{
			System.out.println("We do not support: " + input);
				}
		if(input.equals("adduser"))
		{
			System.out.println("What is your desired username? (no spaces plz) ");
			String username = keyboard.nextLine();
			if(!(username.equals("exit")))
			{
			Users newuser = new Users(username);
			Users.add(username); }
			//System.out.println(username);
			if(username.equals("exit")) {
			System.out.println("Meag's Todo List Program: ");
			System.out.println("---Enter help for menu---"); }
		}
		if(input.equals("users"))
		{
		//	Users use = new Users();
			Users.users();
			//Users.toString();
			//System.out.println()
		}
		if(input.equals("userinfo"))
		{
			System.out.println("Enter username/ID:");
			String ID = keyboard.nextLine();
			Users.userInfo(ID);
			
		}
		if(input.equals("login"))
		{
			System.out.println("Enter username/ID:");
			login = keyboard.nextLine();
			Users.login(login);
			if(Users.in(true))
			{
				in = true;
			}
			//if(Users.in(false))
			//{
			//	in = false;
			//}
		}
		if(input.equals("logout"))
		{
			in = false;
			System.out.println("Logging out of " + login);
		}
		if(input.equals("addtask") && in == true)
		{
			System.out.println("Enter a title for your new task:");
			String title = keyboard.nextLine();
			Tasks.addTask(title, login);
			
		}
		if(input.equals("tasks"))
		{
			//System.out.println("??");
			if(in == true)
			{
				//System.out.println("in");
				Tasks.getTask(login);
			}
			else {
				System.out.println("Login first please");
			}
		}
		if(input.equals("taskinfo"))
		{
			if(in == true)
			{
				//System.out.println("in");
			System.out.println("Enter the title of the task:");
			String tinfo = keyboard.nextLine();
			Tasks.getInfo(tinfo);
			}
			else {
				System.out.println("Login first please");
			}
		}
		}
	}

}
