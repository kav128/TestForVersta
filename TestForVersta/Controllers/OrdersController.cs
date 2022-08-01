using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using TestForVersta.BLL.Services;
using TestForVersta.Models;

namespace TestForVersta.Controllers;

/// <summary>
/// Represents an MVC controller that gets and adds orders.
/// </summary>
public class OrdersController : Controller
{
    private readonly ILogger<OrdersController> _logger;
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;
    private readonly IValidator<OrderInsertModel> _validator;

    public OrdersController(ILogger<OrdersController> logger,
                            IOrderService orderService,
                            IMapper mapper,
                            IValidator<OrderInsertModel> validator)
    {
        _logger = logger;
        _orderService = orderService;
        _mapper = mapper;
        _validator = validator;
    }

    /// <summary>
    /// An endpoint that gets a list of <see cref="Order"/> models.
    /// </summary>
    /// <returns>A view with list of orders.</returns>
    public async Task<IActionResult> Index()
    {
        var orders = await _orderService.GetOrders();
        return View(_mapper.Map<IList<Order>>(orders));
    }

    /// <summary>
    /// An endpoint that gets a form to input data for a new <see cref="Order"/>.
    /// </summary>
    /// <returns>View with a form.</returns>
    public IActionResult Add() => View();

    /// <summary>
    /// An endpoint that creates a new order.
    /// </summary>
    /// <param name="model">Received data from the form.</param>
    /// <returns>A view with a form if the validation is failed, redirect to <see cref="Index"/> otherwise.</returns>
    [HttpPost]
    public async Task<IActionResult> Add(OrderInsertModel model)
    {
        var validationResult = await _validator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, null);
            return View(model);
        }

        var orderInsertModel = _mapper.Map<BLL.Models.OrderInsertModel>(model);
        await _orderService.AddOrder(orderInsertModel);
        return RedirectToAction("Index");
    }
}
