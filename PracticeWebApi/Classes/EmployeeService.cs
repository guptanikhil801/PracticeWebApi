namespace PracticeWebApi.Classes
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDBContext context;

        public EmployeeService(ApplicationDBContext context)
        {
            this.context = context;
        }

        public async Task<bool> AddEmployeeAsync(Employee employee)
        {
            await context.Employees.AddAsync(employee);
            if (context.SaveChanges() == 1)
            {
                var newlyAddedEmployeeID = context.Employees.OrderBy(x => x.EmployeeId).LastOrDefault().EmployeeId;
                employee.Address.EmployeeID = newlyAddedEmployeeID;
            }
            await context.Address.AddAsync(employee.Address);
            return context.SaveChanges() == 1;
        }

        public bool DeleteEmployee(int empId)
        {
            var employee = context.Employees.SingleOrDefault(option => option.EmployeeId == empId);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                return context.SaveChanges() == 1;
            }
            return false;
        }

        public List<Employee> GetAllEmployees()
        {
            var allEmployees = context.Employees.Join(
                                context.Address,
                                employee => employee.EmployeeId,
                                address => address.EmployeeID,
                                (employee, address) => new Employee
                                {
                                    Address = address,
                                    EmployeeId = employee.EmployeeId,
                                    Name = employee.Name,
                                    DepartmentId = employee.DepartmentId,
                                    Salary = employee.Salary
                                }).ToList();
            return allEmployees;
        }

        public Employee GetEmployee(int id)
        {
            return context.Employees.Find(id);
        }

        public bool UpdateEmployee(Employee employee)
        {
            var employeeRecord = context.Employees.SingleOrDefault(option => option.EmployeeId == employee.EmployeeId);
            if (employeeRecord != null)
            {
                context.Update(employee);
            }
            return context.SaveChanges() == 1;
        }
    }
}