using System;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using TopCalendar.Utility.UI;
using TopCalendar.Utility.Validators;

namespace TopCalendar.Client.DataModel
{
    public class Task : NotifyPropertyChanged
    {
    	private string _name;

        public  Task()
        {
        }

        public Task(string name, DateTime startAt)
    	{
    		_name = name;
    		_startAt = startAt;
    	}

    	[StringLengthValidator(1, 50)]
    	public string Name
    	{
    		get { return _name; }
    		set { _name = value; 
				OnPropertyChanged(()=> Name);
			}
    	}

    	private DateTime _startAt;		
    	public DateTime StartAt
    	{
    		get { return _startAt; }
    		set { _startAt = value; 
				OnPropertyChanged(()=> StartAt);
			}
    	}

    	private DateTime? _finishAt;
    	public DateTime? FinishAt
    	{
    		get { return _finishAt; }
    		set { _finishAt = value;
    			OnPropertyChanged(() => FinishAt);
			}
    	}

    	private string _description;
		
		[StringNullableLengthValidator(1,200)]
    	public string Description
    	{
    		get { return _description; }
    		set { _description = value; 
				OnPropertyChanged(()=>Description);
			}
    	}		
    }
}
