using GameOfLife.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GameOfLife.Controllers;

public class GameOfLifeController : Controller
{
    public IActionResult Index()
    {
        GameOfLifeViewModel viewModel = new();

        return View(viewModel);
    }
}