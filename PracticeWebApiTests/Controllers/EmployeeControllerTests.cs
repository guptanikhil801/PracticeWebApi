using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using PracticeWebApi.Classes;
using PracticeWebApi.Controllers;
using PracticeWebApi.Interfaces;

namespace PracticeWebApiTests.Controllers;
public class EmployeeControllerTests
{
    private readonly Mock<IEmployeeService> employeeService;
    private readonly Mock<IConfiguration> configuration;
    public EmployeeControllerTests()
    {
        employeeService = new Mock<IEmployeeService>();
        configuration = new Mock<IConfiguration>();
    }

    [Fact]
    public void GetAllEmployees_ShouldReturnOk()
    {
        //Arrange
        employeeService.Setup(x => x.GetAllEmployees()).Returns(GetMockEmployee());
        var productController = new EmployeeController(configuration.Object, employeeService.Object);

        var result = productController.GetAllEmployees();
        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(okObjectResult.StatusCode, 200);
        Assert.NotNull(okObjectResult.Value);
    }
    [Fact]
    public void GetAllEmployees_ShouldReturnBadRequest_IfNoEmployeeFound()
    {
        //Arrange
        employeeService.Setup(x => x.GetAllEmployees()).Returns(new List<Employee> { });
        var productController = new EmployeeController(configuration.Object, employeeService.Object);

        var result = productController.GetAllEmployees();
        var okObjectResult = Assert.IsType<BadRequestResult>(result);
        Assert.Equal(okObjectResult.StatusCode, 400);
    }

    private List<Employee> GetMockEmployee()
    {
        return new List<Employee>()
        {
            new Employee { Name = "John Doe     ", Salary = 50000, DepartmentId = 1, Address = new Address { Street = "123 Main St", City = "Anytown", State = "CA", ZipCode = 12345, Country = "USA" } },
            new Employee { Name = "Jane Smith   ", Salary = 60000, DepartmentId = 2, Address = new Address { Street = "456 Oak St", City = "Sometown", State = "NY", ZipCode = 67890, Country = "USA" } },
            new Employee { Name = "Bob Johnson  ", Salary = 55000, DepartmentId = 1, Address = new Address { Street = "789 Pine St", City = "Othertown", State = "TX", ZipCode = 54321, Country = "USA" } },
            new Employee { Name = "Alice White  ", Salary = 70000, DepartmentId = 3, Address = new Address { Street = "101 Elm St", City = "Newtown", State = "FL", ZipCode = 98765, Country = "USA" } },
            new Employee { Name = "Sam Brown    ", Salary = 48000, DepartmentId = 2, Address = new Address { Street = "202 Cedar St", City = "Smalltown", State = "WA", ZipCode = 24680, Country = "USA" } },
            new Employee { Name = "Emily Davis  ", Salary = 62000, DepartmentId = 1, Address = new Address { Street = "303 Maple St", City = "Hometown", State = "IL", ZipCode = 13579, Country = "USA" } },
            new Employee { Name = "Michael Lee  ", Salary = 53000, DepartmentId = 3, Address = new Address { Street = "404 Birch St", City = "Yourtown", State = "OH", ZipCode = 11223, Country = "USA" } },
            new Employee { Name = "Sara Taylor  ", Salary = 58000, DepartmentId = 2, Address = new Address { Street = "505 Pine St", City = "Largetown", State = "GA", ZipCode = 44556, Country = "USA" } },
            new Employee { Name = "David Adams  ", Salary = 66000, DepartmentId = 1, Address = new Address { Street = "606 Oak St", City = "Bigtown", State = "MI", ZipCode = 77889, Country = "USA" } },
            new Employee { Name = "Lisa Miller  ", Salary = 51000, DepartmentId = 3, Address = new Address { Street = "707 Elm St", City = "Oldtown", State = "PA", ZipCode = 66778, Country = "USA" } },
        };
    }
}
