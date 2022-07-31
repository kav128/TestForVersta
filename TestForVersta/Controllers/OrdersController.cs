using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using TestForVersta.BLL.Services;
using TestForVersta.Models;

namespace TestForVersta.Controllers;

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

    public async Task<IActionResult> Index()
    {
        var orders = await _orderService.GetOrders();
        return View(_mapper.Map<IList<Order>>(orders));
    }
    
    public IActionResult Add()
    {
        return View();
    }
    
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
