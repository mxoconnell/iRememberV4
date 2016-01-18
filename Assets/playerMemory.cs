using SimpleSQL;
using System;
using UnityEngine;//for debug.log

//This class holds the playerMemory object
public class playerMemory
{
	[PrimaryKey]
	public string displayText { get; set; }
	
	public string imageLocation { get; set; } // Used to find the image to display 

    public string timeOfLastRecall { get; set; } // This will be used to judge against the current time to determine elapsed time

    public int minutesSinceLastRecall(){
        Debug.Log("remember TOLR binary:" + timeOfLastRecall);
       
        DateTime currentTime = DateTime.Now;
        long longOfLastRecall = Convert.ToInt64(timeOfLastRecall);
        DateTime dateOfLastRecall = DateTime.FromBinary(longOfLastRecall);
        Debug.Log("remember TOLR:" + dateOfLastRecall);
        Debug.Log("inbetween:" + (currentTime.Subtract(dateOfLastRecall)));
        Debug.Log((currentTime.Subtract(dateOfLastRecall).TotalMinutes));


        return (int)(currentTime.Subtract(dateOfLastRecall).TotalMinutes);
    }
}

//This class is used to store male and female names when read from the names table of the database
public class nameOptions
{

    [PrimaryKey]
    public string femaleName { get; set; }

    public string maleName { get; set; }

    public string lastName { get; set; }

}
