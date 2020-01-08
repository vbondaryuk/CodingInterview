using System.Collections.Generic;

namespace CodingInterview.Coding.Graph
{
    public class DFS
    {
        public void Traversal(Employee graph)
        {
            var visited = new HashSet<Employee>();

            void DFS(Employee employeeVertex)
            {
                if (visited.Contains(employeeVertex))
                    return;
                foreach (var employee in employeeVertex.Employees)
                {
                    if (visited.Contains(employee))
                        continue;

                    visited.Add(employee);
                    DFS(employee);
                }
            }

            DFS(graph);
        }

        /// <summary>
        /// Find and mark connected components
        /// </summary>
        public List<(int, Employee)> Traversal_ConnectedComponents(Employee[] graph)
        {
            var visited = new HashSet<Employee>();

            List<(int, Employee)> Traversal(Employee[] employeeVertex)
            {
                var components = new List<(int, Employee)>();
                var count = 0;
                foreach (var employee in employeeVertex)
                {
                    if (visited.Contains(employee))
                        continue;

                    components.Add((count, employee));
                    count++;
                    DFS(employee);
                }

                return components;
            }

            void DFS(Employee employeeVertex)
            {
                if (visited.Contains(employeeVertex))
                    return;
                foreach (var employee in employeeVertex.Employees)
                {
                    if (visited.Contains(employee))
                        continue;

                    visited.Add(employee);
                    DFS(employee);
                }
            }

            return Traversal(graph);
        }
    }

    #region Employee Graph

    //https://www.csharpstar.com/csharp-depth-first-seach-using-list/
    public class Employee
    {
        private readonly List<Employee> EmployeesList = new List<Employee>();

        public Employee(string name)
        {
            this.Name = name;
        }

        public IReadOnlyCollection<Employee> Employees => EmployeesList;

        public string Name { get; }

        public void IsEmployeeOf(Employee e) => EmployeesList.Add(e);

        public override string ToString() => Name;
    }

    public class EmployeeFactory
    {
        public static Employee Build()
        {
            var Eva = new Employee("Eva");
            var Sophia = new Employee("Sophia");
            var Brian = new Employee("Brian");
            Eva.IsEmployeeOf(Sophia);
            Eva.IsEmployeeOf(Brian);

            var Lisa = new Employee("Lisa");
            var Tina = new Employee("Tina");
            var John = new Employee("John");
            var Mike = new Employee("Mike");
            Sophia.IsEmployeeOf(Lisa);
            Sophia.IsEmployeeOf(John);
            Brian.IsEmployeeOf(Tina);
            Brian.IsEmployeeOf(Mike);

            return Eva;
        }
    }

    #endregion
}