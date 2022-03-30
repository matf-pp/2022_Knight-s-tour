using System.Collections.Generic;

namespace Planner.ViewModels;

public class TodoControlViewModel
{
	public string NewTask { get; set; }
	public List<TaskViewModel> Tasks { get; set; }
}