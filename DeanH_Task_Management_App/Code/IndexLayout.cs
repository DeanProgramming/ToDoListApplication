using DeanH_Task_Management_App.Pages;
using Microsoft.AspNetCore.Components;

namespace DeanH_Task_Management_App.Code
{
    public class IndexLayout : ComponentBase
    {
        protected bool _showCreateNewEntry = false;
        protected bool _showEditEntry = false;
        protected bool _showDescription = false;
        protected bool _suppressTabs = false;

        protected ToDoItem _selectedItem = null;
        protected List<ToDoItem> _todos = new();

        protected string _newTodoTitle = "";
        protected string _newTodoDescription = "";
        protected DateOnly _newTodoTime = DateOnly.FromDateTime(DateTime.Now.Date);
        protected int _priority = 1;

        private List<string> _usedChores = new List<string>();
        private List<string> _usedChoresDescriptions = new List<string>();

        protected void AddTodo()
        {
            if (!string.IsNullOrWhiteSpace(_newTodoTitle))
            {
                _todos.Add(new ToDoItem { Title = _newTodoTitle, Description = _newTodoDescription, DeadLine = _newTodoTime, Priority = _priority });
                _newTodoTitle = string.Empty;
                _newTodoDescription = string.Empty;
                _newTodoTime = DateOnly.FromDateTime(DateTime.Now.Date);
                _priority = 1;
                HideAllTabs();
                _suppressTabs = false;
            }
        }

        protected void HideAllTabs()
        {
            _showCreateNewEntry = false;
            _showDescription = false;
            _showEditEntry = false;
            _suppressTabs = true;
        }

        protected void ToggleCreateNewEntry()
        {
            HideAllTabs();
            _newTodoTitle = string.Empty;
            _newTodoDescription = string.Empty;
            _newTodoTime = DateOnly.FromDateTime(DateTime.Now.Date);
            _priority = 1;
            _showCreateNewEntry = true;
        }

        protected void ToggleDescription()
        {
            HideAllTabs();
        }

        protected void SelectEntry(ToDoItem item)
        {
            _selectedItem = item;
            ToggleDescription();
            _showDescription = true;
        }

        protected void DeleteEntry(ToDoItem item)
        {
            _todos.Remove(item);

            if (_showDescription)
            {
                _suppressTabs = false;
            }

            _showDescription = false;
        }

        protected void ShowEditEntry(ToDoItem item)
        {
            HideAllTabs();
            _showEditEntry = true;
            _selectedItem = item;
            _newTodoTitle = item.Title;
            _newTodoDescription = item.Description;
            _newTodoTime = item.DeadLine;
            _priority = item.Priority;
        }

        protected void ConfirmedEditEntry()
        {
            _selectedItem.Title = _newTodoTitle;
            _selectedItem.Description = _newTodoDescription;
            _selectedItem.DeadLine = _newTodoTime;
            _selectedItem.Priority = _priority;
            HideAllTabs();
            _suppressTabs = false;
        }

        protected int GetRemainingCount(DateOnly deadline)
        {
            return _todos.Count(t => t.DeadLine == deadline && !t.IsDone);
        }

        protected override void OnInitialized()
        {
            Random random = new Random();

            for (int i = 1; i <= 6; i++)
            {
                string[] randomChore = GetRandomChore();
                _todos.Add(new ToDoItem
                {
                    Title = $"{randomChore[0]}",
                    Description = $"{randomChore[1]}",
                    DeadLine = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(random.Next(1, 4))),
                    Priority = random.Next(1, 5)
                });
            }
        }

        private string[] GetRandomChore()
        {
            string[] householdChoresTitles = {
                "Vacuuming",
                "Dusting",
                "Mopping",
                "Doing the dishes",
                "Taking out the trash",
                "Laundry",
                "Cleaning the bathroom",
                "Making the bed",
                "Organizing the pantry",
                "Washing windows",
                "Sweeping the floors",
                "Watering plants",
                "Cleaning the refrigerator",
                "Changing bed sheets",
                "Cleaning the oven",
                "Wiping countertops",
                "Dusting blinds",
                "Cleaning mirrors",
                "Scrubbing the toilet"
                };

            string[] householdChoresDescription = {
                "Using a vacuum cleaner to remove dirt, dust, and debris from carpets and floors, ensuring a clean and tidy living space.",
                "Wiping surfaces and objects with a cloth or duster to eliminate accumulated dust, preventing allergies and maintaining a polished appearance.",
                "Cleaning floors by dampening a mop with a cleaning solution, effectively removing stains, spills, and grime for a sanitized and refreshed atmosphere.",
                "Washing and drying dishes, utensils, and cookware to maintain a hygienic kitchen, promoting a healthy cooking environment.",
                "Disposing of garbage and waste, preventing unpleasant odors and maintaining cleanliness in living spaces.",
                "Washing, drying, and folding clothes and linens to ensure a fresh and organized wardrobe, as well as clean bedding and towels.",
                "Sanitizing surfaces, fixtures, and accessories, ensuring a germ-free and visually appealing bathroom space.",
                "Arranging and smoothing bed linens for a neat and inviting bedroom, contributing to a comfortable and organized sleeping area.",
                "Sorting and arranging food items, ensuring easy access and a well-maintained pantry space.",
                "Cleaning and polishing windows to enhance natural light and maintain a clear view, contributing to the overall cleanliness of the home.",
                "Using a broom to remove dirt, dust, and debris from floors, preventing the buildup of grime and maintaining a clean living space.",
                "Providing plants with the necessary hydration, fostering a healthy and vibrant indoor or outdoor garden.",
                "Removing expired food, spills, and odors, maintaining a hygienic and organized refrigerator.",
                "Replacing bed linens to promote a clean and comfortable sleep environment, preventing the accumulation of allergens.",
                "Removing grease, food residue, and stains from the oven, ensuring safe and efficient cooking.",
                "Cleaning and disinfecting kitchen and bathroom countertops to maintain a hygienic and visually pleasing space.",
                "Removing dust and allergens from window blinds, contributing to a cleaner and healthier living environment.",
                "Wiping and polishing mirrors to maintain a clear and streak-free reflection, enhancing the overall appearance of bathrooms and living spaces.",
                "Thoroughly cleaning and disinfecting the toilet bowl and surrounding areas to ensure a sanitary bathroom environment."
            };

            Random random = new Random();

            var availableChores = householdChoresTitles.Except(_usedChores).ToArray();
            var availableChoresDescriptions = householdChoresDescription.Except(_usedChoresDescriptions).ToArray();

            if (availableChores.Length == 0)
            {
                _usedChores.Clear();
                _usedChoresDescriptions.Clear();
                availableChores = householdChoresTitles;
            }

            int index = random.Next(availableChores.Length);
            string selectedChore = availableChores[index];
            string selectedChoreDescription = availableChoresDescriptions[index];

            _usedChores.Add(selectedChore);
            _usedChoresDescriptions.Add(selectedChoreDescription);

            string[] returnString = new string[2];
            returnString[0] = selectedChore;
            returnString[1] = selectedChoreDescription;
            return returnString;
        }
    }
}
