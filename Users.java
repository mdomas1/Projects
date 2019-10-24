import java.util.Scanner;
import java.io.File;
import java.util.*;
import java.util.Arrays;
import java.io.IOException;
import java.io.PrintWriter;


public class Users {
	private String username;
	private String emails;
	static private String namef;
	static private String namel;
	static String[] totalu = new String[100];
	static String[] totaln = new String[100];
	static String[] totale = new String[100];
	static int i = 0;
	static int m = 0;
	static int space = 0;
	static //boolean containsu;
	Scanner keyboard = new Scanner(System.in);
	
	
	public Users(String username)
	{
		this.username = username;
		this.emails = emails;
	//	this.names = names;
	}
	public void getUsername()
	{
		
	}
	
	public static String add(String username)
	{
		
		//spaces(username);
		while(spaces(username) == false)
		{
			System.out.println("What is your desired username/ID? (NO spaces)");
			username = keyboard.nextLine();
			spaces(username);
		}
		
	boolean containsu = Arrays.stream(totalu).anyMatch(username::equals);
	while(containsu)
	{
		System.out.println("Sorry, username not available, try again");
		username = keyboard.nextLine();
		containsu = Arrays.stream(totalu).anyMatch(username::equals);
		if(username.equals("exit"))
		{
			return "--back to home--";
		}
		if(!(containsu))
		{
			break;
		}	
	}
	
		totalu[i] = username;
		i++;
	//	System.out.println(totalu[i]);
	System.out.println("Now, what is your name? (First and Last)");
	
	 String input = keyboard.nextLine(); 	
	totaln[m] = input;
	if(input.equals("exit"))
	{
		System.out.println("--back to home--");
		return "--back to home--";
	}
	
	 System.out.println("Lastly, what is your email?");
	 
	 String input2 = keyboard.nextLine();
		 while(!(input2.contains("@")))
	 {
		 System.out.println("Sorry, you must use a '@', try again ");
		 input2 = keyboard.next();
	 }
	 
	 if(input2.equals("exit"))
		{
		 	System.out.println("--back to home--");
			return "--back to home--";
		}
	 totale[m] = input2;
	 m++;
	 System.out.println("Thanks! -- back to main menu ");
		
	return "Welcome " + username;
	
	}
	
	static boolean spaces(String username)
	{
		 for (int u = 0; u < username.length(); u++) {
	      if (Character.isWhitespace(username.charAt(u))) {
		          space++;
		        }
		 }    
		    
		 if(space > 0)
		 {
			 System.out.println("Sorry, no spaces allowed in username");
		 space = 0; 
		 
		 return false; } 
		 return true;
	}
	
	public static void users()
	{
		//System.out.println("nothing?");
		for(int y = 0; y < totalu.length; y++)
		{
			if (totalu[y] == null)
			{
				//System.out.println("nothing?");
				break;
			}
			else
				System.out.println(totalu[y]);
		}
		
	}
	
	public static String userInfo(String ID)
	{
		int f = 0;
		for(int w = 0; w < totalu.length; w++)
		{
		if(ID == totalu[w])
		{
			 f = w;
			break;
		}
		}
		System.out.println("username: " +totalu[f]);
		System.out.println("name: " + totaln[f]);
		System.out.println("email: " + totale[f]);
		return totalu[f] + totaln[f] + totale[f] ;
	}
	static boolean loggedin = false;;
	public static String login(String login)
	{
	
		int f = 0;
		for(int w = 0; w < totalu.length; w++)
		{
		if(login == totalu[w])
		{
			 f = w;
			 loggedin = true;
			 in(loggedin);
			break;
		}
		}
		System.out.print("Welcome " + totaln[f]+"!");
		return login;
	}
	
	public static boolean in(Boolean loggedin)
	{
		return true;
	}
	
	static public int getUser(String login)
	{
		for(int w = 0; w < totalu.length; w++)
		{
		if(login == totalu[w])
		{
			//usernum(w);
			return w;
		}
		}
		//System.out.print("No tasks found :(");
		return 0;
	}
	
	static public String savelist(String savedlist) throws IOException
	{
		//savedlist = keyboard.nextLine().trim();
		File userFile = new File(savedlist+ ".txt");
		PrintWriter writer = new PrintWriter(userFile); 
		
		for(int y = 0; y < totalu.length; y++)
		{
			if (!(totalu[y] == null))
			{
				writer.write(totalu[y]);
			}
			
	}
		return savedlist;
	
	
}
		
}