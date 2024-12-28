using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StudentSystem.Models;
using StudentSystem.RabbitMQ;

namespace StudentSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRabbitMQProducer _rabbitMQProducer;

    public HomeController(ILogger<HomeController> logger, IRabbitMQProducer rabbitMQProducer)
    {
        _logger = logger;
        _rabbitMQProducer = rabbitMQProducer;
    }

    public IActionResult Index()
    {
        var student = new Student
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe"
        };
        
        _rabbitMQProducer.SendMessage<Student>(student);
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}