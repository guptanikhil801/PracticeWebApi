namespace PracticeWebApi.Interfaces;
public interface IEmployeeService
{
    List<Employee> GetAllEmployees();
    Employee GetEmployee(int id);
    Task<bool> AddEmployeeAsync(Employee employee);
    bool DeleteEmployee(int empId);
    bool UpdateEmployee(Employee employee);
}