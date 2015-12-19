using SimpleSQL;
//This class holds the playerMemory object
public class playerMemory
{
	// No pimary key used
	//[PrimaryKey]
	
	public string displayText { get; set; }
	
	public string imageLocation { get; set; }

}

//This class is used to store male and female names when read from the names table of the database
public class nameOptions
{
    // No pimary key used
    //[PrimaryKey]

     public string femaleName { get; set; }

    public string maleName { get; set; }

}
