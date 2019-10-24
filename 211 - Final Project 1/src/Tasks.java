import java.io.File;
import java.io.IOException;
import java.io.PrintWriter;

public class Tasks extends Users {
static String[] titles = new String[100];
static String[] desc = new String[100];
static int i = 0;
	public Tasks(String username) {
		super(username);
		
	}

	public static void addTask(String title, String login){
		int y = Users.getUser(login);
		y = y*10;
		int n = y;
		while(!(titles[y] == null))
		{
			//titles[y] = title;
			y++;
		}
		if(titles[y] == null) {
			titles [y] = title;
			}
		
	System.out.println("Enter a description for your task:");
	String des = keyboard.nextLine();
	
		while(!(desc[n] == null))
				{
			n++;
				}
			//desc[n] = des;
			if(desc[n] == null) {
				desc [n] = des;
				}
			
			//System.out.println("Welcome back to main menu");
			System.out.println("--back to menu--");
	}
	
	public static String getTask(String login)
	{
		int y = Users.getUser(login);
		y = y*10;
		if(titles[y] == null)
		{
			System.out.println("No tasks for you!");
			return null;
		}
		else {
			if(!(titles[y] == null))
			{
		System.out.println(titles[y]);
		y++;
		}
			while(!(titles[y] == null))
			{
				System.out.println(titles[y]);
				y++;
			}
				
		return login;
		
	}
}
	public static String getInfo(String tinfo)
	{
		int f = 0;
		for(int w = 0; w < titles.length; w++)
		{
		if(tinfo == desc[w])
		{
			 f = w;
			break;
		}
		}
		System.out.println("Description: ");
		System.out.println(desc[f]);
		return tinfo;
	}
	
	static public String savetask(String savedtask) throws IOException
	{
		//savedlist = keyboard.nextLine().trim();
		File userFile2 = new File(savedtask+ ".txt");
		PrintWriter writer2 = new PrintWriter(userFile2); 
		
		for(int y = 0; y < titles.length; y++)
		{
			if (!(titles[y] == null))
			{
				writer2.write(titles[y]);
			}
			
	}
		return savedtask;
	
	
}

}
